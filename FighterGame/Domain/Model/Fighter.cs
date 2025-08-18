using FighterGame.Domain.Dice;
using FighterGame.Domain.Model.Armor;
using FighterGame.Domain.Model.Class;
using FighterGame.Domain.Model.Races;
using FighterGame.Domain.Model.Weapons;

namespace FighterGame.Domain.Model
{
    public class Fighter : IFighter
    {
        private const double MinDamageVariation = -0.2;
        private const double MaxDamageVariation = 0.3;
        private const int CriticalLuckRoll = 20;
        private const int CriticalUnLuckRoll = 1;
        private const double ResistDamageMultiplier = 0.5;

        public Guid Id { get; }
        public string Name { get; }
        public int Health { get; private set; }
        public IClass Class { get; }
        public IRace Race { get; }
        public IArmor Armor { get; }
        public IWeapon Weapon { get; }

        public Fighter( string fighterName,
            IClass fighterClass,
            IRace fighterRace,
            IArmor fighterArmor,
            IWeapon fighterWeapon )
        {
            Id = Guid.NewGuid();
            if ( string.IsNullOrWhiteSpace( fighterName ) )
            {
                throw new InvalidOperationException( "Имя не может быть пустым." );
            }

            Name = fighterName;
            Class = fighterClass;
            Race = fighterRace;
            Armor = fighterArmor;
            Weapon = fighterWeapon;
            Health = fighterRace.MaxHealth;
        }

        public int RollInitiative()
        {
            return new D20().Roll();
        }

        public int RollHealing()
        {
            return Race.HealDice.Roll();
        }

        public int Attack( IFighter target )
        {
            int attackRoll = new D20().Roll();

            if ( attackRoll == CriticalUnLuckRoll )
            {
                int selfDamage = CalculateDamage( this );
                TakeDamage( selfDamage );
                return -selfDamage;
            }

            bool isCriticalHit = attackRoll == CriticalLuckRoll;
            bool isHit = isCriticalHit || attackRoll >= target.Armor.ArmorClass;
            if ( !isHit )
            {
                return 0;
            }

            int damage = CalculateDamage( target );
            if ( isCriticalHit )
            {
                damage *= 2;
            }

            target.TakeDamage( damage );
            return damage;
        }

        private int CalculateDamage( IFighter target )
        {
            double baseDmg = Weapon.Damage.Roll() * Class.DamageModify;
            double variation = Random.Shared.NextDouble() * MaxDamageVariation + MinDamageVariation;
            int dmg = ( int )( baseDmg * ( 1.0 + variation ) );

            if ( Weapon.DamageType == target.Race.DamageResist )
            {
                dmg = ( int )( dmg * ResistDamageMultiplier );
            }

            return dmg;
        }

        public void TakeDamage( int dmg )
        {
            if ( dmg < 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( dmg ), "Урон не может быть отрицательным." );
            }

            Health -= dmg;
            if ( Health < 0 )
            {
                Health = 0;
            }
        }

        public void Heal( int amount )
        {
            if ( amount < 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( amount ),
                    "Значение лечения не может быть отрицательным." );
            }

            Health += amount;
            if ( Health > Race.MaxHealth )
            {
                Health = Race.MaxHealth;
            }
        }
    }
}