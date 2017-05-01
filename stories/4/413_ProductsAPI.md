# Task 4.1.3 - Create API endpoint for product services

## Creating a .NET Core App

### 1. Create a New WebAPI Project
Our steps to get started with the Products API are the same as our previous step where we created the [Orders API](412_OrdersAPI.md). Again we'll use the terminal, this time to create a new project in the `productsapi` folder

```bash
mkdir productsapi && cd productsapi
dotnet new webapi
dotnet restore
dotnet run
```

### 2. Implement the Products API

Go ahead and implement the Products API endpoints, using `dotnet run` to test locally.

### 3. Package for release

Like before, we'll package our app into the `/bin/Release/netcoreapp1.1/publish` folder when we're done:

```bash
dotnet publish -c Release
```

## References

* [Troubleshooting guide](499_Troubleshooting.md)
* [Step 4.1.2 - Orders API](412_OrdersAPI.md)