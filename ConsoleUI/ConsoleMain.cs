using System;
using CRM.BL;
using GUI;
using DataAccessLibrary;
using HelperLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ConsoleUI
{
    class ConsoleMain
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started");
            Model model = new Model();
            var list = Model.Filter.GetRecords("кур").ToListViewItems();
            Console.WriteLine(list.Length);
            Console.ReadLine();
        }
    }
}
