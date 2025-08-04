using CarFactory.Domain.Model;
using CarFactory.Domain.Model.Body;
using CarFactory.Domain.Model.Engine;
using CarFactory.Domain.Model.Transmission;

namespace CarFactory.Domain
{
    public class CarDto
    {
        public string Number { get; set; }
        public Color Color { get; set; }
        public BodyType Body { get; set; }
        public EngineType Engine { get; set; }
        public TransmissionType Transmission { get; set; }

        public CarDto()
        {
            Number = "A000AA";
            Color = Color.Black;
            Body = BodyType.Coupe;
            Engine = EngineType.Petrol;
            Transmission = TransmissionType.Automatic;
        }
    }
}