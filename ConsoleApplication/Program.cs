using System;
using gcreate.Tetroswap;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Solve...");

            var watch = System.Diagnostics.Stopwatch.StartNew();

            Solver solver = new Solver("bbabbaaa ", 3, 3);
            var result = solver.Solve();

            watch.Stop();

            Console.WriteLine("Transpositions: " + result);
            Console.WriteLine("Execution time: " + watch.ElapsedMilliseconds + " ms");
        }
    }
}
