using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcOOPApp
{
    class Add
    {
        int a;
        int b;
        public Add(int x, int y)
        {
            a = x;
            b = y;
        }
        public string Result()
        {
            return (a + b).ToString();
        }
    }
}
