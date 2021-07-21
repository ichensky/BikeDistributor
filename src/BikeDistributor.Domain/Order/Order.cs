using BikeDistributor.Domain.Dtos;
using BikeDistributor.Domain.SeedWork;
using System.Collections.Generic;

namespace BikeDistributor.Domain.Order
{
    /// <summary>
    /// Order aggregate root entity
    /// </summary>
    public class Order : IAggregateRoot
    {
        private Order() { }

        /// <inheritdoc cref="Order"/>
        /// <param name="orderId">Order Id</param>
        /// <param name="company">Non-nullable company name</param>
        public Order(int orderId, string company)
        {
            if (string.IsNullOrWhiteSpace(company))
            {
                throw new BusinessRuleValidationException($"{nameof(company)} is empty");
            }

            Company = company;
            OrderId = orderId;
            Lines = new List<OrderLine>();
        }

        /// <summary>
        /// Company name
        /// </summary>
        public string Company { get; }

        /// <summary>
        /// Order lines
        /// </summary>
        public IList<OrderLine> Lines { get; }

        /// <summary>
        /// Order Id
        /// </summary>
        public int OrderId { get; }

        /// <summary>
        /// Add order line to Order entity
        /// </summary>
        /// <param name="line">Order line</param>
        public void AddLine(OrderLine line)
        {
            if (line == null)
            {
                throw new BusinessRuleValidationException($"{nameof(line)} is null."); ;
            }

            Lines.Add(line);
        }

        public ReceiptDto Receipt(IList<LotPriceDto> lotPriceDtos, double taxRate)
        {
            if (lotPriceDtos == null || lotPriceDtos.Count < 1)
            {
                throw new BusinessRuleValidationException($"{nameof(lotPriceDtos)} is empty."); ;
            }

            if (taxRate < 0)
            {
                throw new BusinessRuleValidationException($"{nameof(taxRate)} must be bigger than zero."); ;
            }

            (double subTotalAmount, IList<ReceiptLineDto> receiptLineDtos) = CalcPriceOfOrderLines(lotPriceDtos);
            double tax = subTotalAmount * taxRate;

            var receiptDto = new ReceiptDto
            {
                Company = Company,
                ReceiptLineDtos = receiptLineDtos,
                SubTotal = subTotalAmount,
                Tax = tax,
                Total = subTotalAmount + tax,
            };

            return receiptDto;
        }

        /// <summary>
        /// Calculates prices for order lines and converts them to list of DTOs <see cref="ReceiptLineDto">
        /// </summary>
        /// <param name="lotPriceDtos">List of lot prices Dtos <see cref="LotPriceDto"/></param>
        /// <returns>TotalAmount is [total amount of all order lines], ReceiptLineDtos is [list of receipt line Dtos] </returns>
        protected internal virtual (double TotalAmount, IList<ReceiptLineDto> ReceiptLineDtos)
            CalcPriceOfOrderLines(IList<LotPriceDto> lotPriceDtos) 
        {
            if (lotPriceDtos == null || lotPriceDtos.Count < 1)
            {
                throw new BusinessRuleValidationException($"{nameof(lotPriceDtos)} is empty."); ;
            }

            var receiptLineDtos = new List<ReceiptLineDto>();
            var totalAmount = 0d;

            foreach (OrderLine line in Lines)
            {
                double thisAmount = line.CalcPrice(lotPriceDtos);
                receiptLineDtos.Add(new ReceiptLineDto
                {
                    LineQuantity = line.Quantity,
                    BikeBrand = line.Bike.Brand,
                    BikeModel = line.Bike.Model,
                    Amount = thisAmount
                }); ;

                totalAmount += thisAmount;
            }
            return (totalAmount, receiptLineDtos);
        }
    }
}
