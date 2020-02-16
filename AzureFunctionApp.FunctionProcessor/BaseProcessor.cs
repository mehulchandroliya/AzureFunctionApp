#region Using Namespaces
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using AzureFunctionApp.FunctionProcessor.Interfaces;
using Microsoft.Extensions.Logging;
#endregion

namespace AzureFunctionApp.FunctionProcessor
{
    public abstract class BaseProcessor
    {
        #region Private Members
        private IConfigurationRoot config;
        #endregion

        #region Private Methods
        private void LoadConfiguration()
        {
            try
            {
                ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                if (!string.IsNullOrEmpty(ExecutionContext.FunctionAppDirectory))
                {
                    configurationBuilder.SetBasePath(ExecutionContext.FunctionAppDirectory);
                    configurationBuilder.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
                }
                configurationBuilder.AddEnvironmentVariables();
                config = configurationBuilder.Build();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Public Members
        public ExecutionContext ExecutionContext { get; private set; }
        public ILogger Logger { get; private set; }
        protected object Config<T>(string key) => config.GetValue<T>(key);
        #endregion

        #region Public Methods
        protected BaseProcessor(ExecutionContext executionContext, ILogger logger)
        {
            ExecutionContext = executionContext;
            Logger = logger;
            LoadConfiguration();
        }
        #endregion
    }
}