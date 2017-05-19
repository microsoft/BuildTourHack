# Task 4.1.5 - Deploy to Kubernetes on Azure Container Services

## 1. Prerequisites

* Access to Azure subscription.
* Azure Cloud Shell should be [Configured for persistent shell storage](https://github.com/jluk/ACC-Documentation/blob/master/persisting-shell-storage.md).
* Azure Cloud shell should be pointed toward the correct subscription, more details in [Step 4.1.1](./411_DocumentDB.md)
* You'll also need the name of the resource group created in [Step 4.1.1](./411_DocumentDB.md)

### a. Working with Cloud Shell
We're going to be working within the Azure Cloud Shell, as it not only already has the AzureCLI configured, but also 

## 2. Creating and deploiying to a Kubernetes cluster in Azure Container Service

### a. 
but first we'll need a few more variables:

i) Store the name of the resource group created in [Step 4.1.1](./411_DocumentDB.md) if this not the same Azure Cloud Shell session.

        RESOURCE_GROUP=<resource group name>

ii) Store a unique DNS Prefix for your cluster, which will also be the unique Kubernetes cluster name:

        DNS_PREFIX=<unique cluster name>
    
iii) Store the name for the ACS Instance:

    ACS_NAME=<unique ACS instance name>
    
iv) Create the cluster using the Azure CLI along with values we stored.

    We'll also add the `--generate-ssh-keys` parameter which generates the necessary SSH public and private key files for the deployment if they don't exist already in the default `~/.ssh/` directory.

    (Note: These keys are needed to grant access to the cluster, and will be stored in the cloudshell storage, which is why Cloud Shell persistent storage is required as a prerequisite, as ending your Cloud Shell session will otherwise lose your key files forever. If you did not set up [persistent shell storage](https://github.com/jluk/ACC-Documentation/blob/master/persisting-shell-storage.md), then now is the time to do so.)

    ```bash
    az acs create --orchestrator-type=kubernetes --agent-vm-size Standard_A1 --resource-group $RESOURCE_GROUP --name=$ACS_NAME --dns-prefix=$DNS_PREFIX --generate-ssh-keys
    ```

v) Get the config from Kubernetes into your Cloud Terminal session so that we can send commands to it with `kubectl` we can do this with the following command:

    ```bash
    az acs kubernetes get-credentials --resource-group=$RESOURCE_GROUP --name=$ACS_NAME
    ```

vi) Verify that `kubectl` is talking to the cluster.

    kubectl cluster-info

### b. Deploying your container images _(from [Step 4.1.4](./414_Docker.md))_

i) Store the name of your Azure Container Registry

    This registry was generated in step [4.1.4](./414_Docker.md#acr), and is where the container-images containing the apps and services we want to deploy should be located.

    ```bash
    ACR_NAME=<name of registry that contains your images>
    ```

ii) Deploy your two services from their respective Docker images.

    We can deploy by telling kubernetes to get the docker images and deploy them, using the run command in `kubectl`, we're passing the `--env` parameter, to tell the container images the connection string for your data-store, so the running services once deployed know how to connect to the DocumentDB.

    ```bash
    kubectl run ordersapi --image=$ACR_NAME.azurecr.io/knowzy/ordersapi:1 --env "COSMOSDB_ENDPOINT=https://<< your cosmosdb name>>.documents.azure.com:443/" --env "COSMOSDB_KEY=<your Cosmos DB key string>"
    kubectl run productsapi --image=$ACR_NAME.azurecr.io/knowzy/productsapi:1 --env "COSMOSDB_ENDPOINT=https://<< your cosmosdb name>>.documents.azure.com:443/" --env "COSMOSDB_KEY=<your Cosmos DB key string>"
    ```

iii) Expose your services

    ```bash
    kubectl expose deployment ordersapi --port=80 --type=LoadBalancer
    kubectl expose deployment productsapi --port=80 --type=LoadBalancer
    ```

    Run the following command:

    ```bash
    kubectl get svc
    ```

    You should see the following output.

    ```bash
    NAME         CLUSTER-IP   EXTERNAL-IP   PORT(S)   AGE
    kubernetes   10.0.0.1     <none>        443/TCP   2d
    ordersapi   10.0.8.27   <pending>   80:30213/TCP   3h
    productsapi   10.0.148.198   <pending>   80:30175/TCP   19s
    ```

    Now, we wait a couple of minutes and re run the previous command `kubectl get sv`.

    ```bash
    NAME          CLUSTER-IP     EXTERNAL-IP     PORT(S)          AGE
    kubernetes    10.0.0.1       <none>          443/TCP          2d
    ordersapi     10.0.8.27      13.81.60.236    8080:30213/TCP   3h
    productsapi   10.0.148.198   52.232.72.172   80:30175/TCP     5m
    ```

    You can test your deployment as soon as the `<pending>` external IP is provided. Simply go into your browser to the url provided `http://<<External IP>>/api/values`.


iii) Deploy your webapp from the Docker image.

    We deploy the same way as above, but we don't need the database parameter, as the front end web application should have no direct access to the DocumentDB. Instead we should provide the url to the api services

    ```bash
    kubectl run webapp --image=$ACR_NAME.azurecr.io/knowzy/webapp:1 --env "PRODUCTS_API=<<Orders API IP>>" --env "ORDERS_API=<<Orders API IP>>"
    ```

iv) Expose your webapp and consume your APIs


## 3. References

* [Explanation of persistent storage for cloudshell](https://github.com/jluk/ACC-Documentation/blob/master/persisting-shell-storage.md)
* [Azure resource naming best practices](https://docs.microsoft.com/en-us/azure/architecture/best-practices/naming-conventions)
* [Azure CLI reference](https://docs.microsoft.com/en-us/cli/azure/get-started-with-azure-cli)
* [kubectl overview](https://kubernetes.io/docs/user-guide/kubectl-overview/)