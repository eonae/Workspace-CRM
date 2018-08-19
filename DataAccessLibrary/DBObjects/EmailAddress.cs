using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperLibrary;

namespace DataAccessLibrary
{
    public class EmailAddress : IDBObject
    {
        public int EmailID { get; set; }
        public string Email { get; set; }
        public int PersonID { get; set; }
    }
}
