using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using HelperLibrary;

namespace DataAccessLibrary
{
    public static class DataReader
    {
        public static List<Person> GetPeople()
        {
            using (IDbConnection conn = new SqlConnection(Helper.GetConnString("workspace_crm")))
            {
                var output = conn.Query<Person>("SELECT * FROM People").ToList();
                return output;
            }
        }
        public static List<EmailAddress> GetEmails(string surname)
        {
            using (IDbConnection conn = new SqlConnection(Helper.GetConnString("workspace_crm")))
            {
                var output = conn.Query<EmailAddress>($"SELECT * FROM EmailAddresses WHERE PersonID = (" +
                                                      $"SELECT PersonID FROM People WHERE Surname = '{ surname }')").ToList();
                return output;
            }
        }
        public static bool AddEmail(string surname, string email)
        {
            using (IDbConnection conn = new SqlConnection(Helper.GetConnString("workspace_crm")))
            {
                try
                {
                    conn.Execute("dbo.spAddEmail @Email, @Surname", new { Email = email, Surname = surname });

                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }
        public static AccessRigths GetPermissions(string userName)
        {
            using (IDbConnection conn = new SqlConnection(Helper.GetConnString("workspace_crm")))
            {
                try
                {
                    var output = conn.Query<AccessRigths>("dbo.spGetAccessRigths @UserName", new { UserName = userName });
                    return output.ToList()[0];
                }
                catch (SqlException e) { Console.WriteLine(e.Message); return null; }
            }
        }
        public static IEnumerable<IDBObject> ExecuteQuery(string sql, Type returnType)
        {
            using (IDbConnection conn = new SqlConnection(Helper.GetConnString("workspace_crm")))
            {
                try
                {
                    return conn.Query<Person>(sql); ///////////// С интерфесом не получится? Или хотя бы динамически определять тип?
                }
                catch (SqlException e) { Console.WriteLine(e.Message); return null; }
            }
        }


        public static IEnumerable<T> ExecQuery<T>(string storedProcedureName, params FilterParameter[] parameters)
        {
            var dapperParameters = new DynamicParameters();
            foreach(var parameter in parameters)
            {
                DbType dbType;
                switch (parameter.Type)
                {
                    case FilterParameterType.Int:
                        dbType = DbType.Int32; break;
                    case FilterParameterType.String:
                        dbType = DbType.String; break;
                    case FilterParameterType.Date:
                        dbType = DbType.Date; break;
                    default:
                        dbType = DbType.String; break; //// Хм.. хотелось бы избежать этой херни.
                }
                // В будущем лучше сделать метод расширения. Что-то типа dbType = Type.GetDBType();
                dapperParameters.Add(parameter.Name, parameter.Value, dbType, ParameterDirection.Input);
            }

            using (IDbConnection conn = new SqlConnection(Helper.GetConnString("workspace_crm")))
            {
                try
                {
                    var result = conn.Query<T>(sql: storedProcedureName, param: dapperParameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }
    }
}

