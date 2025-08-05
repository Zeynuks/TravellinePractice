using System.ComponentModel;

namespace CarFactory.Domain.Model.Transmission
{
    public enum TransmissionType
    {
        [Description( "Ручная коробка передач" )]
        Manual,

        [Description( "Автоматическая коробка передач" )]
        Automatic,

        [Description( "Полуавтоматическая коробка передач" )]
        SemiAutomatic,

        [Description( "Вариатор (CVT)" )] Cvt
    }
}