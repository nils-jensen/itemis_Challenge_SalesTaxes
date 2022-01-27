using SalesAndTaxes.SharedTypes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SalesAndTaxes.Client
{
    public class InputToBasketConverter
    {
        public List<string> InputLines { get; private set; }

        public Basket Basket { get; }

        public InputToBasketConverter(IEnumerable<string> pInputLines)
        {
            InputLines = pInputLines?.ToList() ?? throw new ArgumentNullException(nameof(pInputLines));

            Basket = new Basket(
                InputLines.Select(
                 pLine =>
                 BasketItem.ParseItem(pLine)
                ).ToList()
            );
        }

        public string Convert()
        {
            var sb = new StringBuilder();

            foreach (var item in Basket.Items)
            {
                sb.AppendLine($"{item.Amount} {item.Name}: {item.NetPrice.ToString(CultureInfo.InvariantCulture)}");
            }

            sb.AppendLine($"Sales Taxes: {Basket.SalesTaxesTotal.ToString(CultureInfo.InvariantCulture)}");
            sb.AppendLine($"Total: {Basket.NetTotal.ToString(CultureInfo.InvariantCulture)}");

            return sb.ToString();
        }
    }
}
