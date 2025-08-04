using CarFactory.Domain.Model;

namespace CarFactory.Domain.Repository
{
    public interface ICarRepository
    {
        public void AddCar( ICar car );
        public IReadOnlyList<ICar> GetAllCars();
    }
}