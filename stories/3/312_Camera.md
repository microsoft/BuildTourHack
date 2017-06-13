# Task 3.1.2 - Capture Images

Our marketing department wants to allow users of our app to capture images, and position Knowzy products over the image to see how they would look. It's a fun way to try the product without actually buying it. The marketing department is hoping that these images will be shared on social media to spread the word.

**Goals for this task:** Enable your Android and UWP app to capture images from the camera.

For this task, you'll need to access APIs that are specific to each platform. We've done the research on how to do it and we've included the steps below.

## Prerequisites

This task has a dependency on [Task 3.1.1](311_XamarinForms.md) and all of it's prerequisites.

## Task

### Create and navigate to a new page

1. Right-click the Shared project, and then select **Add -> New Item**.

2. Under **Visual C# -> Cross-Platform** select **Forms Blank Content Page Xaml**, give it a name (we use CameraPage in this guide), and then click the **Add** button.
   
   > Note: The new page might also be found under **Visual C# -> Xamarin.Forms -> Content Page**
   
   This will create a new page which you can navigate to once a product has been selected on the Main page.

   There are [multiple ways to navigate between pages](https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/navigation/). In this example, we'll use a **NavigationPage** to act as a host for our pages and provide hierarchical navigation.

3. In the Shared project, open the App.xaml.cs file.

    Notice that the constructor sets the MainPage to a new MainPage (the default page when the app is created):

        MainPage = new Knowzy.Mobile.MainPage();

    > Note: the **Knowzy.Mobile** namespace above might be different for you depending on what you named your project

4. Instead of setting the MainPage to a new MainPage, set it to a new NavigationPage and pass a new MainPage as a parameter.

        MainPage = new NavigationPage(new MainPage());
   
    This will set it as the first page in our hierarchical navigation system.

    You're now ready to navigate to the new page. We want to navigate to the new page when a product (nose) is clicked in the main page, and we want to pass the nose as a parameter. The easiest way to do that is to pass the clicked nose as a parameter to the constructor when navigating to the new page.

5. Open the code-behind file for the new page that you created (CameraPage.xaml.cs in our example), and modify the constructor to accept a Nose object as a parameter.

        Nose _nose;

        public CameraPage(Nose nose)
        {
            _nose = nose;
            InitializeComponent ();
        }

4. Open the XAML file for the main page, and add an ``ItemTapped`` event handler to handle an event that is raised when an item is taped on the ListView.

        <ListView x:Name="ProductListView" ItemTapped="ProductListViewItemTapped">
            <!-- ... -->
        </ListView>

5. In the code-behind for the main page (MainPage.xaml.cs), implement the event handler, and add the following code to the handler:

        private void ProductListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new CameraPage(e.Item as Nose));
        }

    This code navigates to the new page by passing the tapped item.

You should now be able to navigate to the new (empty) page. Test it out to make sure it all works as expected.

### Capture image from camera on Android and UWP

Once we've navigated to the new page, the goal is to capture an image from the camera. Because each platform has a different native API for camera capture, we'll design an interface and then implement that interface on each platform. Then, we'll use the [DependencyService](https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/dependency-service/introduction/) from Xamarin.Forms to call the right implementation.

1. Create a new interface class in the Shared Project and name it *IPhotoService*.

2. Add a method definition for capturing the photo. It should look like this:

        public interface IPhotoService
        {
            Task<byte[]> TakePhotoAsync();
        }

    You'll need to add few namespaces:

        using System.Threading.Tasks;
        using Xamarin.Forms;

    To use the native APIs, you'll need to implement this interface for each platform. Let's start with UWP.

#### Implement the IPhotoService interface for UWP

1. In the UWP project, create a new class and name it PhotoService. Extend IPhotoService and register with the DependencyService by adding a metadata attribute above the namespace. The class would look like this:

        using Xamarin.Forms;
        using YourNamespace.UWP;

        [assembly: Dependency(typeof(PhotoService))]
        namespace YourNamespace.UWP
        {
                public class PhotoService : IPhotoService
                {
                    public async Task<byte[]> TakePhotoAsync()
                    {

                    }
                }
        }

2. Implement the TakePhotoAsync method to use the native [CameraCaptureUI](https://docs.microsoft.com/en-us/uwp/api/windows.media.capture.cameracaptureui) from UWP and make it async:

        public async Task<byte[]> TakePhotoAsync()
        {
                CameraCaptureUI captureUI = new CameraCaptureUI();
                captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;

                StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

                if (photo == null) return null;

                using (var stream = await photo.OpenReadAsync())
                {
                    var buffer = new Windows.Storage.Streams.Buffer((uint)stream.Size);
                    var data = await stream.ReadAsync(buffer, (uint)stream.Size, Windows.Storage.Streams.InputStreamOptions.None);
                    return data.ToArray();
                }
        }

    You'll also need few namespaces:

        using Windows.Media.Capture;
        using Windows.Storage;
        using System.Runtime.InteropServices.WindowsRuntime;

    That's all for UWP.

#### Implement IPhotoService for Android

Implementing the Android version is a bit more complicated because it requires the use of Android intents.

1. In the Android project, open the MainActivity.cs file.

    This file is the entry point for the Android application.

2. In that file, create a new static readonly property of the type **File**. This property will store the captured image

        static readonly File file =
        new File(Android.OS.Environment.GetExternalStoragePublicDirectory(
            Android.OS.Environment.DirectoryPictures), "tmp.jpg");

3. Create a new method to start the new Image Capture intent and place the results in a the new file:

        public void StartMediaCaptureActivity()
        {
                var intent = new Intent(MediaStore.ActionImageCapture);
                intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(file));
                StartActivityForResult(intent, 0);
        }

4. Add these namespaces to the file:

        using Java.IO;
        using Android.Content;
        using Android.Provider;

5. Add a new event, and then override the OnActivityResult method so that it can respond when the intent has completed and an image has been captured.

        public event EventHandler<File> ImageCaptured;

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
                if (requestCode == 0 && resultCode == Result.Ok)
                {
                    ImageCaptured?.Invoke(this, file);
                }
        }

    Later, we can subscribe to the ImageCaptured event that you created so that we're notified when an image has been captured.

6. Just like we did in the UWP project, create a new class in the Android project and name it PhotoService. Extend IPhotoService and register with the DependencyService by adding a metadata attribute above the namespace. Your class should look like this:

        using Xamarin.Forms;
        using System.Threading.Tasks;
        using YourNamespace.Droid;

        [assembly: Dependency(typeof(PhotoService))]
        namespace YourNamespace.Droid
        {
                public class PhotoService : IPhotoService
                {
                    public Task<byte[]> TakePhotoAsync()
                    {

                    }
                }
            }

    > Note: your namespace for Android might be **Droid** or **Android**


You're now ready to implement the Android version of the TakePhotoAsync method.

7. In the method, add code that does these things:

    * Calls the StartMediaCaptureActivity method you created in MainActivity.
    * Creates an event handler to listen to when the image has been captured.
    * Creates TaskCompletionSource instance that will complete once the image has been captured and the event has raised.

    Here's the code.

        public Task<byte[]> TakePhotoAsync()
        {
            var mainActivity = Forms.Context as MainActivity;
            var tcs = new TaskCompletionSource<byte[]>();
            EventHandler<Java.IO.File> handler = null;
            handler = (s, e) =>
            {
                using (var streamReader = new StreamReader(e.Path))
                {
                    using (var memstream = new MemoryStream())
                    {
                        streamReader.BaseStream.CopyTo(memstream);
                        tcs.SetResult(memstream.ToArray());
                    }
                }
                mainActivity.ImageCaptured -= handler;
            };

            mainActivity.ImageCaptured += handler;
            mainActivity.StartMediaCaptureActivity();
            return tcs.Task;
        }

    You're now done with Android.

#### Consume the PhotoService class

1. In the new page that you created (CameraPage.xaml in this example), add a Button and an Image element to host the capture image. Create an event handler for the Clicked event of the button.

        <StackLayout VerticalOptions="FillAndExpand"
                        HorizontalOptions="FillAndExpand"
                        Orientation="Vertical"
                        Spacing="15">
            <Button x:Name="captureButton"
                    Text="Capture Image"
                    Clicked="captureButton_Clicked"></Button>
            <Image x:Name="image"></Image>
        </StackLayout>

6. In the button event handler, create an instance of the PhotoService class via the DependencyService, and then call the TakePhotoAsync method to capture an image. Once the image is captured, set the source of the image:

        using System.IO;

        private async void captureButton_Clicked(object sender, EventArgs e)
        {
            var photoService = DependencyService.Get<IPhotoService>();
            if(photoService != null)
            {
                var imageBytes = await photoService.TakePhotoAsync();
                if(imageBytes != null)
                {
                    image.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                }
            }
        }

And you're done! Run the app and try it out. You should be able to navigate to the new page when you click on a nose. There should be a button to capture an image that will open the platform specific UI for capturing images. Once the image is captured, it should display the image below the button.

[Go to the next Task](313_InkCanvas.md) where you'll extend this page to overlay the noses on top of the image and add Inking capabilities on Windows.

## continue to [next task >> ](313_InkCanvas.md)

## The solution for this task is located [here](https://github.com/Knowzy/KnowzyAppsFinal/tree/master/stories/3/3.1.2) 
