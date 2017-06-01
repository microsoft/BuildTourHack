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
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Input.Inking;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Microsoft.Knowzy.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SharePage : Page
    {
        private ShareOperation operation = null;
        private StorageFile _shareFile = null;
        private String _shareFileName = "";

        public SharePage()
        {
            this.InitializeComponent();

            inkCanvas.InkPresenter.InputDeviceTypes = CoreInputDeviceTypes.Mouse | CoreInputDeviceTypes.Touch;
            var attr = new InkDrawingAttributes();
            attr.Color = Colors.Red;
            attr.IgnorePressure = true;
            attr.PenTip = PenTipShape.Circle;
            attr.Size = new Size(4, 10);
            inkCanvas.InkPresenter.UpdateDefaultDrawingAttributes(attr);

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
                        _shareFile = (StorageFile)(storageItems[0]);
                        _shareFileName = _shareFile.Name;
                        var stream = await _shareFile.OpenReadAsync();
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
            if (_shareFile != null)
            {
                // copy file to app's local folder. Desktop Bridge app will detect new file with its FileWatcher
                try
                {
                    var imageFile = await _shareFile.CopyAsync(ApplicationData.Current.TemporaryFolder, _shareFile.Name, NameCollisionOption.ReplaceExisting);
                    await saveImage(imageFile);
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

        void CanvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
        }

        private async Task<bool> saveImage(StorageFile imageFile)
        {
            try
            {
                CanvasDevice device = CanvasDevice.GetSharedDevice(true);
                CanvasRenderTarget renderTarget = new CanvasRenderTarget(device, (int)inkCanvas.ActualWidth, (int)inkCanvas.ActualHeight, 96);
                using (var ds = renderTarget.CreateDrawingSession())
                {
                    ds.Clear(Colors.White);
                    var image = await CanvasBitmap.LoadAsync(device, imageFile.Path);
                    // draw your image first
                    ds.DrawImage(image);
                    // then draw contents of your ink canvas over it
                    ds.DrawInk(inkCanvas.InkPresenter.StrokeContainer.GetStrokes());
                }

                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                var file = await storageFolder.CreateFileAsync(_shareFile.Name, CreationCollisionOption.ReplaceExisting);

                // save results
                using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    await renderTarget.SaveAsync(fileStream, CanvasBitmapFileFormat.Jpeg, 1f);
                }
                return true;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return false;

            }

        }
    }
}
