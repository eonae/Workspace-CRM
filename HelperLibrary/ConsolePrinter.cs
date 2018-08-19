using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace HelperLibrary
{
    public static class ConsolePrinter
    {
        public const char simpleDelimeterChar = '-';
        public const char strongDelimeterChar = '=';
        public const int leftMargin = 1;

        public static void ConsolePrint(this IDBObject obj)
        {
            var type = obj.GetType();
            foreach (var property in type.GetProperties())
                Console.WriteLine("{0}: {1}", property.Name, property.GetValue(obj));
        }
        public static void ConsolePrint(this IEnumerable<IDBObject> list)
        {
            list.ConsolePrint("");
        }
        public static void ConsolePrint(this IEnumerable<IDBObject> list, string collectionTitle)
        {
            if (list != null && list.Count<IDBObject>() > 0)
            {
                var type = list.ElementAt(0).GetType();
                var columnsInfo = list.GetColumnsInfo(type, out int tableWidth);
                if (collectionTitle != "") Console.WriteLine($"Title: {collectionTitle} ");
                PrintTitles(columnsInfo, tableWidth);

                string simpleDelimeter = new string(simpleDelimeterChar, tableWidth);
                foreach (var record in list)
                {
                    Console.Write(new string(' ', leftMargin) + "| ");
                    foreach (var key in columnsInfo.Keys)
                    {
                        object value = type.GetProperty(key).GetValue(record);
                        string toPrint;

                        if (value != null) toPrint = value.ToString();
                        else toPrint = "null";
                        Console.Write(toPrint);
                        int spacesToInsert = columnsInfo[key] - toPrint.Length;
                        Console.Write(new string(' ', spacesToInsert) + " | ");
                    }
                    Console.WriteLine();
                    Console.WriteLine(new string(' ', leftMargin) + simpleDelimeter);
                }
            }
        }

        private static void PrintTitles(Dictionary<string, int> columnsInfo, int tableWidth)
        {
            string strongDelimeter = new string(' ', leftMargin) + new string(strongDelimeterChar, tableWidth);
            Console.WriteLine(strongDelimeter);
            Console.Write(new string(' ', leftMargin) + "| ");
            foreach (var column in columnsInfo)
            {
                Console.Write(column.Key);
                int spacesToInsert = column.Value - column.Key.Length;
                Console.Write(new string(' ', spacesToInsert) + " | ");
            }
            Console.WriteLine();
            Console.WriteLine(strongDelimeter);
        }
        private static Dictionary<string, int> GetColumnsInfo(this IEnumerable<IDBObject> list, Type type, out int tableWidth)
        {

            if (list != null && list.Count() > 0)
            {
                var result = new Dictionary<string, int>();
                int numberOfFields = type.GetProperties().Count();
                tableWidth = 1;

                foreach (var property in type.GetProperties())
                {
                    int maxLength = 0;
                    foreach (var record in list)
                    {
                        int length;
                        if (property.GetValue(record) == null) length = 4;
                        else length = property.GetValue(record).ToString().Length;
                        if (length > maxLength) maxLength = length;
                    }
                    if (property.Name.Length > maxLength) maxLength = property.Name.Length;
                    result.Add(property.Name, maxLength);
                    tableWidth += (maxLength + 3); // " | "
                }
                return result;
            }
            else
            {
                tableWidth = 0;
                return null;
            }
        }

        /// ///////////////////

        public static Dictionary<string, object> GetProperties(this IDBObject obj)
        {
            throw new NotImplementedException();
        }
    }
}