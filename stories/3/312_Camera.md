# Task 3.1.2 - Capture Images

Our marketing department wants to allow users of our app to capture images and position Knowzy products over the image to see how they would look. It's a fun way to try the product without actually buying it, and the marketing department is hoping for these images to be shared on social media to spread the word.

**Goals for this task:** Enable your Android and UWP app to capture images from the camera

For this task, you will need to access APIs that are specific to each platform. We've done the research on how to do it and included the steps below.

## Prerequisites 

This task has a dependency on [Task 3.1.1](311_XamarinForms.md) and all of it's prerequisites

## Task 

#### Create and navigate to a new page

1. Right click on the Shared project, select **Add -> New Item**. Under **Visual C# -> Cross-Platform** select **Forms Blank Content Page Xaml**. Give it a name (we use CameraPage in this guide) and click **Add**. This will create a new page which you can navigate to once a product has been selected on the Main page. 

2. There are [multiple ways to navigate between pages](https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/navigation/). In this example, we will use a **NavigationPage** to act as a host for our pages and provide hierarchical navigation. Open App.xaml.cs in the Shared project. Notice the constructor sets the MainPage to a new MainPage (the default page when the app is created):

```CSharp
MainPage = new Knowzy.Mobile.MainPage();
```

    > Note: the **Knowzy.Mobile** namespace above might be different for you depending on what you named your project

Instead of setting the MainPage to a new MainPage, set it to a new NavigationPage and pass a new MainPage as a parameter. This will set it as the first page in our hierarchical navigation system.

```CSharp
MainPage = new NavigationPage(new MainPage());
```

3. You are now ready to navigate to the new page. We want to navigate to the new page when a product (nose) is clicked in the main page, and we want to pass the nose as a parameter. The easiest way to do that is to pass the clicked nose as a parameter to the constructor when navigating to the new page. Open the code-behind file for the new page you created (CameraPage.xaml.cs in our example) and modify the constructor to accept a Nose object as a parameter.

```CSharp
Nose _nose;

public CameraPage(Nose nose)
{
    _nose = nose;
    InitializeComponent ();
}
```

4. Open the XAML file for the main page and add an ItemTapped event handler for when an item has been taped on the ListView.

```xml
<ListView x:Name="ProductListView" ItemTapped="ProductListViewItemTapped">
    <!-- ... -->
</ListView>
```
5. In the code-behind for the main page (MainPage.xaml.cs), implement the event handler and add the following code to navigate to the new page by passing the tapped item:

```CSharp
private void ProductListViewItemTapped(object sender, ItemTappedEventArgs e)
{
    Navigation.PushAsync(new CameraPage(e.Item as Nose));
}
```

You should now be able to navigate to the new (empty) page. Test it out to make sure it all works as expected. 

#### Capture image from camera on Android and UWP

Once we've navigated to the new page, the goal is to capture an image from the camera. Because each platform has a different native API for camera capture, we will design an interface and implement it on each platform. We will then use the [DependencyService](https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/dependency-service/introduction/) from Xamarin.Forms to call the right implementation.

1. Create a new interface class in the Shared Project and change it to an interface called *IPhotoService* (Right Click on Shared Project -> Add -> Class). Add a method definition for capturing the photo. It should look like this:

```CSharp
public interface IPhotoService
{
    Task<ImageSource> TakePhotoAsync();
}
```

You will need to add few namespaces:

```CSharp
using System.Threading.Tasks;
using Xamarin.Forms;
```

2. You now need to implement this interface for each platform to use the native APIs. Let's start with UWP.

    * In the UWP project, create a new class and call it PhotoService. Extend IPhotoService and register with the DependencyService by adding a metadata attribute above the namespace. The class would look like this:

```CSharp
using Xamarin.Forms;
using YourNamespace.UWP;

[assembly: Dependency(typeof(PhotoService))]
namespace YourNamespace.UWP
{
        public class PhotoService : IPhotoService
        {
            public async Task<ImageSource> TakePhotoAsync()
            {

            }
        }
}
```

* Implement the TakePhotoAsync method to use the native [CameraCaptureUI](https://docs.microsoft.com/en-us/uwp/api/windows.media.capture.cameracaptureui) from UWP and make it async:

```CSharp
public Task<ImageSource> TakePhotoAsync()
{
        CameraCaptureUI captureUI = new CameraCaptureUI();
        captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;

        StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

        if (photo == null) return null;

        return ImageSource.FromFile(photo.Path);
}
```
You will also need few namespaces:

```CSharp
using Windows.Media.Capture;
using Windows.Storage;
```
That's all for UWP.

3. Implementing the Android version is a bit more complicated because it requires the use of Android intents. 

* Open MainActivity.cs in the Android project. This file is the entry point for the Android application. Create a new static readonly property of the type **File** to store the captured image and create a new method to start the new Image Capture intent to place the results in a the new file:

```CSharp
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

Add these namespaces: 

```CSharp
using Java.IO;
using Android.Content;
using Android.Provider;
```

* Next, in the same file, override the OnActivityResult method to respond when the intent has completed and an image has been captured. In addition create a new event as part of the MainActivity, so we can subscribe later to be notified when an image has been captured.

```CSharp
public event EventHandler<File> ImageCaptured;

protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
{
        if (requestCode == 0 && resultCode == Result.Ok)
        {
            ImageCaptured?.Invoke(this, file);
        }
}
```

* Just like in the UWP project, create a new class in the Android project and name it PhotoService. Extend IPhotoService and register with the DependencyService by adding a metadata attribute above the namespace. Your class should look like this:

```CSharp
using Xamarin.Forms;
using System.Threading.Tasks;
using YourNamespace.Droid;

[assembly: Dependency(typeof(PhotoService))]
namespace YourNamespace.Droid
{
        public class PhotoService : IPhotoService
        {
            public Task<ImageSource> TakePhotoAsync()
            {

            }
        }
}
```
> Note: your namespace for Android might be **Droid** or **Android**


* You are now ready to implement the Android version of the TakePhotoAsync method. In the method (1) call the StartMediaCaptureActivity method you created in MainActivity, (2) create an event handler to listen to when the image has been captured, and (3) create a TaskCompletionSource that will complete once the image has been captured and the event has fired. It should look like something like this:

```CSharp
public Task<ImageSource> TakePhotoAsync()
  {
      var mainActivity = Forms.Context as MainActivity;
      var tcs = new TaskCompletionSource<ImageSource>();
      EventHandler<Java.IO.File> handler = null;
      handler = (s, e) =>
      {
          tcs.SetResult(e.Path);
          mainActivity.ImageCaptured -= handler;
      };

      mainActivity.ImageCaptured += handler;
      mainActivity.StartMediaCaptureActivity();
      return tcs.Task;
  }
```
    You are now done with Android.

5. Time to use the PhotoService. In the new page you created (CameraPage.xaml in this example), create a new Button and a new Image element to host the capture image. Create an event handler for the button when clicked.

```XAML
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

6. In the event handler, create an instance of the PhotoService via the DependencyService and call the TakePhotoAsync method to capture an image. Once the image is captured, set the source of the image:

```CSharp
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

And you're done! Run the app and try it out. You should be able to navigate to the new page once you click on a nose. There should be a button to capture an image that will open the platform specific UI for capturing images. Once the image is captured, it should display the image below the button.

[Go to the next Task](313_InkCanvas.md) where you will extend this page to overlay the noses on top of the image and add Inking capabilities on Windows.

## continue to [next task >> ](313_InkCanvas.md)
