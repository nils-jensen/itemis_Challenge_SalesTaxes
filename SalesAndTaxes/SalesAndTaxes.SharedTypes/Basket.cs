using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesAndTaxes.SharedTypes
{
    public class Basket
    {
        public IList<BasketItem> Items { get; private set; }

        public decimal GrossTotal { get => throw new NotImplementedException(); }

        public decimal NetTotal { get => throw new NotImplementedException(); }

        public decimal SalesTaxesTotal { get => throw new NotImplementedException(); }

        public Basket(IList<BasketItem> pItems)
        {
            Items = pItems.Where(pItem => pItem != null).ToList();
        }
    }
}
