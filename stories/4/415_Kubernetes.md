# Task 4.1.5 - Deploy to Kubernetes on Azure Container Services
## Prerequisites

* Access to Azure subscription.
* Azure Cloud Shell should be [Configured for persistent shell storage](https://github.com/jluk/ACC-Documentation/blob/master/persisting-shell-storage.md).

## Working with Cloud Shell
Azure Cloud Shell comes with the Azure CLI and kubectl already configured, which makes setup straightforward. To get started we need to get Cloud Shell pointed toward the subscription we want to work with.

### 1. Access Azure Cloud Shell using the '>_' Icon in the top right corner of the portal.
### 2. Point to your Azure subscription
        
If you have more than one subscription in your azure portal, its a good idea to first check which one your CLI is pointed to, the command for this is:

    az account show

If it's not pointed to the subscription you want to use, you can re-point your CLI to the correct subscription using:

    az account set --subscription <SubscriptionId>

## Create a resource group 

We need to create a resource group to ring-fence all of our work, we'll start out by storing a couple of variables we're going to use again in our shell. _(Note: many resources within Azure require a unique name, as such we recommend you use the guidelines in the published [naming conventions](https://docs.microsoft.com/en-us/azure/architecture/best-practices/naming-conventions))_

### 1. First store the name you want to use for your resource group:

    RESOURCE_GROUP=<unique name>


### 2. Then the location we're going to create it in _(for our example you should pick from: 'southcentralus', )_

    LOCATION=southcentralus

### 3. Create our new resource group within our current subscription:
```bash 
az group create --name $RESOURCE_GROUP --location $LOCATION
```
## Creating a Kubernetes cluster in Azure Container Service

Now we have a resource group we can create our Kubernetes cluster in Azure Container Service, but first we'll need a few more variables:

### 1. Store a unique DNS Prefix for our cluster, which will also be the unique Kubernetes cluster name:
    
    DNS_PREFIX=<unique cluster name>

### 2. Store the name for the ACS Instance:

    ACS_NAME=<unique ACS instance name>

### 3. Create the cluster using the Azure CLI along with values we stored. 
We'll also add the ```--generate-ssh-keys``` parameter which generates the necessary SSH public and private key files for the deployment if they don't exist already in the default ~/.ssh/ directory. 

_(Note: These keys are needed to grant access to the cluster, and will be stored in the cloudshell storage, which is why Cloud Shell persistent storage is required as a prerequisite, as ending your Cloud Shell session will otherwise lose your key files forever. If you did not set up [persistent shell storage](https://github.com/jluk/ACC-Documentation/blob/master/persisting-shell-storage.md), then now is the time to do so.)_
```bash 
az acs create --orchestrator-type=kubernetes --agent-vm-size Standard_A1 --resource-group $RESOURCE_GROUP --name=$ACS_NAME --dns-prefix=$DNS_PREFIX --generate-ssh-keys
```
### 4. Get the config from Kubernetes into our Cloud Terminal session so that we can send commands to it with kubectl we can do this with the following command:

```bash 
az acs kubernetes get-credentials --resource-group=$RESOURCE_GROUP --name=$ACS_NAME
```

### 5. Check that your kubectl is talking to the cluster.

    kubectl config current-context

## Deploying our container images _(from [Step 4.1.4](./414_Docker.md))_

### 1. Store the name of your Azure Container Registry
This registry was generated in step [4.1.4](./414_Docker.md#acr)

    ACR_NAME=<name of registry that contains your images>

### 2. Deploy our two services from their respective Docker images.

```bash
kubectl run --image=$ACR_NAME.azurecr.io/knowzy/ordersapi:1 --env "DOCDB_CONNSTRING=<your connection string>"
kubectl run --image=$ACR_NAME.azurecr.io/knowzy/productsapi:1 --env "DOCDB_CONNSTRING=<your connection string>"
```

### 3. Deploy our webapp from the Docker image.
```bash
kubectl run --image=$ACR_NAME.azurecr.io/knowzy/shippingwebapp:1  
```

### 4. Expose each of our applications

    kubectl expose deployment ordersapi --port=80
    kubectl expose deployment productsapi --port=80 --type=LoadBalancer
    kubectl expose deployment shippingwebapp --port=80 --type=LoadBalancer

## References
* [Explanation of persistent storage for cloudshell](https://github.com/jluk/ACC-Documentation/blob/master/persisting-shell-storage.md)
* [Azure resource naming best practices](https://docs.microsoft.com/en-us/azure/architecture/best-practices/naming-conventions)
* [Azure CLI reference](https://docs.microsoft.com/en-us/cli/azure/get-started-with-azure-cli)
* [kubectl overview](https://kubernetes.io/docs/user-guide/kubectl-overview/)