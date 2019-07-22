using System.Collections.Generic;

namespace adapter_patterns
{
    public class Customer : ICustomerRepository
    {
        public IList<Customer> GetCustomers()
        {
            //simulate database operation
            return new List<Customer>();
        }
    }
    public interface ICustomerRepository
    {
        IList<Customer> GetCustomers();
    }
}
