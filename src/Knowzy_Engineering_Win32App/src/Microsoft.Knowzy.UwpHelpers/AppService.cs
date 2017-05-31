using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;

namespace Microsoft.Knowzy.UwpHelpers
{
    public class AppService
    {
        private AppServiceConnection _connection = null;
        private String _listenerId;

        public AppService()
        {
        }

        public async Task<bool> StartAppServiceConnection(String listenerId)
        {
            var result = false;
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }

            // Open a connection to the App Service
            _listenerId = listenerId;
            _connection = new AppServiceConnection();
            _connection.AppServiceName = "com.microsoft.knowzy.appservice";
            _connection.PackageFamilyName = Windows.ApplicationModel.Package.Current.Id.FamilyName;
            _connection.RequestReceived += Connection_RequestReceived;
            _connection.ServiceClosed += Connection_ServiceClosed;
            AppServiceConnectionStatus status = await _connection.OpenAsync();
            if (status == AppServiceConnectionStatus.Success)
            {
                // register this App Service Connection as a listener
                ValueSet registerData = new ValueSet();
                registerData.Add("Type", "Register");
                registerData.Add("Id", listenerId);
                var response = await _connection.SendMessageAsync(registerData);
                if (response.Status == AppServiceResponseStatus.Success)
                {
                    var message = response.Message;
                    result = message.ContainsKey("Status") && message["Status"].ToString() == "OK";
                }
            }
            return result;
        }

        private async void Connection_RequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            var messageDeferral = args.GetDeferral();
            ValueSet returnData = new ValueSet();
            returnData.Add("Status", "OK");
            returnData.Add("Data", "Knowzy WPF app received message: " + args.Request.Message["Data"]);
            await args.Request.SendResponseAsync(returnData);
            messageDeferral.Complete(); // Complete the deferral so that the platform knows that we're done responding to the app service call.
        }

        private void Connection_ServiceClosed(AppServiceConnection sender, AppServiceClosedEventArgs args)
        {
            _connection.Dispose();
            _connection = null;
        }
    }
}
