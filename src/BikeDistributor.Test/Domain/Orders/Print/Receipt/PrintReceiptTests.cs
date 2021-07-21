using AutoFixture.Xunit2;
using BikeDistributor.Domain.Dtos;
using BikeDistributor.Domain.SeedWork.Print;
using BikeDistributor.TestsComon;
using Moq;
using System;
using Xunit;

namespace BikeDistributor.Infrastructure.Domain.Orders.Print.Receipt.Tests
{
    public class PrintReceiptTests
    {
        private PrintReceipt<IPrintStategy<ReceiptDto>> printReceipt;

        public PrintReceiptTests()
        {
            this.printReceipt = Mock.Of<PrintReceipt<IPrintStategy<ReceiptDto>>>();
        }

        [Theory]
        [AutoDomainData]
        public void PrintReceipt_SetPrintProcessor([Frozen] IPrintStategy<ReceiptDto> printStategy)
        {
            // Act
            var result = new PrintReceipt<IPrintStategy<ReceiptDto>>(printStategy);

            // Assert
            Assert.Equal(printStategy, result.Processor);
        }

        [Fact]
        public void PrintReceipt_NullableProcessor_ThrowsArgumentNullException()
        {
            // Act
            Action act = () => new PrintReceipt<IPrintStategy<ReceiptDto>>(null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void Print_NullableReceiptDto_ThrowsArgumentNullException()
        {
            // Arrange
            Mock.Get(printReceipt).CallBase = true;

            // Act
            Action act = () => printReceipt.Print(null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Theory]
        [AutoData]
        public void Print_ReceiptDto_ReturnsPrintedString(ReceiptDto receiptDto, string printedString)
        {
            // Arrange
            Mock.Get(printReceipt).Setup(x => x.Print(receiptDto)).Returns(printedString);

            // Act
            string result = printReceipt.Print(receiptDto);

            // Assert
            Assert.Equal(printedString, result);
        }
    }
}