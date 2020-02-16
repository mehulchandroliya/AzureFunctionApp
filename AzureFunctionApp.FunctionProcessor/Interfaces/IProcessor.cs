#region Using Namespaces
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
#endregion

namespace AzureFunctionApp.FunctionProcessor.Interfaces
{
    public interface IProcessor
    {
        void Validate();
        void PreRun();
        void Run();
        void PostRun();
    }
}