using FighterGame.Domain;
using FighterGame.Domain.Model;
using FighterGame.Domain.Repository;
using Menu.Commands;
using Menu.Core;
using Menu.Infrastructure;

namespace FighterGame.Command
{
    public class BuildFighterCommand : ICommand
    {
        private readonly IMenuRegistry _registry;
        private readonly IFighterRepository _fighterRepository;
        private FighterDto _fighterDto;
        public string Title => "Подтвердить";

        public BuildFighterCommand(
            IMenuRegistry registry,
            IFighterRepository fighterRepository,
            FighterDto fighterDto
        )
        {
            _registry = registry;
            _fighterRepository = fighterRepository;
            _fighterDto = fighterDto;
        }

        public CommandResult Execute()
        {
            _fighterRepository.AddFighter( new FighterBuilder().Build( _fighterDto ) );
            _registry.Remove( "create-fighter" );

            return Results.Back();
        }
    }
}