using System;
namespace Try
{
    public class Customer
    {
        public Customer(Guid customerId)
        {
            this.Id = customerId;
        }

        public Guid Id { get; }
    }
}
