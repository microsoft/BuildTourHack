# Adventure Works KNOWZY Backlog

There are four User Stories that we are focusing on for this iteration. Each User Story is split into multiple deliverables that each contains several tasks. Tasks marked as committed have been committed to our leadership, and task marked as proposed have been requested or proposed.

User stories are mostly self contained, they do not have dependencies to other stories. However, as you get farther along in a specific user story, you might run into tasks that depend on other User Stories. Therefore, the recommendation is to assign the user stories to members of your team and to tackle them in parallel. 

If you are blocked, a representative from the leadership team is always there to help, don’t be afraid to reach out.

The source code for our applications is all open source and can be found on [our github account](https://github.com/Knowzy/KnowzyInternalApps).


1. **User Story** - The shipping department has a fast, responsive, and powerful application for managing day to day duties
    * **Deliverable** - Make app more responsive
        * [1.1.1][111] [Committed] - Build a responsive Web App
        * [1.1.2][112] [Committed] - Generate Progressive Web App (d. 1.1.1)
        * [1.1.3][113] [Committed] - Update Web App to PWA (d. 1.1.1) 
        * [1.1.4][114] [Committed] - Test Your App (d. 1.1.1) 
    * **Deliverable** - Add Native functionality
        * [1.2.1][121] [Committed] - Add Live Tile (d. 1.1.1) 
        * [1.2.3][123] [Proposed] - Add Share and Secondary Pinning (d. 1.1.2)
        * [1.2.4][124] [Proposed] - Make PWA Linkable (d. 1.1.2)
        * [1.2.5][125] [Proposed] - Add In-Memory Caching

2. **User Story** - The product department has a modern, secure and forward-looking platform for managing product development life cycle
    * **Deliverable** - Enable integration of UWP APIs
        * [2.1.1][211] [Committed] - Add Desktop Bridge support in Visual Studio
        * [2.1.2][212] [Committed] - Debugging a Windows Desktop Bridge App (d. 2.1.1)
        * [2.1.3][213] [Committed] - Adding UWP APIs to a Desktop Bridge App (d. 2.1.2)        
        * [2.1.4][214] [Committed] - Integrate Windows Hello authentication (d. 2.1.3)
    * **Deliverable** - Add UWP XAML support
        * [2.2.1][221] [Proposed] - Create a new XAML view as part of app package (d. 2.1.1)
        * [2.2.2][222] [Proposed] - Add support for other apps to share images and create new items (d. 2.1.1)
        * [2.2.3][223] [Proposed] - Create a new UWP app that integrates with App Services (d. 2.1.1)
    * **Deliverable** - Build enhanced UWP experience (d. 2.2.*)
        * [2.3.1][231] [Proposed] - Add support for ink (d. 2.2.*)
        * [2.3.2][232] [Proposed] - Add complete support for Windows Hello Authentication (d. 2.1.3)
        * [2.3.3][233] [Proposed] - Add support for more UWP features (d. 2.2.*)

3. **User Story** - Consumers have a fun mobile experience 
    * **Deliverable** - Create a UWP and Android mobile app
        * [3.1.1][311] [Committed] - Create a Xamarin.Forms app with shared UI
        * [3.1.2][312] [Committed] - Integrate native camera to capture image for each platform (d. 3.1.1)
        * [3.1.3][313] [Committed] - Add InkCanvas support for UWP (d. 3.1.2)
    * **Deliverable** - Create a fun social experience
        * [3.2.1][321] [Proposed] - Support sharing images to Social Networks (d. 3.1.2)
        * [3.2.2][322] [Proposed] - Support cross device scenarios (Project Rome) (d. 3.1.2)
    * **Deliverable** - Add automatic image analysis
        * [3.3.1][331] [Proposed] - Set up Cognitive Services for image face analysis in Azure (d. 312)
        * [3.3.2][332] [Proposed] - Create an Azure Function to analyze an image and return nose location to automatically position in app (d. 3.3.1)
    * **Deliverable** - Set up Continuous Integration and Deployment
        * [3.4.1][341] [Proposed] - Set up Continuous Integration and Deployment for the Windows App using Visual Studio Mobile Center
        * [3.4.2][342] [Proposed] - Set up Continuous Integration and Deployment for the Android App using Visual Studio Mobile Center
        * [3.4.3][343] [Proposed] - Add Custom Event Logging using Visual Studio Mobile Center
    * **Deliverable** - Create a chat bot for support and for order status management 
        * [3.5.1][351] [Proposed] - Create a bot using the Microsoft Bot Framework

4. **User Story** - All platform services are integrated in one platform
    * **Deliverable** - Unify and Publish all services
        * [4.1.1][411] - [Committed] Create a shared CosmosDB to store all data
        * [4.1.2][412] - [Committed] Create API endpoint for shipping services (d. 4.1.1)
        * [4.1.3][413] - [Committed] Create API endpoint for product services (d. 4.1.1)
        * [4.1.4][414] - [Committed] Create Docker images (d. 4.1.2, 4.1.3)
        * [4.1.5][415] - [Committed] Configure Kubernetes and publish to Azure (d. 4.1.4)
        * [4.1.6][416] - [Proposed] Integrate Website and APIs (d. 4.1.5)
    * **Deliverable** - Set up Continuous Delivery
        * [4.2.1][421] - [Proposed] Set up Visual Studio Team Services
        * [4.2.2][422] - [Proposed] Continuous Delivery to Kubernetes using VSTS (d. 4.1.5, 4.2.0)
    * **Deliverable** - Set up Telemetry
        * [4.3.1][431] - [Proposed] Set up telemetry for the web app and APIs


[Bonus tasks that will be removed from the home page](stories/5/story_5.md)



[111]: stories/1/111_BuildWebApp.md
[112]: stories/1/112_GeneratePWA.md
[113]: stories/1/113_ConfigureSW.md
[114]: stories/1/114_Test_App.md
[121]: stories/1/121_Add_WIndows_Feature.md
[123]: stories/1/124_BONUS-RenoFeatures.md
[124]: stories/1/123_BONUS-APP-Links.md 
[125]: stories/1/125_BONUS_InMemoryCaching.md

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
[422]: stories/4/421_DevopsKubernetes.md
[431]: stories/4/431_Telemetry.md





