# Task 3.1.2 - Capture Images

Our marketing department has the idea to allow our app users to capture images and position Knowzy products over the image to see what how they would look like. It's a fun way to try the product without actually buying it, and the marketing department is hoping for these images to be shared on social media and spread the word.

Requirements for this task:
* Capture image from camera on Android and UWP

For this task, you will need to access APIs that are specific for each platform. We've done the research on how to do it and we've included the steps below.

## Prerequisites 

This task has a dependency on [Task 3.1.1](311_XamarinForms.md) and all of it's prerequisites

## Task 

#### Create and navigate to a new page

1. Right click on Shared project, go to **Add -> New Item**. Under **Visual C# -> Cross-Platform** select *Forms Blank Content Page Xaml*. Give it a name and click Add. This will create a new Page where you can navigate to once a product has been selected on the Main page. 

2. There are [multiple ways to navigate between pages](https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/navigation/). In this example, we will use a **NavigationPage** to act as a host for our pages and provide hierarchical navigation. Open App.xaml.cs in the Shared project. Notice the constructor sets the MainPage to a new MainPage (the default page when the app is created):

    ```csharp
    MainPage = new App1.MainPage();
    ```

    Instead of setting the MainPage to a new MainPage, set it to a new NavigationPage and pass a new MainPage as a parameter which will set it as the first page in our hierarchical navigation.

    ```csharp
    MainPage = new NavigationPage(new App1.MainPage());
    ```

3. You are now ready to navigate to the new page. We want to navigate to the new page when a product (nose) is clicked in the main page and we want to pass the nose as a parameter. The easiest way to do that is to pass the clicked nose as a parameter to the constructor when navigating to the new page. Open the code behind of the new page you created and modify the constructor to accept a parameter of type Nose.

    ```csharp
    Nose _nose;
    public CameraPage(Nose nose)
    {
        _nose = nose;
        InitializeComponent ();
    }
    ```

4. Open the xaml file for the main page and add a click event handler for when an item has been taped on the ListView.

    ```xaml
    <ListView x:Name="ProductListView" ItemTapped="ProductListViewItemTapped">
        <!-- ... -->
    </ListView>
    ```

5. In the code behind for the main page, implement the event handler and add the code to navigate to the new page by passing the taped item

    ```csharp
    private void ProductListViewItemTapped(object sender, ItemTappedEventArgs e)
    {
        Navigation.PushAsync(new CameraPage(e.Item as Nose));
    }
    ```

That's it. Test it out to make sure it all works as expected.

#### Capture image from camera on Android and UWP

Once we've navigated to the new page, the goal is to capture an image from the camera. Because each platform has a different native API for camera capture, we will create an interface that we will implement on each platform. We will then use the [DependencyService](https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/dependency-service/introduction/) from Xamarin.Forms to call the right implementation.

1. Create a new interface class in the Shared Project and change it to an interface called *IPhotoService* (Right Click on Shared Project -> Add -> Class). Add a method definition for capturing the photo. It should look like this:

    ```csharp
    public interface IPhotoService
    {
        Task<ImageSource> TakePhotoAsync();
    }
    ```

2. You now need to implement this method for each platform to use the native APIs. In both the Android and UWP project, create a new class and call it PhotoService. Extend IPhotoService and register with the DependencyService by adding a metadata attribute above the namespace. The class would look like this:

    ```csharp
    [assembly: Dependency(typeof(PhotoService))]
    namespace [Yournamespace].[UWP/Android]
    {
        public class PhotoService : IPhotoService
        {
            public async Task<ImageSource> TakePhotoAsync()
            {
                
            }
        }
    }
    ```

3. In the UWP version of the PhotoService class, implement the TakePhotoAsync method to use the native [CameraCaptureUI](https://docs.microsoft.com/en-us/uwp/api/windows.media.capture.cameracaptureui) from UWP:

     ```csharp
     public async Task<ImageSource> TakePhotoAsync()
    {
        CameraCaptureUI captureUI = new CameraCaptureUI();
        captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;

        StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
        var stream =  await photo.OpenStreamForReadAsync();
        return ImageSource.FromStream(() => stream);
    }
     ```

     That's all for UWP

4. Implementing the Android version is a bit more complicated because it requires the use of Android intents. 
    * In the Android project, open MainActivity.cs. This file is the entry point for the Android application. Create a new File property and a new file to store the captured image and create a new method to start the new Image Capture intent and to place the results in a the new file:

        ```csharp
        static readonly File file = 
            new File(Android.OS.Environment.GetExternalStoragePublicDirectory(
                Android.OS.Environment.DirectoryPictures), "tmp.jpg");

        public void StartMediaCaptureActivity()
        {
            var intent = new Intent(MediaStore.ActionImageCapture);
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(file));
            StartActivityForResult(intent, 0);
        }

        ```
    
    * Next, override the OnActivityResult method to respond when the intent has completed and the image has been captured. In addition create a new event as part of the MainActivity so we can subscribe later to be notified when the image has been captured.

        ```csharp
        public event EventHandler<File> ImageCaptured;

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == 0 && resultCode == Result.Ok)
            {
                ImageCaptured?.Invoke(this, file);
            }
        }
        ```

    * You are now ready to implement the Android version of the TakePhotoAsync method in the PhotoService. In the method, (1) call the StartMediaCaptureActivity you created in MainActivity, (2) create an event handler to listen to when the image has been captured, and (3) create a TaskCompletionSource that will complete once the image has been captured. It should look like something like this:

        ```csharp
        public Task<ImageSource> TakePhotoAsync()
        {
            var mainActivity = Forms.Context as MainActivity;
            var tcs = new TaskCompletionSource<ImageSource>();
            EventHandler<Java.IO.File> handler = (s, e) =>
            {
                tcs.SetResult(e.Path);
            };

            mainActivity.ImageCaptured += handler;
            mainActivity.StartMediaCaptureActivity();
            return tcs.Task;
        }
        ```

    You are now done with Android.

5. Time to use the PhotoService. In your new page xaml, create a new Button and a new Image element to host the capture image. Create an event handler for the button when clicked.

    ```xaml
    <StackLayout VerticalOptions="FillAndExpand"
                 HorizontalOptions="FillAndExpand"
                 Orientation="Vertical"
                 Spacing="15">
        <Button x:Name="captureButton" 
                Text="Capture Image" 
                Clicked="captureButton_Clicked"></Button>
        <Image x:Name="image"></Image>
    </StackLayout>
    ```

6. In the event handler, create an instance of the PhotoService via the DependencyService and call the TakePhotoAsync to capture an image. Once the image is captured, set the source of the image:

    ```csharp
    private async void captureButton_Clicked(object sender, EventArgs e)
    {
        var photoService = DependencyService.Get<IPhotoService>();
        if(photoService != null)
        {
            var source = await photoService.TakePhotoAsync();
            image.Source = source;
        }
    }
    ```

That's it, run the app and try it out.
