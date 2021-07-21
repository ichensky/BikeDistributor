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
    public class OrderLineTests
    {
        private OrderLine orderLine;
        private Bike bike;

        public OrderLineTests()
        {
            var fixture = new Fixture();
            this.orderLine = fixture.Create<OrderLine>();
            this.bike = fixture.Create<Bike>();
        }

        #region Order

        [Fact()]
        public void OrderLine_CreatedSuccessfully()
        {
            // Act
            var orderLine = new OrderLine(bike, this.orderLine.Quantity);

            // Assert
            Assert.Equal(bike, orderLine.Bike);
            Assert.Equal(this.orderLine.Quantity, orderLine.Quantity);
        }

        [Fact]
        public void OrderLine_NullabeBike_ThrowsBusinessRuleValidationException()
        {
            // Act
            Action act = () => new OrderLine(null, this.orderLine.Quantity);

            // Assert
            Assert.Throws<BusinessRuleValidationException>(act);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-0)]
        public void Order_LessThanOneQuantity_ThrowsBusinessRuleValidationException(int quantity)
        {
            // Act
            Action act = () => new OrderLine(bike, quantity);

            // Assert
            Assert.Throws<BusinessRuleValidationException>(act);
        }

        #endregion Order

        #region CalcPrice

        [Fact]
        public void CalcPrice_NullableListOfPriceDtos_ThrowsBusinessRuleValidationException()
        {
            // Act
            Action act = () => orderLine.CalcPrice(null);

            // Assert
            Assert.Throws<BusinessRuleValidationException>(act);
        }

        [Fact]
        public void CalcPrice_EmptyListOfPriceDtos_ThrowsBusinessRuleValidationException()
        {
            // Act
            Action act = () => orderLine.CalcPrice(Enumerable.Empty<LotPriceDto>().ToList());

            // Assert
            Assert.Throws<BusinessRuleValidationException>(act);
        }

        [Theory]
        [AutoDomainData]
        public void CalcPrice_NotFoundLotPrice_CallsCalcPriceWithDiscountWithoutDiscount(
            [Frozen] Mock<OrderLine> orderLineMock, List<LotPriceDto> lotPriceDtos)
        {
            // Arrange
            foreach (LotPriceDto item in lotPriceDtos.Where(item => item.BikePrice == orderLineMock.Object.Bike.Price))
            {
                item.BikePrice = int.MaxValue;
            }

            // Act
            orderLineMock.Object.CalcPrice(lotPriceDtos);

            // Assert
            orderLineMock.Verify(x => x.CalcPriceWithDiscount(orderLineMock.Object.Quantity, orderLineMock.Object.Bike.Price, 1d));
        }

        [Theory]
        [AutoDomainData]
        public void CalcPrice_FoundLotPrice_CallsCalcPriceWithDiscountWithLotPriceDiscount(
            [Frozen] Mock<OrderLine> orderLineMock, List<LotPriceDto> lotPriceDtos)
        {
            // Arrange
            foreach (LotPriceDto item in lotPriceDtos)
            {
                item.BikePrice = int.MaxValue;
            }

            lotPriceDtos[0].BikePrice = orderLineMock.Object.Bike.Price;
            lotPriceDtos[0].Quantity = orderLineMock.Object.Quantity;

            // Act
            orderLineMock.Object.CalcPrice(lotPriceDtos);

            // Assert
            orderLineMock.Verify(x => x.CalcPriceWithDiscount(orderLineMock.Object.Quantity, orderLineMock.Object.Bike.Price, lotPriceDtos[0].Discount));
        }

        #endregion CalcPrice

        #region CalcPriceWithDiscount

        [Theory]
        [InlineData(0, 1, 1)]
        [InlineData(1, 1, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(-1, 1, 1)]
        [InlineData(1, 1, -1)]
        [InlineData(1, -1, 1)]
        public void CalcPriceWithDiscount_InvalidData_ThrowsBusinessRuleValidationException(int quantity, double bikePrice, double discount)
        {
            // Act
            Action act = () => orderLine.CalcPriceWithDiscount(quantity, bikePrice, discount);

            // Assert
            Assert.Throws<BusinessRuleValidationException>(act);
        }

        [Theory]
        [InlineData(8, 3, 6)]
        [InlineData(10, 2, 7)]
        public void CalcPriceWithDiscount_Data_MultiplesParams(int quantity, double bikePrice, double discount)
        {
            // Act
            double result = orderLine.CalcPriceWithDiscount(quantity, bikePrice, discount);

            // Assert
            Assert.Equal(quantity * bikePrice * discount, result);
        }

        #endregion CalcPriceWithDiscount
    }
}