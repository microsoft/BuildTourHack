# Challenge 4.2.2 - Continuous Delivery to Kubernetes using VSTS


The CTO is pleased that you've deployed to Azure and Kubernetes.  However, now they'd like you to set up Continuous Delivery using the Visual Studio Team Services (VSTS) account you set up in [Task 4.2.1][421].  Using VSTS will enable our development and operations department to have work together efficiently as a team and push new stable releases into production. The primary goal of this challenge is to automatically post a new release of our code into our production Kubernetes cluster with any commit to our git repository.

![Release Pipeline](images/DevOpsPipeline.png)

## Prerequisites 

* This task has a dependency on [Task 4.1.5][415], [Task 4.2.1][421], and all of their prerequisites.
* Access to the Azure subscription you used in [Task 4.1.5][415].

## Task

1.  Set up CI / CD with your application and Kubernetes on Azure.

## Comments

###### @ 3:37am
I wasn't really sure where to get started with continuous integration or delivery but [this](https://www.visualstudio.com/en-us/docs/build/get-started/ci-cd-part-1) really helped me understand!

###### @ 5:23am
It looks like you can install the [Kubernetes Extension for VSTS here](https://marketplace.visualstudio.com/items?itemName=tsuyoshiushio.k8s-endpoint).

###### @ 10:15am
I think I found [step by step instructions][497] on setting this up!

###### @ 4:58pm
If all goes according to plan, I should be able to demonstrate this working by [following this][498].


[414]: /stories/4/414_Docker.md
[415]: /stories/4/415_Kubernetes.md
[421]: /stories/4/421_SetupVSTS.md
[497]: /stories/4/497_KubernetesVSTS.md
[498]: /stories/4/498_KubernetesCICDBehavior.md

## continue to [next task >> ](431_Telemetry.md)