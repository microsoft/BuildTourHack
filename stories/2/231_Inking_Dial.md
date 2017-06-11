# Task 2.3.1 - Add support for ink

Our development department is pleased with the results of [Task 2.2.2 Add support for other apps to share images with the Knowzy App](222_Share.md) 
that allowed Knowzy app users to share images from other Windows 10 applications with the Knowzy app. However, we would like Knowzy users to be able
to annotate the image with the Windows 10 Inking APIs before sharing the image.


## Prerequisites 

This task has a dependency on [Task 2.2.2](222_Share.md) and all of it's prerequisites

## Task 

1. Share an image from another Windows 10 App. The Knowzy app should appear as one of the options for Share Targets.

2. Display a XAML UI for sharing with the Knowzy app.

3. Enable Inking controls on the SharePage to allow the user to annotate the image. Look [here](https://stackoverflow.com/questions/37179815/displaying-a-background-image-on-a-uwp-ink-canvas)
for some sample code. You will need to add the Win2D.uwp NuGet Package to your UWP project.

4. Display a Toast that displays the annotated image after the sharing operation has completed.

## Comments

###### @ 8:12am
Our research has found a starting point for the inking idea [here](https://docs.microsoft.com/en-us/windows/uwp/input-and-devices/pen-and-stylus-interactions )

###### @ 10:43am
We also found sample code for Inking [here](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/SimpleInk)

###### @ 10:49am

You might need to add the following to the Dependencies tag of Package.appxmanifest in Microsoft.Knowzy.UWP in order to be able to use Win2D with Visual Studio 2017

```xml
  <Dependencies>
    <TargetDeviceFamily Name="Windows.Desktop" MinVersion="10.0.14393.0" MaxVersionTested="10.0.14393.0" />
    <PackageDependency Name="Microsoft.VCLibs.140.00" MinVersion="14.0.22929.0" Publisher="CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US" />
  </Dependencies>
```

###### @ 10:58am

Hint: save the image file you receive from the Sharing Protocol to ApplicationData.Current.TemporaryFolder before trying to open it with Win2D.

## The solution for this task is located [here](https://github.com/Knowzy/KnowzyAppsFinal/tree/master/stories/2/2.3.1)

## continue to [next task >> ](232_Windows_Hello.md)
