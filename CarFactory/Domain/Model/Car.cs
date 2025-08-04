using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using CarFactory.Domain.Model.Body;
using CarFactory.Domain.Model.Engine;
using CarFactory.Domain.Model.Transmission;

namespace CarFactory.Domain.Model
{
    public class Car : ICar
    {
        public string Number { get; }
        public Color Color { get; }
        public ITransmission Transmission { get; }
        public IEngine Engine { get; }
        public IBody Body { get; }

        public Car( string number, Color color, ITransmission transmission, IEngine engine, IBody body )
        {
            if ( !IsValidNumber( number ) )
            {
                throw new ValidationException( "Невалидный номер" );
            }

            Number = number;
            Color = color;
            Transmission = transmission;
            Engine = engine;
            Body = body;
        }

        public int GetMaxSpeed()
        {
            double transmissionFactor = ( Transmission.NumberOfGears / 10.0 );
            double bodyWeightFactor = 1 - ( Body.Weight / 3000.0 );

            int maxSpeed = ( int )( Engine.BaseMaxSpeed * transmissionFactor * bodyWeightFactor );

            if ( maxSpeed < 0 )
            {
                throw new InvalidOperationException( "Полученная максимальная скорость не может быть меньше нуля." );
            }

            return maxSpeed;
        }


        private static bool IsValidNumber( string number )
        {
            const string pattern = @"^[A-Z]{1}\d{3}[A-Z]{2}$";
            Regex regex = new( pattern, RegexOptions.IgnoreCase );

            return regex.IsMatch( number );
        }
    }
}