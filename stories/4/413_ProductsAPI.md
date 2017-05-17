# Task 4.1.3 - Create API endpoint for product services

## Creating a .NET Core App

### 1. Create a New WebAPI Project
Our steps to get started with the Products API are the same as our previous step where we created the [Orders API](412_OrdersAPI.md). 

Again you can use the terminal or Visual Studio 2017 to create a new project in the `src\2. Services\APIs\Microsoft.Knowzy.ProductsAPI` folder.

### 2. Implement the Products API

Go ahead and implement the Products API endpoints, using `dotnet run` or Visual Studio 2017 to test locally.

Implement the following methods in the Products API that are used by the website:
- Get all products
- Get Product by id (includes product stock availability)
- Add new product

### 3. Package for release

Like before, package the Products API using the console or Visual Studio, then continue to [Task 4.1.4 - Create Docker images](414_Docker.md). 

## 4. References

* [Troubleshooting guide](499_Troubleshooting.md)
* [Step 4.1.2 - Orders API](412_OrdersAPI.md)