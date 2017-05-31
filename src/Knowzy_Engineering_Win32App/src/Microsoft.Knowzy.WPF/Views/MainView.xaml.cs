// ******************************************************************

// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.

// ******************************************************************
using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Knowzy.UwpHelpers;

namespace Microsoft.Knowzy.WPF.Views
{
    public partial class MainView
    {
        AppService _appService = null;

        public MainView()
        {
            InitializeComponent();
            if (ExecutionMode.IsRunningAsUwp())
            {
                try
                {
                    // get the path to the App folder (WPF or UWP).
                    var path = AppFolders.Local;
                    FileSystemWatcher watcher = new FileSystemWatcher(path);
                    watcher.EnableRaisingEvents = true;
                    watcher.Changed += Watcher_Changed;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("FileSystemWatcher Error:" + ex.Message);
                }
            }
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (ExecutionMode.IsRunningAsUwp())
            {
                if (File.Exists(e.FullPath))
                {
                    var xml = "<toast><visual><binding template='ToastGeneric'><image src='" + e.FullPath + "'/><text hint-maxLines='1'>Microsoft.Knowzy.WPF received a new image</text></binding></visual></toast>";
                    Toast.CreateToast(xml);
                }
            }
        }

        private async void Menu_Click(object sender, EventArgs e)
        {
            if (ExecutionMode.IsRunningAsUwp())
            {
                if (_appService == null)
                {
                    // start the app service
                    _appService = new AppService();
                    var result = await _appService.StartAppServiceConnection("com.microsoft.knowzy.appservice.test");
                }

                // start the XAML UI that will communicate with the App Service
                Uri uri = new Uri("com.microsoft.knowzy.protocol.test://" + "message?appserviceid=" + "com.microsoft.knowzy.appservice.test");
                await UriProtocol.SendUri(uri);
            }
        }
    }
}
