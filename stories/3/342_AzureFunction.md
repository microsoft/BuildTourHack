# Task 3.4.2 - Create an Azure Function to analyze an image and return nose location

Now that you've created a Cognitive Service to tell you where noses are in pictures sent from the Knowzy mobile app, you'll need to create an endpoint to accept those pictures and talk to your Cognitive Service.  This task requires you to set up a new Azure Function, and then code and design it to expose an HTTP endpoint that accepts an image file and returns the location of the nose(s) in any faces found in the picture.

## Prerequisites

This task has a dependency on [Task 3.4.1](341_CognitiveServices.md) and all of it's prerequisites

This walkthrough assumes that you have:

1. [Visual Studio 2017 Preview 3](https://www.visualstudio.com/vs/preview/).
2.  [Azure Functions extension](https://blogs.msdn.microsoft.com/webdev/2017/05/10/azure-function-tools-for-visual-studio-2017/) installed.


## Task

1.  Create a new Azure Functions project in Visual Studio.  
2.  Create an HTTP Trigger which will take in a picture and return data.
3.  Within your Function, use the *Name* and *Key* from [Task 3.4.1](341_CognitiveServices.md) to connect to your Cognitive Service and run face detection on the image.
4.  Return the data for the nose location(s) from your Function.
5.  Position Knowzy nose(s) on top of the image in app.

## Comments

###### @ 11:48am
[This](https://blogs.msdn.microsoft.com/webdev/2017/05/10/azure-function-tools-for-visual-studio-2017/) blog post shows off the Visual Studio tooling that you can use to create a new Azure function.

###### @ 1:03pm
I found this explanation for Azure Functions of how to do [HTTP and Webhook Bindings](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-http-webhook).

###### @ 2:57pm
I was curious about what the Face API is capable of so I found the [REST Docs](https://westus.dev.cognitive.microsoft.com/docs/services/563879b61984550e40cbbe8d/operations/563879b61984550f30395236).

###### @ 4:32pm
This [quickstart](https://docs.microsoft.com/en-us/azure/cognitive-services/face/quickstarts/csharp) really walks through how to call the Face API from C#.

## continue to [next task >> ](351_CICD_WindowsApp.md)
