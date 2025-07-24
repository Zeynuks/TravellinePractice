using OrderManager.Core.Model;

namespace OrderManager.Core.Repository
{
    public class CustomerRepository
    {
        private readonly Dictionary<Guid, Customer> _customers = new();

        public IReadOnlyList<Customer> GetAllCustomers()
        {
            return _customers.Values.ToList();
        }

        public Customer? GetCustomerById( Guid id )
        {
            _customers.TryGetValue( id, out Customer? customer );

            return customer;
        }

        public bool AddCustomer( Customer customer )
        {
            return _customers.TryAdd( customer.Id, customer );
        }

        public bool RemoveCustomer( Guid customerId )
        {
            return _customers.Remove( customerId );
        }
    }
}