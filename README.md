 BuildTourContent

Get started now, here are your tasks

* User Story (100) - The shipping department has a fast, responsive, and powerful application for managing day to day duties 
  * Deliverable (110) - Make app more responsive
    * Task (111) [Commited] - Create Service Worker
    * Task (112) [Commited] - Generate Progressive Web App (d. 111)
    * Task (113) [Proposed] - Make app offline (d. 111)
  * Deliverable (120) - Add Native functionality
    * Task (121) [Proposed] - Register for push notifications (d. 112)
    * Task (122) [Proposed] - Integrate camera for scanning barcodes (d. 112)

* User Story (200) - The product department has a modern, secure and forward-looking platform for managing product development lifecycle
    * Deliverable (210) - Enable integration of UWP APIs
        * Task (211) [Commited] - Add Centennial support in Visual Studio
        * Task (212) [Commited] - Integrate LiveTiles (d. 211)
        * Task (213) [Proposed] - Integrate Windows Hello authentication (d. 211)
    * Deliverable (220) - Add UWP XAML support
        * Task (221) [Proposed] - Create a new XAML view as part of app package (d. 211)
        * Task (222) [Proposed] - Add support for other apps to share images and create new items (d. 211)
        * Task (223) [Proposed] - Create a new UWP app that integrates with App Services (d. 211)
    * Deliverable (230) - Build rich UWP xaml UI (d. 22*)
        * Task (231) [Proposed] - Integrate the UWP Community Toolkit to add animations, new controls, services to the new view (d. 22*)
        * Task (232) [Proposed] - Add support for ink or dial (d. 22*)
        * Task (233) [Proposed] - Add ccomposition effects or (connected) annimations (d. 22*)

* User Story (300) - Consumers have a fun mobile experience 
    * Deliverable (310) - Create a UWP and Andorid mobile app
        * Task (311) [Commited] - Create a Xamarin.Forms app with shared UI
        * Task (312) [Commited] - Integrate native camera to capture image for each platform (d. 311)
        * Task (313) [Commited] - Add InkCanvas support for UWP (d. 312)
    * Deliverable (320) - Create a fun social experience
        * Task (321) [Proposed] - Support sharing images to Social Networks (d. 312)
        * Task (322) [Proposed] - Support cross device scenarios (Project Rome) (d. 312)
    * Deliverable (330) - Add automatic image analysis
        * Task (331) [Proposed] - Set up Cogntive Services for image face analysis in Azure (d. 312)
        * Task (332) [Proposed] - Create an Azure Function to analyze an image and return nose location (d. 331)
        * Task (333) [Proposed] - Integrate mobile app to draw clown nose on top of user's nose (d. 332)

* User Story (400) - All platform services are integrated in one platform
    * Deliverable (410) - Unify and Publish all services
        * Task (411) - Create a shared DocumentDB to store all data
        * Task (412) - Crete API endpoint for shipping services (d. 411)
        * Task (413) - Crete API endpoint for product services (d. 411)
        * Task (414) - Configure Service Fabric and publish to Azure (d. 412, 413)