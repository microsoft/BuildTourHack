# Task 4.3.1 - Set up Telemetry

Our development team want to be able to monitor the health of the applications as well as find the source of bugs. The team also want to learn from production and use it to decide on future projects. 

## Prerequisites 

This task has a dependency on the Web App from [1.1.1](../1/111_BuildWebApp.md) and the APIs created under section [4.1.2](../4/412_OrdersAPI.md) and [4.1.3](../4/413_ProductsAPI.md) and all of their prerequisites.

## Task

1. Create an Application Insights account from the Azure Portal or Visual Studio. 
2. Add Application Insights telemetry logging to the Web App, Orders API, and Products API.
3. Update the containers and kubernetes implementation to pass in the value of your instrumentation key to the applications in the containers.
4. Use the Azure Portal to see the telemetry that is being captured.

## Comments

###### @ 9:02am
I've found the following instructions that should help us get started:
* [Application Insights for ASP.NET Core](https://docs.microsoft.com/en-us/azure/application-insights/app-insights-asp-net-core)
* [Managing ASP.NET Core App Settings on Kubernetes](http://anthonychu.ca/post/aspnet-core-appsettings-secrets-kubernetes/)
