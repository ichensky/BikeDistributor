using AutoFixture;
using BikeDistributor.Domain.SeedWork;
using System;
using Xunit;

namespace BikeDistributor.Domain.Order.Tests
{
    public class BikeTests
    {
        private Bike bike;

        public BikeTests()
        {
            this.bike = new Fixture().Create<Bike>();
        }

        [Fact()]
        public void Bike_CreatedSuccessfully()
        {
            // Act
            var bike = new Bike(this.bike.Brand, this.bike.Model, this.bike.Price);

            // Assert
            Assert.Equal(this.bike.Brand, bike.Brand);
            Assert.Equal(this.bike.Model, bike.Model);
            Assert.Equal(this.bike.Price, bike.Price);
        }

        [Theory()]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Bike_InvalidBrand_ThrowsBusinessRuleValidationException(string brand)
        {
            // Act
            Action act = () => new Bike(brand, this.bike.Model, this.bike.Price);

            // Assert
            Assert.Throws<BusinessRuleValidationException>(act);
        }

        [Theory()]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Bike_InvalidModel_ThrowsBusinessRuleValidationException(string model)
        {
            // Act
            Action act = () => new Bike(this.bike.Brand, model, this.bike.Price);

            // Assert
            Assert.Throws<BusinessRuleValidationException>(act);
        }

        [Fact]
        public void Bike_LessThanZeroPrice_ThrowsBusinessRuleValidationException()
        {
            // Act
            Action act = () => new Bike(this.bike.Brand, this.bike.Model, -1);

            // Assert
            Assert.Throws<BusinessRuleValidationException>(act);
        }
    }
}