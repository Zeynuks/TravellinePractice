using System.Drawing;
using CarFactory.Domain;
using CarFactory.Domain.Model.Body;
using Menu.Commands;
using Menu.Core;
using Menu.Infrastructure;
using Menu.Infrastructure.Menu;
using Menu.UI;
using Color = CarFactory.Domain.Model.Color;

namespace CarFactory.Command
{
    public class SelectColorCommand : ICommand
    {
        private readonly IUserInterface _ui;
        private readonly IMenuRegistry _registry;
        private readonly CarDto _carDto;
        public string Title { get; private set; }
        private const string MenuId = "select-color";

        public SelectColorCommand( IUserInterface ui, IMenuRegistry registry, CarDto carDto )
        {
            _ui = ui;
            _registry = registry;
            _carDto = carDto;
            Title = $"Выберите цвет ({_carDto.Color})";
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

                EnumMenu<Color> selectMenu = new( _ui, MenuId, value =>
                {
                    _carDto.Color = value;
                    Title = $"Выберите цвет ({_carDto.Color})";
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