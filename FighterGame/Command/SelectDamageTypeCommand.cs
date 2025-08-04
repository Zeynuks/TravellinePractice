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
        private const string MenuId = "select-damage";
        private readonly IUserInterface _ui;
        private readonly IMenuRegistry _registry;
        private readonly FighterDto _fighterDto;
        public string Title { get; private set; }

        public SelectDamageTypeCommand( IUserInterface ui, IMenuRegistry registry, FighterDto fighterDto )
        {
            _ui = ui;
            _registry = registry;
            _fighterDto = fighterDto;
            Title = $"Выберите желаемый тип урона ({_fighterDto.Damage})";
        }

        public CommandResult Execute()
        {
            try
            {
                IMenu? menu = _registry.TryGet( MenuId, out menu ) ? menu : null;
                if ( menu != null )
                {
                    menu.Title = Title;
                    return CommandResults.Navigate( menu.MenuId );
                }

                EnumMenu<DamageType> selectMenu = new( _ui, MenuId, value =>
                {
                    _fighterDto.Damage = value;
                    Title = $"Выберите желаемый тип урона ({_fighterDto.Damage})";
                } );
                _registry.Add( selectMenu );

                return CommandResults.Navigate( selectMenu.MenuId );
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( $"Ошибка: {ex.Message}" );
                return CommandResults.Continue();
            }
        }
    }
}