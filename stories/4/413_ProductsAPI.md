# Task 4.1.3 - Create API endpoint for product services

You've finished creating the Orders API, and now it's time to create one for Products.  

## Prerequisites 

* This task has a dependency on [Task 4.1.2][412] and all of its prerequisites.
* [.NET Core SDK 1.1](https://www.microsoft.com/net/download/core)

## Creating a .NET Core App

### 1. Create a New WebAPI Project
The steps to get started with the Products API are the same as our previous step where we created the [Orders API][412]. 

Again you can use Visual Studio 2017 to create a new project in the `src\2. Services\APIs\Microsoft.Knowzy.ProductsAPI` folder.

### 2. Implement the Products API

Go ahead and implement the Products API endpoints, using Visual Studio 2017 to test locally.

Implement the following methods in the Products API that are used by the website:
- Get all products
- Get Product by id (includes product stock availability)
- Add new product

Again: you have access to an [end version of the APIs for your reference in the `azurecompleted` branch of the `KnowzyInternalApps` repo](https://github.com/Knowzy/KnowzyInternalApps/tree/azurecompleted/src/Knowzy_Shipping_WebApp/src/2.%20Services/APIs) to help.

### 3. Package for release

Like before, package the Products API using Visual Studio, then continue to [Task 4.1.4 - Create Docker images][414]. 

## 4. References

* [Troubleshooting guide][499]
* [Step 4.1.2 - Orders API][412]

[412]: /stories/4/412_OrdersAPI.md
[414]: /stories/4/414_Docker.md
[499]: /stories/4/499_Troubleshooting.md

## continue to [next task >> ](414_Docker.md)
