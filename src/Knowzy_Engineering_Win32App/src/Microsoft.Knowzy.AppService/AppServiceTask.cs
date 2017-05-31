using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using System.Threading;
using Windows.Foundation.Collections;

namespace Microsoft.Knowzy.AppService
{
    public sealed class AppServiceTask : IBackgroundTask
    {
        private BackgroundTaskDeferral backgroundTaskDeferral;
        private AppServiceConnection appServiceconnection;

        // Map of AppService message Listeners
        private static IDictionary<string, AppServiceConnection> _connectionMap = new Dictionary<string, AppServiceConnection>();

        // Mutex to protect access to _connectionMap
        private static Mutex _mutex = new Mutex();

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            this.backgroundTaskDeferral = taskInstance.GetDeferral(); // Get a deferral so that the service isn't terminated.
            taskInstance.Canceled += OnTaskCanceled; // Associate a cancellation handler with the background task.

            // Retrieve the app service connection and set up a listener for incoming app service requests.
            var details = taskInstance.TriggerDetails as AppServiceTriggerDetails;
            appServiceconnection = details.AppServiceConnection;
            appServiceconnection.RequestReceived += OnRequestReceived;
        }

        private void AddListener(String id, AppServiceConnection connection)
        {
            _mutex.WaitOne();
            _connectionMap[id] = connection;
            _mutex.ReleaseMutex();
        }

        private void RemoveListener(String id)
        {
            _mutex.WaitOne();
            if (_connectionMap.ContainsKey(id))
            {
                _connectionMap.Remove(id);
            }
            _mutex.ReleaseMutex();
        }

        private async Task<ValueSet> SendMessage(String id, ValueSet message)
        {
            String errorMessage = "";

            _mutex.WaitOne();
            AppServiceConnection appServiceConnection = null;
            if (_connectionMap.ContainsKey(id))
            {
                appServiceConnection = _connectionMap[id];
            }
            _mutex.ReleaseMutex();

            if (appServiceConnection != null)
            {
                var response = await appServiceConnection.SendMessageAsync(message);
                if (response.Status == AppServiceResponseStatus.Success)
                {
                    return response.Message;
                }
                else
                {
                    errorMessage = "SendMessageAsync result: " + response.Status;
                }
            }
            else
            {
                errorMessage = "No registered Listener for Id: " + id;
            }

            // build the error response
            ValueSet error = new ValueSet();
            error.Add("Status", "Error");
            error.Add("ErrorMessage", errorMessage);
            return error;
        }

        async void OnRequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            // Get a deferral because we use an awaitable API below to respond to the message
            // and we don't want this call to get cancelled while we are waiting.
            var messageDeferral = args.GetDeferral();

            var message = args.Request.Message;
            ValueSet response = new ValueSet();

            if (message.ContainsKey("Type") && message.ContainsKey("Id"))
            {
                var type = message["Type"];
                var id = message["Id"].ToString();
                switch (type)
                {
                    case "Register":
                        AddListener(id, sender);
                        response.Add("Status", "OK");
                        break;

                    case "Unregister":
                        RemoveListener(id);
                        response.Add("Status", "OK");
                        break;

                    case "Message":
                        response = await SendMessage(id, message);
                        break;

                    default:
                        response.Add("Status", "Error");
                        response.Add("ErrorMessage", "Unknown KnowzyAppServiceMessage type");
                        break;
                }
            }
            else
            {
                response.Add("Status", "Error");
                response.Add("ErrorMessage", "Missing valid Type or Id parameters");
            }

            await args.Request.SendResponseAsync(response);
            messageDeferral.Complete(); // Complete the deferral so that the platform knows that we're done responding to the app service call.
        }

        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            if (this.backgroundTaskDeferral != null)
            {
                // Complete the service deferral.
                this.backgroundTaskDeferral.Complete();
            }
        }
    }
}
