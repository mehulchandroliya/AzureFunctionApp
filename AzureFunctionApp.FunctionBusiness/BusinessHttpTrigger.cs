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
    public class BusinessHttpTrigger : IProcessor
    {
        #region Private Members
        private Processor<BusinessHttpTrigger> htppProcessor;
        #endregion

        #region Public Methods
        public BusinessHttpTrigger()
        {
        }
        public BusinessHttpTrigger(Processor<BusinessHttpTrigger> _htppProcessor)
        {
            htppProcessor = _htppProcessor;
        }

        public void PostRun()
        {
            htppProcessor.Logger.LogInformation("Post Run: C# HTTP trigger function processed a request.");
            for (var i = 0; i < 10000; i++)
            {
            }
            Thread.Sleep(30000);
            htppProcessor.Logger.LogInformation("Post Run: C# HTTP trigger function processed a request(30 Sec).");
        }

        public void PreRun()
        {
            htppProcessor.Logger.LogInformation("Pre Run: C# HTTP trigger function processed a request.");
            for (var i = 0; i < 100000; i++)
            {
            }
            Thread.Sleep(15000);
            htppProcessor.Logger.LogInformation("Pre Run: C# HTTP trigger function processed a request(15 Sec).");
        }

        public void Run()
        {
            htppProcessor.Logger.LogInformation("C# HTTP trigger function processed a request.");
            string name = htppProcessor.HttpRequest.Query["name"];
            string requestBody = new StreamReader(htppProcessor.HttpRequest.Body).ReadToEnd();
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
            for (var i = 0; i < 1000000; i++)
            {
            }
            Thread.Sleep(10000);
            htppProcessor.Logger.LogInformation("Validate: C# HTTP trigger function processed a request(10 Sec).");
        }
        #endregion
    }
}
