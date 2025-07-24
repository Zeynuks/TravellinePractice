using OrderManager.Core.Model;

namespace OrderManager.Core.Repository
{
    public class OrderRepository
    {
        private readonly Dictionary<Guid, Order> _orders = new();

        public IReadOnlyList<Order> GetOrdersByCustomerId( Guid customerId )
        {
            return _orders.Values.Where( order => order.CustomerId == customerId ).ToList();
        }

        public Order? GetOrderById( Guid orderId )
        {
            _orders.TryGetValue( orderId, out Order? order );

            return order;
        }

        public void UpdateOrder( Order order )
        {
            _orders[ order.Id ] = order;
        }

        public bool AddOrder( Order order )
        {
            return _orders.TryAdd( order.Id, order );
        }

        public void DeleteOrdersByCustomerId( Guid customerId )
        {
            List<Guid> idsToRemove = _orders.Values
                .Where( order => order.CustomerId == customerId )
                .Select( order => order.Id )
                .ToList();
            
            foreach ( Guid id in idsToRemove )
            {
                _orders.Remove( id );
            }
        }
    }
}