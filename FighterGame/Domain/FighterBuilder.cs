using FighterGame.Domain.Factory;
using FighterGame.Domain.Model.Armor;
using FighterGame.Domain.Model.Class;
using FighterGame.Domain.Model.Races;
using FighterGame.Domain.Model.Types;
using FighterGame.Domain.Model.Weapons;

namespace FighterGame.Domain
{
    public class FighterBuilder
    {
        private readonly ClassFactory _classFactory = new();
        private readonly RaceFactory _raceFactory = new();
        private readonly ArmorFactory _armorFactory = new();
        private readonly WeaponFactory _weaponFactory = new();

        public IFighter Build( string name, ClassType classType, RaceType raceType, ArmorType armorType, WeaponType weaponType, DamageType damageType )
        {
            IClass fighterClass = _classFactory.CreateClass( classType );
            IRace fighterRace = _raceFactory.CreateRace( raceType );
            IArmor fighterArmor = _armorFactory.CreateArmor( armorType );
            IWeapon fighterWeapon = _weaponFactory.CreateWeapon( weaponType, damageType );

            return new Fighter( name, fighterClass, fighterRace, fighterArmor, fighterWeapon );
        }
    }
}