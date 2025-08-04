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
    public class SelectDamageTypeCommand : ICommand
    {
        private readonly IUserInterface _ui;
        private readonly IMenuRegistry _registry;
        private readonly FighterDto _fighterDto;
        public string Title { get; private set; }
        private const string MenuId = "select-damage";

        public SelectDamageTypeCommand( IUserInterface ui, IMenuRegistry registry, FighterDto fighterDto )
        {
            _ui = ui;
            _registry = registry;
            _fighterDto = fighterDto;
            Title = $"Выберите желаемый тип урона ({_fighterDto.Damage})";
        }

        public CommandResult Execute()
        {
            if ( _registry.TryGet( MenuId, out IMenu? menu ) )
            {
                if ( menu is null )
                {
                    throw new Exception( "Меню не найдено" );
                }

                menu.Title = Title;

                return Results.Navigate( menu.MenuId );
            }

            EnumMenu<DamageType> selectMenu = new( _ui, MenuId, value =>
            {
                _fighterDto.Damage = value;
                Title = $"Выберите желаемый тип урона ({_fighterDto.Damage})";
            } );
            _registry.Add( selectMenu );

            return Results.Navigate( selectMenu.MenuId );
        }
    }
}