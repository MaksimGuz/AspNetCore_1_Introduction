### Azure Kubernetes Service (AKS)
https://docs.microsoft.com/en-us/azure/aks/kubernetes-walkthrough-portal

## To open your Kubernetes dashboard, complete the following steps:
# 1. If you do not already have kubectl installed in your CLI, run the following command:
az aks install-cli
# 2. Get the credentials for your cluster by running the following command:
az aks get-credentials --resource-group aspnetcorek8s --name aspnetcorecluster
# 3. Open the Kubernetes dashboard by running the following command:
az aks browse --resource-group aspnetcorek8s --name aspnetcorecluster

# deploy to AKS
kubectl apply -f k8.yml

### Google Kubernetes Engine (GKE)
https://cloud.google.com/kubernetes-engine/docs/quickstart

### Amazon Elastic Container Service for Kubernetes (Amazon EKS)
https://docs.aws.amazon.com/eks/latest/userguide/getting-started.html




