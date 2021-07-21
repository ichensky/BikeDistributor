namespace BikeDistributor.Domain.Dtos
{
    /// <summary>
    /// Bikeses lot price DTO
    /// </summary>
    public class LotPriceDto
    {
        /// <summary>
        /// Quantity bikes in the lot
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Bike price
        /// </summary>
        public int BikePrice { get; set; }

        /// <summary>
        /// Bike discount
        /// </summary>
        public double Discount { get; set; } = 1d;
    }
}
