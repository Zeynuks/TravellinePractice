using CarFactory.Domain.Model;
using CarFactory.Domain.Model.Engine;

namespace CarFactory.Domain.Factory
{
    public class EngineFactory
    {
        public IEngine CreateEngine( EngineType engineType )
        {
            return engineType switch
            {
                EngineType.Petrol => new Petrol(),
                EngineType.Diesel => new Diesel(),
                EngineType.Electric => new Electric(),
                EngineType.Hybrid => new Hybrid(),
                _ => throw new ArgumentOutOfRangeException( nameof( engineType ) )
            };
        }
    }
}