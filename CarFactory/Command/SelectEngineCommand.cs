using CarFactory.Domain;
using CarFactory.Domain.Model.Engine;
using Menu.Commands;
using Menu.Core;
using Menu.Infrastructure;
using Menu.Infrastructure.Menu;
using Menu.UI;

namespace CarFactory.Command
{
    public class SelectEngineCommand : ICommand
    {
        private const string MenuId = "select-engine";

        private readonly IUserInterface _ui;
        private readonly IMenuRegistry _registry;
        private readonly CarDto _carDto;

        public string Title { get; private set; }

        public SelectEngineCommand( IUserInterface ui, IMenuRegistry registry, CarDto carDto )
        {
            _ui = ui;
            _registry = registry;
            _carDto = carDto;
            Title = $"Выберите двигатель " +
                    $"({EnumParser.GetEnumDescription( _carDto.Engine ) ?? _carDto.Engine.ToString()})";
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

                EnumMenu<EngineType> selectMenu = new( _ui, MenuId, value =>
                {
                    _carDto.Engine = value;
                    Title = $"Выберите двигатель " +
                            $"({EnumParser.GetEnumDescription( _carDto.Engine ) ?? _carDto.Engine.ToString()})";
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