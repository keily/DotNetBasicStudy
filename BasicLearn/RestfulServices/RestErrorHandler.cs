using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.Net;
using System.Xml;

namespace RestfulApp.RestfulServices
{
    public class RestErrorHandler : IErrorHandler
    {
        private readonly IErrorHandler _restErrorHandler;

        public RestErrorHandler()
        {
        }

        public RestErrorHandler(IErrorHandler restErrorHandler)
        {
            this._restErrorHandler = restErrorHandler;
        }

        public bool HandleError(Exception error)
        {
            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (error != null)
            {
                fault = Message.CreateMessage(version, null, new ValidationErrorBodyWriter(error));
                var prop = new HttpResponseMessageProperty();
                prop.StatusCode = HttpStatusCode.BadRequest;
                prop.Headers[HttpResponseHeader.ContentType] = "application/json; charset=UTF-8";

                fault.Properties.Add(HttpResponseMessageProperty.Name, prop);
                fault.Properties.Add(WebBodyFormatMessageProperty.Name, new WebBodyFormatMessageProperty(WebContentFormat.Json));
            }
            else
            {
                this._restErrorHandler.ProvideFault(error, version, ref fault);
            }
        }

        class ValidationErrorBodyWriter : BodyWriter
        {
            private Exception exception;
            public ValidationErrorBodyWriter(Exception exception)
                : base(true)
            {
                this.exception = exception;

            }

            protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
            {
                writer.WriteStartElement("root");
                writer.WriteAttributeString("type", "object");

                writer.WriteStartElement("message");
                writer.WriteAttributeString("type", "string");
                writer.WriteString(this.exception.Message);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }
    }

}
