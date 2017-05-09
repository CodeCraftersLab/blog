using System;
namespace Try
{
    public class CustomerRepository
    {
        public ITry<Customer> GetCustomer(int random)
        {
            if (random % 2 == 0)
            {
                var customer = new Customer(Guid.NewGuid());
                return customer.ToSucess();
            }
            try
            {
                //To simulate an error
                throw new InvalidOperationException("is a odd number");
            }
            catch (Exception ex)
            {
                return ex.ToFailed<Customer>();
            }
        }
    }
}
