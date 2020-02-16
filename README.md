Create Common Azure Function App using .Net Core and Visual Studio Code

1. Create Folder <Folder Name> (Open Folder  in Visual Sudio)
2. Create Solution - dotnet new sln (This will create empty solution)
3. Create Folder for Function Main App
4. Install Azure Function Project and Item Templates
5. Create Azure Function Project .Net Core Project - dotnet new func
6. Create Azure Function - dotnet new http -name <FunctionName> (Create Function for Http Trigger)

=====================================================================
1. Install Azure Function Project templates
=====================================================================

dotnet new --install Microsoft.Azure.WebJobs.ProjectTemplates::2.0.10369
dotnet new --install Microsoft.Azure.WebJobs.ItemTemplates::2.0.10369


=====================================================================
2. Download Azure Function Project templates
=====================================================================
https://github.com/Azure/azure-functions-templates/wiki/Using-the-templates-directly-via-dotnet-new

