# Task 4.1.1 - Create a shared DocumentDB to store all data

## 1. Pre-Requisites

* You'll need access to an azure subscription.

### a. Working with Cloud Shell

Azure Cloud Shell comes with the Azure CLI already configured, which makes setup straightforward. We'll be using Azure Cloud Shell throughout, so to get started we need to get Cloud Shell pointed toward the subscription we want to work with.

i) Access Azure Cloud Shell using the `>_` Icon in the top right corner of the portal.

ii) Point to your Azure subscription

    If you have more than one subscription in your azure portal, its a good idea to first check which one your CLI is pointed to, the command for this is:

        az account show

    If it's not pointed to the subscription you want to use, you can re-point your CLI to the correct subscription using:

        az account set --subscription <SubscriptionId>

### b. Create a resource group 

We need to create a resource-group to ring-fence all of our work, we'll start out by storing a couple of variables we're going to use again in our shell. _(Note; many resources within Azure require a unique name, as such we reccomend you use the guidelines in the published [naming conventions](https://docs.microsoft.com/en-us/azure/architecture/best-practices/naming-conventions))_

### c. First store the name you want to use for your resource group:
Resource group names must be globally unique within azure, so make sure its both memorable, and specific to you and your project.

    RESOURCE_GROUP=<unique name>


### d. Then the location we're going to create it in _(for our example you should pick from: 'eastus', 'westeurope' or 'southeastasia')_

    LOCATION=eastus

### e. Create our new resource group within our current subscription:
```bash 
    az group create --name $RESOURCE_GROUP --location $LOCATION
```

## 2. Create and initialise DocumentDB

### a. Store the name of our database:
Like resource group names, DocumentDB names must be globally unique within azure, so again we should select something specific.

    DOCUMENTDB_NAME = <unique database name>

### b. Create the DocumentDb instance
We can now go ahead and use the Azure CLI within the Cloud Shell to create our DocumentDB Instance.

    az documentdb create -g $RESOURCE_GROUP -n $DOCUMENTDB_NAME --locations "EAST US2"=0

This command will take some time to complete. You'll know it's succeeded when the cloud shell console outputs something like this, containing the name you specified in step 1 above in the 'documentEndpoint':

![image of share screen](images/DocDbCreateSuccess.jpg)

### c. Create our collections within the documentDB
Once your db is created, select your new documentdb instance within the portal


## 3. References
[DocumentDB Migration Tool Download](https://www.microsoft.com/en-us/download/details.aspx?id=46436)