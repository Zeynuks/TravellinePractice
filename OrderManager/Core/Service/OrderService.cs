using OrderManager.Core.Model;
using OrderManager.Core.Repository;

namespace OrderManager.Core.Service
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService( OrderRepository orderRepository )
        {
            _orderRepository = orderRepository;
        }

        public IReadOnlyList<Order> GetOrdersByCustomerId( Guid customerId )
        {
            IReadOnlyList<Order> orders = _orderRepository.GetOrdersByCustomerId( customerId );
            if ( orders is null || !orders.Any() )
            {
                throw new Exception( "У вас нет заказов." );
            }

            return orders;
        }

        public void DeleteOrder( Guid orderId )
        {
            Order? order = _orderRepository.GetOrderById( orderId );
            if ( order is null )
            {
                throw new Exception( "Заказ не найден." );
            }

            if ( order.OrderStatus == Order.Status.Cancelled || order.OrderStatus == Order.Status.Delivered )
            {
                throw new Exception( "Этот заказ нельзя отменить, он уже доставлен или отменен." );
            }

            order.OrderStatus = Order.Status.Cancelled;
            _orderRepository.UpdateOrder( order );
        }

        public Order GetOrderById( Guid orderId )
        {
            Order? order = _orderRepository.GetOrderById( orderId );
            if ( order is null )
            {
                throw new Exception( "Заказ не найден." );
            }

            return order;
        }

        public void UpdateOrder( Guid orderId, DateTime newDate, string? newAddress )
        {
            Order? order = _orderRepository.GetOrderById( orderId );
            if ( order is null )
            {
                throw new Exception( "Заказ не найден." );
            }

            if ( order.OrderStatus == Order.Status.Delivered || order.OrderStatus == Order.Status.Cancelled )
            {
                throw new Exception( "Этот заказ нельзя обновить, он уже доставлен или отменен." );
            }

            if ( newDate < DateTime.Now || order.ExpectedDelivery > newDate )
            {
                throw new Exception(
                    "Дата доставки не может быть раньше, чем когда вы сделали заказ или текущей даты доставки." );
            }

            order.ExpectedDelivery = newDate;
            if ( !string.IsNullOrWhiteSpace( newAddress ) )
            {
                order.Address = newAddress;
            }

            _orderRepository.UpdateOrder( order );
        }

        public Order CreateOrder( Guid customerId, string? product, int quantity, string? address )
        {
            if ( string.IsNullOrWhiteSpace( product ) )
            {
                throw new Exception( "Название товара не может быть пустым." );
            }

            if ( quantity <= 0 )
            {
                throw new Exception( "Количество товара должно быть больше нуля." );
            }

            if ( string.IsNullOrWhiteSpace( address ) )
            {
                throw new Exception( "Адрес доставки не может быть пустым." );
            }

            Order newOrder = new( product, quantity, customerId, address );
            if ( _orderRepository.AddOrder( newOrder ) )
            {
                return newOrder;
            }

            throw new Exception( "Не удалось создать новый заказ." );
        }

        public void DeleteOrdersByCustomerId( Guid customerId )
        {
            _orderRepository.DeleteOrdersByCustomerId( customerId );
        }
    }
}