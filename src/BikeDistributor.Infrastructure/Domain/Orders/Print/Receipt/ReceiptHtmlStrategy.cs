using BikeDistributor.Domain.Dtos;
using BikeDistributor.Domain.SeedWork.Print;
using System;
using System.Text;

namespace BikeDistributor.Infrastructure.Domain.Orders.Print.Receipt
{
    /// <summary>
    /// Prints receipt in html view
    /// </summary>
    public class ReceiptHtmlStrategy : IPrintStategy<ReceiptDto>
    {
        /// <inheritdoc cref="IPrintStategy{TDto}.Print(TDto)"/>
        public string Print(ReceiptDto receiptDto)
        {
            if (receiptDto == null)
            {
                throw new ArgumentNullException(nameof(receiptDto));
            }

            if (receiptDto.ReceiptLineDtos == null || receiptDto.ReceiptLineDtos.Count == 0)
            {
                throw new ArgumentException(nameof(receiptDto.ReceiptLineDtos));
            }

            var result = new StringBuilder($"<html><body><h1>Order Receipt for {receiptDto.Company}</h1>");

            if (receiptDto.ReceiptLineDtos.Count > 0)
            {
                result.Append("<ul>");

                foreach (ReceiptLineDto line in receiptDto.ReceiptLineDtos)
                {
                    result.Append($"<li>{line.LineQuantity} x {line.BikeBrand} {line.BikeModel} = {line.Amount:C}</li>");
                }

                result.Append("</ul>");
            }

            result.Append($"<h3>Sub-Total: {receiptDto.SubTotal:C}</h3>");
            result.Append($"<h3>Tax: {receiptDto.Tax:C}</h3>");
            result.Append($"<h2>Total: {receiptDto.Total:C}</h2>");
            result.Append("</body></html>");

            return result.ToString();
        }
    }
}
