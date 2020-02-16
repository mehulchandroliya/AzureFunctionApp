using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AzureFunctionApp.FunctionBusiness;
using AzureFunctionApp.FunctionProcessor.Http;

namespace AzureFunctionApp.Functions
{
    public static class Function2
    {
        [FunctionName("Function2")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ExecutionContext context, ILogger log)
        {
            var businessHttpTrigger = new Processor<BusinessHttpTriggerAsync>(context, log);
            await businessHttpTrigger.RunAsync(req);
            return businessHttpTrigger.Action;
        }
    }
}
