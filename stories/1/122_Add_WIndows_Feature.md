# Task 1.2.2 - Add Windows 10 feature

## Prerequisites 

This task has a dependency on [Task 1.2.1(121_Test_App.md) and all of it's prerequisites

## Task 

### Add a Windows feature to your web content.
 One of the coolest things about PWAs on Windows 10 is the technical advantages you get over just running in the browser:

- Standalone Window
- Independent from browser process (Less overhead / Isolated cache)
- Nearly unlimited storage (indexed DB, localStorage, etc.)
- Offline & background processes
- Access to Windows Runtime (WinRT) APIs via JavaScript (Calendar / Cortana / Address Book / etc)


### feature detect for Windows Store APIs
In order to utilize a Windows UWP API, you'll want to implement a pattern called "feature detection".  This allows you to write JavaScript on your page that will only be executed in the context where the APIs are available.  In this case, it will only execute while inside a Windows PWA.
  
Feature detection can be as simple as looking for the Windows object as below:

```

if(window.Windows){
/*execute code that calls WIndows APIs */
}

```

Keep in mind that not all Windows APIs are available on all Windows 10 devices.  Think about the fact that a Windows Phone has a "dialer" for making phone calls, so it has unique APIs that are not available on other device types.  Another example is the "surface dial" that can be paired to Many Windows devices, but not the Xbox, so the "radial dial" APIs are not present on the Xbox.  For more specific feature detection, you'll also want to feature detect the API family as well like below:
```

if(window.Windows && Windows.UI.Core.SystemNavigationManager){
/*execute code that calls WIndows APIs */
}

``


### Add code for specific feature

###Deploy changes

### test your changes
1. Uninstall your app
2. Reinstall app from 


Walkthrough goes here

## References






