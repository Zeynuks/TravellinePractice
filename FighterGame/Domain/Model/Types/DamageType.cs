using System.ComponentModel;

namespace FighterGame.Domain.Model.Types
{
    public enum DamageType
    {
        [Description( "Физический урон" )] Physical,

        [Description( "Огненный урон" )] Fire,

        [Description( "Некротический урон" )] Necrotic,

        [Description( "Радиационный урон" )] Radiant
    }
}