using System;
using System.Collections.Generic;
using System.Text;

namespace LibProject
{
    class Sinence : Book
    {
        string sienceType;

        public override void Welcome(string sentince)
        {
            Console.WriteLine("Welcome " + sentince + " Sinece Class");
        }
    }
}
