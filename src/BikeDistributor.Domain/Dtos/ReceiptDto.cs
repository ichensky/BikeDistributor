using System.Collections.Generic;

namespace BikeDistributor.Domain.Dtos
{
    /// <summary>
    /// Receipt DTO
    /// </summary>
    public class ReceiptDto
    {
        /// <summary>
        /// Company name
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Item tax
        /// </summary>
        public double Tax { get; set; }

        /// <summary>
        /// Total item cost including <see cref="Tax"/>
        /// </summary>
        public double Total { get; set; }

        /// <summary>
        /// Item cost without <see cref="Tax"/>
        /// </summary>
        public double SubTotal { get; set; }

        /// <summary>
        /// Receipt order lines
        /// </summary>
        public IList<ReceiptLineDto> ReceiptLineDtos { get; set; }
    }
}
