using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth_Nanda
{
    public class GanjilGenap
    {
        public static void PrintEvenOdd(int limit, string choice)
        {
            if (limit < 1)
            {
                Console.WriteLine("Batas harus minimal 1.");
                return;
            }

            string result = string.Join(", ",
                Enumerable.Range(1, limit)
                    .Where(i => (choice == "Genap" && i % 2 == 0) || (choice == "Ganjil" && i % 2 != 0))
            );

            Console.WriteLine($"Print bilangan 1 - {limit} : {result}");
        }

        public static string EvenOddCheck(int input)
        {
            if (input < 1)
            {
                return "Invalid Input!!!";
            }

            return input % 2 == 0 ? "Genap" : "Ganjil";
        }
    }
}
