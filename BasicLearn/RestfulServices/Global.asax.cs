using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using System.Web.Routing;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace RestfulApp.RestfulServices
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            RegisterRoutes();
        }

        protected void RegisterRoutes()
        {
            //个人服务
            RouteTable.Routes.Add(new ServiceRoute("Person", new WebServiceHostFactory(), typeof(PersonService)));
            //公共服务
            RouteTable.Routes.Add(new ServiceRoute("Common", new CommonServiceHostFactory(), typeof(CommonService)));
        }

        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码

        }

        void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码

        }

        void Session_Start(object sender, EventArgs e)
        {
            // 在新会话启动时运行的代码

        }

        void Session_End(object sender, EventArgs e)
        {

        }

    }
}
