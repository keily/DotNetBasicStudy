using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using RestfulApp.Bussiness;

namespace RestfulApp.RestfulServices.Contracts
{
    [ServiceContract]
    interface ICustomerService
    {

        [OperationContract]
        PersonEntity GetPersonsById(string id);

        [OperationContract]
        IList<PersonEntity> GetPersons();

        [OperationContract]
        bool UpdatePersonNameById(string id , string name);

        [OperationContract]
        bool InsertPerson(PersonEntity person);

        [OperationContract]
        bool DeletePerson(string id);
    }
}
