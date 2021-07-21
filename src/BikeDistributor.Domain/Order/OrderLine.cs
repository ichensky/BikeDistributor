using BikeDistributor.Domain.Dtos;
using BikeDistributor.Domain.SeedWork;
using System.Collections.Generic;
using System.Linq;

namespace BikeDistributor.Domain.Order
{
    /// <summary>
    /// Bike entity order line
    /// </summary>
    public class OrderLine
    {
        /// <inheritdoc cref="OrderLine"/>
        private OrderLine() { }

        /// <inheritdoc cref="OrderLine"/>
        /// <param name="bike">Non-nullable bike entity.</param>
        /// <param name="quantity">Bikes quantity</param>
        public OrderLine(Bike bike, int quantity)
        {
            Bike = bike ?? throw new BusinessRuleValidationException($"{nameof(bike)} is null.");

            if (quantity < 1)
            {
                throw new BusinessRuleValidationException($"{nameof(quantity)} must be bigger than zero.");
            }

            Quantity = quantity;
        }

        /// <summary>
        /// Bike entity
        /// </summary>
        public Bike Bike { get; }

        /// <summary>
        /// Bike quantity in order line
        /// </summary>
        public int Quantity { get; }

        /// <summary>
        /// Calculates price of all bikes in order line
        /// </summary>
        /// <param name="lotPriceDtos">Not-emply list of <see cref="LotPriceDto"/></param>
        /// <returns>Total price of all bikes in the lot.</returns>
        internal virtual double CalcPrice(IList<LotPriceDto> lotPriceDtos)
        {
            if (lotPriceDtos == null || lotPriceDtos.Count < 1)
            {
                throw new BusinessRuleValidationException($"{nameof(lotPriceDtos)} is empty."); ;
            }

            LotPriceDto lotPriceDto = lotPriceDtos.OrderBy(x => x.BikePrice).SingleOrDefault(x => x.BikePrice == Bike.Price);
            var discount = 1d;

            if (lotPriceDto != null && Quantity >= lotPriceDto.Quantity)
            {
                discount = lotPriceDto.Discount;
            }

            return CalcPriceWithDiscount(Quantity, Bike.Price, discount);
        }

        protected internal virtual double CalcPriceWithDiscount(int quantity, double bikePrice, double discount)
        {
            if (quantity < 1)
            {
                throw new BusinessRuleValidationException($"{nameof(quantity)} must be bigger than zero.");
            }

            if (bikePrice <= 0)
            {
                throw new BusinessRuleValidationException($"{nameof(bikePrice)} must be bigger than zero.");
            }

            if (discount <= 0)
            {
                throw new BusinessRuleValidationException($"{nameof(quantity)} must be bigger than zero.");
            }

            return quantity * bikePrice * discount;
        }
    }
}
