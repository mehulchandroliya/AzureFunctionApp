#region Using Namespaces
using System;
using Xunit;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AzureFunctionApp.FunctionProcessor.Interfaces;
using AzureFunctionApp.FunctionProcessor.Http;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
#endregion

namespace AzureFunctionApp.FunctionBusiness.Tests
{
    public class BusinessHttpTriggerTest
    {
        #region Private Members
        public Mock<ILogger> MockLogger;
        public Mock<ExecutionContext> MockExecutionContext;
        public Processor<BusinessHttpTriggerAsync> HttpProcessor;
        Mock<HttpRequest> MockHttpRequest;
        Mock<HttpRequest> MockHttpRequest_NoValue;
        #endregion

        #region Private Methods
        private void SteupTest()
        {
            MockHttpRequest = new Mock<HttpRequest>();
            var query = new Dictionary<String, StringValues>();
            var body = "";
            query.TryAdd("name", "Mehul");
            MockHttpRequest.Setup(req => req.Query).Returns(new QueryCollection(query));
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(body);
            writer.Flush();
            stream.Position = 0;
            MockHttpRequest.Setup(req => req.Body).Returns(stream);

            MockHttpRequest_NoValue = new Mock<HttpRequest>();
            MockHttpRequest_NoValue.Setup(req => req.Query).Returns(new QueryCollection(new Dictionary<String, StringValues>()));
            stream = new MemoryStream();
            writer = new StreamWriter(stream);
            writer.Write("");
            writer.Flush();
            stream.Position = 0;
            MockHttpRequest_NoValue.Setup(req => req.Body).Returns(stream);

            MockLogger = new Mock<ILogger>();
            MockExecutionContext = new Mock<ExecutionContext>();
            HttpProcessor = new Processor<BusinessHttpTriggerAsync>(MockExecutionContext.Object, MockLogger.Object);
        }
        #endregion

        public BusinessHttpTriggerTest()
        {
            SteupTest();
        }

        [Fact]
        public void Function_Name_Value()
        {
            HttpProcessor.Run(MockHttpRequest.Object);
            Assert.Equal(new OkObjectResult("Hello, Mehul").Value, ((OkObjectResult)HttpProcessor.Action).Value);
        }

        [Fact]
        public void Function_Name_Value_Body()
        {
            MockHttpRequest = new Mock<HttpRequest>();
            var query = new Dictionary<String, StringValues>();
            var body = @"{ 'name' : 'Mehul' }";
            MockHttpRequest.Setup(req => req.Query).Returns(new QueryCollection(query));
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(body);
            writer.Flush();
            stream.Position = 0;
            MockHttpRequest.Setup(req => req.Body).Returns(stream);

            HttpProcessor.Run(MockHttpRequest.Object);
            Assert.Equal(new OkObjectResult("Hello, Mehul").Value, ((OkObjectResult)HttpProcessor.Action).Value);
        }

        [Fact]
        public void Function_Name_NoValue()
        {
            HttpProcessor.Run(MockHttpRequest_NoValue.Object);
            Assert.Equal(new BadRequestObjectResult("Please pass a name on the query string or in the request body").Value, ((Microsoft.AspNetCore.Mvc.BadRequestObjectResult)HttpProcessor.Action).Value);
        }
    }
}
