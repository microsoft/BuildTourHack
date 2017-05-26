# Task 3.1.3 - Overlay noses and support inking for UWP

Our marketing department has the idea to allow our app users to capture images and position Knowzy products over the image to see what how they would look like. It's a fun way to try the product without actually buying it, and the marketing department is hoping for these images to be shared on social media and spread the word.

**Goals for this task:**
* Overlay noses on top of image and allow nose to be manipulated
* Support inking on UWP devices

For this task, you will need to access APIs that are specific for each platform. We've done the research on how to do it and we've included the steps below.

## Prerequisites 

This task has a dependency on [Task 3.1.2](312_Camera.md) and all of it's prerequisites

## Task 

#### Overlay nose on image and allow to be manipulated

Once the image is captured, let's add the nose image on top and allow the user to move it by panning and resize it by pinching.

1. In the new page xaml, wrap the image element hosting the camera image in a new Grid element. This will allow you to have multiple elements on top of each other. In addition, add a new child element to the grid of type [AbsoluteLayout](https://developer.xamarin.com/guides/xamarin-forms/user-interface/layouts/absolute-layout/) with content of a new image element used for hosting the nose image. Here is what the final result will look like:

    Before:

    ```xaml
    <Image x:Name="image"></Image>
    ```

    After:

    ```xaml
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

    ```

2. Notice in the xaml above, we've set the visibility of the Grid to False. Once the image has been captured, we can set the visibility to visible and set the source of the noseImage. Here is what the new captureButton click handler looks like:

    ```csharp
    private async void captureButton_Clicked(object sender, EventArgs e)
    {
        var photoService = DependencyService.Get<IPhotoService>();
        if (photoService != null)
        {
            var source = await photoService.TakePhotoAsync();
            noseImage.Source = ImageSource.FromUri(new Uri(_nose.Image)); // set source of nose image
            image.Source = source;
            imageGrid.IsVisible = true; // set visibility to true
        }
    }
    ```

3. To allow the nose image to be manipulated by panning or pinch, Xamarin.Forms has built in [Gestures](https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/gestures/) on UI elements. In your xaml, add a new PanGestureRecognizer and a new PinchGestureRecognizer and subscribe to the relevant events:

    ```xaml
    <Image x:Name="noseImage"
            HeightRequest="120" 
            WidthRequest="120" 
            AbsoluteLayout.LayoutBounds="0, 0, AutoSize, AutoSize" 
            AbsoluteLayout.LayoutFlags="None">
        <Image.GestureRecognizers>
            <PanGestureRecognizer PanUpdated="OnPanUpdated" />
            <PinchGestureRecognizer PinchUpdated="OnPinchUpdated" />
        </Image.GestureRecognizers>
    </Image>
    ```

4. In your code behind, implement the event handlers for the gestures so when panning, the nose moves with the finger or mouse, and when pinching, the scale of the image changes appropriately:

    ```csharp
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
    ```

That's all. Run the app, take a photo, position the nose and have fun.

#### Adding inking support on UWP devices.

In addition to using the built in Xamarin.Forms controls, developers have full access to native platform controls as well through [native view declaration](https://developer.xamarin.com/guides/xamarin-forms/user-interface/native-views/). This allows developers to use native  or custom control (such as the UWP Community Toolkit) and mix them with Xamarin.Forms controls directly in their Xaml files. In our case, we can use the native InkCanvas control and InkToolbar control when the app runs on UWP.

1. To make native views consumable via XAML, we must first add XML namespaces for each platform we’ll be embedding views from. In this case, we will add the namespace for the UWP native controls as part of the ContentPage declaration of the new page:

    ```xaml
    <ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:win="clr-namespace:Windows.UI.Xaml.Controls;assembly=Windows,
                Version=255.255.255.255, Culture=neutral, PublicKeyToken=null,
                ContentType=WindowsRuntime;targetPlatform=Windows"
             x:Class="App1.CameraPage">
    ```

2. We can now add UWP XAML controls directly on the page. Add the InkCanvas on top of the camera image but under the nose image, and add the InkToolbar control between the button and imageGrid.

    ```xaml
    <StackLayout VerticalOptions="FillAndExpand"
                 HorizontalOptions="FillAndExpand"
                 Orientation="Vertical"
                 Spacing="15">
        <Button x:Name="captureButton" 
                Text="Capture Image"
                Clicked="captureButton_Clicked"></Button>

        <ContentView x:Name="InkingToolbar">
            <win:InkToolbar></win:InkToolbar>
        </ContentView>

        <Grid x:Name="ImageGrid" IsVisible="False">
            <Image x:Name="image"></Image>

            <ContentView x:Name="InkingContent">
                <win:InkCanvas></win:InkCanvas>
            </ContentView>

            <AbsoluteLayout>
                <!-- ... -->
            </AbsoluteLayout>
            
        </Grid>
    </StackLayout>
    ```

    > Note: It is not possible to name native views, so we use a ContentView as a way to get a reference to the native views in our code behind

3. To use the native views in the code behind, we need to use compilation directives as the native views will only be used on the platform they are available. In this case, the InkCanvas and InkToolbar are only available on UWP, so we need to use the **WINDOWS_UWP** directive to wrap our code. In the constructor of our new page, we need to bind the InkToolbar to the InkCanvas and set the input device type of the InkCanvas to all input types:

    ```csharp
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
    ```

That's it. Run the app and draw the perfect masterpiece.

## References

* [Xamarin Native Views](https://developer.xamarin.com/guides/xamarin-forms/user-interface/native-views/)
