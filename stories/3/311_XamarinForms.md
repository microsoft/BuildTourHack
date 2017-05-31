# Task 3.1.1 - Create a Xamarin.Forms app with shared UI

Building a cross platform mobile application will help our marketing department reach an even wider audience and potential customers. Xamarin.Forms will allow us to build the application only once and still be able to reach multiple platforms. 

**Goals for this task:**
* Mobile application with Shared App running on Android and UWP

This is going to be an entire new product for Knowzy and we will start from scratch. We've already done some investigation from the requirements that our management has given us and we have written a guide for the developer on how to get started.

## Prerequisites 

This walkthrough assumes that you have:
* Windows 10 Creators Update
* Visual Studio 2017 with the Mobile Development with .NET workload the Universal Windows Platform development workload installed. If not, make sure you [do that first](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio) and then come back here.

## Task 

#### Start by creating a new Xamarin.Forms application

1. In Visual Studio, click on **File -> New -> Project**. 

2. Under **Templates -> Visual C# -> Cross-Platform** select *Cross Platform App (Xamarin.Forms or Native)*. Pick a name and Create the project
    > Note: pick a short name and place the project closer to the root of your drive (ex: c:\source) in order to avoid long names that might cause issues later when running your project

3. We will start with a Blank App. Make sure Xamarin.Forms is selected under **UI Technology** and Shared Project under **Code Sharing Strategy**

    ![New Project](images/new_project.png)

> Note: A *Xamarin Mac Agent* window might open asking you to connect to a Mac as soon as you create the project. You can safely ignore and close this window.

> Note: A *New Universal Windows Project* might open asking you to choose target and minimum platform version. Make sure **Target Version** is *Windows 10 Creators Update*. Minimum version can be anything.

![New UWP](images/new_uwp.png)

That's it. At this point, you should probably spend some time checking out the new solution. You will notice there are four projects in the solution, one shared project and three platform specific projects. To run the app on the specific platform, use the drop down at the top of Visual Studio to select what project to run:

![Select Project](images/select_platform.png)

We will focus on UWP and Android for our first release. To run on your machine as UWP, select the UWP project first. Then change the architecture (the dropdown on the left of the Startup projects dropdown) and select x86 or x64. Then simply click the play button to build and run the app:

![Run](images/run.png)

To test and debug the app on Andorid, there are several options:
* [Use the Android SDK Emulator](https://developer.xamarin.com/guides/android/deployment,_testing,_and_metrics/debug-on-emulator/android-sdk-emulator/)
* [Use the Visual Studio Emulator](https://developer.xamarin.com/guides/android/deployment,_testing,_and_metrics/debug-on-emulator/visual-studio-android-emulator/)
* [Use a real device](https://developer.xamarin.com/guides/android/deployment,_testing,_and_metrics/debug-on-device/)

> Note: If you try to run the faster x86 version of the Android SDK Emulator and get an exception, you might need to turn of the hypervisor by running the following command in Command Prompt as Administrator: ```bcdedit /set hypervisorlaunchtype off``` and reboot

Change the Startup Project to the Android project and use the dropdown on the right to select the emulator (or device). Then click the play icon to build and run in the emulator.

![Run Android](images/run_android.png)

Now get to know your new app.

> Note: Since we will not be using the iOS project for this release, feel free to remove it from your solution

#### Add shared Business Logic

For our first task, we want to be able to list all the different Knowzy products. Fortunately, there is already a public feed for all products that we can use located [here](https://raw.githubusercontent.com/Knowzy/KnowzyInternalApps/master/src/Noses/noses.json). We can use this to get all of the data for our app. 

1. Let's create a new class that we can use to represent our nose model. Right click on the Shared project (the one without a platform specifier at the end) and click **Add -> Class**:

    ![Add Class](images/add_class.png)

    Name the new class **Nose**. Erase everything between the namespace definition. We need our new class to match the data we get from our JSON feed, so we will create a new class from the JSON. Copy this JSON but don't paste it anywhere yet:

    ```json
    {
        "Id": "RN3454",
        "Name": "Black Nose",
        "RawMaterial": "Black foam",
        "Notes": "Everything you'd expect, and a little something more.",
        "Image": "https://raw.githubusercontent.com/Knowzy/KnowzyInternalApps/master/src/Noses/Frabicnose400x300.jpg"
    }
    ```

    Switch to Visual Studio, place the cursor where you want to copy the new class (between the namespace braces). In Visual Studio, click on **Edit -> Paste Special -> Paste JSON as Classes**. This will generate a new class for you by using the JSON you just copied and you just need to change the name from RootObject to **Nose**.

    ![JSON as Class](images/json_as_class.png)

2. Now that we have our model, let's create a way to retrieve the data from our feed. 
    * First, we will use Json.Net to deserialize the JSON, so you will need to first reference the Nuget package to both the UWP and Android project. Right click on each project, click on **Manage Nuget Packages**. Search for **Newtonsoft.Json** and install it (make sure to switch to the **Browse** tab when searching).

    * Follow the same steps as above to create a new class in the shared project. 
    * Name the new class **DataProvider**
    * Make the class public. 
    * Add this static method in the class to pull in the data from the link above:

        ```csharp
        public static async Task<Nose[]> GetProducts()
        {
            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync("https://raw.githubusercontent.com/Knowzy/KnowzyInternalApps/master/src/Noses/noses.json");

                return JsonConvert.DeserializeObject<Nose[]>(json);
            }
        }
        ```

        You will need to add few namespaces for this function to work:

        ```csharp
            using Newtonsoft.Json;
            using System.Net.Http;
            using System.Threading.Tasks;
        ```

   We now have a static method that retrieves the JSON feed and deserializes into Nose objects that we can use in out app.

#### Add shared UI

Now that we have the business logic out of the way, on to the UI. Xamarin.Forms uses XAML to define the shared UI, so if you've used XAML before, you will feel right at home. All the shared code is in the shared project in the solution, and there is already a page created for us: MainPage.xaml. Go ahead and open the page. Currently there is only one element there, a [Label](https://developer.xamarin.com/guides/xamarin-forms/user-interface/text/label/). Instead of a Label, we will use a [ListView](https://developer.xamarin.com/guides/xamarin-forms/user-interface/listview/) to display all of the products.

1. Remove the Label and add a new element ListView instead. Give it a name. In this case it's *ProductListView**

```xaml
    <ListView x:Name="ProductListView">

    </ListView>
    
 ```

2. Open MainPage.xaml.cs. This is where the code goes for your view. Here we can override the *OnAppearing* method which will allows us to get the list of products and set them as the source of the ListView. Add the following code:

```csharp
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        ProductListView.ItemsSource = await DataProvider.GetProducts();
    }
    
 ```

3. Finally, we need to define how each product will look like. For that we will create a data template to customize each [Cell](https://developer.xamarin.com/guides/xamarin-forms/user-interface/listview/customizing-cell-appearance/). Here is what the final XAML looks like for the ListView

```xaml
    <ListView x:Name="ProductListView">
        <ListView.ItemTemplate>
            <DataTemplate>
                <ViewCell>
                    <StackLayout Orientation="Horizontal">
                        <Image Source="{Binding Image}" HeightRequest="150" WidthRequest="150"></Image>
                        <Label Text="{Binding Name}"></Label>
                    </StackLayout>
                </ViewCell>
            </DataTemplate>
        </ListView.ItemTemplate>
    </ListView>
    
```

**Task Complete**. Go ahead and run the the app on your machine and run the app in the Android emulator.

![Finished Noses](images/noses_finished.png)

[Go to the next Task](312_Camera.md) where you will add another page and the capability to capture an image by using APIs specific to each platform.

## Resources

1. [Xamarin.Forms Quickstart](https://developer.xamarin.com/guides/xamarin-forms/getting-started/hello-xamarin-forms/quickstart/)
2. [Introduction to Xamarin.Forms](https://developer.xamarin.com/guides/xamarin-forms/getting-started/introduction-to-xamarin-forms/)
3. [Xamarin.Forms XAML documentation](https://developer.xamarin.com/guides/xamarin-forms/xaml/)

## continue to [next task >> ](312_Camera.md)
