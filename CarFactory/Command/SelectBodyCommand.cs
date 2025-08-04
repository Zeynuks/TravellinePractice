using CarFactory.Domain;
using CarFactory.Domain.Model.Body;
using Menu.Commands;
using Menu.Core;
using Menu.Infrastructure;
using Menu.Infrastructure.Menu;
using Menu.UI;

namespace CarFactory.Command
{
    public class SelectBodyCommand : ICommand
    {
        private readonly IUserInterface _ui;
        private readonly IMenuRegistry _registry;
        private readonly CarDto _carDto;
        public string Title { get; private set; }
        private const string MenuId = "select-body";

        public SelectBodyCommand( IUserInterface ui, IMenuRegistry registry, CarDto carDto )
        {
            _ui = ui;
            _registry = registry;
            _carDto = carDto;
            Title = $"Выберите корпус ({_carDto.Body})";
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

                EnumMenu<BodyType> selectMenu = new( _ui, MenuId, value =>
                {
                    _carDto.Body = value;
                    Title = $"Выберите корпус ({_carDto.Body})";
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