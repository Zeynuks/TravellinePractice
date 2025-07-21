namespace OrderManager.Model
{
    public class Customer
    {
        public Guid Id { get; }
        public string Name { get; }

        public Customer( string? name )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new Exception( "Имя не может быть пустым." );
            }

            Id = Guid.NewGuid();
            Name = name.Trim();
        }
    }
}