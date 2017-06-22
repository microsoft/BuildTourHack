# Task 3.2.3 - Update Xamarin App to call Cognitive Services APIs

It is now time to integrate calls to the Cognitive Services that you created in the previous steps into the Xamarin application. These calls can be made directly from the app. An alternative design would be to host functionality on Azure to make these calls.

**Goals for this task:** Create new functionality to the previously created Camera Page which will call the Cognitive Services APIs and display the results to the user.

## Prerequisites

* This task has a dependency on [Task 3.2.2](322_EmotionAPI.md) and all of it's prerequisites.

## Task

### Add a new layout to the Camera Page to display predictions

1. In Visual Studio open CameraPage.xaml
2. Update the XAML to add a new `Grid` and other elements to display prediction results after calling Cognitive Services. Add the following snippet immediately below the `Button` element:

        <Grid>
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="Auto" />
                <ColumnDefinition Width="*" />
            </Grid.ColumnDefinitions>
            <Grid.RowDefinitions>
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
            </Grid.RowDefinitions>
            <Label Grid.Row="0" Grid.Column="0" Text="Knowzy Products: "></Label>
            <Label Grid.Row="0" Grid.Column="1" FontAttributes="Bold"  x:Name="productTags"></Label>
            <Label Grid.Row="1" Grid.Column="0" Text="Probability (%): "></Label>
            <Label Grid.Row="1" Grid.Column="1" FontAttributes="Bold" x:Name="productTagProbability"></Label>
            <Label Grid.Row="2" Grid.Column="0" Text="Emotion: "></Label>
            <Label Grid.Row="2" Grid.Column="1" FontAttributes="Bold" x:Name="emotionTag"></Label>
        </Grid>

### Add code to call the Custom Vision service

1. Open the code-behind file for CameraPage.xaml. 

    * Add the following `using` statements at the top of the file:

            using System.Net.Http;
            using System.Net.Http.Headers;
            using System.IO;
            using Newtonsoft.Json;
            using Newtonsoft.Json.Linq;

    * Navigate to the `captureButton_Clicked` method and update the body with the following code to invoke the **Custom Vision** service and display the results.
      
      **Important:** You must update the **bolded** lines shown below: replace the value used to set the `Prediction-Key` header with the **Prediction Key** value you saved when you created your Custom Vision project in the earlier task. Additionally, you must replace the `url` with the **Prediction URL** you saved after training your model: 

<pre><code>            var photoService = DependencyService.Get<IPhotoService>();
            if (photoService != null)
            {
                var imageBytes = await photoService.TakePhotoAsync();
                noseImage.Source = ImageSource.FromUri(new Uri(_nose.Image)); // set source of nose image
                image.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                imageGrid.IsVisible = true; // set visibility to true

                // Invoke Cognitive Services to get predictions on the image
                productTags.Text = "Predicting...";
                productTagProbability.Text = "";
                emotionTag.Text = "";

                // Invoke the custom vision prediction api
                var client = new HttpClient();

                // Request headers - replace this example key with your valid subscription key.
                <b>client.DefaultRequestHeaders.Add("Prediction-Key", "63fe389c4f96433ba807ee948e7aa98f");</b>

                // Prediction URL - replace this example URL with your valid prediction URL obtained after training the model.
                <b>string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v1.0/Prediction/a2545d9c-f6e9-41d5-9807-28991bec747c/image?iterationId=2f51acdf-f96c-481c-af49-6cae71e7a2cb";</b>
                using (var content = new ByteArrayContent(imageBytes))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    var response = await client.PostAsync(url, content);
                    dynamic predictionResponse = await response.Content.ReadAsStringAsync()
                        .ContinueWith((readTask) => JsonConvert.DeserializeObject(readTask.Result));
                    productTags.Text = predictionResponse.Predictions[0].Tag;
                    productTagProbability.Text = (predictionResponse.Predictions[0].Probability.Value * 100).ToString();
                }
            }
</code></pre>

2. You can now build and run the project. When you capture the image, make sure you are wearing one of the Knowzy products. The Custom Vision service will predict which product you are wearing with a specified probability.

### Add code to call the Emotion API service

1. Open the code-behind file for CameraPage.xaml. 

    * Navigate to the `captureButton_Clicked` method and update the body with the following code to invoke the **Emotions API** services and display the results. 
      Because we are now calling both the **Custom Vision** and **Emotions API** services, we do this in parallel to improve performance. 
      
      **Important:** You must update the **bolded** lines shown below: replace the `Ocp-Apim-Subscription-Key` header and `url` value with the values from your Emotions API account that you saved in the previous task. Also in the code that calls the Custom Vision service, just as you did in the previous step, you must replace the value used to set the `Prediction-Key` header with the **Prediction Key** value you saved when creating your own Custom Vision service, and the `url` with the **Prediction URL** for your service again: 

<pre><code>            var photoService = DependencyService.Get<IPhotoService>();
            if (photoService != null)
            {
                var imageBytes = await photoService.TakePhotoAsync();
                noseImage.Source = ImageSource.FromUri(new Uri(_nose.Image)); // set source of nose image
                image.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                imageGrid.IsVisible = true; // set visibility to true

                // Invoke Cognitive Services to get predictions on the image
                productTags.Text = "Predicting...";
                productTagProbability.Text = "";
                emotionTag.Text = "";

                // Invoke the custom vision prediction api
                var customVisionTask = Task.Run(async () =>
                {
                    var client = new HttpClient();
                    // Request headers - replace this example key with your valid subscription key.
                    <b>client.DefaultRequestHeaders.Add("Prediction-Key", "63fe389c4f96433ba807ee948e7aa98f");</b>

                    // Prediction URL - replace this example URL with your valid prediction URL obtained after training the model.
                    <b>string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v1.0/Prediction/a2545d9c-f6e9-41d5-9807-28991bec747c/image?iterationId=2f51acdf-f96c-481c-af49-6cae71e7a2cb";</b>
                    using (var content = new ByteArrayContent(imageBytes))
                    {
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        var response = await client.PostAsync(url, content);
                        dynamic predictionResponse = await response.Content.ReadAsStringAsync()
                            .ContinueWith((readTask) => JsonConvert.DeserializeObject(readTask.Result));
                        return Tuple.Create(predictionResponse.Predictions[0].Tag, predictionResponse.Predictions[0].Probability.Value * 100);
                    }
                });

                // Invoke the Emotion API in parallel
                var emotionTask = Task.Run(async () =>
                {
                    var client = new HttpClient();
                    // Request headers - replace this example key with your valid key.
                    <b>client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "7af37d1e3e6048539c76274fd4c64d72");</b>

                    // NOTE: You must use the same region in your REST call as you used to obtain your subscription keys.
                    //   For example, if you obtained your subscription keys from westcentralus, replace "westus" in the 
                    //   URI below with "westcentralus".
                    <b>string uri = "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize";</b>
                    using (var content = new ByteArrayContent(imageBytes))
                    {
                        // This example uses content type "application/octet-stream".
                        // The other content types you can use are "application/json" and "multipart/form-data".
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        var response = await client.PostAsync(uri, content);
                        dynamic detectionResponse = await response.Content.ReadAsStringAsync()
                            .ContinueWith((readTask) => JsonConvert.DeserializeObject(readTask.Result));
                        // See the format of the JSON response here: https://westus.dev.cognitive.microsoft.com/docs/services/5639d931ca73072154c1ce89/operations/563b31ea778daf121cc3a5fa
                        JObject scores = detectionResponse[0].scores;
                        var highestScore = scores.Properties().OrderByDescending(score => (double)((JValue)score.Value).Value)
                            .First();
                        return Tuple.Create(highestScore.Name, (double)((JValue)highestScore.Value).Value);
                    }
                });

                await Task.WhenAll(customVisionTask, emotionTask);

                // Update the UI
                productTags.Text = customVisionTask.Result.Item1;
                productTagProbability.Text = customVisionTask.Result.Item2.ToString();
                emotionTag.Text = emotionTask.Result.Item1;
            }
</code></pre>

2. You can now build and run the project. When you now capture an image, both the **Custom Vision** AND **Emotions API** will be called to detect the Knowzy product and how excited the user feels when wearing it. Make sure you wear a Knowzy product and try out different facial expressions to represent emotions.

**Note:** If your call to the Custom Vision service returns an empty array of predictions, check that you have substituted the **Prediction Key** value and the **Prediction URL** in the code, using the correct values for your own service.

## References

* [Custom Vision Service Quickstart](https://docs.microsoft.com/en-us/azure/cognitive-services/custom-vision-service/use-prediction-api)
* [Emotion API Service Quickstart](https://docs.microsoft.com/en-us/azure/cognitive-services/emotion/quickstarts/csharp)



## continue to [next task >> ](331_Social.md)
