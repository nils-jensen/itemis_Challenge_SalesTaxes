using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesAndTaxes.SharedTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesAndTaxes.Tests.SharedTypesTests
{
    [TestClass]
    public class BasketItemTests
    {
        private const string EXEMPT_UNIMPORTED_RAW = "1 book at 12.49";

        private const string EXEMPT_IMPORTED_RAW = "1 imported box of chocolates at 10.00";

        private const string UNEXEMPT_UNIMPORTED_RAW = "1 bottle of perfume at 18.99";

        private const string UNEXEMPT_IMPORTED_RAW = "1 imported bottle of perfume at 27.99";

        private const string NAME_CONTAINING_IMPORTED = "1 box of imported chocolates at 11.25";

        private const string BOX_OF_CHOCOLATES = "imported box of chocolates";


        [TestMethod]
        public void ParseExemptUnimportedItem()
        {
            var exemptUnimported = BasketItem.ParseItem(EXEMPT_UNIMPORTED_RAW);

            Assert.IsTrue(exemptUnimported.IsExempt);

            Assert.IsFalse(exemptUnimported.IsImported);
        }

        [TestMethod]
        public void ParseExemptImportedItem()
        {
            var exemptImported = BasketItem.ParseItem(EXEMPT_IMPORTED_RAW);

            Assert.IsTrue(exemptImported.IsExempt);

            Assert.IsTrue(exemptImported.IsImported);
        }

        [TestMethod]
        public void ParseUnexemptUnimportedItem()
        {
            var unexemptUnimported = BasketItem.ParseItem(UNEXEMPT_UNIMPORTED_RAW);

            Assert.IsFalse(unexemptUnimported.IsExempt);

            Assert.IsFalse(unexemptUnimported.IsImported);
        }

        [TestMethod]
        public void ParseUnexemptImportedItem()
        {
            var unexemptImported = BasketItem.ParseItem(UNEXEMPT_IMPORTED_RAW);

            Assert.IsFalse(unexemptImported.IsExempt);

            Assert.IsTrue(unexemptImported.IsImported);
        }

        [TestMethod]
        public void ParseNameBeginningWithImported()
        {
            var beginningWithImported = BasketItem.ParseItem(EXEMPT_IMPORTED_RAW);

            Assert.IsTrue(string.Equals(beginningWithImported.Name, BOX_OF_CHOCOLATES));
        }

        [TestMethod]
        public void ParseNameContainingImported()
        {
            var containingImported = BasketItem.ParseItem(NAME_CONTAINING_IMPORTED);

            Assert.IsTrue(string.Equals(containingImported.Name, BOX_OF_CHOCOLATES));
        }

        [TestMethod]
        public void ParseSimpleName()
        {
            var book = BasketItem.ParseItem(EXEMPT_UNIMPORTED_RAW);

            Assert.IsTrue(string.Equals(book.Name, "book"));
        }

        [TestMethod]
        public void ExemptItemTax()
        {
            var exemptUnimported = BasketItem.ParseItem(EXEMPT_UNIMPORTED_RAW);

            var expectedPrice = 12.49m;

            var expectedTaxRate = 1.00m;

            Assert.AreEqual(expectedTaxRate, exemptUnimported.TaxRate);

            Assert.AreEqual(expectedPrice, exemptUnimported.NetPrice);
        }

        [TestMethod]
        public void ExemptImportedTax()
        {
            var exemptImported = BasketItem.ParseItem(EXEMPT_IMPORTED_RAW);

            var expectedPrice = 10.50m;

            var expectedTaxRate = 1.05m;

            Assert.AreEqual(expectedTaxRate, exemptImported.TaxRate);

            Assert.AreEqual(expectedPrice, exemptImported.NetPrice);
        }

        [TestMethod]
        public void RegularItemTax()
        {
            var unexemptUnimported = BasketItem.ParseItem(UNEXEMPT_UNIMPORTED_RAW);

            var expectedPrice = 20.89m;

            var expectedTaxRate = 1.10m;

            Assert.AreEqual(expectedTaxRate, unexemptUnimported.TaxRate);

            Assert.AreEqual(expectedPrice, unexemptUnimported.NetPrice);
        }

        [TestMethod]
        public void RegularImportedItemTax()
        {
            var unexemptImported = BasketItem.ParseItem(UNEXEMPT_IMPORTED_RAW);

            var expectedPrice = 32.19m;

            var expectedTaxRate = 1.15m;

            Assert.AreEqual(expectedTaxRate, unexemptImported.TaxRate);

            Assert.AreEqual(expectedPrice, unexemptImported.NetPrice);
        }
    }
}
