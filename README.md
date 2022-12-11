# **CentralPlay**

## **Three-Tier Architecture**

- **App Layer:** CentralPlay.Backend.FunctionApp
- **Business Layer:** CentralPlay.Backend.Service
- **Repository Layer:** CentralPlay.Backend.Repository

## **Settings**

Settings for running Azure functions in Azure og locally

```
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "<Storage account for web jobs connection string>",
    "ModelsEndpointUrl": "<Storage account endpoint url>",
    "TestUserId": "663461be-7dea-4fa4-8944-f705a7b1e38d"
  },
  "Connections": {
    "ModelBlobStorage": {
      "ConnectionString": "<Storage account conenction string>",
      "ContainerName": "<The name of the container for the models in the storage account>"
    },
    "CosmosDB": {
      "EndpointUrl": "<Endpoint url for CosmosDB>",
      "PrimaryKey": "<Primary ket for CosmosDB>",
      "DatabaseName": "database",
      "Containers": [
        {
          "Name": "users",
          "PartitionKey": "/id"
        },
        {
          "Name": "models",
          "PartitionKey": "/userId"
        }
      ]
    }
  }
}
```