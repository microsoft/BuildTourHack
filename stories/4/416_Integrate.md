# Task 4.1.6 - Integrate Website and APIs

Now that the APIs and the website and clients have been implemented, our team wants to integrate the website code with the APIs. 

## Prerequisites 

This task has a dependency on the Web App from [1.1.1](../1/111_BuildWebApp.md), the APIs created under section [4.1.2][412] and [4.1.3][413], and the deployment to Kubernetes steps in section [4.1.5][415], and all of their prerequisites.

## Task

1. Update the website code to use the APIs instead of static files for data access.

2. Redeploy the website with the updates.

## Comments

###### @ 9:02am
I looked at the website code, and it seems to currently use a mock implementation of the API/data access. It's getting the initial data from static json files, and keeping the data in memory. In order to override the existing implementation for accesing data and integrate with the APIs we should implement the `IOrderRepository` interface whith our own custom class implementation that calls the APIs (using `OrderRepositoryMock.cs` as guidance), and then change the service configuration in `Startup.cs` class adding the new implementation files:

 ```c#
services.AddScoped<IOrderRepository, OrderRepository>();
```

###### @ 9:15am
We can check the [end version of the code for reference, in the `azurecompleted` branch of the `KnowzyInternalApps` repo](https://github.com/Knowzy/KnowzyInternalApps/tree/azurecompleted/src/Knowzy_Shipping_WebApp) to help. Especially the implementation of [OrderRepository](https://github.com/Knowzy/KnowzyInternalApps/blob/azurecompleted/src/Knowzy_Shipping_WebApp/src/2.%20Services/Repositories/Microsoft.Knowzy.Repositories.Core/OrderRepository.cs).

###### @ 11:15am
In the Docker instructions in [4.1.4][414] for the dev machine, and Kubernetes instructions in [4.1.5][415] the web app container has the ORDERSAPI_URL and PRODUCTSAPI_URL environment variables with the two API endpoints URLs passed in to it. Now we can run website in the dev machine and deploy it via kubernetes again to get it to use the APIs.

[412]: /stories/4/412_OrdersAPI.md
[413]: /stories/4/413_ProductsAPI.md
[414]: /stories/4/414_Docker.md
[415]: /stories/4/415_Kubernetes.md

## continue to [next task >> ](421_SetupVSTS.md)
