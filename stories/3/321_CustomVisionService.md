# Task 3.2.1 - Set up Cognitive Services Custom Vision Service to Recognize People Wearing Knowzy Products

Our marketing department wants to let users to capture pictures of themselves wearing Knowzy products to share with their friends. Knowzy can use this information to automatically detect what products are being worn and determine the user's excitement for each of the products. This information can be used to drive improvements to those products.

**Goals for this task:** Enable your Android and UWP app to use a Cognitive Services Custom Vision service to detect Knowzy products and the user's emotion from a captured image.

## Prerequisites

* This task has a dependency on [Task 3.1.3](313_InkCanvas.md) and all of it's prerequisites.
* A Microsoft Account

## Task

### Create a Cognitive Services **Custom Vision Service** API

1. In your web browser, navigate to [https://customvision.ai](https://customvision.ai)
2. Click the **Sign In** button and enter your Microsoft Account credentials.
3. Create a new Custom Vision project by click the **New Project** tile
4. Enter a name for the project (you will need to remember this later), and select **General** for the domain
5. Click the **Create Project** button to create the new Custom Vision project
6. Click the **Settings** button and copy both the **Training Key** and **Prediction Key** values - these will be required later

### Train the Custom Vision Service with Knowzy Products ###

In order for the Custom Vision Service to detect which Knowzy Inc. products appear in images submitted by the app, it must first be *trained*. Training the service requires uploading a small set of images with people wearing Knowzy Inc. products in a diverse set of lighting, zooming and other conditions. Each image is *tagged* to tell the service what *classification* the image represents. Once the service has sufficient training images, it will then be able to *classify* other images (that are not part of the training set) based on matching characteristics.

Because we need between 10-20 images per tag to successfully train the service, we have provided a set of images that are ready to be ingested and tagged. We have also provided a tool which is able to ingest the training images directly from an **Azure Storage** blob account (this will save needing to upload the images over the hack event wifi). The training images are organized into separate containers for each tag.

1. You must first build the training tool. Open the solution at `\src\Tools\Tools.sln` in Visual Studio 2017.
2. Build the solution
3. Open a command prompt and navigate to the location where Visual Studio output the built `CustomVisionTrainer.exe`. This is `src\Tools\CustomVisionTrainer\bin\Debug`.
4. Run the `CustomVisionTrainer.exe` tool to upload training images for the three different Knowzy Inc. products. The tool requires you to specify the **Training Key** of your Custom Vision Service project and the name of the project:

		CustomVisionTrainer.exe {Training Key} {Project Name} https://bthackcustomvisiontrain.blob.core.windows.net/knowzy8s Knowzy8s
		CustomVisionTrainer.exe {Training Key} {Project Name} https://bthackcustomvisiontrain.blob.core.windows.net/knowzyvr KnowzyVR
		CustomVisionTrainer.exe {Training Key} {Project Name} https://bthackcustomvisiontrain.blob.core.windows.net/knowzybowzy KnowzyBowzy

[Go to the next Task](322_EmotionAPI.md) where you'll create a Cognitive Services Emotion API to detect the level of excitement of a user.

## continue to [next task >> ](322_EmotionAPI.md)
