using OrderManager.Core.Model;
using OrderManager.Core.Repository;

namespace OrderManager.Core.Service
{
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService( CustomerRepository customerRepository )
        {
            _customerRepository = customerRepository;
        }

        public IReadOnlyList<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAllCustomers();
        }

        public Customer? GetCustomerById( Guid customerId )
        {
            return _customerRepository.GetCustomerById( customerId );
        }

        public Guid CreateCustomer( string? name )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new Exception( "Имя клиента не может быть пустым." );
            }

            Customer newCustomer = new( name );

            if ( _customerRepository.AddCustomer( newCustomer ) )
            {
                return newCustomer.Id;
            }

            throw new Exception( "Не удалось создать нового клиента." );
        }

        public bool RemoveCustomer( Guid customerId )
        {
            Customer? customer = _customerRepository.GetCustomerById( customerId );
            if ( customer == null )
            {
                throw new Exception( "Клиент не найден." );
            }

            return _customerRepository.RemoveCustomer( customerId );
        }
    }
}