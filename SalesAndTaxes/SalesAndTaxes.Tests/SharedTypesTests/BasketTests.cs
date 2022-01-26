using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesAndTaxes.SharedTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesAndTaxes.Tests.SharedTypesTests
{
    [TestClass]
    public class BasketTests
    {
        [TestMethod]
        public void NoTaxes()
        {
            var basket = new Basket(
                new List<BasketItem>() 
                { 
                    BasketItem.ParseItem("1 book at 12.49"),
                    BasketItem.ParseItem("1 chocolate bar at 0.85"),
                    BasketItem.ParseItem("1 packet of headache pills at 9.75"),
                }
            );

            var expectedGrossTotal = 12.49m + 0.85m + 9.75m;

            var expectedNetTotal = expectedGrossTotal;

            var expectedTaxes = 0.00m;

            Assert.AreEqual(expectedGrossTotal, basket.GrossTotal);

            Assert.AreEqual(expectedNetTotal, basket.NetTotal);

            Assert.AreEqual(expectedTaxes, basket.SalesTaxesTotal);
        }

        [TestMethod]
        public void NoImports()
        {
            var basket = new Basket(
                new List<BasketItem>()
                {
                    BasketItem.ParseItem("1 book at 12.49"),
                    BasketItem.ParseItem("1 music CD at 14.99"),
                    BasketItem.ParseItem("1 bottle of perfume at 18.99"),
                }
            );

            var expectedGrossTotal = 12.49m + 0.85m + 9.75m;

            var expectedNetTotal = 0.00m;

            foreach (var item in basket.Items)
            {
                expectedNetTotal += item.NetPrice;
            }

            var expectedTaxes = expectedNetTotal - expectedGrossTotal;

            Assert.AreEqual(expectedGrossTotal, basket.GrossTotal);

            Assert.AreEqual(expectedNetTotal, basket.NetTotal);

            Assert.AreEqual(expectedTaxes, basket.SalesTaxesTotal);
        }

        [TestMethod]
        public void WithImports()
        {
            var basket = new Basket(
                new List<BasketItem>()
                {
                    BasketItem.ParseItem("1 box of imported chocolates at 11.25"),
                    BasketItem.ParseItem("1 imported bottle of perfume at 47.50"),
                    BasketItem.ParseItem("1 imported box of chocolates at 10.00"),
                }
            );

            var expectedGrossTotal = 11.25m + 47.50m + 10.00m;

            var expectedNetTotal = 0.00m;

            foreach (var item in basket.Items)
            {
                expectedNetTotal += item.NetPrice;
            }

            var expectedTaxes = expectedNetTotal - expectedGrossTotal;

            Assert.AreEqual(expectedGrossTotal, basket.GrossTotal);

            Assert.AreEqual(expectedNetTotal, basket.NetTotal);

            Assert.AreEqual(expectedTaxes, basket.SalesTaxesTotal);
        }
    }
}
