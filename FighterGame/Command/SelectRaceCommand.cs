using FighterGame.Domain.Model;
using FighterGame.Domain.Model.Types;
using Menu.Commands;
using Menu.Core;
using Menu.Infrastructure;
using Menu.Infrastructure.Menu;
using Menu.UI;

namespace FighterGame.Command
{
    public class SelectRaceCommand : ICommand
    {
        private const string MenuId = "select-race";
        private readonly IUserInterface _ui;
        private readonly IMenuRegistry _registry;
        private readonly FighterDto _fighterDto;
        public string Title { get; private set; }

        public SelectRaceCommand( IUserInterface ui, IMenuRegistry registry, FighterDto fighterDto )
        {
            _ui = ui;
            _registry = registry;
            _fighterDto = fighterDto;
            Title = $"Выберите расу ({_fighterDto.Race})";
        }

        public CommandResult Execute()
        {
            try
            {
                IMenu? menu = _registry.TryGet( MenuId, out menu ) ? menu : null;
                if ( menu != null )
                {
                    menu.Title = Title;
                    return Results.Navigate( menu.MenuId );
                }

                EnumMenu<RaceType> selectMenu = new( _ui, MenuId, value =>
                {
                    _fighterDto.Race = value;
                    Title = $"Выберите расу ({_fighterDto.Race})";
                } );
                _registry.Add( selectMenu );

                return Results.Navigate( selectMenu.MenuId );
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( $"Ошибка: {ex.Message}" );
                return Results.Continue();
            }
        }
    }
}