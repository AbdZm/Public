using System;
using System.Collections.Generic;
using System.Text;

namespace LibProject
{
    class Story : Book
    {
        string storyType;

        public override void Welcome(string sentince)
        {
            Console.WriteLine("Welcome "+sentince+" Story Class");
        }
    }
}
