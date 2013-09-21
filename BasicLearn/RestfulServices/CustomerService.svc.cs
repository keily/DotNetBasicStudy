using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.ServiceModel.Activation;
using RestfulApp.Bussiness;

namespace RestfulApp.RestfulServices
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“CustomerService”。
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CustomerService : Contracts.ICustomerService
    {
        protected Bussiness.Person.PersonBLL _personBLL = new Bussiness.Person.PersonBLL();
        
        #region ICustomerService 成员

        public Bussiness.PersonEntity GetPersonsById(string id)
        {
            return _personBLL.GetPersonById(long.Parse(id));
        }

        public IList<Bussiness.PersonEntity> GetPersons()
        {
            return _personBLL.GetPerson();
        }

        public bool UpdatePersonNameById(string id ,string name)
        {
            return _personBLL.UpdatePersonName(int.Parse(id) , name);
        }

        public bool InsertPerson(Bussiness.PersonEntity person)
        {
            return _personBLL.InsertPerson(person);
        }

        public bool DeletePerson(string id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
