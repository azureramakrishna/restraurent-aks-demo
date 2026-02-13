# AKS Deployment Flow

```
┌─────────────────────────────────────────────────────────────────────┐
│                         GITHUB ACTIONS WORKFLOW                      │
└─────────────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────────────┐
│  TRIGGER: Push to main branch or manual workflow_dispatch           │
└─────────────────────────────────────────────────────────────────────┘
                                  │
                                  ▼
┌─────────────────────────────────────────────────────────────────────┐
│                          BUILD JOB                                   │
├─────────────────────────────────────────────────────────────────────┤
│  1. Checkout code                                                    │
│     └─> Get source code from repository                             │
│                                                                       │
│  2. Set image tag                                                    │
│     └─> Tag = Git commit SHA (e.g., abc123def)                      │
│                                                                       │
│  3. Azure Login                                                      │
│     └─> Authenticate using Service Principal                        │
│         (AZURE_CREDENTIALS secret)                                   │
│                                                                       │
│  4. Login to ACR                                                     │
│     └─> az acr login --name saanvikit                               │
│                                                                       │
│  5. Build Docker Image                                               │
│     └─> docker build -t saanvikit.azurecr.io/restaurant-app:SHA     │
│         • Multi-stage build (SDK → Runtime)                          │
│         • Optimized .NET 8 container                                 │
│                                                                       │
│  6. Push to ACR                                                      │
│     └─> docker push saanvikit.azurecr.io/restaurant-app:SHA         │
│                                                                       │
│  Output: image-tag (passed to deploy job)                            │
└─────────────────────────────────────────────────────────────────────┘
                                  │
                                  ▼
┌─────────────────────────────────────────────────────────────────────┐
│                         DEPLOY JOB                                   │
├─────────────────────────────────────────────────────────────────────┤
│  1. Checkout code                                                    │
│     └─> Get k8s manifests                                           │
│                                                                       │
│  2. Azure Login                                                      │
│     └─> Authenticate using Service Principal                        │
│                                                                       │
│  3. Set AKS context                                                  │
│     └─> az aks get-credentials --name saanvikit-aks                 │
│     └─> kubelogin convert-kubeconfig -l azurecli                    │
│         (Azure AD authentication)                                    │
│                                                                       │
│  4. Update manifest                                                  │
│     └─> Replace IMAGE_TAG with actual SHA                           │
│         saanvikit.azurecr.io/restaurant-app:IMAGE_TAG               │
│         becomes                                                      │
│         saanvikit.azurecr.io/restaurant-app:abc123def               │
│                                                                       │
│  5. Deploy to AKS                                                    │
│     └─> kubectl apply -f k8s/deployment.yaml                        │
│         • Creates/Updates Deployment (2 replicas)                   │
│         • Creates/Updates LoadBalancer Service                      │
└─────────────────────────────────────────────────────────────────────┘
                                  │
                                  ▼
┌─────────────────────────────────────────────────────────────────────┐
│                      AZURE CONTAINER REGISTRY                        │
├─────────────────────────────────────────────────────────────────────┤
│  Registry: saanvikit.azurecr.io                                     │
│  Image: restaurant-app:abc123def                                    │
│                                                                       │
│  • Stores Docker images                                              │
│  • Integrated with AKS via attach-acr                                │
└─────────────────────────────────────────────────────────────────────┘
                                  │
                                  ▼
┌─────────────────────────────────────────────────────────────────────┐
│                   AZURE KUBERNETES SERVICE (AKS)                     │
├─────────────────────────────────────────────────────────────────────┤
│  Cluster: saanvikit-aks                                             │
│                                                                       │
│  ┌───────────────────────────────────────────────────────┐          │
│  │  Deployment: restaurant-app                           │          │
│  │  ┌─────────────────┐      ┌─────────────────┐        │          │
│  │  │   Pod 1         │      │   Pod 2         │        │          │
│  │  │  Container:     │      │  Container:     │        │          │
│  │  │  restaurant-app │      │  restaurant-app │        │          │
│  │  │  Port: 8080     │      │  Port: 8080     │        │          │
│  │  └─────────────────┘      └─────────────────┘        │          │
│  └───────────────────────────────────────────────────────┘          │
│                          │                                           │
│                          ▼                                           │
│  ┌───────────────────────────────────────────────────────┐          │
│  │  Service: restaurant-app-service                      │          │
│  │  Type: LoadBalancer                                   │          │
│  │  Port: 80 → TargetPort: 8080                          │          │
│  └───────────────────────────────────────────────────────┘          │
│                          │                                           │
└──────────────────────────┼───────────────────────────────────────────┘
                           │
                           ▼
                  ┌─────────────────┐
                  │  External IP    │
                  │  (Public Access)│
                  └─────────────────┘
                           │
                           ▼
                    ┌─────────────┐
                    │   Users     │
                    └─────────────┘
```

## Key Components:

**GitHub Secrets Required:**
- `AZURE_CREDENTIALS` - Service Principal JSON
- `AKS_RESOURCE_GROUP` - Resource group name

**Azure Resources:**
- ACR: `saanvikit.azurecr.io`
- AKS: `saanvikit-aks`
- Image: `restaurant-app`

**Deployment Strategy:**
- Rolling update (default)
- 2 replicas for high availability
- LoadBalancer exposes app on port 80
- Image tagged with Git commit SHA for traceability
