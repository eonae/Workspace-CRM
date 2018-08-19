using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperLibrary;

namespace DataAccessLibrary
{
    public class AccessRights : IDBObject
    {
        public int ViewClientDetails { get; set; }
        public int EditClientDetails { get; set; }
    }
}
