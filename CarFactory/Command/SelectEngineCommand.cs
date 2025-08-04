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
        private readonly IUserInterface _ui;
        private readonly IMenuRegistry _registry;
        private readonly CarDto _carDto;
        public string Title { get; private set; }
        private const string MenuId = "select-engine";

        public SelectEngineCommand( IUserInterface ui, IMenuRegistry registry, CarDto carDto )
        {
            _ui = ui;
            _registry = registry;
            _carDto = carDto;
            Title = $"Выберите двигатель ({_carDto.Engine})";
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

                EnumMenu<EngineType> selectMenu = new( _ui, MenuId, value =>
                {
                    _carDto.Engine = value;
                    Title = $"Выберите двигатель ({_carDto.Engine})";
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