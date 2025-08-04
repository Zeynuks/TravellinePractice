using CarFactory.Domain.Model;
using CarFactory.Domain.Model.Body;
using CarFactory.Domain.Model.Engine;
using CarFactory.Domain.Model.Transmission;

namespace CarFactory.Domain
{
    public class CarDto
    {
        public string Number { get; set; } = "A000AA";
        public Color Color { get; set; } = Color.Black;
        public BodyType Body { get; set; } = BodyType.Coupe;
        public EngineType Engine { get; set; } = EngineType.Petrol;
        public TransmissionType Transmission { get; set; } = TransmissionType.Automatic;
    }
}