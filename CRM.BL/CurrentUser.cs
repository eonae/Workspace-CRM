using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary;
using HelperLibrary;

namespace CRM.BL
{
    public class CurrentUser
    {
        public string UserName { get; private set; }
        public AccessRigths Permissions { get; private set; }

        public delegate void UserChangedEventHandler();
        public event UserChangedEventHandler OnUserChange;


        public void Change(string userName)
        {
            UserName = userName;
            Permissions = DataReader.GetPermissions(userName);
            OnUserChange();
        }

        public void ConsolePrint()
        {
            Console.WriteLine("CurrentUser");
            Console.WriteLine("-----------");
            Console.WriteLine($"UserName: { UserName}");
            Console.WriteLine("Permissions:" );
            Permissions.ConsolePrint();
        }


        // Нужно добавить событие UserChanged и подписать на него GUI.
    }
}
