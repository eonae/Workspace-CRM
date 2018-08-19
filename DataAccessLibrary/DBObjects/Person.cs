using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperLibrary;

namespace DataAccessLibrary
{
    public class Person : IDBObject
    {
        public int PersonID { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int PersonsStatusID { get; set; }
    }

}
