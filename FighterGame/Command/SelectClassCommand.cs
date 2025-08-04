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
    public class SelectClassCommand : ICommand
    {
        private readonly IUserInterface _ui;
        private readonly IMenuRegistry _registry;
        private readonly FighterDto _fighterDto;
        public string Title { get; private set; }

        public SelectClassCommand( IUserInterface ui, IMenuRegistry registry, FighterDto fighterDto )
        {
            _ui = ui;
            _registry = registry;
            _fighterDto = fighterDto;
            Title = $"Выберите класс ({_fighterDto.Class})";
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

            EnumMenu<ClassType> selectMenu = new( _ui, "select-class", value =>
            {
                _fighterDto.Class = value;
                Title = $"Выберите класс ({_fighterDto.Class})";
            } );
            _registry.Add( selectMenu );

            return Results.Navigate( selectMenu.MenuId );
        }
    }
}