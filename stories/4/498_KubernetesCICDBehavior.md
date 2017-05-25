# Kubernetes and VSTS Expected Behavior

At the end of this challenge you should be able to follow these steps and get similar results:

1. Commit a new change into your `master` branch.
2. VSTS will build your project and test it.
3. If all the tests have been passed, VSTS will create and push a new Docker image with the new code. Please refer to previous section ['Create Docker images'][414] for the definition of the image.
4. Your Kubernetes task created with the help of the VSTS Kubernetes [extension](https://marketplace.visualstudio.com/items?itemName=tsuyoshiushio.k8s-endpoint) will now run to pull the new image into the cluster.
5. Do a request into your freshly deployed API and verify that your new changes are up and running.
[414]: /stories/4/414_Docker.md
[415]: /stories/4/415_Kubernetes.md
[421]: /stories/4/421_SetupVSTS.md