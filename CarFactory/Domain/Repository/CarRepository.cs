using CarFactory.Domain.Model;

namespace CarFactory.Domain.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly Dictionary<string, ICar> _cars = new();

        public void AddCar( ICar car )
        {
            if ( car == null )
            {
                throw new ArgumentNullException( nameof( car ), "Некорректная конфигурация." );
            }

            if ( !_cars.TryAdd( car.Number, car ) )
            {
                throw new InvalidOperationException( $"Автомобиль с номером {car.Number} уже существует." );
            }
        }

        public IReadOnlyList<ICar> GetAllCars()
        {
            return _cars.Values.ToList().AsReadOnly();
        }
    }
}