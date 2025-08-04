using CarFactory.Domain.Model;
using CarFactory.Domain.Model.Body;

namespace CarFactory.Domain.Factory
{
    public class BodyFactory
    {
        public IBody CreateBody( BodyType bodyType )
        {
            return bodyType switch
            {
                BodyType.Sedan => new Sedan(),
                BodyType.Hatchback => new Hatchback(),
                BodyType.Coupe => new Coupe(),
                BodyType.Suv => new SUV(),
                _ => throw new ArgumentOutOfRangeException( nameof( bodyType ) )
            };
        }
    }
}