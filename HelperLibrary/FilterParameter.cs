using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary
{
    public enum FilterParameterType
    {
        String, Int, Date
    }
    public struct FilterParameter
    {
        public string Name;
        public FilterParameterType Type;
        public object Value;
    }
}
