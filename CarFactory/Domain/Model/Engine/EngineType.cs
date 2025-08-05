using System.ComponentModel;

namespace CarFactory.Domain.Model.Engine
{
    public enum EngineType
    {
        [Description( "Бензиновый двигатель" )]
        Petrol,

        [Description( "Дизельный двигатель" )]
        Diesel,

        [Description( "Электрический двигатель" )]
        Electric,

        [Description( "Гибридный двигатель" )]
        Hybrid
    }
}