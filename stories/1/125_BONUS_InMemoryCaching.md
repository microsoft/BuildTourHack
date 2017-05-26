# Task 1.2.5 - Add In-Memory Caching

Now that you have a running PWA, we'd like you to improve the speed and performance of the Web App.

## Prerequisites 

This task has a dependency on [Task 1.2.2][122] and all of it's prerequisites.

## Task 
1.  Speed up the performance of your website by caching results from reading data from the local JSON files.
2.  Update the local JSON files while you are running and notice the data does not update when you refresh.
3.  Experiment with different cache sliding expirations to see what happens when you use a more or less frequent expiration and change the local data.
4.  Whenever you update data using the Shipping or Orders APIs, invalidate your cached data.

## Comments

###### @ 3:37am
I found [this guide](https://docs.microsoft.com/en-us/aspnet/core/performance/caching/memory) that explains how in-memory caching works with ASP.NET Core.

[122]: /stories/1/122_Add_Windows_Feature.md