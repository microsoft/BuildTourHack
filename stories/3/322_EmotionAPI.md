# Task 3.2.2 - Set up Cognitive Services Emotion Service to Determine User's Excitement

Our marketing department also wants to verify that Knowzy's users are super excited when wearing their Knowzy products. 

You will create a Cognitive Services Emotion API to assess the emotional state of user's images. You will subsequently integrate calls to this account into the Xamarin app.

**Goals for this task:** Create a Cognitive Services Emotion API account..

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
7. Enter a name, subscription and location for your account. Select **F0** for the Pricing tier (this tier enables 20 calls / minute). Specify a name for a new **Resource Group**. Check the acknowledgment and the **Create** button to create a new account.
8. Once the new Emotions API account has been created, on the **Overview** tab, select the **Endpoint** value. This value will be required to be specified when you integrate calling this service from the Xamarin application.
9. Click **Show Access Keys...**. Copy the value of **Key 1**. This value will be required to be specified when you integrate calling this service from the Xamarin application.

[Go to the next Task](323_IntegrateCogSvc.md) where you'll add the calls to your Xamarin app to call both the Custom Vision service and the Emotion API service.

## References

* [Emotion API Service](https://azure.microsoft.com/en-us/services/cognitive-services/emotion)


## continue to [next task >> ](323_IntegrateCogSvc.md)
