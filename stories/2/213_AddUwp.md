# Task 2.1.3 - Adding Windows 10 UWP APIs to your Desktop Bridge App

This task will guide you through the process of adding Windows 10 UWP APIs to your Desktop Bridge app. 

## Prerequisites 

* Basic knowledge of C# development

* Basic knowledge of client development with the .NET framework

* Basic knowledge of Windows 10 and the Universal Windows Platform

* A computer with Windows 10 Anniversary Update or Windows 10 Creators Update. If you want to use the Desktop App Converter with an installer, you will need at least a Pro or Enterprise version, since it leverages a feature called Containers which is not available in the Home version.

* Visual Studio 2017 with the tools to develop applications for the Universal Windows Platform. Any edition is supported, including the free [Visual Studio 2017 Community](https://www.visualstudio.com/vs/community/)

* Complete the section on [Debugging a Windows Desktop Bridge App](212_Debugging.md)

To get started, please open the **Microsoft.Knowzy.WPF.sln** in the **src\Knowzy_Engineering_Win32App** folder with Visual Studio 2017.

>Note: If you are starting with this from the **2.1.2** solution, you will need to do the following:

* Set the Build configuration to **Debug | x86**

    ![Debug | x86](images/212-debug-x86.png)

* Select **Options** from the **Debug** menu, From the **Debugging | General** Tab,  disable the **Enable Just My Code** option.

    ![Just My Code](images/212-just-my-code.png)

* Set the **Microsoft.Knowzy.Debug** project as the startup project.

Verify these settings before you continue with this task.

## Task

In this task we will do the following:

* Configure our Desktop Bridge projects to be able to use Windows 10 UWP APIs.

* Fix the issue from the previous tasks where the Desktop Bridge app was not able to load the Product.json file when running as a UWP app.

In the previous tasks we discovered that our Desktop Bridge version of the Knowzy app cannot find the required file Products.json. The UWP version is looking for Products.json in the location expected by the 
WPF version. We need to correct this code so that when the UWP version of Knowzy is running it will look for the file in the correct location.

* Searching the code for Products.json we find it in a file called Config.json. 

        {
        "JsonFilePath": "Products.json",
        "DataSourceUrl": "http://"
        }

* Searching for JsonFilePath, we find it in src\Microsoft.KnowzyJsonDataProvider\JsonDataProvider.cs

        public Product[] GetData()
        {
            var jsonFilePath = _configuration.Configuration.JsonFilePath;
            return _jsonHelper.Deserialize<Product[]>(_fileHelper.ReadTextFile(jsonFilePath));
        }

* Setting a breakpoint around line 40 of JsonDataProvider.cs and stepping through the code we eventually find that ReadTextFile is looking for the Products.json file in the installed directory of the app.

When we created the Desktop Bridge version of Knowzy we copied all of the WPF Knowzy binaries to the desktop folder of the Desktop Bridge app. 
After a build and deployment of the Knowzy app, we will find the Products.json file in the directory

src\Microsoft.Knowzy.UWP\bin\x86\Release\AppX\desktop

![AppX Desktop Folder](images/213-appx-desktop-folder.png)

So an easy fix would be to try something like:

    public Product[] GetData()
    {
        String jsonFilePath;
        
        if(IsRunningAsUwp())
        {
            jsonFilePath = "desktop\\" + _configuration.Configuration.JsonFilePath;
        }
        else
        {
            jsonFilePath = _configuration.Configuration.JsonFilePath;
        }

        return _jsonHelper.Deserialize<Product[]>(_fileHelper.ReadTextFile(jsonFilePath));
    }

I'll save you some time and tell you that this won't work either. 

![File not found](images/213-file-not-found.png)

We actually need to do something like this:

    public Product[] GetData()
    {
        String jsonFilePath;
        
        if(IsRunningAsUwp())
        {
            jsonFilePath = Path.Combine(GetUWPAppDir(),"desktop", _configuration.Configuration.JsonFilePath);
        }
        else
        {
            jsonFilePath = _configuration.Configuration.JsonFilePath;
        }

        return _jsonHelper.Deserialize<Product[]>(_fileHelper.ReadTextFile(jsonFilePath));
    }

We are going to need to add at least 2 UWP methods to our DeskTop Bridge version of Knowzy in order to be able to load the Products.json file.

1. A method to detect if we are running the UWP version

1. A method that returns the directory of the UWP application

#### Step 1: Adding UWP support to the Knowzy App

We can add UWP APIs to our Knowzy app at any location we need the UWP code. However, it will be easier if we create a set of UWP helper classes and place then all in a single C# library. 

Since all of the other dependencies in the Knowzy WPF solution are Windows Classic Desktop C# Class Libraries, we will add a new C# Class library called Microsoft.Knowzy.UwpHelpers.

* Right-click on the src folder in the solution and select **Add | New Project...**

* Select the **Visual C# | Windows Classic Desktop | Class Library** project template.

* Name the library Microsoft.Knowzy.UwpHelpers. Make sure you are saving the project to the **Knowzy_Engineering_Win32App\src** directory. Make sure you check that the .Net Framework version is 4.6.1 or lower. (Do not select version 4.6.2).

    ![Create C# Class Library](images/213-create-lib.png)

* Right-click on the Microsoft.Knowzy.UwpHelpers project and select **Add** and then **Reference...**

    ![Add Reference](images/213-add-reference.png)


* On the left of the Reference Manager, choose **Browse** and click the **Browse** button.

    ![Browse Reference](images/213-browse-reference.png)


* Find the following file: **"C:\Program Files (x86)\Windows Kits\10\UnionMetadata\10.0.15063.0\Windows.winmd"**.  Add it to your project as a reference. Note: You will need to change the filter to “All Files” to see this file.


    ![Windows.winmd](images/213-winmd.png)


* Right-click on the Microsoft.Knowzy.UwpHelpers project and select **Add** and then **Reference...**

* On the left of the Reference Manager, go to Browse and find the following file **"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETCore\v4.5\System.Runtime.WindowsRuntime.dll"**. Add a reference to this file.

    ![Windows.winmd](images/213-runtime.png)

* You should now having the following references. Click **OK**.

    ![References](images/213-references-complete.png)

* Rebuild the solution


Note: If you have the 14393 SDK installed in Visual Studio, instead of manually adding the above references to your porject, you can use a convenient NuGet package called [UwpDesktop](https://www.nuget.org/packages/UwpDesktop) that makes it easy for you call into UWP APIs 
from Desktop and Centennial apps (WPF, WinForms, etc.) 

#### Step 2: Adding a UWP Helper Class

* Add a new C# class to the Microsoft.Knowzy.UwpHelpers . Name the file ExecutionMode.cs.

* Add the following code to ExecutionMode.cs. This code detects if the app is running as a UWP app.

        using System;
        using System.Runtime.InteropServices;
        using System.Text;
        using Windows.System.Profile;

        namespace Microsoft.Knowzy.UwpHelpers
        {
            public class ExecutionMode
            {
                [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
                static extern int GetCurrentPackageFullName(ref int packageFullNameLength, ref StringBuilder packageFullName);

                public static bool IsRunningAsUwp()
                {
                    if (isWindows7OrLower())
                    {
                        return false;
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder(1024);
                        int length = 0;
                        int result = GetCurrentPackageFullName(ref length, ref sb);

                        return result != 15700;
                    }
                }

                internal static bool isWindows7OrLower()
                {
                    int versionMajor = Environment.OSVersion.Version.Major;
                    int versionMinor = Environment.OSVersion.Version.Minor;
                    double version = versionMajor + (double)versionMinor / 10;
                    return version <= 6.1;
                }
            }
        }

* Right-click on the Microsoft.Knowzy.JsonDataProvider project, select **Add | Reference...** and a reference to the Microsoft.Knowzy.UwpHelpers project.

* We can now modify src\Microsoft.KnowzyJsonDataProvider\JsonDataProvider.cs as follows:


        using Microsoft.Knowzy.UwpHelpers;
        using System;

        public Product[] GetData()
        {
            String jsonFilePath;

            if (ExecutionMode.IsRunningAsUwp())
            {
                jsonFilePath = "desktop\\" + _configuration.Configuration.JsonFilePath;
            }
            else
            {
                jsonFilePath = _configuration.Configuration.JsonFilePath;
            }

            return _jsonHelper.Deserialize<Product[]>(_fileHelper.ReadTextFile(jsonFilePath));
        }

* Build your solution and then set a break point at the following line of JsonDataProvider.cs (around line 40). 

        if (ExecutionMode.IsRunningAsUwp())
   
* Press F5 to launch your app and...

    **execution does not stop at the breakpoint!**
    
The build did not pick up our changes to Microsoft.Knowzy.JsonDataProvider. (Or if you rebuilt the entire solution, the changes did make it into the build).
In order to prevent having to do complete rebuild of our solution every time we change some code, we need to tell the DesktopBridge Debugging Project (Microsoft.Knowzy.Debug) which DLL's to copy to the AppX.
Unfortunately, the current version of Visual Studio 2017 is not able to correctly handle code changes Desktop Bridge dependencies.

Since we will be modifying the Microsoft.Knowzy.JsonDataProvider and Microsoft.Knowzy.UwpHelpers projects, let's add them to AppXPackageFileList.xml in the Microsoft.Knowzy.Debug project.

    <?xml version="1.0" encoding="utf-8"?>
    <Project ToolsVersion="14.0"
            xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
    <PropertyGroup>
        <MyProjectOutputPath>..\..\bin\Debug</MyProjectOutputPath>
    </PropertyGroup>
    <ItemGroup>
        <LayoutFile Include="$(MyProjectOutputPath)\Microsoft.Knowzy.WPF.exe">
        <PackagePath>$(PackageLayout)\desktop\Microsoft.Knowzy.WPF.exe</PackagePath>
        </LayoutFile>
        <LayoutFile Include="$(MyProjectOutputPath)\Microsoft.Knowzy.JsonDataProvider.dll">
        <PackagePath>$(PackageLayout)\desktop\Microsoft.Knowzy.JsonDataProvider.dll</PackagePath>
        </LayoutFile>
        <LayoutFile Include="$(MyProjectOutputPath)\Microsoft.Knowzy.UwpHelpers.dll">
        <PackagePath>$(PackageLayout)\desktop\Microsoft.Knowzy.UwpHelpers.dll</PackagePath>
        </LayoutFile>
    </ItemGroup>
    </Project>
    
Now every time you make a code change to Microsoft.Knowzy.JsonDataProvider or Microsoft.Knowzy.UwpHelpers, the changes will be part of the build.

Press F5 again and now you should be able to hit the breakpoint in JsonDataProvider.cs.

#### Step 3: Adding UWP support to detect the AppX Installation Folder

We are now going to start adding Windows 10 UWP APIs to our app in order to find the AppX folder's install location and in later tasks to add new Windows 10 features to our app. 

* Add a new C# class to theMicrosoft.Knowzy.UwpHelpers project. Name the file AppFolders.cs.

* Add the following code to AppFolders.cs. This code uses methods from the Windows 10 UWP API

        namespace Microsoft.Knowzy.UwpHelpers
        {
            public class AppFolders
            {
                public static string Current
                {
                    get
                    {
                        string path = null;
                        if (ExecutionMode.IsRunningAsUwp())
                        {
                            path = GetSafeAppxFolder();
                        }
                        return path;
                    }
                }

                internal static string GetSafeAppxFolder()
                {
                    try
                    {
                        return Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
                    }
                    catch (Exception ex)
                    {

                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                    return null;
                }
            }
        }

This code uses the Package class in Windows.ApplicationModel.Package to determine the installed location of the UWP AppX folder.

* We can now modify JsonDataProvider.cs as follows:

        using System.IO;

        public Product[] GetData()
        {
            String jsonFilePath;

            if (ExecutionMode.IsRunningAsUwp())
            {
                jsonFilePath = Path.Combine(AppFolders.Current, "desktop", _configuration.Configuration.JsonFilePath);
            }
            else
            {
                jsonFilePath = _configuration.Configuration.JsonFilePath;
            }

            return _jsonHelper.Deserialize<Product[]>(_fileHelper.ReadTextFile(jsonFilePath));
        }


In the ViewModels folder of the Microsoft.Knowzy.WPF project, open the MainViewModel.cs file and re-enable lines lines 72-75.


Press F5 to run the Microsoft.Knowzy.Debug project. Finally our Knowzy UWP app can load the Products.json file from the correct location and display the information correctly.

![Knowzy UWP](images/213-knowzy-uwp.png)

We can also still run our original WPF version. The UWP additions are ignored by the WPF version. To try this out right-click on the Microsoft.Knowzy.WPF project and select **Debug | Start new instance**

![Knowzy WPF Start New Instance](images/213-knowzy-wpf-start.png)

The WPF app still continues to work as it was originally coded.

![Knowzy WPF](images/213-knowzy-wpf.png)

We will continue to add more Windows 10 UWP features to our app in the [next task](214_WindowsHello.md).



## References
* [Calling Windows 10 APIs From a Desktop Application](https://blogs.windows.com/buildingapps/2017/01/25/calling-windows-10-apis-desktop-application/#USXJmIhfukL14wpE.97)

* [Package Class](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.package)

* [Desktop Bridge: Identify the application's context](https://blogs.msdn.microsoft.com/appconsult/2016/11/03/desktop-bridge-identify-the-applications-context/)

* [Announcing UWPDesktop NuGet Package Version 14393](https://blogs.windows.com/buildingapps/2017/01/17/announcing-uwpdesktop-nuget-package-version-14393/#SfO8ORg9vZY6h9dj.97)

* [UWP for Desktop NuGet Package](https://www.nuget.org/packages/UwpDesktop)

* [UWP for Desktop](https://github.com/ljw1004/uwp-desktop)

* [Run, debug, and test a packaged desktop app (Desktop Bridge)](https://docs.microsoft.com/en-us/windows/uwp/porting/desktop-to-uwp-debug)

* [DesktopBridge To UWP Samples](https://github.com/Microsoft/DesktopBridgeToUWP-Samples)

* [Package a .NET app using Visual Studio ](https://docs.microsoft.com/en-us/windows/uwp/porting/desktop-to-uwp-packaging-dot-net)

* [BridgeTour Workshop](https://github.com/qmatteoq/BridgeTour-Workshop)

* [Developers Guide to the Desktop Bridge](https://mva.microsoft.com/en-us/training-courses/developers-guide-to-the-desktop-bridge-17373)

## The solution for this task is located [here](https://github.com/Knowzy/KnowzyAppsFinal/tree/master/stories/2/2.1.3)

## continue to [next task >> ](214_WindowsHello.md)
