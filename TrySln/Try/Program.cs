using System;
using System.Linq;
namespace Try
{
    class Program
    {
        static void Main(string[] args)
        {
            var customerRepository = new CustomerRepository();

            for (int index = 0; index < 2; index++)
            {
                var searchResult = customerRepository.GetCustomer(index);
                var processResult = ProcessData(searchResult);

                switch (processResult)
                {
                    case Success<CustomerDTO> success:
                        {
                            Console.WriteLine(success.Value.CustomerId);
                            break;
                        }
                    case Failure<CustomerDTO> fail:
                        {
                            Console.WriteLine($"Problems when get customer. Error : { fail.GetException().Message}");
                            break;
                        }
                }
            }

            Console.ReadLine();
        }

        static ITry<CustomerDTO> ProcessData(ITry<Customer> customers)
        {
            return customers.Select(Map);
        }

        static ITry<CustomerDTO> Map(Customer source)
        {
            var dto = new CustomerDTO { CustomerId = source.Id };
            return new Success<CustomerDTO>(dto);
        }
    }
}
