using FighterGame.Domain;
using FighterGame.Domain.Model;
using FighterGame.Domain.Model.Types;
using Menu.Commands;
using Menu.Core;
using Menu.Infrastructure;
using Menu.Infrastructure.Menu;
using Menu.UI;

namespace FighterGame.Command
{
    public class SelectWeaponCommand: ICommand
    {
        private readonly IUserInterface _ui;
        private readonly IMenuRegistry _registry;
        private readonly FighterDto _fighterDto;
        public string Title { get; private set; }

        public SelectWeaponCommand( IUserInterface ui, IMenuRegistry registry, FighterDto fighterDto )
        {
            _ui = ui;
            _registry = registry;
            _fighterDto = fighterDto;
            Title = $"Выберите оружие ({_fighterDto.Weapon})";
        }

        public CommandResult Execute()
        {
            if ( _registry.TryGet( "select-class", out IMenu? menu ) )
            {
                if ( menu is null )
                {
                    throw new Exception( "Меню не найдено" );
                }

                menu.Title = Title;

                return Results.Navigate( menu.MenuId );
            }
            
            EnumMenu<WeaponType> selectMenu = new( _ui, "select-weapon", value =>
            {
                _fighterDto.Weapon = value;
                Title = $"Выберите класс ({_fighterDto.Weapon})";
            } );
            _registry.Add( selectMenu );
            
            return Results.Navigate( selectMenu.MenuId );
        }
    }
}