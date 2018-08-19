
using DataAccessLibrary;

namespace CRM.BL
{
    public class Model
    {
        public static CurrentUser CurrentUser { get; set; } = new CurrentUser();
        public static SingleFieldFilter Filter { get; set; } = new SingleFieldFilter();
    }
}

