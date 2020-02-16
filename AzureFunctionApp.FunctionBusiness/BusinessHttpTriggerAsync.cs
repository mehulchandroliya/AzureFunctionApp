#region Using Namespaces
using System;
using System.IO;
using System.Threading;
using AzureFunctionApp.FunctionProcessor.Http;
using AzureFunctionApp.FunctionProcessor.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
#endregion

namespace AzureFunctionApp.FunctionBusiness
{
    public class BusinessHttpTriggerAsync : IProcessor
    {
        #region Private Members
        private Processor<BusinessHttpTriggerAsync> htppProcessor;
        #endregion

        #region Public Methods
        public BusinessHttpTriggerAsync()
        {
        }
        public BusinessHttpTriggerAsync(Processor<BusinessHttpTriggerAsync> _htppProcessor)
        {
            htppProcessor = _htppProcessor;
        }

        public void PostRun()
        {
            htppProcessor.Logger.LogInformation("Post Run: C# HTTP trigger function processed a request.");
            htppProcessor.Logger.LogInformation("Post Run: C# HTTP trigger function processed a request(30 Sec).");
        }

        public void PreRun()
        {
            htppProcessor.Logger.LogInformation("Pre Run: C# HTTP trigger function processed a request.");
            htppProcessor.Logger.LogInformation("Pre Run: C# HTTP trigger function processed a request(15 Sec).");
        }

        public async void Run()
        {
            htppProcessor.Logger.LogInformation("C# HTTP trigger function processed a request.");
            string name = htppProcessor.HttpRequest.Query["name"];
            string requestBody = await new StreamReader(htppProcessor.HttpRequest.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            htppProcessor.Action = name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
            htppProcessor.Logger.LogInformation("C# HTTP trigger function processed a request(Run Complete).");
        }

        public void Validate()
        {
            htppProcessor.Logger.LogInformation("Validate: C# HTTP trigger function processed a request.");            
            htppProcessor.Logger.LogInformation("Validate: C# HTTP trigger function processed a request(10 Sec).");
        }
        #endregion
    }
}
