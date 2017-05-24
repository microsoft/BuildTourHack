# Task 3.4.2 - Add Custom Event Logging using Visual Studio Mobile Center

Now that you've completed Continuous Integration and Delivery, it's time to get some more value out of Mobile Center.  Specifically, we want you to use the Mobile Center SDK to start recording analytics on the usage of your app.  This will help show the execs how many people are using the app and how.

## Prerequisites 

* This task has a dependency on [Task 3.4.1][341] or [Task 3.4.2][342] so your app is already connected to Mobile Center.

## Task 

1.  Add the Mobile Center SDK to your solution and projects.
2.  Configure the SDK on app start.
3.  Add some custom logging and log an event in your app.

## Comments

###### @ 12:20pm
I found a great walkthrough on installing the Mobile Center SDK [here](https://docs.microsoft.com/en-us/mobile-center/sdk/getting-started/xamarin).

###### @ 2:31pm
[This](https://docs.microsoft.com/en-us/mobile-center/sdk/analytics/xamarin) made adding logging to my Xamarin app super easy.

###### @ 4:17pm
It looks like there is a bit of customization you need to do for UWP.  [Check it out](https://docs.microsoft.com/en-us/mobile-center/sdk/analytics/uwp).


[341]: /stories/3/341_CICD_WindowsApp.md
[342]: /stories/3/342_CICD_AndroidApp.md