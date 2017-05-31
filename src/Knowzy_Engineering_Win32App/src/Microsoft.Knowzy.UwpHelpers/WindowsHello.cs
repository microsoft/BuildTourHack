using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace Microsoft.Knowzy.UwpHelpers
{
    public class WindowsHello
    {
        public static async Task<bool> Login()
        {
            var result = await KeyCredentialManager.IsSupportedAsync();
            String message;

            if (result)
            {
                var authenticationResult = await KeyCredentialManager.RequestCreateAsync("login", KeyCredentialCreationOption.ReplaceExisting);
                if (authenticationResult.Status == KeyCredentialStatus.Success)
                {
                    message = "User is logged in";
                }
                else
                {
                    message = "Login error: " + authenticationResult.Status;
                }
            }
            else
            {
                message = "Windows Hello is not enabled for this device.";
            }

            String imagePath = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            String xml = "<toast><visual><binding template='ToastGeneric'><text hint-maxLines='1'>" + message + "</text></binding></visual></toast>";

            Toast.CreateToast(xml);

            return result;
        }
    }
}