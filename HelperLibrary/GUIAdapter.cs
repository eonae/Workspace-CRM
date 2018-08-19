using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace HelperLibrary
{
    public static class GUIAdapter
    {
        public static ListViewItem[] ToListViewItems(this IEnumerable<IDBObject> list)
        {
            var result = new List<ListViewItem>();
            foreach (var record in list)
            {
                Type type = record.GetType();
                int fieldsCount = type.GetProperties().Length;
                string[] fields = new string[fieldsCount];
                for (int i = 0; i < fieldsCount; i++)
                {
                    
                    fields[i] = type.GetProperties()[i].GetValue(record).ConvertToString();
                }
                result.Add(new ListViewItem(fields));
            }
            return result.ToArray();
        }

        public static string ConvertToString(this object obj)
        {
            if (obj == null) return "null";
            else
            {
                switch (obj)
                {
                    case DateTime dt:
                        return String.Format(dt.ToString("dd.MM.yyyy"));
                    default:
                        return obj.ToString();
                }
            }
        }

    }
}
