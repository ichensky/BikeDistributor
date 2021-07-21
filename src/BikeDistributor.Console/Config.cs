using BikeDistributor.Domain.Dtos;
using BikeDistributor.Domain.Order;
using System.Collections.Generic;

namespace BikeDistributor.Console
{
    internal class Config
    {
        public const int DiscountBikePriceOneThousand = 1000;
        public const int DiscountBikePriceTwoThousand = 2000;
        public const int DiscountBikePriceFiveThousand = 5000;

        /// <summary>
        /// Lot bike prices
        /// </summary>
        internal static List<LotPriceDto> LotPriceDtos = new List<LotPriceDto> {
            new LotPriceDto{ BikePrice = DiscountBikePriceOneThousand, Quantity = 20, Discount = 0.9 },
            new LotPriceDto{ BikePrice = DiscountBikePriceTwoThousand, Quantity = 10, Discount = 0.8 },
            new LotPriceDto{ BikePrice = DiscountBikePriceFiveThousand, Quantity = 5, Discount = 0.8 },
            };

        /// <summary>
        /// Tax rate
        /// </summary>
        internal const double TaxRate = .0725d;
    }
}
