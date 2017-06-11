# Task 3.5.1 - Set up Continuous Integration and Deployment for the Windows app using Visual Studio Mobile Center

Now that you've made an app that works, the CTO would like to ensure higher quality by setting up Continuous Integration and Delivery.  To that end, you've been instructed to make sure that after every code check-in the app compiles, all tests are run, and new versions can be delivered to beta testers with ease.  This task will focus on the Windows version of the app with the next task covering Android.

## Prerequisites

* This task has a dependency on [Task 3.1.1][311] and all of it's prerequisites.
* If you've already set up a code repository for [Task 3.4.2][342], you can use that and skip to step 2 below.


## Task

1.  Add your application to a compatible source control system.
2.  Create a UWP app in the Mobile Center, and connect it to your repo.
3.  Ensure your app builds the first time.  
4.  Add your teammates so that they will receive notifications on build.
5.  Make sure your teammates can install your app.

## Comments

###### @ 7:37am
It sounds like we can use multiple different source control systems with Mobile Center.  Check out [these docs](https://docs.microsoft.com/en-us/mobile-center/build/connect) I found about connecting to a repository.

###### @ 9:23am
It sounds like the Build config in Mobile Center let's us trigger a build with every code check-in.  That means our builds will happen automatically!

###### @ 11:56am
My friends wanted to know whenever I built an app so I figured out how to create a distribution group [here](https://docs.microsoft.com/en-us/mobile-center/distribution/groups).

[311]: /stories/3/311_XamarinForms.md
[352]: /stories/3/352_CICD_AndroidApp.md
[420]: /stories/4/420_SetupVSTS.md

## continue to [next task >> ](352_CICD_AndroidApp.md)
