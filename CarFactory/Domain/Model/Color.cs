using System.ComponentModel;

namespace CarFactory.Domain.Model
{
    public enum Color
    {
        [Description( "Белый" )] White,

        [Description( "Чёрный" )] Black,

        [Description( "Красный" )] Red,

        [Description( "Зелёный" )] Green,

        [Description( "Синий" )] Blue,

        [Description( "Жёлтый" )] Yellow,

        [Description( "Оранжевый" )] Orange,

        [Description( "Фиолетовый" )] Purple
    }
}