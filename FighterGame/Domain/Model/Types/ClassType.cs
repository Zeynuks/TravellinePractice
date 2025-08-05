using System.ComponentModel;

namespace FighterGame.Domain.Model.Types
{
    public enum ClassType
    {
        [Description( "Гладиатор" )] Gladiator,

        [Description( "Рыцарь" )] Knight,

        [Description( "Разбойник" )] Rogue
    }
}