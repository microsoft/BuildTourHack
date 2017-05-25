# Task 3.1.1 - Create a Xamarin.Forms app with shared UI

Building a cross platform mobile application will help our marketing department reach an even wider audience and potential customers. Xamarin.Forms will allow us to build the application only once and still be able to reach multiple platforms. 

**Requirements for this task:**
* Mobile application with Shared App running on Android and UWP

This is going to be an entire new product for Knowzy and we will start from scratch. We've already done some investigation from the requirements that our management has given us and we have written a guide for the developer on how to get started.

## Prerequisites 

This walkthrough assumes that you have:
* Windows 10 Creators Update
* Visual Studio 2017 with the Mobile Development with .NET workload installed. If not, make sure you [do that first](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio) and then come back here.

## Task 

#### Start by creating a new Xamarin.Forms application

1. In Visual Studio, click on **File -> New -> Project**. 
    
    Alternatively, you can start with the existing Visual Studio Solution for the Win32 app and add the new project there. This will allow you to share code more easily across projects. To do so, right click on the solution in Solution explorer and click on **Add -> New Project**

2. Under **Templates -> Visual C# -> Cross-Platform** select *Cross Platform App (Xamarin.Forms or Native)*. Pick a name and Create the project
3. We will start with a Blank App. Make sure Xamarin.Forms is selected under **Technology** and Shared Project under **Code Sharing Strategy**

    ![New Project](images/new_project.png)

That's it. At this point, you should probably spend some time checking out the new solution. You will notice there are four project in the solution, one shared project and three platform specific projects. To run the app on the specific platform, use the drop down at the top of Visual Studio to select what project to run:

![Select Project](images/select_platform.png)

We will focus on UWP and Android for our first release. To run on your machine as UWP, select the UWP project first. Then change the architecture (the dropdown on the left of the Startup projects dropdown) and select x86 or x64. Then simply click the play button to build and run the app:

![Run](images/run.png)

To run the app on Android, you could use the emulator directly from Visual Studio. Change the Startup Project to the Android project and use the dropdown on the right to select the emulator you'd like. Then click the play icon to build and run in the emulator.

![Run Android](images/run_android.png)

Now get to know your new app.

> Note: Use the x86 version of the Android emulator as that will run much faster than the ARM version

> Note: If you have Hyper-V enabled, the Android emulator will not work. You will need to disable Hyper-V and reboot your machine

> Note: Since we will not be using the iOS project for this release, feel free to remove it from your solution



#### Add shared Business Logic

For our first task, we want to be able to list all the different Knowzy products. Fortunately, there is already a public feed for all products that we can use located [here](https://raw.githubusercontent.com/Microsoft/BuildTourHack/master/src/Noses/noses.json?token=AArcuB1FvHzTWESdpopKZiYk0C0H-4Kkks5ZL5AuwA%3D%3D). We can use this to get all of the data for our app. Open the shared project and add two new classes (right click on Shared project, click **Add -> Class**:

1. Name the new class **Nose**. Erase everything between the namespace definition. Copy this json:

    ```json
    {
        "Id": "RN3454",
        "Name": "Black Nose",
        "RawMaterial": "Black foam",
        "Notes": "Everything you'd expect, and a little something more.",
        "ImageSource": "https://raw.githubusercontent.com/Microsoft/BuildTourHack/master/src/Noses/Frabicnose400x300.jpg?token=AArcuJLPN_TgeZBrqvm81EbVaDsh9eh2ks5ZL47AwA%3D%3D"
    }
    ```

    Switch to Visual Studio, place the cursor where you want to copy the new class. In Visual Studio, click on **Edit -> Paste Special -> Paste JSON as Classes**. This will generate a new class for you and you just need to change the name from RootObject to **Nose**.

2. name the new class **DataProvider** and make it public. Add this static method in the class to pull in the data from the link above:

    ```csharp
    public static async Task<Nose[]> GetProducts()
    {
        using (var client = new HttpClient())
        {
            var json = await client.GetStringAsync("https://raw.githubusercontent.com/Microsoft/BuildTourHack/master/src/Noses/noses.json?token=AArcuB1FvHzTWESdpopKZiYk0C0H-4Kkks5ZL5AuwA%3D%3D");

            return JsonConvert.DeserializeObject<Nose[]>(json);
        }
    }
    ```

    This class uses Json.Net to deserialize the Json, so you will need to reference the Nuget package to both the UWP and Android project. Right click on each project, click on **Manage Nuget Packages**. Search for **Newtonsoft.Json** and install it.

    Add the appropriate namespaces to the DataProvider class

#### Add shared UI

Now that we have the business logic out of the way, on to the UI. Xamarin.Forms uses XAML to define the shared UI, so if you've used XAML before, you will feel right at home. All the shared code is in the shared project in the solution, and there is already a page created for us: MainPage.xaml. Go ahead and open the page. Currently there is only one element there, a [Label](https://developer.xamarin.com/guides/xamarin-forms/user-interface/text/label/). Instead of a Label, we will use a [ListView](https://developer.xamarin.com/guides/xamarin-forms/user-interface/listview/) to display all of the products.

1. Remove the Label and add a new element ListView instead. Give it a name. In this case it's *ProductListView**

```xaml
<ListView x:Name="ProductListView">

</ListView>
```

2. Open MainPage.Xaml.cs. This is where the code goes for your view. Here we can override the *OnAppearing* method which will allows us to get the list of products and set them as the source of the ListView. Add the following code:

```csharp
// TODO - needs real code
protected async override void OnAppearing()
{
    ProductListView.ItemsSource = await DataProvider.GetProducts();
}
```

3. Finally, we need to define how each product will look like. For that we will create a data template to customize each [Cell](https://developer.xamarin.com/guides/xamarin-forms/user-interface/listview/customizing-cell-appearance/):

```xaml
<ListView x:Name="ProductListView">
    <ListView.ItemTemplate>
        <DataTemplate>
            <ViewCell>
                <Image Source="{Binding Image}" HeightRequest="150" WidthRequest="150"></Image>
            </ViewCell>
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```

**Task Complete**. Go ahead and run the the app on your machine and in the Android emulator.

[Go to the next Task](312_Camera.md) where you will add another page and the capability to capture an image by using APIs specific to each platform.

## Resources

1. [Xamarin.Forms Quickstart](https://developer.xamarin.com/guides/xamarin-forms/getting-started/hello-xamarin-forms/quickstart/)
2. [Introduction to Xamarin.Forms](https://developer.xamarin.com/guides/xamarin-forms/getting-started/introduction-to-xamarin-forms/)
3. [Xamarin.Forms XAML documentation](https://developer.xamarin.com/guides/xamarin-forms/xaml/)
