 BuildTourContent

Get started now, here are your tasks

* User Story (100) - The shipping department has a fast, responsive, and powerful application for managing day to day duties 
  * Deliverable (110) - Make app more responsive
    * [111][111] [Commited] - Create Service Worker
    * [112][112] [Commited] - Generate Progressive Web App (d. 111)
    * [113][113] [Proposed] - Make app offline (d. 111)
  * Deliverable (120) - Add Native functionality
    * [121][121] [Proposed] - Register for push notifications (d. 112)
    * [122][122] [Proposed] - Integrate camera for scanning barcodes (d. 112)

* User Story (200) - The product department has a modern, secure and forward-looking platform for managing product development lifecycle
    * Deliverable (210) - Enable integration of UWP APIs
        * [211][211] [Commited] - Add Centennial support in Visual Studio
        * [212][212] [Commited] - Integrate LiveTiles (d. 211)
        * [213][213] [Proposed] - Integrate Windows Hello authentication (d. 211)
    * Deliverable (220) - Add UWP XAML support
        * [221][221] [Proposed] - Create a new XAML view as part of app package (d. 211)
        * [222][222] [Proposed] - Add support for other apps to share images and create new items (d. 211)
        * [223][223] [Proposed] - Create a new UWP app that integrates with App Services (d. 211)
    * Deliverable (230) - Build rich UWP xaml UI (d. 22*)
        * [231][231] [Proposed] - Integrate the UWP Community Toolkit to add animations, new controls, services to the new view (d. 22*)
        * [232][232] [Proposed] - Add support for ink or dial (d. 22*)
        * [233][233] [Proposed] - Add ccomposition effects or (connected) annimations (d. 22*)

* User Story (300) - Consumers have a fun mobile experience 
    * Deliverable (310) - Create a UWP and Andorid mobile app
        * [311][311] [Commited] - Create a Xamarin.Forms app with shared UI
        * [312][312] [Commited] - Integrate native camera to capture image for each platform (d. 311)
        * [313][313] [Commited] - Add InkCanvas support for UWP (d. 312)
    * Deliverable (320) - Create a fun social experience
        * [321][321] [Proposed] - Support sharing images to Social Networks (d. 312)
        * [322][322] [Proposed] - Support cross device scenarios (Project Rome) (d. 312)
    * Deliverable (330) - Add automatic image analysis
        * [331][331] [Proposed] - Set up Cogntive Services for image face analysis in Azure (d. 312)
        * [332][332] [Proposed] - Create an Azure Function to analyze an image and return nose location (d. 331)
        * [333][333] [Proposed] - Integrate mobile app to draw clown nose on top of user's nose (d. 332)

* User Story (400) - All platform services are integrated in one platform
    * Deliverable (410) - Unify and Publish all services
        * [411][411] - Create a shared DocumentDB to store all data
        * [412][412] - Crete API endpoint for shipping services (d. 411)
        * [413][412] - Crete API endpoint for product services (d. 411)
        * [414][414] - Configure Service Fabric and publish to Azure (d. 412, 413)



[111]: stories/100/111_CreateServiceWorker.md
[112]: stories/100/112_GeneratePWA.md
[113]: stories/100/113_Offline.md
[121]: stories/100/121_PushNotifications.md
[122]: stories/100/122_Camera.md

[211]: stories/200/211_Centennial.md
[212]: stories/200/212_LiveTiles.md
[213]: stories/200/213_WindowsHello.md
[221]: stories/200/221_XAMLView.md
[222]: stories/200/222_Share.md
[223]: stories/200/223_AppServices.md
[231]: stories/200/231_Toolkit.md
[232]: stories/200/232_Inking_Dial.md
[233]: stories/200/233_Composition.md

[311]: stories/300/311_XamarinForms.md
[312]: stories/300/312_Camera.md
[313]: stories/300/313_InkCanvas.md
[321]: stories/300/321_Social.md
[322]: stories/300/322_Rome.md
[331]: stories/300/331_CognitiveServices.md
[332]: stories/300/332_AzureFunction.md
[333]: stories/300/333_NoseAnalysys.md

[411]: stories/400/411_DocumentDB.md
[412]: stories/400/412_413_API.md
[414]: stories/400/414_ServiceFabric.md
