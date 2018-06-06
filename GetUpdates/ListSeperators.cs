using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetUpdates
{
    public sealed class ListSeperators
    {
        private static readonly ListSeperators instance = new ListSeperators();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static ListSeperators()
        {
           
        }

        private ListSeperators()
        {
            lListSeparator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
            lSeperators = new string[] { "|" ,";", ","};
            cListSeparator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
        }

        public static ListSeperators Instance
        {
            get
            {
                return instance;
            }
        }


        
        public string lListSeparator { get; private set; }

        //= System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
        public string[] lSeperators { get; private set; }
        // { ";", ",", "|" };
        public string cListSeparator { get; set; }
    }
}
