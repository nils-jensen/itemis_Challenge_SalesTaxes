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

        public decimal SalesTaxesTotal 
        {
            get
            {
                var sum =
                Items.Select(pItem => pItem.NetPrice).Sum() - Items.Select(pItem => pItem.GrossPrice).Sum();

                return Math.Round(sum / 0.05m, 0) * 0.05m;
            }
        }

        public Basket(IList<BasketItem> pItems)
        {
            Items = pItems.Where(pItem => pItem != null).ToList();
        }
    }
}
