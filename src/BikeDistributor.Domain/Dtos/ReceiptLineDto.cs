namespace BikeDistributor.Domain.Dtos
{
    /// <summary>
    /// Receipt order line DTO
    /// </summary>
    public class ReceiptLineDto
    {
        /// <summary>
        /// Quantity of bikes in order line
        /// </summary>
        public int LineQuantity { get; set; }

        /// <summary>
        /// Bike brand name
        /// </summary>
        public string BikeBrand { get; set; }

        /// <summary>
        /// Bike model name
        /// </summary>
        public string BikeModel { get; set; }

        /// <summary>
        /// Order line bikeses total cost without taxes
        /// </summary>
        public double Amount { get; set; }
    }
}
