using FighterGame.Domain;
using FighterGame.Domain.Model;
using FighterGame.Domain.Repository;
using Menu.Commands;
using Menu.Core;
using Menu.Infrastructure;
using Menu.UI;

namespace FighterGame.Command
{
    public class BuildFighterCommand : ICommand
    {
        private const string MenuId = "create-fighter";
        private readonly IUserInterface _ui;
        private readonly IMenuRegistry _registry;
        private readonly IFighterRepository _fighterRepository;
        private readonly FighterDto _fighterDto;
        private readonly FighterBuilder _fighterBuilder = new();
        public string Title => "Подтвердить";

        public BuildFighterCommand(
            IUserInterface ui,
            IMenuRegistry registry,
            IFighterRepository fighterRepository,
            FighterDto fighterDto )
        {
            _ui = ui;
            _registry = registry;
            _fighterRepository = fighterRepository;
            _fighterDto = fighterDto;
        }

        public CommandResult Execute()
        {
            try
            {
                _fighterRepository.AddFighter( _fighterBuilder.Build( _fighterDto ) );
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