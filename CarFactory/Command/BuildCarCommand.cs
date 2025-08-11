using CarFactory.Domain;
using CarFactory.Domain.Repository;
using Menu.Commands;
using Menu.Core;
using Menu.Infrastructure;
using Menu.UI;

namespace CarFactory.Command
{
    public class BuildCarCommand : ICommand
    {
        private const string MenuId = "create-car";

        private readonly IUserInterface _ui;
        private readonly IMenuRegistry _registry;
        private readonly ICarRepository _repository;
        private readonly CarDto _carDto;

        public string Title => "Подтвердить";

        public BuildCarCommand( IUserInterface ui, IMenuRegistry registry, ICarRepository repository, CarDto carDto )
        {
            _ui = ui;
            _registry = registry;
            _repository = repository;
            _carDto = carDto;
        }

        public CommandResult Execute()
        {
            try
            {
                _repository.AddCar( new CarBuilder().Build( _carDto ) );
                _registry.Remove( MenuId );

                return CommandResults.Back();
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( $"Ошибка: {ex.Message}" );
                return CommandResults.Back();
            }
        }
    }
}