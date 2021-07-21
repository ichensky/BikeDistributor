using BikeDistributor.Domain.Dtos;
using BikeDistributor.Domain.SeedWork.Print;
using System;
using System.Text;

namespace BikeDistributor.Infrastructure.Domain.Orders.Print.Receipt
{
    /// <summary>
    /// Prints receipt in string view
    /// </summary>
    public class ReceiptStringStrategy : IPrintStategy<ReceiptDto>
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

            var result = new StringBuilder($"Order Receipt for {receiptDto.Company}{Environment.NewLine}");

            foreach (ReceiptLineDto line in receiptDto.ReceiptLineDtos)
            {
                result.AppendLine($"\t{line.LineQuantity} x {line.BikeBrand} {line.BikeModel} = {line.Amount:C}");
            }

            result.AppendLine($"Sub-Total: {receiptDto.SubTotal:C}");
            result.AppendLine($"Tax: {receiptDto.Tax:C}");
            result.Append($"Total: {receiptDto.Total:C}");

            return result.ToString();
        }
    }
}
