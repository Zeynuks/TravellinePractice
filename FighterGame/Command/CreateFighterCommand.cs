using FighterGame.Domain;
using FighterGame.Domain.Model.Armor;
using FighterGame.Domain.Model.Class;
using FighterGame.Domain.Model.Races;
using FighterGame.Domain.Model.Types;
using FighterGame.Domain.Model.Weapons;
using Menu.Commands;
using Menu.Core;
using Menu.Infrastructure.Menu;
using Menu.UI;

namespace FighterGame.Command
{
    public sealed class CreateFighterCommand : ICommand
    {
        public string Title => "Добавить нового бойца на арену";

        private readonly IUserInterface _ui;
        private readonly FighterRepository _fighterRepository;
        private readonly FighterBuilder _fighterBuilder;

        public CreateFighterCommand(
            IUserInterface ui,
            FighterRepository fighterRepository,
            FighterBuilder fighterBuilder
        )
        {
            _ui = ui;
            _fighterRepository = fighterRepository;
            _fighterBuilder = fighterBuilder;
        }

        public CommandResult Execute()
        {
            string name = _ui.ReadLine( "Введите имя персонажа (Алекс): " ) ?? "Алекс";
            _ui.Clear();

            ClassType classType = EnumSelection<ClassType>(
                "create-fighter-class",
                "Выберите класс из списка:"
            );

            RaceType raceType = EnumSelection<RaceType>(
                "create-fighter-race",
                "Выберите расу из списка:"
            );

            ArmorType armorType = EnumSelection<ArmorType>(
                "create-fighter-armor",
                "Выберите броню из списка:"
            );

            WeaponType weaponType = EnumSelection<WeaponType>(
                "create-fighter-weapon",
                "Выберите оружие из списка:"
            );

            DamageType damageType = EnumSelection<DamageType>(
                "create-fighter-damage",
                "Выберите зачарование (тип урона):"
            );

            IFighter fighter = _fighterBuilder.Build( name, classType, raceType, armorType, weaponType, damageType );

            _fighterRepository.AddFighter( fighter );

            return Results.Continue();
        }

        private TEnum EnumSelection<TEnum>( string menuId, string title ) where TEnum : Enum
        {
            TEnum selected = default!;

            new EnumMenu<TEnum>(
                _ui,
                menuId,
                value => selected = value,
                title
            ).Execute();

            return selected;
        }
    }
}