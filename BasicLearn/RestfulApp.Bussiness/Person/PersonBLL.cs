using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RestfulApp.Bussiness.Person
{
    public class PersonBLL
    {
        public IList<PersonEntity> GetPerson()
        {
            IList<PersonEntity> persons = new List<PersonEntity>()
            {
                new PersonEntity(){
                    UID=1,
                    Name="张伟",
                    NickName="keily",
                    Age=23,
                    Description="RestfulApp.Bussiness.Common",
                    Property=300
                },
                new PersonEntity(){
                    UID=1,
                    Name="王婷婷",
                    NickName="fay",
                    Age=23,
                    Description="RestfulApp.Bussiness.Common",
                    Property=100
                }
            };

            return persons;
        }

        public PersonEntity GetPersonById(long p)
        {
            var persons = new PersonEntity()
            {
                UID = 1,
                Name = "张伟",
                NickName = "keily",
                Age = 23,
                Description = "RestfulApp.Bussiness.Common",
                Property = 300
            };
            System.Diagnostics.Debug.Print(persons.Name);

            return persons;
        }

        public bool UpdatePersonName(int personId, string name)
        {
            var persons = new PersonEntity()
            {
                UID = 1,
                Name = "张伟",
                NickName = "keily",
                Age = 23,
                Description = "RestfulApp.Bussiness.Common",
                Property = 300
            };

            persons.Name = name;
            return true;
        }

        public bool InsertPerson(PersonEntity entity)
        {
            var persons = entity;
            return true;
        }
    }
}
