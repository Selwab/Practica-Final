# Practica-Final

This is a simple web API that manages clients data using CRUD operations and a backing service.

##Group members
[@AleDiazT] https://github.com/AleDiazT 
[@SebastianItamari] https://github.com/SebastianItamari
[@Selwab] https://github.com/Selwab 

##Configuration
This web API has different configurations for each environment, such as DEV, QA and UAT. Each environment is configured from the appsettings.(environment).json file, where you can set specific configuration values for that environment.

1. The "Title" attribute allows you to personalize the title in SwaggerUI.
2. The "BackingService" attribute allows you to add the direction to any backing service.
3. The "PathClients" attribute let you define the file path where the information of your clients will be stored.
4. In the case of Serilog, you can configure two attributes: "path", which allows you to specify the file where logs will be stored, and "rollingInterval", which sets the frequency at which these files will be created.

Here is an example of the configuration file:

{
    "Title": "Practica en QA",
    "PathClients": "c:/websites/crm/clients.json",
    "BackingService": "https://random-data-api.com/api/users/random_user?size=10",
    "Serilog": {
        "WriteTo": [
          {
            "Name": "File",
            "Args": {
              "path": "logs/log-.txt",
              "rollingInterval": "Day",
              "retainedFileCountLimit": 7
            }
          }
        ],
        "MinimumLevel": {
          "Default": "Warning",
          "Override": {
            "Microsoft": "Information",
            "System": "Information"
          }
        }
      }
}

