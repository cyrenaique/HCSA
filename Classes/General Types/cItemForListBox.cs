using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HCSAnalyzer.Classes.General_Types
{
    public class cItemForListBox
    {

        public cItemForListBox(object ObjectToBeIncluded)
        {
            this.IncludedObject = ObjectToBeIncluded;
        }

        public string UserName { get; set; }
        public object IncludedObject { get; set; }
    }
}
