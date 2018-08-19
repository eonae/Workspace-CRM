using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class QueryBuilder
    {
        private StringBuilder sb = new StringBuilder("SELECT ");

        public QueryBuilder(string[] tableNames, string[] fieldNames, params ICondition[] conditions)
        {
            // Пока сделаем на основе неявного соединения таблиц.
            for (int i = 0; i < fieldNames.Length; i++)
            {
                sb.Append($"[{fieldNames}]");
                if (i != fieldNames.Length-1)
                    sb.Append(",");
                else
                    sb.Append(" FROM ");
            }
            for (int i = 0; i < tableNames.Length; i++)
            {
                sb.Append($"[{tableNames}]");
                if (i != tableNames.Length - 1)
                    sb.Append(",");
            }
            if (conditions.Length != 0)
            {
                sb.Append(" WHERE ");
                for (int i = 0; i < conditions.Length; i++)
                {
                    sb.Append(conditions[i].GetString());
                    if (i != conditions.Length - 1)
                        sb.Append(" AND ");
                }
            }
        }
        //private string GetWhereClause()
        //{
        //    if (Conditions.Length > 0)
        //    {
        //        var sb = new StringBuilder($"WHERE {Conditions[0].GetString()} ");
        //        if (Conditions.Length > 1)
        //            for (int i = 1; i < Conditions.Length; i++)
        //                sb.Append($"AND {Conditions[i].GetString()}");
        //        return sb.ToString();
        //    }
        //    else return "";
        //}

        //public QueryBuilder(string tableName, string[] fieldNames, params ICondition[] conditions)
        //{

        //}
        //public QueryBuilder(string tableName, params ICondition[] conditions)
        //{

        //}


        public override string ToString()
        {
            return sb.ToString();
        }
    }
}
