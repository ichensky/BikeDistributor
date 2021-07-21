using BikeDistributor.Domain.Order;
using BikeDistributor.Infrastructure.Domain.Orders.Print.Receipt;
using System.Threading.Tasks;

namespace BikeDistributor.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bike1 = new Bike("xxx", "aaa", Bike.OneThousand);
            var bike2 = new Bike("yyy", "bbb", Bike.TwoThousand);
            var bike3 = new Bike("zzz", "ccc", Bike.FiveThousand);
            var orderLine1 = new OrderLine(bike1, 30);
            var orderLine2 = new OrderLine(bike2, 40);
            var order = new Order(1, "MS");
            order.AddLine(orderLine1);
            order.AddLine(orderLine2);

            Domain.Dtos.ReceiptDto receipt = order.Receipt(Config.LotPriceDtos, Config.TaxRate);

            PrintReceipt<ReceiptStringStrategy> printReceipt = new PrintReceipt<ReceiptStringStrategy>(new ReceiptStringStrategy());
            string text = printReceipt.Print(receipt);

            //var repo = (IOrderRepository)null;
            //var unitOfWork = (UnitOfWork)null;
            //unitOfWork.Query<Bike>();
        }
    }
}
