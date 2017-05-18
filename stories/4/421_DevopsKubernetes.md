# Challenge 4.2.1 - Continuous Delivery to Kubernetes using VSTS


The CTO is pleased that you've deployed to Azure and Kubernetes.  However, now they'd like you to set up Continuous Delivery using the Visual Studio Team Services (VSTS) account you set up in [Task 4.2.0](420_SetupVSTS.md).  Using VSTS will enable our development and operations department to have work together efficiently as a team and push new stable releases into production. The primary goal of this challenge is to automatically post a new release of our code into our production Kubernetes cluster with any commit to our git repository.

![Release Pipeline](images/DevOpsPipeline.png)

## Prerequisites 

* This task has a dependency on [Task 4.1.5][415], [Task 4.2.0](420_SetupVSTS.md), and all of their prerequisites.
* Access to the Azure subscription you used in [Task 4.1.5][415].

## Task

For the sake of this exercise, **lets focus only on the API code** and any new releases you have. These changes will be tested by VSTS and, pushed into Azure Container Registry (ACR) once all your tests have passed (we won't go into writing tests today). Then VSTS will automatically deploy the latest version of the app into your cluster by obtaining the latest image from ACR.

The following two resources will provide you all the answers on how to accomplish this task, it´s your duty to understand them and consolidate the concepts to set up a full development cycle. Please note that none of these are a step-by-step tutorial to achieve what you want, but jointly you will be able to make it work:

1. [CI/CD to Kubernetes clusters using VSTS](https://github.com/dtzar/blog/tree/master/CD-Kubernetes-VSTS)
2. [Use Visual Studio to automatically generate a CI/CD pipeline to deploy an ASP.NET Core web app with Docker to Azure](https://www.visualstudio.com/en-us/docs/build/apps/aspnet/aspnetcore-docker-to-azure)

You should be able to do the following steps to provide CI/CD in a Kubernetes cluster using Azure.

1. Create a new project in your VSTS account.
2. Push your API code into the project's repository in VSTS.
3. Install the VSTS Kubernetes [extension](https://marketplace.visualstudio.com/items?itemName=tsuyoshiushio.k8s-endpoint).
4. Provide Continuous Integration with VSTS by pushing your docker images with the latest version of the tested code into ACR.
5. Set up a trigger to pull your new docker image into your Kubernetes cluster after it has been properly tested and deployed.

## Expected behavior

At the end of this challenge you should be able to follow these steps and get similar results:

1. Commit a new change into your `master` branch.
2. VSTS will build your project and test it.
3. If all the tests have been passed, VSTS will create and push a new Docker image with the new code. Please refer to previous section ['Create Docker images'][414] for the definition of the image.
4. Your Kubernetes task created with the help of the VSTS Kubernetes [extension](https://marketplace.visualstudio.com/items?itemName=tsuyoshiushio.k8s-endpoint) will now run to pull the new image into the cluster.
5. Do a request into your freshly deployed API and verify that your new changes are up and running.

## Resources

* Understanding of DevOps concepts including [Continuous Integration, Continuous Delivery and, Build and Release Management](https://www.visualstudio.com/en-us/docs/build/get-started/ci-cd-part-1).
* [Installing the Kubernetes extension for VSTS](https://marketplace.visualstudio.com/items?itemName=tsuyoshiushio.k8s-endpoint)

[414]: /stories/4/414_Docker.md
[415]: /stories/4/415_Kubernetes.md
