# Contoso KNOWZY Backlog

There are four User Stories that we are focusing on for this iteration. Each User Story is split into multiple deliverables that each contains several tasks. Tasks marked as committed have been committed to our leadership, and task marked as proposed have been requested or proposed.

User stories are self contained, they do not have dependencies to other stories. Therefore, the recommendation is to assign the user stories to members of your team and to tackle them in parallel. 

If you are blocked, a representative from the leadership team is always there to help, don’t be afraid to reach out. 


1. **User Story** - The shipping department has a fast, responsive, and powerful application for managing day to day duties 
    * **Deliverable** - Make app more responsive
        * [1.1.1][111] [Commited] - Create Service Worker
        * [1.1.2][112] [Commited] - Generate Progressive Web App (d. 1.1.1)
        * [1.1.3][113] [Proposed] - Make app offline (d. 1.1.1) 
    * **Deliverable** - Add Native functionality
        * [1.2.1][121] [Proposed] - Register for push notifications (d. 1.1.2)
        * [1.2.2][122] [Proposed] - Integrate camera for scanning barcodes (d. 1.1.2)

2. **User Story** - The product department has a modern, secure and forward-looking platform for managing product development lifecycle
    * **Deliverable** - Enable integration of UWP APIs
        * [2.1.1][211] [Commited] - Add Centennial support in Visual Studio
        * [2.1.2][212] [Commited] - Integrate LiveTiles (d. 2.1.1)
        * [2.1.3][213] [Proposed] - Integrate Windows Hello authentication (d. 2.1.1)
    * **Deliverable** - Add UWP XAML support
        * [2.2.1][221] [Proposed] - Create a new XAML view as part of app package (d. 2.1.1)
        * [2.2.2][222] [Proposed] - Add support for other apps to share images and create new items (d. 2.1.1)
        * [2.2.3][223] [Proposed] - Create a new UWP app that integrates with App Services (d. 2.1.1)
    * **Deliverable** - Build rich UWP xaml UI (d. 2.2.*)
        * [2.3.1][231] [Proposed] - Integrate the UWP Community Toolkit to add animations, new controls, services to the new view (d. 2.2.*)
        * [2.3.2][232] [Proposed] - Add support for ink or dial (d. 2.2.*)
        * [2.3.3][233] [Proposed] - Add ccomposition effects or (connected) annimations (d. 2.2.*)

3. **User Story** - Consumers have a fun mobile experience 
    * **Deliverable** - Create a UWP and Andorid mobile app
        * [3.1.1][311] [Commited] - Create a Xamarin.Forms app with shared UI
        * [3.1.2][312] [Commited] - Integrate native camera to capture image for each platform (d. 3.1.1)
        * [3.1.3][313] [Commited] - Add InkCanvas support for UWP (d. 3.1.2)
    * **Deliverable** - Create a fun social experience
        * [3.2.1][321] [Proposed] - Support sharing images to Social Networks (d. 3.1.2)
        * [3.2.2][322] [Proposed] - Support cross device scenarios (Project Rome) (d. 3.1.2)
    * **Deliverable** - Add automatic image analysis
        * [3.3.1][331] [Proposed] - Set up Cogntive Services for image face analysis in Azure (d. 312)
        * [3.3.2][332] [Proposed] - Create an Azure Function to analyze an image and return nose location (d. 3.3.1)
        * [3.3.3][333] [Proposed] - Integrate mobile app to draw clown nose on top of user's nose (d. 3.3.2)

4. **User Story** - All platform services are integrated in one platform
    * **Deliverable** - Unify and Publish all services
        * [4.1.1][411] - Create a shared DocumentDB to store all data
        * [4.1.2][412] - Create API endpoint for shipping services (d. 4.1.1)
        * [4.1.3][412] - Create API endpoint for product services (d. 4.1.1)
        * [4.1.4][414] - Configure Kubernetes and publish to Azure (d. 4.1.2, 4.1.3)



[111]: stories/1/111_CreateServiceWorker.md
[112]: stories/1/112_GeneratePWA.md
[113]: stories/1/113_Offline.md
[121]: stories/1/121_PushNotifications.md
[122]: stories/1/122_Camera.md

[211]: stories/2/211_Centennial.md
[212]: stories/2/212_LiveTiles.md
[213]: stories/2/213_WindowsHello.md
[221]: stories/2/221_XAMLView.md
[222]: stories/2/222_Share.md
[223]: stories/2/223_AppServices.md
[231]: stories/2/231_Toolkit.md
[232]: stories/2/232_Inking_Dial.md
[233]: stories/2/233_Composition.md

[311]: stories/3/311_XamarinForms.md
[312]: stories/3/312_Camera.md
[313]: stories/3/313_InkCanvas.md
[321]: stories/3/321_Social.md
[322]: stories/3/322_Rome.md
[331]: stories/3/331_CognitiveServices.md
[332]: stories/3/332_AzureFunction.md
[333]: stories/3/333_NoseAnalysys.md

[411]: stories/4/411_DocumentDB.md
[412]: stories/4/412_413_API.md
[414]: stories/4/414_Kubernetes.md
