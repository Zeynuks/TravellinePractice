using FighterGame.Domain.Factory;
using FighterGame.Domain.Model;
using FighterGame.Domain.Model.Armor;
using FighterGame.Domain.Model.Class;
using FighterGame.Domain.Model.Races;
using FighterGame.Domain.Model.Weapons;

namespace FighterGame.Domain
{
    public class FighterBuilder
    {
        private readonly ClassFactory _classFactory = new();
        private readonly RaceFactory _raceFactory = new();
        private readonly ArmorFactory _armorFactory = new();
        private readonly WeaponFactory _weaponFactory = new();

        public IFighter Build( FighterDto fighterDto )
        {
            IClass fighterClass = _classFactory.CreateClass( fighterDto.Class );
            IRace fighterRace = _raceFactory.CreateRace( fighterDto.Race );
            IArmor fighterArmor = _armorFactory.CreateArmor( fighterDto.Armor );
            IWeapon fighterWeapon = _weaponFactory.CreateWeapon( fighterDto.Weapon, fighterDto.Damage );

            return new Fighter( fighterDto.Name, fighterClass, fighterRace, fighterArmor, fighterWeapon );
        }
    }
}