using BikeDistributor.Domain.SeedWork;

namespace BikeDistributor.Domain.Order
{
    /// <summary>
    /// Bike entity
    /// </summary>
    public class Bike
    {
        /// <inheritdoc cref="Bike"/>
        private Bike() { }

        /// <inheritdoc cref="Bike"/>
        /// <param name="brand">Non-nullable bike brand</param>
        /// <param name="model">Not-nullabel bike model</param>
        /// <param name="price">Bike price</param>
        public Bike(int bikeId, string brand, string model, int price)
        {
            if (string.IsNullOrWhiteSpace(brand))
            {
                throw new BusinessRuleValidationException($"{nameof(brand)} is empty.");
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                throw new BusinessRuleValidationException($"{nameof(model)} is empty.");
            }

            if (price < 0)
            {
                throw new BusinessRuleValidationException($"{nameof(price)} is less than zero.");
            }

            BikeId = bikeId;
            Brand = brand;
            Model = model;
            Price = price;
        }

        /// <summary>
        /// BikeId
        /// </summary>
        public int BikeId { get; }

        /// <summary>
        /// Bike brand
        /// </summary>
        public string Brand { get; }

        /// <summary>
        /// Bike model
        /// </summary>
        public string Model { get; }

        /// <summary>
        /// Bike price
        /// </summary>
        public int Price { get; }
    }
}
