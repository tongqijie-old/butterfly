using System;
using Petecat.DynamicProxy;
using Petecat.Logging;
using Butterfly.ServiceModel;
using Petecat.DependencyInjection.Attribute;

namespace Butterfly.ArticleManagement
{
    [DependencyInjectable(Inference = typeof(IHandlingInterceptor), Singleton = true)]
    public class HandlingInterceptor : IHandlingInterceptor
    {
        private IFileLogger _FileLogger;

        public HandlingInterceptor(IFileLogger fileLogger)
        {
            _FileLogger = fileLogger;            
        }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Process();
            }
            catch (Exception e)
            {
                _FileLogger.LogEvent("HandlingInterceptor", Severity.Error, "failed to execute article handler.", e);

                invocation.ReturnValue = new ApiResponse() { Error = true, Message = e.Message };
            }
        }
    }
}
