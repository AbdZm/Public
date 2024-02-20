using System;

namespace LibProject
{
    class Program
    {
        static void Main()
        {
            /*Department department = new Department("Math","Homs");
            string Name = department.Info(department);
            Console.WriteLine(Name);*/
            try
            {
                int x = Convert.ToInt32(Console.ReadLine());
                int y = Convert.ToInt32(Console.ReadLine());
                int result = x / y;
                Console.WriteLine(result);
            }
            catch (Exception e)
            { 

            }
        }
    }
}
