using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.AppService;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Microsoft.Knowzy.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppServiceTest : Page
    {
        private String _connectionId;
        private AppServiceConnection _connection = null;

        public AppServiceTest()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            if (args.Parameter != null)
            {
                WwwFormUrlDecoder decoder = new WwwFormUrlDecoder(args.Parameter.ToString());
                try
                {
                    _connectionId = decoder.GetFirstValueByName("appserviceid");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("AppServiceTest OnNavigatedTo Error: " + ex.Message);
                }
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_connection == null)
            {
                _connection = new AppServiceConnection();

                // Here, we use the app service name defined in the app service provider's Package.appxmanifest file in the <Extension> section.
                _connection.AppServiceName = "com.microsoft.knowzy.appservice"; ;

                // Use Windows.ApplicationModel.Package.Current.Id.FamilyName within the app service provider to get this value.
                _connection.PackageFamilyName = Windows.ApplicationModel.Package.Current.Id.FamilyName;

                var status = await _connection.OpenAsync();
                if (status != AppServiceConnectionStatus.Success)
                {
                    textBox.Text = "Failed to connect " + status;
                    return;
                }
            }

            ValueSet data = new ValueSet();
            data.Add("Type", "Message");
            data.Add("Id", _connectionId);
            data.Add("Data", "Message from AppServiceTest XAML UI");
            textBox.Text = "Sending message to App Service connection listener: " + _connectionId;

            var response = await _connection.SendMessageAsync(data);
            if (response.Status == AppServiceResponseStatus.Success)
            {
                var message = response.Message;
                bool result = message.ContainsKey("Status") && message["Status"].ToString() == "OK";
                if (result)
                {
                    var text = message["Data"] as String;
                    textBox.Text = text;
                }
            }
        }
    }
}
