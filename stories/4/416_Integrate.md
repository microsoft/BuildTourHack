# Task 4.1.6 - Integrate Website and APIs

Now that the APIs and the website and clients have been implemented, our team wants to integrate the website code with the APIs. 

## Prerequisites 

This task has a dependency on the Web App from [1.1.1](../1/111_BuildWebApp.md), the APIs created under section [4.1.2][412] and [4.1.3][413], and the deployment to Kubernetes steps in section [4.1.5][415], and all of their prerequisites.

## Task

1. The website code currently uses a mock implementation of its data access, getting the initial data from static json files, and keeping the data in memory. In order to override the existing implementation for accesing data and integrate with the APIs you must implement the `IOrderRepository` interface whith your own custom class implementation (use `OrderRepositoryMock.cs` as guidance). You also have to change the service configuration in `Startup.cs` class adding the new implementation files:

 ```c#
services.AddScoped<IOrderRepository, OrderRepository>();
```

2. In the Docker and Kubernetes instructions in [4.1.4][414] and [4.1.5][415] the web app container should have the ORDERSAPI_URL and PRODUCTSAPI_URL environment variables with the two API endpoints URLs passed in to it. Update the website code to use those values to interact with the APIs from the your implementation of `IOrderRepository`. 

[412]: /stories/4/412_OrdersAPI.md
[413]: /stories/4/413_ProductsAPI.md
[414]: /stories/4/414_Docker.md
[415]: /stories/4/415_Kubernetes.md