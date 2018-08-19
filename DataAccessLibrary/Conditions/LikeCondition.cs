using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class LikeCondition : ICondition
    {
        // Класс уязвим к Sql-инъекциям
        // Надо посмотреть, что будет, если ссылочный тип передавать. Может быть всё-таки строки надо?
        public string FieldName { get; private set; }
        public object SearchString { get; private set; }

        public LikeCondition(string fieldName, string searchString)
        {
            FieldName = fieldName;
            SearchString = searchString;
        }

        public string GetString()
        {
            return $"{FieldName} LIKE '%{SearchString}%'";
        }

        public bool Match(object value)
        {
            throw new NotImplementedException();
        }
    }
}
