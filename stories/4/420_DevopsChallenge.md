# Challenge 4.1.5 - Continuous Delivery to Kubernetes using VSTS

![Release Pipeline](images/DevOpsPipeline.png)

Using Visual Studio Team Services will help our development and operations department to have a proper team environment to efficientely provide new stable releases into production. The main goal of this challenge is to automatically post a new release of our code into our production kubernetes cluster starting from a single git commit.

## Resources

* Understanding of DevOps concepts like [Continuous Integration, Continuous Delivery and, Build and Release Management](https://www.visualstudio.com/en-us/docs/build/get-started/ci-cd-part-1).
* Access to Azure subscription.
* [Visual Studio Team Services account.](https://www.visualstudio.com/team-services/)
* [Installing the Kubernetes extension for VSTS](https://marketplace.visualstudio.com/items?itemName=tsuyoshiushio.k8s-endpoint)

## Suggestion on how to accomplish this challenge

For the sake of this exercise, lets focus **only on the API code** and any new releases you can have. These changes will be tested by VSTS and, pushed into ACR once all your tests have passed (You should provide your own tests). Then VSTS will automatically deploy the last version of the app into your cluster by obtaining the latest image from the ACR.

The following two resources will provide you all the answers on how to accomplish this task, it´s your duty to understand them and consolidate the concepts to set up a full development cycle. Please note that none of them is a step-by-step tutorial to achieve what you want, but jointly you will be able to make it work.:

1. [CI/CD to Kubernetes clusters using VSTS](https://github.com/dtzar/blog/tree/master/CD-Kubernetes-VSTS)
1. [Build and deploy your ASP.NET Core app to Azure](https://www.visualstudio.com/en-us/docs/build/apps/aspnet/aspnetcore-to-azure#enable-continuous-integration-ci)

Overall, you should be able to do the following steps to provide CI/CD in a Kubernetes cluster using Azure.

1. Create an acccount and a new project in [Visual Studio Team Services](https://www.visualstudio.com/team-services/).
1. Add or connect your API code repository to VSTS.
1. Install the VSTS Kubernetes [extension](https://marketplace.visualstudio.com/items?itemName=tsuyoshiushio.k8s-endpoint).
1. Provide Continuous Integration with VSTS by pushing your docker images with the latest version of the tested code into ACR.
1. Set up a trigger to pull your new docker image into your Kubernetes cluster after it has been properly tested and deployed.

## Expected behavior

At the end of this challenge you should be able to have the follow these steps and get similar results:

1. Commit a new change into your `master` branch.
1. VSTS will build your project and test it.
1. If all the tests have been passed, VSTS will create and push a new Docker image with the new code. Please refer to previous section ['Create Docker images'][414] for the definition of the image.
1. Your kubernetes task created with the help of the VSTS Kubernetes [extension](https://marketplace.visualstudio.com/items?itemName=tsuyoshiushio.k8s-endpoint) will now run to pull the new image into the cluster.
1. Do a request into your freshly deployed API and verify that your new changes are up and running.


[414]: stories/4/414_Docker.md


