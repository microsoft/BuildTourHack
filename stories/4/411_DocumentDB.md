# Task 4.1.1 - Create a shared DocumentDB to store all data

## Pre-Requisites

* Access to azure subscription.
* Azure Cloud shell should be [configured for persistent shell storage](https://github.com/jluk/ACC-Documentation/blob/master/persisting-shell-storage.md).

## Create a resource group 

We need to create a resource-group to ring-fence all of our work, we'll start out by storing a couple of variables we're going to use again in our shell. _(Note; many resources within Azure require a unique name, as such we reccomend you use the guidelines in the published [naming conventions](https://docs.microsoft.com/en-us/azure/architecture/best-practices/naming-conventions))_

### 1. First store the name you want to use for your resource group:

    RESOURCE_GROUP=<unique name>


### 2. Then the location we're going to create it in _(for our example you should pick from: 'southcentralus', )_

    LOCATION=southcentralus

### 3. Create our new resource group within our current subscription:
```bash 
    az group create --name $RESOURCE_GROUP --location $LOCATION
```

## Create and initialise DocumentDB

### 1. Create the document database

    az documentdb create -g $RESOURCE_GROUP -n buildtour-test --locations "West US2"=0


## References
[DocumentDB Migration Tool Download](https://www.microsoft.com/en-us/download/details.aspx?id=46436)