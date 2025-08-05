using System.ComponentModel;

namespace FighterGame.Domain.Model.Types
{
    public enum WeaponType
    {
        [Description( "Без оружия" )] Unarmed,

        [Description( "Меч" )] Sword,

        [Description( "Копьё" )] Spear,

        [Description( "Кинжал" )] Dagger,

        [Description( "Дубина" )] Club
    }
}