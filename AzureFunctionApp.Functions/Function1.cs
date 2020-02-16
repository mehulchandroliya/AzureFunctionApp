using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AzureFunctionApp.FunctionBusiness;
using AzureFunctionApp.FunctionProcessor.Http;

namespace AzureFunctionApp.Functions
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ExecutionContext context, ILogger log)
        {
            var businessHttpTrigger = new Processor<BusinessHttpTrigger>(context, log);
            businessHttpTrigger.Run(req);
            return businessHttpTrigger.Action;
        }
    }
}
