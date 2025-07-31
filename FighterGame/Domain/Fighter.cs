using FighterGame.Domain.Dice;
using FighterGame.Domain.Model.Armor;
using FighterGame.Domain.Model.Class;
using FighterGame.Domain.Model.Races;
using FighterGame.Domain.Model.Weapons;

namespace FighterGame.Domain
{
    public class Fighter : IFighter
    {
        public Guid Id { get; }
        public string Name { get; }
        public int Health { get; private set; }
        public IClass Class { get; }
        public IRace Race { get; }
        public IArmor Armor { get; }
        public IWeapon Weapon { get; }

        public Fighter(
            string fighterName,
            IClass fighterClass,
            IRace fighterRace,
            IArmor fighterArmor,
            IWeapon fighterWeapon
        )
        {
            Id = Guid.NewGuid();
            Name = fighterName;
            Class = fighterClass;
            Race = fighterRace;
            Armor = fighterArmor;
            Weapon = fighterWeapon;
            Health = fighterRace.MaxHealth;
        }

        public int RollInitiative()
        {
            return new DiceTypes.D20().Roll();
        }

        public int RollHealing()
        {
            return Race.HealDice.Roll();
        }

        public int Attack( IFighter target )
        {
            int attackRoll = new DiceTypes.D20().Roll();

            if ( attackRoll == 1 )
            {
                int selfDamage = CalculateDamage( this );
                TakeDamage( selfDamage );
                return -selfDamage;
            }

            bool isCriticalHit = attackRoll == 20;
            bool isHit = isCriticalHit || attackRoll >= target.Armor.ArmorClass;
            if ( !isHit )
            {
                return 0;
            }

            int damage = CalculateDamage( target );
            if ( isCriticalHit ) damage *= 2;

            target.TakeDamage( damage );
            return damage;
        }

        private int CalculateDamage( IFighter target )
        {
            double baseDmg = Weapon.Damage * Class.DamageModify;
            double variation = Random.Shared.NextDouble() * 0.3 - 0.2;
            int dmg = ( int )( baseDmg * ( 1.0 + variation ) );
            if ( Weapon.DamageType == target.Race.DamageResist )
            {
                dmg /= 2;
            }

            return dmg;
        }

        public void TakeDamage( int dmg )
        {
            Health -= dmg;
            if ( Health < 0 )
            {
                Health = 0;
            }
        }

        public void Heal( int amount )
        {
            Health += amount;
            if ( Health > Race.MaxHealth )
            {
                Health = Race.MaxHealth;
            }
        }
    }
}