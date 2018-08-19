using HelperLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary;

namespace CRM.BL
{
    public interface IFilter
    {
        IEnumerable<IDBObject> GetRecords(string searchSubstring);
    }
}
