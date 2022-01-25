using System;
using System.Collections.Generic;
using System.Text;

namespace SalesAndTaxes.SharedTypes
{
    public class Basket
    {
        public IList<BasketItem> Items { get; private set; }


        public Basket(IList<BasketItem> pItems)
        {
            Items = pItems;
        }
    }
}
