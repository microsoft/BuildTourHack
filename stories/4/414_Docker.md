# Task 4.1.4 - Create Docker images

## Prerequisites 

* [Docker for Windows](https://docs.docker.com/docker-for-windows/install/) (Stable channel)

### Configuring Docker for Windows

On Windows, you'll need to share your drive with Docker in order to build images.

1. Open up Docker settings by right-clicking the Docker icon in the taskbar, and choosing Settings
2. Go to the `Shared Drives` tab and share the C drive ![image of share screen](images/docker-sharedrive.png)

## 1. Building Images

To build a Docker images, we need to create a `Dockerfile` for each of our 3 apps. In the root of each app, create a new file named `Dockerfile` and update with the following contents. Note, in some editors like Visual Studio 2017, you may need to create a new text file, then remove the .txt extension.

### OrdersApi
```Dockerfile
# Use the aspnetcore image as a base
FROM microsoft/aspnetcore:1.1

# Use /app inside the created container to hold our files
WORKDIR /app

# Copy our app into the current folder (/app)
# Note we used the path from the previous step
COPY ./bin/Release/netcoreapp1.1/publish .

# Set an environment variable to listen on all addresses using port 80
ENV ASPNETCORE_URLS http://+:80

# Expose port 80 from our created container
EXPOSE 80

# Run our app using "dotnet ordersapi.dll"
ENTRYPOINT ["dotnet", "ordersapi.dll"]
```

### ProductsApi
```Dockerfile
# Use the aspnetcore image as a base
FROM microsoft/aspnetcore:1.1

# Use /app inside the created container to hold our files
WORKDIR /app

# Copy our app into the current folder (/app)
# Note we used the path from the previous step
COPY ./bin/Release/netcoreapp1.1/publish .

# Set an environment variable to listen on all addresses using port 80
ENV ASPNETCORE_URLS http://+:80

# Expose port 80 from our created container
EXPOSE 80

# Run our app using "dotnet productsapi.dll"
ENTRYPOINT ["dotnet", "productsapi.dll"]
```

### Shipping App
```Dockerfile
# Use the nginx image as a base
FROM nginx:1.13.0

# Copy the site files to the nginx html folder
COPY . /usr/share/nginx/html
```

In a terminal, let's build our images, giving them specific names and tags:

```bash
docker build -t ordersapi:1 ./ordersapi
docker build -t productsapi:1 ./productsapi
docker build -t shippingwebapp:1 ./shippingwebapp
```

An explanation of what's going on here:

|Argument|Description|
|---|---|
| `build` | tells Docker to build a new image |
| `-t ordersapi:1` | tell Docker the image has the name `ordersapi` and the tag `1` <br/> Note: in Docker, tags are used as version numbers |
| `./ordersapi` | where to find the Dockerfile to be built |

### 2. Running locally on Docker

You're ready to run your apps on your local Docker host. In a terminal:

```bash
docker run -d --name ordersapi -p 8080:80 ordersapi:1
docker run -d --name productsapi -p 8081:80 productsapi:1
docker run -d --name shippingapp -p 8082:80 shippingwebapp:1
```

Several interesting things are going on here. A quick breakdown:

|Argument|Description|
|---|---|
| `run` | tells Docker to run a new container |
| `-d` | run the container in the background |
| `--name ordersapi` | give the container a friendly name so we can find it easily later |
| `-p 8080:80` | map the port 8080 on the host (your computer) to port 80 inside the container |
| `ordersapi:1` | the image to be run is `ordersapi`, with the tag `11 |

Now that your app is running in a container on your local Docker host, you can see it by running the following in a terminal:

```bash
docker ps
```

Note the port mappings (ex: from `0.0.0.0:8080->80/tcp`). This is great! Without Docker, we'd only be able to launch one app at a time, or we'd have to figure out how to share port 80 across several apps, making them less independent. Since we've mapped host ports to containers, we're now able to interact with them by navigating to these urls:

* Orders API - [http://localhost:8080/api/values/5](http://localhost:8080/api/values/5)
* Products API - [http://localhost:8081/api/values/5](http://localhost:8081/api/values/5)
* Shipping App - [http://localhost:8082](http://localhost:8082)

Docker containers come and go frequently. We'll stop our currently running containers to free up some computing resources and ports on our host for future work.  In a terminal, run the following:

```bash
docker stop ordersapi productsapi shippingapp
```

Your containers are now stopped. To verify, run:

```bash
docker ps
```

Note that the containers still exist, they're just not currently running. You can see them by listing all containers by running:

```bash
docker ps -a
```

To restart your containers, run:

```bash
docker start ordersapi productsapi shippingapp
```

You'll see that your apps are back up and running, and their endpoints are available once again

Finally, destroy your local containers by running:

```bash
docker stop ordersapi productsapi shippingapp
docker rm ordersapi productsapi shippingapp
```

## Deploying to a Docker Registry

So far, we've developed a few applications, packaged them in Docker images, and have tested them in a production-like environment by running them inside a container on our local computer. To get our apps running in the cloud, our next step is to push our images to a Docker registry.

We're going to create a private registry to hold our images, using [Azure Container Registry](https://docs.microsoft.com/en-us/azure/container-registry/container-registry-intro). We'll do this for a few reasons. First, we want to make sure our images aren't available to just anyone on the Internet. We want to be able to control access to our images! We also want our images to be available in the same region as our compute resources for quick deployments.

### 1. Create an Azure Container Registry

Using the Azure Cloud Shell, set up some variables for your registry. These will be specific to you:

```bash
RESOURCE_GROUP=myResourceGroup
ACR_NAME=buildtourregistry
```

Next, we'll fire off a command to create your container registry. 

Note the use of the `--admin-enabled` switch, which enables a simple username/password logon. In a production environment, you'll likely want to disable this option and create a Service Principal, but this will get us up and running for now.

```bash
az acr create --resource-group $RESOURCE_GROUP --name $ACR_NAME --sku Basic --admin-enabled
```

You're all set up with a registry, with the full name of `$ACR_NAME.azurecr.io`.

Execute the following command to get the admin password for your newly created registry:

```bash
az acr credential show -n $ACR_NAME -g $RESOURCE_GROUP
```

### 2. Connect to Your Registry

Hop back over to a local terminal. Let's login to our registry so we can push some images. Run the following command, using either the `password` or `password2` result from the previous command as your password:

```bash
docker login -u $ACR_NAME -p <your password> $ACR_NAME.azurecr.io
```

### 3. Push an Image

We've already got some tagged images, and now we need to tell Docker that they're associated with our newly created registry. We'll specify the `knowzy` namespace before we push them to keep these apps grouped together. This is as simple as adding another tag for our existing images, using the following commands.

```bash
docker tag ordersapi:1 $ACR_NAME.azurecr.io/knowzy/ordersapi:1
docker tag productsapi:1 $ACR_NAME.azurecr.io/knowzy/productsapi:1
docker tag shippingwebapp:1 $ACR_NAME.azurecr.io/knowzy/shippingwebapp:1
```

To push images to your registry, use the following commands:

```bash
docker push $ACR_NAME.azurecr.io/knowzy/ordersapi:1
docker push $ACR_NAME.azurecr.io/knowzy/productsapi:1
docker push $ACR_NAME.azurecr.io/knowzy/shippingwebapp:1
```

When finished, you can verify that your repositories and tags have been created by using the following commands:

```bash
az acr repository list -n $ACR_NAME
az acr repository show-tags -n $ACR_NAME --repository knowzy/ordersapi
```

## References

* [Troubleshooting guide](499_Troubleshooting.md)
* [Dockerfile reference](https://docs.docker.com/engine/reference/builder/)
* [Docker CLI reference](https://docs.docker.com/engine/reference/commandline/cli/)
* [Azure CLI reference](https://docs.microsoft.com/en-us/cli/azure/get-started-with-azure-cli)
* [Azure Container Registry reference](https://docs.microsoft.com/en-us/azure/container-registry/container-registry-intro)