using System.Collections.Generic;
using System.Web;
using adapter_patterns;

namespace Service
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICacheStorage _cacheStorage;


        // source - https://dotnetcodr.com/2013/04/25/design-patterns-and-practices-in-net-the-adapter-pattern/
        public CustomerService(ICustomerRepository customerRepository, ICacheStorage cacheStorage)
        {
            _customerRepository = customerRepository;
            _cacheStorage = cacheStorage;
        }

        public IList<Customer> GetACustomers()
        {
            IList<Customer> customers;
            string storageKey = "GetAllCustomers";
            customers = _cacheStorage.Retrieve<List<Customer>>(storageKey);
            if (customers == null)
            {
                customers = _customerRepository.GetCustomers();
                HttpContext.Current.Cache.Insert(storageKey, customers);
            }

            return customers;
        }
    }
}
