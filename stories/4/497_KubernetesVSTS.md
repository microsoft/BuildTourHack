# Setting Up Kubernetes and VSTS

For the sake of this exercise, **lets focus only on the API code** and any new releases you have. These changes will be tested by VSTS and, pushed into Azure Container Registry (ACR) once all your tests have passed (we won't go into writing tests today). Then VSTS will automatically deploy the latest version of the app into your cluster by obtaining the latest image from ACR.

The following two resources will provide you all the answers on how to accomplish this task, it´s your duty to understand them and consolidate the concepts to set up a full development cycle. Please note that none of these are a step-by-step tutorial to achieve what you want, but jointly you will be able to make it work:

1. [CI/CD to Kubernetes clusters using VSTS](https://github.com/dtzar/blog/tree/master/CD-Kubernetes-VSTS)
2. [Build and deploy your ASP.NET Core app to Azure](https://www.visualstudio.com/en-us/docs/build/apps/aspnet/aspnetcore-to-azure#enable-continuous-integration-ci)

You should be able to do the following steps to provide CI/CD in a Kubernetes cluster using Azure.

1. Create a new project in your VSTS account.
2. Push your API code into the project's repository in VSTS.
3. Install the VSTS Kubernetes [extension](https://marketplace.visualstudio.com/items?itemName=tsuyoshiushio.k8s-endpoint).
4. Provide Continuous Integration with VSTS by pushing your docker images with the latest version of the tested code into ACR.
5. Set up a trigger to pull your new docker image into your Kubernetes cluster after it has been properly tested and deployed.

[414]: /stories/4/414_Docker.md
[415]: /stories/4/415_Kubernetes.md
[421]: /stories/4/421_SetupVSTS.md