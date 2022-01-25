using System;
using System.Collections.Generic;
using System.Text;

namespace SalesAndTaxes.SharedTypes
{
    public class BasketItem
    {
        private BasketItem() { }

        public bool IsExempt { get => throw new NotImplementedException(); }
        public bool IsImported { get => throw new NotImplementedException(); }

        public static BasketItem ParseItem(string pRaw)
        {
            throw new NotImplementedException();
        }
    }
}
