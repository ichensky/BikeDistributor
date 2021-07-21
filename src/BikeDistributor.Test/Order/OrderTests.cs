using AutoFixture;
using AutoFixture.Xunit2;
using BikeDistributor.Domain.Dtos;
using BikeDistributor.Domain.SeedWork;
using BikeDistributor.TestsComon;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BikeDistributor.Domain.Order.Tests
{
    public class OrderTests
    {
        private Fixture fixture;
        private Order order;
        private OrderLine orderLine;
        private Bike bike;

        public OrderTests()
        {
            this.fixture = new Fixture();
            this.order = fixture.Create<Order>();
            this.orderLine = fixture.Create<OrderLine>();
            this.bike = fixture.Create<Bike>();
        }

        #region Order

        [Fact()]
        public void Order_CreatedSuccessfully()
        {
            // Act
            var order = new Order(this.order.OrderId, this.order.Company);

            // Assert
            Assert.Equal(this.order.OrderId, order.OrderId);
            Assert.Equal(this.order.Company, order.Company);
        }

        [Theory()]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Order_InvalidCompany_ThrowsBusinessRuleValidationException(string company)
        {
            // Act
            Action act = () => new Order(this.order.OrderId, company);

            // Assert
            Assert.Throws<BusinessRuleValidationException>(act);
        }

        #endregion Order

        #region AddLine

        [Fact]
        public void AddLine_NullableEmptyLine_ThrowsBusinessRuleValidationException()
        {
            // Act
            Action act = () => order.AddLine(null);

            // Assert
            Assert.Throws<BusinessRuleValidationException>(act);
        }

        [Fact]
        public void AddLine_OrderLine_AddsOrderLineToOrder()
        {
            // Act
            order.AddLine(orderLine);

            // Assert
            Assert.Contains(orderLine, order.Lines);
        }

        #endregion AddLine

        #region Receipt

        [Fact]
        public void Receipt_NegativeTaxRate_ThrowsBusinessRuleValidationException()
        {
            // Arrange
            List<LotPriceDto> lotPricesDtos = fixture.Create<List<LotPriceDto>>();

            // Act
            Action act = () => order.Receipt(lotPricesDtos, -1);

            // Assert
            Assert.Throws<BusinessRuleValidationException>(act);
        }

        [Fact]
        public void Receipt_EmptyLotPrice_ThrowsBusinessRuleValidationException()
        {
            // Arrange
            List<LotPriceDto> lotPricesDtos = Enumerable.Empty<LotPriceDto>().ToList();

            // Act
            Action act = () => order.Receipt(lotPricesDtos, 10);

            // Assert
            Assert.Throws<BusinessRuleValidationException>(act);
        }

        [Theory]
        [AutoDomainData]
        public void Receipt_LotPrices_ReturnsReceipt(
            [Frozen] Mock<Order> orderMock, List<LotPriceDto> lotPriceDtos, double taxRate)
        {
            // Arrange
            double subTotalAmount = fixture.Create<double>();
            List<ReceiptLineDto> receiptLineDtos = fixture.Create<List<ReceiptLineDto>>();
            double tax = taxRate * subTotalAmount;

            orderMock.Setup(x => x.CalcPriceOfOrderLines(lotPriceDtos))
                .Returns((subTotalAmount, receiptLineDtos));

            // Act
            ReceiptDto result = orderMock.Object.Receipt(lotPriceDtos, taxRate);

            // Assert
            Assert.Equal(orderMock.Object.Company, result.Company);
            Assert.Equal(receiptLineDtos, result.ReceiptLineDtos);
            Assert.Equal(subTotalAmount, result.SubTotal);
            Assert.Equal(tax, result.Tax);
            Assert.Equal(tax + subTotalAmount, result.Total);
        }

        #endregion Receipt

        #region CalcPriceOfOrderLines

        [Fact]
        public void CalcPriceOfOrderLines_EmptyLotPrice_ThrowsBusinessRuleValidationException()
        {
            // Arrange
            List<LotPriceDto> lotPricesDtos = Enumerable.Empty<LotPriceDto>().ToList();

            // Act
            Action act = () => order.Receipt(lotPricesDtos, 10);

            // Assert
            Assert.Throws<BusinessRuleValidationException>(act);
        }

        [Theory]
        [AutoDomainData]
        public void CalcPriceOfOrderLines_LotPrices_ReturnsTotalAmountWithDtos(
                        [Frozen] Mock<Order> orderMock, List<LotPriceDto> lotPriceDtos, double taxRate)
        {
            // Arrange
            var orderLineTotal = 7d;
            foreach (OrderLine line in orderMock.Object.Lines)
            {
                Mock.Get(line).Setup(y => y.CalcPrice(lotPriceDtos)).Returns(orderLineTotal);
            }

            // Act
            var (TotalAmount, ReceiptLineDtos) = orderMock.Object.CalcPriceOfOrderLines(lotPriceDtos);

            // Assert
            Assert.Equal(orderLineTotal * orderMock.Object.Lines.Count, TotalAmount);
            Assert.Equal(orderMock.Object.Lines.Count, ReceiptLineDtos.Count);
            foreach (OrderLine line in orderMock.Object.Lines)
            {
                Assert.Contains(ReceiptLineDtos, x => x.Amount == orderLineTotal
                && x.BikeBrand == line.Bike.Brand
                && x.BikeModel == line.Bike.Model
                && x.LineQuantity == line.Quantity);
            }
        }

        #endregion CalcPriceOfOrderLines
    }
}