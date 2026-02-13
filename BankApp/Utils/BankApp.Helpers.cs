using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Utils;

internal static class InputValidator
{
    public static string GetValidAccountNumber(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input) && input.All(char.IsDigit))
            {
                return input;
            }

            Console.Clear();
            Console.WriteLine("Ogiltigt format! Ange endast siffror (t.ex. 12345).");
        }
    }

    public static decimal GetValidDecimal(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (decimal.TryParse(Console.ReadLine(), out decimal result) && result >= 0)
            {
                return result;
            }
            Console.Clear();
            Console.WriteLine("Ogiltigt belopp! Ange ett positivt tal.");
        }
    }
}
