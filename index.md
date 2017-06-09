# Adventure Works Knowzy Backlog 

There are four User Stories that we are focusing on for this iteration. Each User Story is split into multiple deliverables that each contains several tasks. You will find two types of tasks:

* Committed - we've done our research, you just need to implement the work and learn the technology along the way
* Proposed - we like this idea, but we have not decided on the implementation, it's up to you

User stories are mostly self contained and can be completed in parallel. However, as you get farther along in a specific user story, you might run into tasks that depend on other User Stories. Therefore, the recommendation is to assign the user stories to members of your team and to tackle them in parallel. 

If you are blocked, a representative from the leadership team is always there to help, don't be afraid to reach out.

The source code for our applications is all open source and can be found on [our github account](https://github.com/Knowzy/KnowzyInternalApps).

0. **[Machine Setup Instructions][0]**

1. **User Story** - The shipping department has a fast, responsive, and powerful application for managing day to day duties
    * **Deliverable** - Make app more responsive
        * [1.1.1 [Committed] - Build a responsive Web App][111]
        * [1.1.2 [Committed] - Generate Progressive Web App (d. 1.1.1)][112]
        * [1.1.3 [Committed] - Update Web App to PWA (d. 1.1.1)][113]
        * [1.1.4 [Committed] - Test Your App (d. 1.1.1)][114]
    * **Deliverable** - Add Native functionality
        * [1.2.1 [Committed] - Add Live Tile (d. 1.1.1) ][121]
        * [1.2.2 [Proposed] - Add Share and Secondary Pinning (d. 1.1.2)][122]
        * [1.2.3 [Proposed] - Make PWA Linkable (d. 1.1.2)][123]
        * [1.2.4 [Proposed] - Add In-Memory Caching][124]

2. **User Story** - The product department has a modern, secure and forward-looking platform for managing product development life cycle
    * **Deliverable** - Enable integration of UWP APIs
        * [2.1.1 [Committed] - Add Desktop Bridge support in Visual Studio][211]
        * [2.1.2 [Committed] - Debugging a Windows Desktop Bridge App (d. 2.1.1)][212]
        * [2.1.3 [Committed] - Adding UWP APIs to a Desktop Bridge App (d. 2.1.2)][213]        
        * [2.1.4 [Committed] - Integrate Windows Hello authentication (d. 2.1.3)][214]
    * **Deliverable** - Add UWP XAML support
        * [2.2.1 [Proposed] - Create a new XAML view as part of app package (d. 2.1.1)][221]
        * [2.2.2 [Proposed] - Add support for other apps to share images and create new items (d. 2.1.1)][222]
        * [2.2.3 [Proposed] - Create a new UWP app that integrates with App Services (d. 2.1.1)][223]
    * **Deliverable** - Build enhanced UWP experience (d. 2.2.*)
        * [2.3.1 [Proposed] - Add support for ink (d. 2.2.*)][231]
        * [2.3.2 [Proposed] - Add complete support for Windows Hello Authentication (d. 2.1.3)][232]
        * [2.3.3 [Proposed] - Add support for more UWP features (d. 2.2.*)][233]

3. **User Story** - Consumers have a fun mobile experience 
    * **Deliverable** - Create a UWP and Android mobile app
        * [3.1.1 [Committed] - Create a Xamarin.Forms app with shared UI][311]
        * [3.1.2 [Committed] - Integrate native camera to capture image for each platform (d. 3.1.1)][312]
        * [3.1.3 [Committed] - Add InkCanvas support for UWP (d. 3.1.2)][313]
    * **Deliverable** - Create a fun social experience
        <!-- * [3.2.1 [Proposed] - Support sharing images to Social Networks (d. 3.1.2)][321] -->
        * [3.2.2 [Proposed] - Support cross device scenarios (Project Rome) (d. 3.1.2)][322]
    * **Deliverable** - Add automatic image analysis
        * [3.3.1 [Proposed] - Set up Cognitive Services for image face analysis in Azure (d. 312)][331]
        * [3.3.2 [Proposed] - Create an Azure Function to analyze an image and return nose location to automatically position in app (d. 3.3.1)][332]
    * **Deliverable** - Set up Continuous Integration and Deployment
        * [3.4.1 [Proposed] - Set up Continuous Integration and Deployment for the Windows App using Visual Studio Mobile Center][341]
        * [3.4.2 [Proposed] - Set up Continuous Integration and Deployment for the Android App using Visual Studio Mobile Center][342]
        * [3.4.3 [Proposed] - Add Custom Event Logging using Visual Studio Mobile Center][343]
    * **Deliverable** - Create a chat bot for support and for order status management 
        * [3.5.1 [Proposed] - Create a bot using the Microsoft Bot Framework][351]

4. **User Story** - All platform services are integrated in one platform
    * **Deliverable** - Unify and Publish all services
        * [4.1.1 - [Committed] Create a shared CosmosDB to store all data][411]
        * [4.1.2 - [Committed] Create API endpoint for shipping services (d. 4.1.1)][412]
        * [4.1.3 - [Committed] Create API endpoint for product services (d. 4.1.1)][413]
        * [4.1.4 - [Committed] Create Docker images (d. 4.1.2, 4.1.3)][414]
        * [4.1.5 - [Committed] Configure Kubernetes and publish to Azure (d. 4.1.4)][415]
        * [4.1.6 - [Proposed] Integrate Website and APIs (d. 4.1.5)][416]
    * **Deliverable** - Set up Continuous Delivery
        * [4.2.1 - [Proposed] Set up Visual Studio Team Services][421]
        * [4.2.2 - [Proposed] Continuous Delivery to Kubernetes using VSTS (d. 4.1.5, 4.2.0)][422]
    * **Deliverable** - Set up Telemetry
        * [4.3.1 - [Proposed] Set up telemetry for the web app and APIs][431]

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
[321]: stories/3/321_Social.md
[322]: stories/3/322_Rome.md
[331]: stories/3/331_CognitiveServices.md
[332]: stories/3/332_AzureFunction.md
[333]: stories/3/333_NoseAnalysys.md
[341]: stories/3/341_CICD_WindowsApp.md
[342]: stories/3/342_CICD_AndroidApp.md
[343]: stories/3/343_EventLogging.md
[351]: stories/3/351_Bot.md

[411]: stories/4/411_CosmosDB.md
[412]: stories/4/412_OrdersAPI.md
[413]: stories/4/413_ProductsAPI.md
[414]: stories/4/414_Docker.md
[415]: stories/4/415_Kubernetes.md
[416]: stories/4/416_Integrate.md
[421]: stories/4/421_SetupVSTS.md
[422]: stories/4/422_DevopsKubernetes.md
[431]: stories/4/431_Telemetry.md





