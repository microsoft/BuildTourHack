# Task 3.2.2 - Set up Cognitive Services Emotion Service to Recognize People Wearing Knowzy Products

Our marketing department wants to let users to capture pictures of themselves wearing Knowzy products to share with their friends. Knowzy can use this information to automatically detect what products are being worn and determine the user's excitement for each of the products. This information can be used to drive improvements to those products.

**Goals for this task:** Enable your Android and UWP app to use a Cognitive Services Custom Vision service to detect Knowzy products and the user's emotion from a captured image.

## Prerequisites

* This task has a dependency on [Task 3.2.1](321_CustomVisionService.md) and all of it's prerequisites.
* An Azure subscription

## Task

### Create a Cognitive Services **Emotion API** account

1. In your web browser, open the Azure Portal [https://portal.azure.com](https://portal.azure.com)
2. Create a new Emotion API account by clicking the **New Resource** button.
3. Select **AI + Cognitive Services**
4. Click the **See All** link
5. In the filter bar, type **emotion** and hit Enter. Select the **Emotion API (Preview)** item
6. Click the **Create** button
7. Enter a name, subscription and location for your account. Select **F0** for the Pricing tier (this tier enables 20 calls / minute). Select **TODO: Determine resource group** as an existing Resource Group. Check the acknowledgment and the **Create** button to create a new account.

[Go to the next Task](322_EmotionAPI.md) where you'll create a Cognitive Services Emotion API to detect the level of excitement of a user.

## continue to [next task >> ](322_EmotionAPI.md)
