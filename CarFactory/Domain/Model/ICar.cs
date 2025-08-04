using CarFactory.Domain.Model.Body;
using CarFactory.Domain.Model.Engine;
using CarFactory.Domain.Model.Transmission;

namespace CarFactory.Domain.Model
{
    public interface ICar
    {
        public string Number { get; }
        Color Color { get; }
        ITransmission Transmission { get; }
        IEngine Engine { get; }
        IBody Body { get; }

        public int GetMaxSpeed();
    }
}