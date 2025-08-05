using System.ComponentModel;

namespace FighterGame.Domain.Model.Types
{
    public enum ArmorType
    {
        [Description( "Кольчуга" )] ChainMail,

        [Description( "Кожаная броня" )] LeatherArmor,

        [Description( "Без брони" )] NullArmor,

        [Description( "Латная броня" )] PlateArmor,

        [Description( "Стёганная броня" )] PaddedArmor
    }
}