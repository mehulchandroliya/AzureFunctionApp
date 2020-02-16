#region Using Namespaces
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AzureFunctionApp.FunctionProcessor.Interfaces;
using System;
#endregion

namespace AzureFunctionApp.FunctionProcessor.Http
{
    public class Processor<T> : BaseProcessor where T : IProcessor, new()
    {
        #region Private Members
        #endregion

        #region Private Methods
        #endregion

        #region Public Members
        public IProcessor BusinessProcessor { get; set; }
        public IActionResult Action { get; set; }
        public HttpRequest HttpRequest { get; protected set; }
        #endregion

        #region Public Public Methods
        public Processor(ExecutionContext executionContext, ILogger logger) : base(executionContext, logger)
        {
            BusinessProcessor = (T)Activator.CreateInstance(typeof(T), this);
        }
        public void Run(HttpRequest httpRequest)
        {
            try
            {
                HttpRequest = httpRequest;
                BusinessProcessor.Validate();
                BusinessProcessor.PreRun();
                BusinessProcessor.Run();
                BusinessProcessor.PostRun();
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public async Task<IActionResult> RunAsync(HttpRequest httpRequest)
        {
            try
            {
                HttpRequest = httpRequest;
                await Task.Run(() => BusinessProcessor.Validate());
                await Task.Run(() => BusinessProcessor.PreRun());
                await Task.Run(() => BusinessProcessor.Run());
                await Task.Run(() => BusinessProcessor.PostRun());
                return Action;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        #endregion
    }
}