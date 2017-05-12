# Task 3.3.2 - Create an Azure Function to analyze an image and return nose location

Now that you've created a Cognitive Service to tell you where noses are in pictures sent from the KNOWZY mobile app, you need to create an endpoint to accept those pictures and talk to your Cognitive Service.  This task will require you to set up a new Azure Function and then to code and design it to expose an HTTP endpoint to accept an image file and return the location of the nose(s) in any faces found in the picture.

## Prerequisites 

This task has a dependency on [Task 3.3.1](331_CognitiveServices.md) and all of it's prerequisites

This walkthrough assumes that you have:
* Visual Studio 2017 with the Azure Functions extension installed. If not, make sure you [do that first](http://docs.micrsoft.com) and then come back here.  **TODO:  Update link and text to point to correct extension.**



## Task 

1.  In Visual Studio, create a new Azure Functions project.  
2.  Expose an HTTP endpoint which expects a file to be sent across in the body of the request and returns a JSON object.
3.  Within your Function, use the *Name* and *Key* from [Task 3.3.1](331_CognitiveServices.md) to connect to your Cognitive Service and run face detection on the image.
4.  Return the JSON for the nose location(s) from your Function.

## References

1.  [Face API V1.0](https://westus.dev.cognitive.microsoft.com/docs/services/563879b61984550e40cbbe8d/operations/563879b61984550f30395236)
2.  [Face API C# Quick Start](https://docs.microsoft.com/en-us/azure/cognitive-services/face/quickstarts/csharp)
3.  [HTTP and Webhook Bindings](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-http-webhook)