# Task 3.3.2 - Capture images remotely

Many users like to launch and control desktop apps remotely from their phone.

## Prerequisites

This task has a dependency on [Task 3.1.2](312_Camera.md) and all of it's prerequisites.

## Task

1. Support launching the app remotely on UWP from Android.
2. Support controlling the app remotely on UWP from Android.

## Comments

###### @ 9:12am
I found [this blog post](https://blog.xamarin.com/building-remote-control-companion-app-android-project-rome/) that walks through using the Project Rome SDK to use android to launch and control the app on PC, it's exactly what we need.

###### @ 10:21am
Check out [Project Rome](https://github.com/Microsoft/project-rome) for docs and more samples

###### @ 10:45am
We can use an App Service to support the messaging between Android and UWP. Here is great [blog post](https://blogs.windows.com/buildingapps/2017/03/23/project-rome-android-update-now-app-services-support) on exactly that. I found some resources on creating app services:

* [Docs on creating and consuming app service](https://docs.microsoft.com/en-us/windows/uwp/launch-resume/how-to-create-and-consume-an-app-service)
* [Docs on communicating with a remote app service](https://docs.microsoft.com/en-us/windows/uwp/launch-resume/communicate-with-a-remote-app-service)
* [App service sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/AppServices)

## continue to [next task >> ](341_CognitiveServices.md)
