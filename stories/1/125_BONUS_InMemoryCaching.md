# Task 1.2.5 - Add In-Memory Caching

Now that you have a running PWA, we'd like you to improve the speed and performance of the Web App.

## Prerequisites 

This task has a dependency on [Task 1.2.2](122_Add_WIndows_Features.md) and all of it's prerequisites.

## Task 
1.  Speed up the performance of your website by caching results from polling the Shipping and Orders APIs into memory.
2.  Whenever you update data using the Shipping or Orders APIs, invalidate your cached data.
3.  Experiment with different cache sliding expirations to see what happens when you use a more or less frequent expiration.

## Comments

###### @ 3:37am
I found [this guide](https://docs.microsoft.com/en-us/aspnet/core/performance/caching/memory) that explains how in-memory caching works with ASP.NET Core.
