using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Yesterday: " + DateTime.Now.Date.AddDays(-1).Ticks);
            Console.WriteLine("Today: " + DateTime.Now.Date.Ticks);
            Console.WriteLine("Tomorrow: " + DateTime.Now.Date.AddDays(1).Ticks);
            Console.ReadLine();
        }
    }
}
