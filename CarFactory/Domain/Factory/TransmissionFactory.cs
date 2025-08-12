using CarFactory.Domain.Model.Transmission;

namespace CarFactory.Domain.Factory
{
    public class TransmissionFactory
    {
        public ITransmission CreateTransmission( TransmissionType transmissionType )
        {
            return transmissionType switch
            {
                TransmissionType.Manual => new Manual(),
                TransmissionType.Automatic => new Automatic(),
                TransmissionType.SemiAutomatic => new SemiAutomatic(),
                TransmissionType.Cvt => new Cvt(),
                _ => throw new ArgumentOutOfRangeException( nameof( transmissionType ) )
            };
        }
    }
}