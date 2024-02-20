using System;
using System.Collections.Generic;
using System.Text;

namespace LibProject
{
    class Department : Library
    {
        string depName;
        public Department(string depName, string libName) : base(libName)
        {
            this.depName = depName;
        }
        public string Info(Library lib)
        {
            return lib.Name;
        }
    }
}
