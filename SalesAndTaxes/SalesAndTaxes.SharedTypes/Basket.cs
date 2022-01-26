using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesAndTaxes.SharedTypes
{
    public class Basket
    {
        public IList<BasketItem> Items { get; private set; }

        public decimal GrossTotal { get => Items.Select(pItem => pItem.GrossPrice).Sum(); }

        public decimal NetTotal { get => Items.Select(pItem => pItem.NetPrice).Sum(); }

        public decimal SalesTaxesTotal { get => NetTotal - GrossTotal; }

        public Basket(IList<BasketItem> pItems)
        {
            Items = pItems.Where(pItem => pItem != null).ToList();
        }
    }
}
