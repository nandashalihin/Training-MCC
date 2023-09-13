using System;

public class Program
{
    static void Menu()
    {
        Console.WriteLine("==============================");
        Console.WriteLine("         MENU GANJIL GENAP");
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("1. Cek Ganjil Genap");
        Console.WriteLine("2. Print Ganjil Genap (dengan Limit)");
        Console.WriteLine("3. Exit");
        Console.WriteLine("-----------------------------------------------");
    }

    static void PrintEvenOdd(int limit, string choice)
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

    static string EvenOddCheck(int input)
    {
        if (input < 1)
        {
            return "Invalid Input!!!";
        }

        return input % 2 == 0 ? "Genap" : "Ganjil";
    }

    public static void Main(string[] args)
    {
        while (true)
        {
            Menu();
            Console.Write("Pilihan : ");
            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Pilihan tidak valid!");
                continue;
            }

            if (choice == 1)
            {
                Console.Write("Masukkan Bilangan yang ingin di cek : ");
                int num;
                if (!int.TryParse(Console.ReadLine(), out num))
                {
                    Console.WriteLine("Input tidak valid!");
                    continue;
                }

                string result = EvenOddCheck(num);
                Console.WriteLine(result);
            }
            else if (choice == 2)
            {
                Console.Write("Pilih (Ganjil/Genap) : ");
                string choice2 = Console.ReadLine();
                Console.Write("Masukkan limit : ");
                int limit;
                if (!int.TryParse(Console.ReadLine(), out limit))
                {
                    Console.WriteLine("Limit tidak valid!");
                    continue;
                }

                if (choice2 != "Genap" && choice2 != "Ganjil")
                {
                    Console.WriteLine("Input pilihan tidak valid!!!");
                    continue;
                }

                PrintEvenOdd(limit, choice2);
            }
            else if (choice == 3)
            {
                break;
            }
            else
            {
                Console.WriteLine("Pilihan tidak valid!");
            }
        }
    }
}

