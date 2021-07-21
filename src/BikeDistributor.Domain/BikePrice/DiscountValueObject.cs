using BikeDistributor.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeDistributor.Domain.BikePrice
{
    public class DiscountValueObject
    {
        public DiscountValueObject(double discount, int quantity) {
            if (quantity < 1)
            {
                throw new BusinessRuleValidationException($"{nameof(discount)} should be gether than zero.");
            }
        
        }

        public double Discount { get; set; }
        
        public int Quantity { get; set; }
    }
}
