using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using RestfulApp.Bussiness;

namespace RestfulApp.RestfulServices
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    [JavascriptCallbackBehavior(UrlParameterName = "callback")]
    public class CommonService
    {
        protected Bussiness.Common.CommonBLL _commonBLL = new Bussiness.Common.CommonBLL();

        [WebGet(UriTemplate = "persondetail")]
        public IList<PersonEntity> GetPerson()
        {
            return _commonBLL.GetPerson();
        }

        [WebGet(UriTemplate = "persondetail/{personId}")]
        public PersonEntity GetPersonById(string personId)
        {
            return _commonBLL.GetPersonById(long.Parse(personId));
        }
        [WebInvoke(UriTemplate = "UpdatePerson", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public bool UpdatePerson(int personId, string name)
        {
            return _commonBLL.UpdatePersonName(personId, name);
        }
        [WebInvoke(UriTemplate = "InsertPerson", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public bool InsertPerson(PersonEntity entity)
        {
            if (entity == null)
                throw new Exception("entity is null object");
            return _commonBLL.InsertPerson(entity);
        }
    }
}
