using FighterGame.Domain.Model;
using Menu.Commands;
using Menu.Core;
using Menu.UI;

namespace FighterGame.Command
{
    public class SelectNameCommand : ICommand
    {
        private readonly IUserInterface _ui;
        private readonly FighterDto _fighterDto;
        
        public string Title { get; private set; }

        public SelectNameCommand( IUserInterface ui, FighterDto fighterDto )
        {
            _ui = ui;
            _fighterDto = fighterDto;
            Title = $"Выберите имя ({_fighterDto.Name})";
        }

        public CommandResult Execute()
        {
            string? name = _ui.ReadLine( $"{Title}: " );

            if ( string.IsNullOrWhiteSpace( name ) )
            {
                return CommandResults.Continue();
            }

            _fighterDto.Name = name;
            Title = $"Выберите имя ({_fighterDto.Name})";
            return CommandResults.Continue();
        }
    }
}