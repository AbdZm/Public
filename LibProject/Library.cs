using System;
using System.Collections.Generic;
using System.Text;

namespace LibProject
{
    class Library
    {
        string name;
        int ID;
        public Library()
        {
        }
        public Library(string name)
        {
            this.name = name;
        }
        public Library(string name, int ID)
        {
            this.name = name;
            this.ID = ID;       
        }
        public void Management()
        {
            Console.WriteLine("Here Is The Management Of Library !!");
        }
        public string Name
        {
            get { return name; }
        }
    }
}
