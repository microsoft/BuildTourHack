# Adventure Works Knowzy Backlog 

There are four User Stories that we are focusing on for this iteration. Each User Story is split into multiple deliverables that each contains several tasks. You will find two types of tasks:

* Required - we've done our research, you just need to implement the work and learn the technology along the way
* Optional - we like this idea, but we have not decided on the implementation, it's up to you

User stories are mostly self contained and can be completed in parallel. However, as you get farther along in a specific user story, you might run into tasks that depend on other User Stories. Therefore, the recommendation is to assign the user stories to members of your team and to tackle them in parallel. 

If you are blocked, a representative from the leadership team is always there to help, don't be afraid to reach out.

The source code for our applications is all open source and can be found on [our github account](https://github.com/Knowzy/KnowzyInternalApps).

* **[Machine Setup Instructions][0]**

1. **User Story** - The shipping department has a fast, responsive, and powerful application for managing day to day duties
    * **Deliverable** - Make app more responsive
        * [1.1.1 [Required] - Build a responsive Web App][111]
        * [1.1.2 [Required] - Generate Progressive Web App (d. 1.1.1)][112]
        * [1.1.3 [Required] - Update Web App to PWA (d. 1.1.1)][113]
        * [1.1.4 [Required] - Test Your App (d. 1.1.1)][114]
    * **Deliverable** - Add Native functionality
        * [1.2.1 [Required] - Add Live Tile (d. 1.1.1) ][121]
        * [1.2.2 [Optional] - Add Share and Secondary Pinning (d. 1.1.2)][122]
        * [1.2.3 [Optional] - Make PWA Linkable (d. 1.1.2)][123]
        * [1.2.4 [Optional] - Add In-Memory Caching][124]

2. **User Story** - The product department has a modern, secure and forward-looking platform for managing product development life cycle
    * **Deliverable** - Enable integration of UWP APIs
        * [2.1.1 [Required] - Add Desktop Bridge support in Visual Studio][211]
        * [2.1.2 [Required] - Debugging a Windows Desktop Bridge App (d. 2.1.1)][212]
        * [2.1.3 [Required] - Adding UWP APIs to a Desktop Bridge App (d. 2.1.2)][213]        
        * [2.1.4 [Required] - Integrate Windows Hello authentication (d. 2.1.3)][214]
    * **Deliverable** - Add UWP XAML support
        * [2.2.1 [Required] - Create a new XAML view as part of app package (d. 2.1.1)][221]
        * [2.2.2 [Required] - Add support for other apps to share images and create new items (d. 2.1.1)][222]
        * [2.2.3 [Required] - Create a new UWP app that integrates with App Services (d. 2.1.1)][223]
    * **Deliverable** - Build enhanced UWP experience (d. 2.2.*)
        * [2.3.1 [Optional] - Add support for ink (d. 2.2.*)][231]
        * [2.3.2 [Optional] - Add complete support for Windows Hello Authentication (d. 2.1.3)][232]
        * [2.3.3 [Optional] - Add support for more UWP features (d. 2.2.*)][233]

3. **User Story** - Consumers have a fun mobile experience 
    * **Deliverable** - Create a UWP and Android mobile app
        * [3.1.1 [Required] - Create a Xamarin.Forms app with shared UI][311]
        * [3.1.2 [Required] - Integrate native camera to capture image for each platform (d. 3.1.1)][312]
        * [3.1.3 [Required] - Add InkCanvas support for UWP (d. 3.1.2)][313]
    * **Deliverable** - Add intelligence to the mobile app
        * [3.2.1 [Required] - Set up Cognitive Services Custom Vision Service][321]
        * [3.2.2 [Required] - Set up Cognitive Services Emotion Service (d. 3.2.1)][322]
        * [3.2.3 [Required] - Update Xamarin App to call Cognitive Services APIs (d. 3.2.2)][323]
    * **Deliverable** - Create a fun social experience
        * [3.3.1 [Optional] - Support sharing images to Social Networks (d. 3.2.3)][331]
        * [3.3.2 [Optional] - Support cross device scenarios (Project Rome) (d. 3.3.1)][332]
    * **Deliverable** - Add automatic image analysis
        * [3.4.1 [Optional] - Set up Cognitive Services for image face analysis in Azure (d. 323)][341]
        * [3.4.2 [Optional] - Create an Azure Function to analyze an image and return nose location to automatically position in app (d. 3.4.1)][342]
    * **Deliverable** - Set up Continuous Integration and Deployment
        * [3.5.1 [Optional] - Set up Continuous Integration and Deployment for the Windows App using Visual Studio Mobile Center][351]
        * [3.5.2 [Optional] - Set up Continuous Integration and Deployment for the Android App using Visual Studio Mobile Center][352]
        * [3.5.3 [Optional] - Add Custom Event Logging using Visual Studio Mobile Center][353]
    * **Deliverable** - Create a chat bot for support and for order status management 
        * [3.6.1 [Optional] - Create a bot using the Microsoft Bot Framework][361]

4. **User Story** - All platform services are integrated in one platform
    * **Deliverable** - Unify and Publish all services
        * [4.1.1 - [Required] Create a shared CosmosDB to store all data][411]
        * [4.1.2 - [Required] Create API endpoint for shipping services (d. 4.1.1)][412]
        * [4.1.3 - [Required] Create API endpoint for product services (d. 4.1.1)][413]
        * [4.1.4 - [Required] Create Docker images (d. 4.1.2, 4.1.3)][414]
        * [4.1.5 - [Required] Configure Kubernetes and publish to Azure (d. 4.1.4)][415]
        * [4.1.6 - [Optional] Integrate Website and APIs (d. 4.1.5)][416]
    * **Deliverable** - Set up Continuous Delivery
        * [4.2.1 - [Optional] Set up Visual Studio Team Services][421]
        * [4.2.2 - [Optional] Continuous Delivery to Kubernetes using VSTS (d. 4.1.5, 4.2.0)][422]
    * **Deliverable** - Set up Telemetry
        * [4.3.1 - [Optional] Set up telemetry for the web app and APIs][431]

Good luck!

[0]: stories/0/0_Setup.md

[111]: stories/1/111_BuildWebApp.md
[112]: stories/1/112_GeneratePWA.md
[113]: stories/1/113_ConfigureSW.md
[114]: stories/1/114_Test_App.md
[121]: stories/1/121_Add_WIndows_Feature.md
[122]: stories/1/122_BONUS-RenoFeatures.md
[123]: stories/1/123_BONUS-APP-Links.md 
[124]: stories/1/124_BONUS_InMemoryCaching.md

[211]: stories/2/211_Centennial.md
[212]: stories/2/212_Debugging.md
[213]: stories/2/213_AddUwp.md
[214]: stories/2/214_WindowsHello.md
[221]: stories/2/221_XAMLView.md
[222]: stories/2/222_Share.md
[223]: stories/2/223_AppServices.md
[231]: stories/2/231_Inking_Dial.md
[232]: stories/2/232_Windows_Hello.md
[233]: stories/2/233_Extend.md

[311]: stories/3/311_XamarinForms.md
[312]: stories/3/312_Camera.md
[313]: stories/3/313_InkCanvas.md
[321]: stories/3/321_CustomVisionService.md
[322]: stories/3/322_EmotionAPI.md
[323]: stories/3/323_IntegrateCogSvc.md
[331]: stories/3/331_Social.md
[332]: stories/3/332_Rome.md
[341]: stories/3/341_CognitiveServices.md
[342]: stories/3/342_AzureFunction.md
[343]: stories/3/343_NoseAnalysys.md
[351]: stories/3/351_CICD_WindowsApp.md
[352]: stories/3/352_CICD_AndroidApp.md
[353]: stories/3/353_EventLogging.md
[361]: stories/3/361_Bot.md

[411]: stories/4/411_CosmosDB.md
[412]: stories/4/412_OrdersAPI.md
[413]: stories/4/413_ProductsAPI.md
[414]: stories/4/414_Docker.md
[415]: stories/4/415_Kubernetes.md
[416]: stories/4/416_Integrate.md
[421]: stories/4/421_SetupVSTS.md
[422]: stories/4/422_DevopsKubernetes.md
[431]: stories/4/431_Telemetry.md





