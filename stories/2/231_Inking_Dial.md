# Task 2.3.1 - Add support for ink

Our development department is pleased with the results of [Task 2.2.2 Add support for other apps to share images with the Knowzy App](222_Share.md) 
that allowed Knowzy app users to share images from other Windows 10 applications with the Knowzy app. However, we would like Knowzy users to be able
to annotate the image with the Windows 10 Inking APIs before sharing the image.


Requirements for this task:
* Share an image from another Windows 10 App. The Knowzy app should appear as one of the options for Share Targets.
* Display a XAML UI for sharing with the Knowzy app.
* Display a Toast that displays the image after the sharing operation has completed.

Our research has found a starting point for the inking idea [here](https://docs.microsoft.com/en-us/windows/uwp/input-and-devices/pen-and-stylus-interactions )

We also found sample code for Inking [here](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/SimpleInk)


## Prerequisites 

This task has a dependency on [Task 2.2.2](222_Share.md) and all of it's prerequisites

## Task 

Modify the SharePage.xaml ui to add Inking controls.
Share the inked image to the Knowzy UWP app.
