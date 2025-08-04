using CarFactory.Domain;
using CarFactory.Domain.Model.Engine;
using CarFactory.Domain.Model.Transmission;
using Menu.Commands;
using Menu.Core;
using Menu.Infrastructure;
using Menu.Infrastructure.Menu;
using Menu.UI;

namespace CarFactory.Command
{
    public class SelectTransmissionCommand : ICommand
    {
        private const string MenuId = "select-transmission";
        private readonly IUserInterface _ui;
        private readonly CarDto _carDto;
        private readonly IMenuRegistry _registry;
        public string Title { get; private set; }

        public SelectTransmissionCommand( IUserInterface ui, IMenuRegistry registry, CarDto carDto )
        {
            _ui = ui;
            _registry = registry;
            _carDto = carDto;
            Title = $"Выберите коробку передач ({_carDto.Transmission})";
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

                EnumMenu<TransmissionType> selectMenu = new( _ui, MenuId, value =>
                {
                    _carDto.Transmission = value;
                    Title = $"Выберите коробку передач ({_carDto.Transmission})";
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