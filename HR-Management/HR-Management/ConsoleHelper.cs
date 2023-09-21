

using System;
using System.Collections.Generic;

namespace HR_Management;
public class ConsoleHelper
{
    public static void DisplayTable(List<Dictionary<string, string>> data)
    {
        if (data.Count > 0)
        {
            // Ambil nama kolom dari entri pertama sebagai header
            var headers = data[0].Keys;

            // Hitung lebar kolom berdasarkan panjang data
            var columnWidths = new Dictionary<string, int>();
            foreach (var header in headers)
            {
                int maxWidth = header.Length;
                foreach (var row in data)
                {
                    if (row.ContainsKey(header))
                    {
                        maxWidth = Math.Max(maxWidth, row[header].Length);
                    }
                }
                columnWidths[header] = maxWidth;
            }

            // Cetak header
            foreach (var header in headers)
            {
                Console.Write(header.PadRight(columnWidths[header] + 2));
            }
            Console.WriteLine();

            // Cetak garis pemisah
            foreach (var header in headers)
            {
                Console.Write(new string('-', columnWidths[header]) + "  ");
            }
            Console.WriteLine();

            foreach (var row in data)
            {
                foreach (var header in headers)
                {
                    if (row.ContainsKey(header))
                    {
                        Console.Write(row[header].PadRight(columnWidths[header] + 2));
                    }
                    else
                    {
                        Console.Write("".PadRight(columnWidths[header] + 2));
                    }
                }
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("No data found");
        }
    }
}
