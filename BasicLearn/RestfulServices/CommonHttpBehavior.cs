using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.ServiceModel.Dispatcher;

namespace RestfulApp.RestfulServices
{
    public class CommonHttpBehavior:WebHttpBehavior
    {
        public CommonHttpBehavior()
        {
            base.AutomaticFormatSelectionEnabled = false;
            base.DefaultOutgoingResponseFormat = WebMessageFormat.Json;
                
        }
        protected override void AddServerErrorHandlers(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(new RestErrorHandler());
        }
    }
}
