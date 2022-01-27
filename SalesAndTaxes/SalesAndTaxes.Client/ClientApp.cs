using SalesAndTaxes.SharedTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesAndTaxes.Client
{
    class ClientApp
    {
        static void Main(string[] pArgs)
        {
            Console.WriteLine("Hello World!");

            var running = true;

            while (running)
            {
                Console.WriteLine("Enter new positions or insert an empty line to proceed.");
                var getsInput = true;

                var inputLines = new LinkedList<string>();

                while (getsInput)
                {
                    var input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        getsInput = false;
                        break;
                    }

                    inputLines.AddLast(input);
                }

                var converter = new InputToBasketConverter(inputLines);

                Console.WriteLine(converter.Convert());
            }

            Console.WriteLine("Goodbye!");
        }
    }
}
