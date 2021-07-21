using BikeDistributor.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeDistributor.Application
{
    public class OrderManager
    {
        private IUnitOfWork unitOfWork;

        public OrderManager(IUnitOfWork unitOfWork) {

            this.unitOfWork = unitOfWork;
        }



    }
}
