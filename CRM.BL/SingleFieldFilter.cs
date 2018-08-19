using System;
using System.Collections.Generic;
using System.Text;
using HelperLibrary;
using DataAccessLibrary;

namespace CRM.BL
{

    public class SingleFieldFilter
    {
        private const string storedProcedureName = "spClientsFilter";

        public IEnumerable<IDBObject> GetRecords(string searchSubstring)
        {
            // Блок отвечающий за создание массива объектов FilterParameter
            FilterParameter[] parameters = new FilterParameter[1];
            parameters[0] = new FilterParameter()
                { Name = "@searchSubstring", Type = FilterParameterType.String, Value = searchSubstring };

            var result = DataReader.ExecQuery<Person>(storedProcedureName, parameters);
            return result;
        }

    }
}
