# Adventure Works KNOWZY Backlog

There are four User Stories that we are focusing on for this iteration. Each User Story is split into multiple deliverables that each contains several tasks. Tasks marked as committed have been committed to our leadership, and task marked as proposed have been requested or proposed.

User stories are self contained, they do not have dependencies to other stories. Therefore, the recommendation is to assign the user stories to members of your team and to tackle them in parallel. 

If you are blocked, a representative from the leadership team is always there to help, don’t be afraid to reach out.


1. **User Story** - The shipping department has a fast, responsive, and powerful application for managing day to day duties
    * **Deliverable** - Make app more responsive
        * [1.1.1][111] [Committed] - Create Service Worker
        * [1.1.2][112] [Committed] - Generate Progressive Web App (d. 1.1.1)
        * [1.1.3][113] [Proposed] - Make app offline (d. 1.1.1) 
    * **Deliverable** - Add Native functionality
        * [1.2.1][121] [Proposed] - Register for push notifications (d. 1.1.2)
        * [1.2.2][122] [Proposed] - Integrate camera for scanning barcodes (d. 1.1.2)

2. **User Story** - The product department has a modern, secure and forward-looking platform for managing product development life cycle
    * **Deliverable** - Enable integration of UWP APIs
        * [2.1.1][211] [Committed] - Add Centennial support in Visual Studio
        * [2.1.2][212] [Committed] - Debugging a Windows Desktop Bridge App (d. 2.1.1)
        * [2.1.3][213] [Proposed] - Integrate Windows Hello authentication (d. 2.1.1)
    * **Deliverable** - Add UWP XAML support
        * [2.2.1][221] [Proposed] - Create a new XAML view as part of app package (d. 2.1.1)
        * [2.2.2][222] [Proposed] - Add support for other apps to share images and create new items (d. 2.1.1)
        * [2.2.3][223] [Proposed] - Create a new UWP app that integrates with App Services (d. 2.1.1)
    * **Deliverable** - Build rich UWP xaml UI (d. 2.2.*)
        * [2.3.1][231] [Proposed] - Integrate the UWP Community Toolkit to add animations, new controls, services to the new view (d. 2.2.*)
        * [2.3.2][232] [Proposed] - Add support for ink or dial (d. 2.2.*)
        * [2.3.3][233] [Proposed] - Add composition effects or (connected) animations (d. 2.2.*)

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
        * [3.3.2][332] [Proposed] - Create an Azure Function to analyze an image and return nose location (d. 3.3.1)
        * [3.3.3][333] [Proposed] - Integrate mobile app to draw clown nose on top of user's nose (d. 3.3.2)
    * **Deliverable** - Set up Continuous Integration and Deployment
        * [3.4.1][341] [Proposed] - Set up Continuous Integration and Deployment using Visual Studio Mobile Center
        * [3.4.2][342] [Proposed] - Add Custom Event Logging using Visual Studio Mobile Center

4. **User Story** - All platform services are integrated in one platform
    * **Deliverable** - Unify and Publish all services
        * [4.1.1][411] - Create a shared DocumentDB to store all data
        * [4.1.2][412] - Create API endpoint for shipping services (d. 4.1.1)
        * [4.1.3][413] - Create API endpoint for product services (d. 4.1.1)
        * [4.1.4][414] - Create Docker images (d. 4.1.2, 4.1.3)
        * [4.1.5][415] - Configure Kubernetes and publish to Azure (d. 4.1.4)
    * **Challenge** - Rub a little DevOps on it.
        * [4.2][420] - Continuous Delivery to Kubernetes using VSTS (d. 4.1.5)


5. **User Story** - Our executives have a way to visualize the products in 3D
   * **Deliverable** - Build a UWP 3D Visualizer
      * [5.1.1][511] - [Committed] Create the model in Paint3D
      * [5.1.2a][512a] - [Committed] Create a Unity solution to visualize 3D Model (d 5.1.1) or
      * [5.1.2b][512b] - [Committed] Create a BabylonJS solution to visualize 3D Model (d 5.1.1)
   * **Deliverable** - Visualize noses in VR
      * [5.2.1][521] - [Proposed] Retarget UWP app to Mixed Reality Platform (d 5.1.2)



[111]: stories/1/111_CreateServiceWorker.md
[112]: stories/1/112_GeneratePWA.md
[113]: stories/1/113_Offline.md
[121]: stories/1/121_PushNotifications.md
[122]: stories/1/122_Camera.md

[211]: stories/2/211_Centennial.md
[212]: stories/2/212_Debugging.md
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
[341]: stories/3/341_CICD.md
[342]: stories/3/342_EventLogging.md

[411]: stories/4/411_DocumentDB.md
[412]: stories/4/412_OrdersAPI.md
[413]: stories/4/413_ProductsAPI.md
[414]: stories/4/414_Docker.md
[415]: stories/4/415_Kubernetes.md
[420]: stories/4/420_DevopsChallenge.md

[511]: stories/5/511_Paint3d.md
[512a]: stories/5/512a_Unity.md
[512b]: stories/5/512b_Babylon.md
[521]: stories/5/521_MR.md