# Introduction 

Knowzy is a webapp that shows how to work using [ASP.NET Core](https://github.com/aspnet/Home). It provides simple CRUD operations, navigation and a responsive way to show the data.

This application is built using:
- ASP.NET MVC Core
- Plain CSS
- Jquery

## Requirements

[Get Started with .NET Core](https://www.microsoft.com/net/core#windowscmd)

## Build and Deployment

### Using Windows and Visual Studio 2017
- Open the solution file within Visual Studio 2017 (Microsoft.Knowzy.sln)
- Once opened, In the Build menu select Rebuild Solution. All dependences and NuGet packages will be downloaded.

### Configuration
#### Mock data object:

- Included in the source code there are three .json files (data.json) that contain mock data for the application. These files are located in the folder: "/src/1. WebApp/Microsoft.Knowzy.WebApp/wwwroot/Data".
- Of course, it is possible to change the location where the app looks for the files. To do so, there are two configuration files located in the folder: "/src/1. WebApp/Microsoft.Knowzy.WebApp/" named appsettings.json and appsettings.Development.json where various configuration params can be set. The first one is used to set general configuration and the second is for development configuration only. The order precedure is if the same property is stablished in the development file and the app is running in development mode it will get the property set in the development configuration file. 
In this case, the configuration property is located in the appsettings.json as follows:
```js
  "AppSettings": {
    "ProductJsonPath": "\\Data\\products.json",
    "CustomerJsonPath": "\\Data\\customers.json",
    "OrderJsonPath": "\\Data\\orders.json"
  }
```
Updating the JsonPath property, the location where the app looks for the json data will be updated.

#### Database configuration

- For development the functionality is out of the box. The application read the mock data and works with them in memory. Data will be initialized automatically when the app starts.
Remember that all changes will be lost when the app is shut down.
- For production, it is mandatory to set the connection string to an existing DB. To do so, in the appsettings.json it is necessary to set the value for KnowzyContext:

```js
"ConnectionStrings": {
    "KnowzyContext": "Server=.\\SQLEXPRESS;Database=Knowzy;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
```

This connection string must include the Database section. If database has no data and no previously created schema, database schema will be automatically created and mock data will be inserted in the created tables by the application on the first run.

#### Change data provider from database to REST API  Service (or whatever you want)

- In order to override the existing implementation for accesing data, users must implement the IOrderRepository interface whith a custom implementation. .
- Also you have to change the service configuration in the Startup.cs class adding the new implementation files:

 ```c#
services.AddScoped<IOrderRepository, OrderRepository>();
```

There are two methods to be modified in the startup class in order to set this custom implementation:
- ConfigureServices: Runs when the application is launch in production mode.
- ConfigureDevelopmentServices: Runs when the application is launch in development mode. 