using FighterGame.Domain.Model.Armor;
using FighterGame.Domain.Model.Class;
using FighterGame.Domain.Model.Races;
using FighterGame.Domain.Model.Weapons;

namespace FighterGame.Domain
{
    public interface IFighter
    {
        string Name { get; }
        int Health { get; }
        IClass Class { get; }
        IRace Race { get; }
        IArmor Armor { get; }
        IWeapon Weapon { get; }

        int RollInitiative();
        int RollHealing();
        int Attack( IFighter target );
        public void TakeDamage( int dmg );
        void Heal( int amount );
    }
}