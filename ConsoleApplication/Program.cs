using System;
using gcreate.Tetroswap;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initialize...");

            Solver solver = new Solver("abababab", 2, 4);
            var result = solver.Solve();

            Console.WriteLine("Result:" + result); // bbaabbaa || aabbaabb
        }
    }
}
