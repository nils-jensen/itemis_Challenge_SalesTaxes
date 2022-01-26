using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SalesAndTaxes.SharedTypes
{
    public class BasketItem
    {
        private const string IMPORTED = "imported";

        /// <summary>
        /// this is a bit hacky
        /// </summary>
        private static readonly HashSet<string> KnownExemptItems = new HashSet<string>()
        {
            "chocolate bar",
            "box of chocolates",
            "book",
            "packet of headache pills"
        };

        public TaxStatus Taxes { get; private set; }

        public bool IsExempt { get => Taxes.HasFlag(TaxStatus.EXEMPT); }

        public bool IsImported { get => Taxes.HasFlag(TaxStatus.IMPORTED); }

        public string Name { get; private set; }

        public decimal GrossPrice { get; private set; }

        public decimal NetPrice { get => decimal.Round(GrossPrice * TaxRate, 2, MidpointRounding.AwayFromZero); }
        public decimal TaxRate
        {
            get
            {
                var rate = 0.00m
                    + (!Taxes.HasFlag(TaxStatus.EXEMPT) ? 0.10m : 0.00m)
                    + (Taxes.HasFlag(TaxStatus.IMPORTED) ? 0.05m : 0.00m);

                return 1.00m + rate;
            }
        }

        private BasketItem() { }


        public static BasketItem ParseItem(string pRaw)
        {
            var item = new BasketItem();

            try
            {
                var split = pRaw.Split(' ');

                var amountRaw = split[0];

                if (!int.TryParse(amountRaw, out var amount))
                {
                    throw new ArgumentException("Can't get article amount.", nameof(pRaw));
                }

                var priceRaw = split[^1];

                if (!decimal.TryParse(priceRaw, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var price))
                {
                    throw new ArgumentException("Can't get article price.", nameof(pRaw));
                }

                item.GrossPrice = price;

                var itemDescriptions = split[1..^2];

                var nameBuilder = new StringBuilder();

                foreach (var descriptionPart in itemDescriptions)
                {
                    if (string.Equals(descriptionPart, IMPORTED, StringComparison.OrdinalIgnoreCase))
                    {
                        item.Taxes |= TaxStatus.IMPORTED;
                    }
                    else
                    {
                        nameBuilder.Append(descriptionPart);
                        nameBuilder.Append(' ');
                    }
                }

                item.Name = nameBuilder.ToString().TrimEnd(); // either this or catchhing it above before appending the whitespace

                if (KnownExemptItems.Contains(item.Name))
                {
                    item.Taxes |= TaxStatus.EXEMPT;
                }
            }
            catch (Exception pExc)
            {
                Console.WriteLine(pExc);

                return null;
            }

            return item;
        }
    }

    [Flags]
    public enum TaxStatus
    {
        NONE = 0,
        EXEMPT = 1 << 0,
        IMPORTED = 1 << 1,
    }
}
