using CarFactory.Domain.Factory;
using CarFactory.Domain.Model;
using CarFactory.Domain.Model.Body;
using CarFactory.Domain.Model.Engine;
using CarFactory.Domain.Model.Transmission;

namespace CarFactory.Domain
{
    public class CarBuilder
    {
        private readonly TransmissionFactory _transmissionFactory = new();
        private readonly EngineFactory _engineFactory = new();
        private readonly BodyFactory _bodyFactory = new();

        public ICar Build( CarDto carDto )
        {
            ITransmission transmission = _transmissionFactory.CreateTransmission( carDto.Transmission );
            IEngine engine = _engineFactory.CreateEngine( carDto.Engine );
            IBody body = _bodyFactory.CreateBody( carDto.Body );

            return new Car( carDto.Number, carDto.Color, transmission, engine, body );
        }
    }
}