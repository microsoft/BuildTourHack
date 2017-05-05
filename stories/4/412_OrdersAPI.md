# Task 4.1.2 - Create API endpoint for shipping services

## Prerequisites 

* [.NET Core SDK 1.1](https://www.microsoft.com/net/download/core)

## Creating a .NET Core App

### 1. Create a New WebAPI Project

We'll use the dotnet cli to create a new .NET Core WebAPI project. In a terminal, run:

```bash
mkdir ordersapi && cd ordersapi
dotnet new webapi
```

Note: The case of the `ordersapi` folder is important - it determines the name of the output dll, which will be used by Docker when running a container. If you change the folder (and therefore dll) name, be sure to update the appropriate `Dockerfile`

These commands get you started with a basic WebAPI project in the `ordersapi` folder with a single controller. Let's test it out to make sure everything is working properly. In the terminal, run:

```bash
dotnet restore
dotnet run
```

Navigate to [http://localhost:5000/api/values/5](http://localhost:5000/api/values/5) to see your app running. Press `Ctrl+C` in the terminal to stop the API

### 2. Add functionality

Let's make a small change to the API so that it responds to our inputs. Modify the code in `/Controllers/ValuesController.cs` with the following changes

```diff
// GET api/values/5
[HttpGet("{id"})]
public string Get(int id)
{
-  return "value";
+  return $"The value is {id}";
}
```

Using the terminal again, let's run the project to see our changes

```bash
dotnet run
```

This time, when you navigate to [http://localhost:5000/api/values/5](http://localhost:5000/api/values/5), you should see "The value is 5"

### 3. Using Environment Variables

In a real-world app, you won't check your secrets into source control, and you won't be writing local code that connects directly to your production data store. Depending on your environment, you might not even have access to production. To address these issues and see how they tie in with Docker, we're going to use the `DOCDB_CONNSTRING` environment variable.

The default Web API template already calls `.AddEnvironmentVariables()`, so we just need to set a variable, then access it in our code.  To set a variable, run the following in a command prompt with the primary connection string for your account:

```
SET DOCDB_CONNSTRING=AccountEndpoint=http://<myaccount>.documents.azure.com:443/;AccountKey=...
```

To use the `DOCDB_CONNSTRING` in our code, we'll just pass the entire configuration object down to our controller.

In `Startup.cs`, make the following changes:

```diff
public void ConfigureServices(IServiceConnection services)
{
  // Add framework services.
  services.AddMvc();
+ services.AddSingleton<IConfiguration>(Configuration);
}
```

And in your controller:

```diff
using System;
using System.Collections.Generic;
...
+using Microsoft.Extensions.Logging;

...
  [Route("api/[controller]")]
  public class ValuesController : Controller
  {
+    private readonly IConfiguration _config;
+ 
+    public ValuesController(IConfiguration config)
+    {
+      _config = config;
+    }

...

    // GET api/values/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
-      return $"The value is {id}";
+      return $"The DOCDB_CONNSTRING value is: {_config["DOCDB_CONNSTRING"]}";
    }
  }
```

### 4. Implement the Orders API

Now it's time to implement the endpoints for the Orders API, using `dotnet run` as needed to verify your app locally before moving on.

### 5. Package for release

Now that we've got a working app, let's package up all of our required files into a single folder for easy distribution. This time, we'll specify the Release configuration. In a terminal, run:

```bash
dotnet publish -c Release
```

By default, this places your app files in a folder named `/bin/Release/netcoreapp1.1/publish`. We'll use this output path in [Step 4.1.4](414_Docker.md) when we build a Docker image for our app.

## 6. References

* [Troubleshooting guide](499_Troubleshooting.md)
* [.NET Core CLI reference](https://docs.microsoft.com/en-us/dotnet/articles/core/tools/)
* [Introduction to ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/)
* [Configuration in .NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration)
* [Build a DocumentDB C# console application on .NET Core](https://docs.microsoft.com/en-us/azure/documentdb/documentdb-dotnetcore-get-started)