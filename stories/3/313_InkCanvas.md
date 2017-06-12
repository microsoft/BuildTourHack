# Task 3.1.3 - Overlay noses and support inking for UWP

Our marketing department wants to allow users of our app to capture images, and position Knowzy products over the image to see how they would look. It's a fun way to try the product without actually buying it. The marketing department is hoping that these images will be shared on social media to spread the word.

**Goals for this task:**
* Overlay noses on top of an image and allow nose to be manipulated.
* Support inking on UWP devices.

For this task, you will need to access APIs that are specific to each platform. We've done the research on how to do it and have included the steps below.

## Prerequisites

This task has a dependency on [Task 3.1.2](312_Camera.md) and all of it's prerequisites.

## Task

#### Overlay nose on image and allow to be manipulated

Once the image is captured, let's add the nose image on top of it, and allow the user to move it by panning and resize it by pinching.

1. Open the XAML page that you added in the previous task (CameraPage.xaml in this example).

2. Add code that does these things:

    * Wraps the image element that hosts the camera image into a new Grid element.

        This will allow you to position multiple elements on top of each other.

    * Adds a new [AbsoluteLayout](https://developer.xamarin.com/guides/xamarin-forms/user-interface/layouts/absolute-layout/) element below the existing Image in the grid.

    * Adds a new Image element inside of the AbsoluteLayout element, which will be used to host the nose image.

    Here is what the final result should look like:

    Before:

        <Image x:Name="image"></Image>

    After:

        <Grid x:Name="imageGrid" IsVisible="False">
            <Image x:Name="image"></Image>
            <AbsoluteLayout>
                <Image x:Name="noseImage"
                    HeightRequest="120"
                    WidthRequest="120"
                    AbsoluteLayout.LayoutBounds="0, 0, AutoSize, AutoSize"
                    AbsoluteLayout.LayoutFlags="None">
                </Image>
            </AbsoluteLayout>
        </Grid>

3. Notice that in the XAML, we've set the visibility of the Grid to False. Once the image has been captured, we can set the visibility to True, and then set the source of the noseImage element. Make those changes in the captureButton_Clicked event handler. When you're done, your code should look something like this:

        private async void captureButton_Clicked(object sender, EventArgs e)
        {
            var photoService = DependencyService.Get<IPhotoService>();
            if (photoService != null)
            {
                var imageBytes = await photoService.TakePhotoAsync();
                noseImage.Source = ImageSource.FromUri(new Uri(_nose.Image)); // set source of nose image
                image.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                imageGrid.IsVisible = true; // set visibility to true
            }
        }

3. To allow elements to be manipulated by panning or pinching, Xamarin.Forms has built in [Gestures](https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/gestures/). Inside of the the noseImage element that we just added to our page, let's add a new PanGestureRecognizer and a new PinchGestureRecognizer, which will subscribe to the relevant events so we can manipulate the nose image with gestures:

        <Image x:Name="noseImage"
                HeightRequest="120"
                WidthRequest="120"
                AbsoluteLayout.LayoutBounds="0, 0, AutoSize, AutoSize"
                AbsoluteLayout.LayoutFlags="None">

            <!-- Gesture Recognizers -->
            <Image.GestureRecognizers>
                <PanGestureRecognizer PanUpdated="OnPanUpdated" />
                <PinchGestureRecognizer PinchUpdated="OnPinchUpdated" />
            </Image.GestureRecognizers>

        </Image>

4. In your code-behind file (CameraPage.xaml.cs in this example), implement the event handlers for the gestures that we just added. The nose moves with the finger or mouse, and the scale of the image changes when the image is pinched:

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    var bounds = AbsoluteLayout.GetLayoutBounds(noseImage);
                    bounds.X += noseImage.TranslationX;
                    bounds.Y += noseImage.TranslationY;
                    AbsoluteLayout.SetLayoutBounds(noseImage, bounds);
                    noseImage.TranslationX = 0;
                    noseImage.TranslationY = 0;
                    break;

                case GestureStatus.Running:
                    noseImage.TranslationX = e.TotalX;
                    noseImage.TranslationY = e.TotalY;
                    break;
            }
        }

        private void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            switch (e.Status)
            {
                case GestureStatus.Running:
                    noseImage.Scale *= e.Scale;
                    break;
            }
        }

    That's it. Run the app, take a photo, position the nose, and have fun.

#### Add inking support on UWP devices.

In addition to using the built in Xamarin.Forms controls, developers have full access to native platform controls through [native view declaration](https://developer.xamarin.com/guides/xamarin-forms/user-interface/native-views/). This allows developers to use native or custom controls (such as the UWP Community Toolkit), and mix them with Xamarin.Forms controls directly in XAML. For our app, we can use the native InkCanvas control and InkToolbar control when the app runs on UWP.

1. To make native views consumable via XAML, add XML namespaces for each platform we'll be embedding views from.

    We'll add the namespace for the UWP native controls as part of the ContentPage declaration that we created in the previous task (CameraPage in our example):

        <ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
                xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
                x:Class="App1.CameraPage"

                xmlns:win="clr-namespace:Windows.UI.Xaml.Controls;assembly=Windows,
                    Version=255.255.255.255, Culture=neutral, PublicKeyToken=null,
                    ContentType=WindowsRuntime;targetPlatform=Windows"
                >


2. We can now add two UWP XAML controls directly to the page.

    * Add the InkCanvas below the camera image but above the nose image.
    * Add the InkToolbar control between the top button and the imageGrid.

            <StackLayout VerticalOptions="FillAndExpand"
                        HorizontalOptions="FillAndExpand"
                        Orientation="Vertical"
                        Spacing="15">
                <Button x:Name="captureButton"
                        Text="Capture Image"
                        Clicked="captureButton_Clicked"></Button>

                <!-- UWP XAML CONTROL InkToolbar -->
                <ContentView x:Name="InkingToolbar">
                    <win:InkToolbar></win:InkToolbar>
                </ContentView>


                <Grid x:Name="ImageGrid" IsVisible="False">
                    <Image x:Name="image"></Image>


                    <!-- UWP XAML CONTROL InkCanvas -->
                    <ContentView x:Name="InkingContent">
                        <win:InkCanvas></win:InkCanvas>
                    </ContentView>



                    <AbsoluteLayout>
                        <!-- ... -->
                    </AbsoluteLayout>

                </Grid>
            </StackLayout>

    > Note: It is not possible to name native views, so we use a ContentView as a way to get a reference to the native views in our code-behind file.

3. To use the native views in the code behind, we need to use compilation directives, as the native views will only be used on the platform in which they are available. In this case, the InkCanvas and InkToolbar are only available on UWP, so we need to use the **WINDOWS_UWP** directive to wrap our code.

    In the constructor of our page, after the call to **InitializeComponent**, we need to bind the InkToolbar to the InkCanvas, and then set the input device type of the InkCanvas to all input types:

        #if WINDOWS_UWP
            var inkingWrapper = (Xamarin.Forms.Platform.UWP.NativeViewWrapper)InkingContent.Content;
            var inkCanvas = (Windows.UI.Xaml.Controls.InkCanvas)inkingWrapper.NativeElement;
            inkCanvas.InkPresenter.InputDeviceTypes =
                Windows.UI.Core.CoreInputDeviceTypes.Touch |
                Windows.UI.Core.CoreInputDeviceTypes.Mouse |
                Windows.UI.Core.CoreInputDeviceTypes.Pen;

            var inkToolbarWrapper = (Xamarin.Forms.Platform.UWP.NativeViewWrapper)InkingToolbar.Content;
            var inkToolbar = (Windows.UI.Xaml.Controls.InkToolbar)inkToolbarWrapper.NativeElement;
            inkToolbar.TargetInkCanvas = inkCanvas;
        #endif

4. If you run into a null reference exception, check to see if the following line is added above your page class definition:

        [XamlCompilation(XamlCompilationOptions.Compile)]

Xamarin adds this line to any new page created to [improve the performance](https://developer.xamarin.com/guides/xamarin-forms/xaml/xamlc/) of XAML pages. However, this optimization will not work when using native views and needs to be deleted.

And you're done! Run the app and draw the perfect masterpiece. You should now be able to start the app, select a nose, capture an image, position the nose where you want, and on UWP, draw using the pen, mouse or touch.

Congratulations, you are now done with the first deliverable. You should now be able to take control and start adding more features on your own. Take a look at the other deliverables and tasks for ideas and small hints about how to implement other features that would be useful for our users.

## References

* [Xamarin Native Views](https://developer.xamarin.com/guides/xamarin-forms/user-interface/native-views/)

## continue to [next task >> ](321_CustomVisionService.md)

## The solution for this task is located [here](https://github.com/Knowzy/KnowzyAppsFinal/tree/master/stories/3/3.1.3) 
