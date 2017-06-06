# Task 4.1.5 - Deploy to Kubernetes on Azure Container Services

## 1. Prerequisites

* Access to Azure subscription.
<!--* Azure Cloud Shell should be [Configured for persistent shell storage](https://github.com/jluk/ACC-Documentation/blob/master/persisting-shell-storage.md). -->
* Azure Cloud shell should be pointed toward the correct subscription, more details in [Step 4.1.1](./411_CosmosDB.md)
* You'll also need the name of the resource group created in [Step 4.1.1](./411_CosmosDB.md)


## 2. Creating and deploying to a Kubernetes cluster in Azure Container Service

### a. Set up the cluster with Azure Cloud Shell

We're going to be working within the Azure Cloud Shell. Lets start by setting up a few variables:

```bash
# Use the name of the Resource Group created in 4.1.1 if this is not the same Cloud Shell session
RESOURCE_GROUP=<resource group name>

# A unique DNS Prefix for your cluster, which will also be the Kubernetes cluster name
DNS_PREFIX=<unique cluster name>

# The name for the ACS Instance
ACS_NAME=<unique ACS instance name>
```

### b. Create the cluster using the Azure CLI

We'll add the `--generate-ssh-keys` parameter which generates the necessary SSH key files for the deployment if they don't exist already in the default `~/.ssh/` directory.

> These keys are needed to grant access to the cluster, and will be stored in the Storage Account created for your Cloud Shell. You can learn more by visiting the [Cloud Shell documentation](https://docs.microsoft.com/en-us/azure/cloud-shell/persisting-shell-storage)

```bash
az acs create --orchestrator-type=kubernetes --agent-vm-size Standard_A1 --resource-group $RESOURCE_GROUP --name=$ACS_NAME --dns-prefix=$DNS_PREFIX --generate-ssh-keys
```

This will take several minutes as your resources are created and configured. Once your cluster is created, you'll need to get the config from Kubernetes into your Cloud Shell session so that you can interact with it.

```bash
az acs kubernetes get-credentials --resource-group=$RESOURCE_GROUP --name=$ACS_NAME
```

Verify that you can used `kubectl` to talk to the cluster

```bash
kubectl cluster-info
```

### c. Deploying your container images _(from [Step 4.1.4](./414_Docker.md))_

i) Store the name of your Azure Container Registry

This registry was generated in step [4.1.4](./414_Docker.md#acr), and is where the images containing the apps and services we want to deploy are located.

```bash
ACR_NAME=<name of registry that contains your images>
```

ii) Deploy your two services from their respective Docker images. We can deploy by telling kubernetes to get the docker images and deploy them, using the run command in `kubectl`, we're passing the `--env` parameter, to tell the container images the connection string for your data-store, so the running services once deployed know how to connect to the CosmosDB.

```bash
# Deploy to Kubernetes
kubectl run ordersapi --image=$ACR_NAME.azurecr.io/knowzy/ordersapi:1 --env "COSMOSDB_ENDPOINT=https://<< your cosmosdb name>>.documents.azure.com:443/" --env "COSMOSDB_KEY=<your Cosmos DB key string>"
kubectl run productsapi --image=$ACR_NAME.azurecr.io/knowzy/productsapi:1 --env "COSMOSDB_ENDPOINT=https://<< your cosmosdb name>>.documents.azure.com:443/" --env "COSMOSDB_KEY=<your Cosmos DB key string>"
kubectl run webapp --image=$ACR_NAME.azurecr.io/knowzy/webapp:1

# View your running pods
kubectl get pods
```

iii) Expose your services

You've got containers running on Kubernetes, but they're not yet exposed to the outside world. Let's do that now

```bash
# This will create an Azure Load Balancer to direct traffic to your app
kubectl expose deployment ordersapi --port=80 --type=LoadBalancer
kubectl expose deployment productsapi --port=80 --type=LoadBalancer

# List Kubernetes services, which includes the external IP
kubectl get svc
```

```bash
NAME         CLUSTER-IP   EXTERNAL-IP   PORT(S)        AGE
kubernetes   10.0.0.1     <none>        443/TCP        2d
ordersapi    10.0.8.27    <pending>     80:30213/TCP   3h
productsapi  10.0.8.28    <pending>     80:30175/TCP   19s
```
Initially, you will see something similar to the above. Wait a couple of minutes for the IP's and load balancers to be provisioned, and re-run `kubectl get svc` to see your public-facing IP's.

```bash
NAME          CLUSTER-IP     EXTERNAL-IP     PORT(S)          AGE
kubernetes    10.0.0.1       <none>          443/TCP          2d
ordersapi     10.0.8.27      13.81.60.236    80:30213/TCP     3h
productsapi   10.0.8.28      52.232.72.172   80:30175/TCP     5m
```

You can test your deployment as soon as the `<pending>` external IP is provided. Simply go into your browser to the url provided `http://<<External IP>>/api/values`.


iii) Deploy your webapp from the Docker image. We deploy the same way as above, but we don't need the database parameter, as the front end web application should have no direct access to the CosmosDB. Instead we should provide the url to the api services

```bash
kubectl run webapp --image=$ACR_NAME.azurecr.io/knowzy/webapp:1 --env "PRODUCTSAPI_URL=<<Orders API IP>>" --env "ORDERAPI_URL=<<Orders API IP>>"
```

iv) Expose your webapp and consume your APIs

```bash
kubectl expose deployment webapp --port=80 --type=LoadBalancer
```

Wait for the IP to come back `kubectl get sv` and access it in your browser

```bash
NAME          CLUSTER-IP     EXTERNAL-IP     PORT(S)          AGE
kubernetes    10.0.0.1       <none>          443/TCP          2d
ordersapi     10.0.8.27      13.8.60.236    8080:30213/TCP   3h
productsapi   10.0.148.198   52.232.72.12   80:30175/TCP     5m
webapp   10.0.142.191   54.22.172.17   80:31945/TCP     5m
```

## 3. References

* [Persisting Files in Azure Cloud Shell](https://docs.microsoft.com/en-us/azure/cloud-shell/persisting-shell-storage)
* [Azure resource naming best practices](https://docs.microsoft.com/en-us/azure/architecture/best-practices/naming-conventions)
* [Azure CLI reference](https://docs.microsoft.com/en-us/cli/azure/get-started-with-azure-cli)
* [kubectl overview](https://kubernetes.io/docs/user-guide/kubectl-overview/)

## continue to [next task >> ](416_Integrate.md)
