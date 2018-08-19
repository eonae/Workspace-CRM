﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperLibrary;

namespace DataAccessLibrary
{
    public class PhoneNumber : IDBObject
    {
        public int PhoneNumberID { get; set; }
        public string Number { get; set; }
        public int PersonID { get; set; }
    }
}
