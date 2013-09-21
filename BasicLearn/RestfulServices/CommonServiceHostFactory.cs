using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel.Activation;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Description;

namespace RestfulApp.RestfulServices
{
    /// <summary>
    /// 在可动态创建主机实例以响应传入消息的托管宿主环境中提供 System.ServiceModel.Web.WebServiceHost 的实例的工厂。
    /// </summary>
    public class CommonServiceHostFactory : ServiceHostFactory
    {
        //初始化 System.ServiceModel.Activation.WebServiceHostFactory 类的新实例。
        public CommonServiceHostFactory(){
            
        }
        /// <summary>
        /// 使用指定基址创建指定的 System.ServiceModel.Web.WebServiceHost 派生类的实例。
        /// </summary>
        /// <param name="serviceType">要创建的服务主机的类型</param>
        /// <param name="baseAddresses">该服务的基址的数组</param>
        /// <returns>System.ServiceModel.ServiceHost 派生类的实例</returns>
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            WebServiceHost host = new WebServiceHost(serviceType, baseAddresses);
            WebHttpBinding binding = new WebHttpBinding();
            binding.ReaderQuotas.MaxStringContentLength = 2147483647;
            ServiceEndpoint endpoint = host.AddServiceEndpoint(serviceType, binding, "");
            endpoint.Behaviors.Add(new CommonHttpBehavior());
            return host;
        }
    }
}
