using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Knowzy.UwpHelpers
{
    public class UriProtocol
    {
        public static async Task<bool> SendUri(Uri uri)
        {
            // Note: DesktopBridge.Helpers NuGet package incorrectly warns that LaunchUriAsync is not supported in a Centennial App
            bool result = await Windows.System.Launcher.LaunchUriAsync(uri);
            return result;
        }
    }
}
