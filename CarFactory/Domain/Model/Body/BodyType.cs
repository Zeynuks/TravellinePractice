using System.ComponentModel;

namespace CarFactory.Domain.Model.Body
{
    public enum BodyType
    {
        [Description( "Седан" )]
        Sedan,

        [Description( "Хэтчбек" )]
        Hatchback,

        [Description( "Купе" )]
        Coupe,

        [Description( "Внедорожник" )]
        Suv
    }
}