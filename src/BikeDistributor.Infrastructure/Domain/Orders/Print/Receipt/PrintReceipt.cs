using BikeDistributor.Domain.Dtos;
using BikeDistributor.Domain.SeedWork.Print;
using System;

namespace BikeDistributor.Infrastructure.Domain.Orders.Print.Receipt
{
    /// <summary>
    /// Print receipt with <see cref="PrintProcessor"/>
    /// </summary>
    /// <typeparam name="PrintProcessor">Print reciept according specified strategy.</typeparam>
    public class PrintReceipt<PrintProcessor> where PrintProcessor : IPrintStategy<ReceiptDto>
    {
        /// <summary>
        /// Processor that print reciept according specified strategy.
        /// </summary>
        protected internal virtual PrintProcessor Processor { get; private set; }

        protected internal PrintReceipt() { }

        /// <inheritdoc cref="PrintReceipt{Processor}"/>
        public PrintReceipt(PrintProcessor processor)
        {
            if (processor == null)
            {
                throw new ArgumentNullException(nameof(processor));
            }

            this.Processor = processor;
        }

        /// <inheritdoc cref="IPrintStategy{TDto}.Print(TDto)"/>
        public virtual string Print(ReceiptDto receiptDto)
        {
            if (receiptDto == null)
            {
                throw new ArgumentNullException(nameof(receiptDto));
            }

            return this.Processor.Print(receiptDto);
        }
    }
}
