# Task 3.4.2 - Set up Continuous Integration and Deployment for the Android app using Visual Studio Mobile Center

Now that you've made an app that works, the CTO would like to ensure higher quality by setting up Continuous Integration and Delivery.  To that end, you've been instructed to make sure the app compiles with every code check-in, any tests are run, and new versions can be delivered to beta testers with ease.  This task will focus on the Android version of the app with another task focusing on Windows.

## Prerequisites 

* This task has a dependency on [Task 3.1.1][311] and all of it's prerequisites.
* If you've already set up a code repository for [Task 3.4.1][341], you can use that and skip to step 2 below.

## Task 

1.  You'll first need to ensure your Mobile App's source code is checked into a valid Version Control System (VCS).  If [Task 4.2.0][420] was already completed, you can reuse that account.  Otherwise, create a Visual Studio Team Services account, Github account, or BitBucket account.
2.  Using whichever VCS you've decided upon, create a new project and check your Mobile App code into it.
3.  Create or sign-in to an account at [Visual Studio Mobile Center](http://mobile.azure.com).
4.  Create a new Android app targeting Xamarin.
5.  Connect the **Build** page to the repository you have created.
6.  Set things to build on every push and distribute builds.
7.  Ensure your app builds the first time.  
8.  Commit a code change to your application and ensure your app is automatically rebuilt.
9.  You should already be a member of the group that receives app update notifications.  Add your teammates.
10.  If someone has a compatible Android device, install your app via Mobile Center and ensure it receives updates when the app is updated.

## References

* [Creating a Keystore for signing an app](https://developer.xamarin.com/guides/android/deployment,_testing,_and_metrics/publishing_an_application/part_2_-_signing_the_android_application_package/visual-studio-xa-4.2.5-and-earlier/)
* [Visual Studio Mobile Center documentation](https://docs.microsoft.com/en-us/mobile-center/general/support-center)

[311]: /stories/3/311_XamarinForms.md
[420]: /stories/4/420_SetupVSTS.md