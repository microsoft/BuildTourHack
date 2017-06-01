# Task 1.2.4 - Add In-Memory Caching

Now that you have a running PWA, we'd like you to improve the speed and performance of the Web App.

## Prerequisites 

This task has a dependency on [Task 1.2.2][122] and all of it's prerequisites.

## Task 

1.  The website currently loads its data from static JSON files in its `wwwroot\Data` folder. Speed up the performance of your website by caching values that are being used by controllers on your website. For example, after enabling caching, update the `GenerateDropdowns` method in `ReceivingsController.cs` to cache the results.
2.  Update the local JSON files while you are running and notice the data does not update when you refresh.
3.  Experiment with different cache sliding expirations to see what happens when you use a more or less frequent expiration and change the local data.
4.  Once the tasks in Section 4 are done, the site will have been updated by your team to get the data from the Shipping and Orders APIs. You can use the caching features to cache the data from the API, and include code to invalidate your cached data from the API after expiration.

## Comments

###### @ 3:37am
I found [this guide](https://docs.microsoft.com/en-us/aspnet/core/performance/caching/memory) that explains how in-memory caching works with ASP.NET Core.

[122]: /stories/1/122_Add_Windows_Feature.md
