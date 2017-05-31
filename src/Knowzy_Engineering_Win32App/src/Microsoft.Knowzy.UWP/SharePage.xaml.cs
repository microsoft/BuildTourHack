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
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Microsoft.Knowzy.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SharePage : Page
    {
        private ShareOperation operation = null;
        private StorageFile file = null;

        public SharePage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs args)
        {
            if (args.Parameter != null)
            {
                // It is recommended to only retrieve the ShareOperation object in the activation handler, return as
                // quickly as possible, and retrieve all data from the share target asynchronously.
                operation = (ShareOperation)args.Parameter;

                await Task.Factory.StartNew(async () =>
                {
                    if (operation.Data.Contains(StandardDataFormats.StorageItems))
                    {
                        var storageItems = await operation.Data.GetStorageItemsAsync();
                        file = (StorageFile)(storageItems[0]);
                        var stream = await file.OpenReadAsync();
                        // Get back to the UI thread using the dispatcher.
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                        {
                            var image = new BitmapImage();
                            img.Source = image;
                            await image.SetSourceAsync(stream);
                        });
                    }
                });
            }
        }

        private async void ShareButton_Click(object sender, RoutedEventArgs e)
        {
            if (file != null)
            {
                // copy file to app's local folder. Desktop Bridge app will detect new file with its FileWatcher
                try
                {
                    await file.CopyAsync(ApplicationData.Current.LocalFolder, file.Name, NameCollisionOption.ReplaceExisting);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("ShareButton_Click Error:" + ex.Message);
                }
            }

            if (operation != null)
            {
                operation.ReportCompleted();
            }
        }
    }
}
