namespace OrderManager.Model
{
    public class Order
    {
        public enum Status
        {
            Shipped,
            Delivered,
            Cancelled,
        }

        public Guid Id { get; }
        public string Product { get; }
        public int Quantity { get; set; }
        public Guid CustomerId { get; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; }
        public DateTime ExpectedDelivery { get; set; }
        public Status OrderStatus { get; set; }

        public Order( string product, int quantity, Guid customerId, string address )
        {
            if ( string.IsNullOrWhiteSpace( product ) )
            {
                throw new Exception( "Название товара не может быть пустым." );
            }

            if ( quantity <= 0 )
            {
                throw new Exception( "Количество должно быть положительным." );
            }

            if ( string.IsNullOrWhiteSpace( address ) )
            {
                throw new Exception( "Адрес доставки не может быть пустым." );
            }

            Id = Guid.NewGuid();
            Product = product.Trim();
            Quantity = quantity;
            CustomerId = customerId;
            Address = address;
            CreatedAt = DateTime.Now;
            ExpectedDelivery = CreatedAt.Date.AddDays( 3 );
            OrderStatus = Status.Shipped;
        }
    }
}