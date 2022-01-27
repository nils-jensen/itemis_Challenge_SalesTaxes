using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesAndTaxes.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesAndTaxes.Tests.ClientTests
{
    [TestClass]
    public class InputToBasketConverterTests
    {
        /// <summary>
        /// Basket is: <br />
        /// 1 book at 12.49 <br />
        /// 1 music CD at 14.99 <br />
        /// 1 chocolate bar at 0.85 <br />
        /// </summary>
        [TestMethod]
        public void FirstBasket()
        {
            var expectedOutput = new StringBuilder()
                                 .AppendLine("1 book: 12.49")
                                 .AppendLine("1 music CD: 16.49")
                                 .AppendLine("1 chocolate bar: 0.85")
                                 .AppendLine("Sales Taxes: 1.50")
                                 .AppendLine("Total: 29.83")
                                 .ToString();

            var input = new string[]
            {
                "1 book at 12.49",
                "1 music CD at 14.99",
                "1 chocolate bar at 0.85",
            };

            var converter = new InputToBasketConverter(input);

            var convertedBasket = converter.Convert();

            Assert.IsTrue(
                string.Equals(expectedOutput, convertedBasket)
            );
        }

        /// <summary>
        /// Basket is: <br />
        /// 1 imported box of chocolates at 10.00 <br />
        /// 1 imported bottle of perfume at 47.50 <br />
        /// </summary>
        [TestMethod]
        public void SecondBasket()
        {
            var expectedOutput = new StringBuilder()
                                 .AppendLine("1 imported box of chocolates: 10.50")
                                 .AppendLine("1 imported bottle of perfume: 54.65")
                                 .AppendLine("Sales Taxes: 7.65")
                                 .AppendLine("Total: 65.15")
                                 .ToString();

            var input = new string[]
            {
                "1 imported box of chocolates at 10.00",
                "1 imported bottle of perfume at 47.50",
            };

            var converter = new InputToBasketConverter(input);

            var convertedBasket = converter.Convert();


            // fails as there are 2 cents missing from the total
            Assert.IsTrue(
                string.Equals(expectedOutput, convertedBasket)
            );
        }

        /// <summary>
        /// Basket is: <br />
        /// 1 imported bottle of perfume at 27.99 <br />
        /// 1 bottle of perfume at 18.99 <br />
        /// 1 packet of headache pills at 9.75 <br />
        /// 1 box of imported chocolates at 11.25 <br />
        /// </summary>
        [TestMethod]
        public void ThirdBasket()
        {
            var expectedOutput = new StringBuilder()
                                 .AppendLine("1 imported bottle of perfume: 32.19")
                                 .AppendLine("1 bottle of perfume: 20.89")
                                 .AppendLine("1 packet of headache pills: 9.75")
                                 .AppendLine("1 imported box of chocolates: 11.85")
                                 .AppendLine("Sales Taxes: 6.70")
                                 .AppendLine("Total: 74.68")
                                 .ToString();

            var input = new string[]
            {
                "1 imported bottle of perfume at 27.99",
                "1 bottle of perfume at 18.99",
                "1 packet of headache pills at 9.75",
                "1 box of imported chocolates at 11.25",
            };

            var converter = new InputToBasketConverter(input);

            var convertedBasket = converter.Convert();

            // fails with 5 cents missing from taxes and 4 from the total.
            Assert.IsTrue(
                string.Equals(expectedOutput, convertedBasket)
            );
        }
    }
}
