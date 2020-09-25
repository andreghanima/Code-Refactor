using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    internal class Program
    {
       
        private static void Main(string[] args)
        {
            var sodas = new[] {
                new Soda { Name = "coke", Nr = 1, Cost = 20 },
                new Soda { Name = "sprite", Nr = 3, Cost = 15 },
                new Soda { Name = "fanta", Nr = 3, Cost = 15 }
            };

            SodaMachine sodaMachine = new SodaMachine(sodas);
            sodaMachine.Start();
        }
    }
}
