using System;
using Moq;
using Xunit;
using FighterGame.Command;
using FighterGame.Domain;
using FighterGame.Domain.Model;
using Menu.Core;
using Menu.UI;

namespace FighterGame.Tests.Command
{
    public class SelectFighterCommandTests
    {
        private readonly Mock<IUserInterface> _uiMock;

        public SelectFighterCommandTests()
        {
            _uiMock = new Mock<IUserInterface>();
        }

        [Fact]
        public void Execute_WhenFighterNotSelected_ShouldAddParticipantToBattleEngine()
        {
            // Arrange
            Guid fighterId = Guid.NewGuid();
            Mock<IFighter> fighterMock = new();
            fighterMock.SetupGet( f => f.Id ).Returns( fighterId );
            fighterMock.SetupGet( f => f.Name ).Returns( "Ivan" );

            BattleEngine battleEngine = new( _uiMock.Object );
            SelectFighterCommand cmd = new( _uiMock.Object, battleEngine, fighterMock.Object );

            // Act
            CommandResult result = cmd.Execute();

            // Assert
            Assert.True( battleEngine.ContainsParticipant( fighterId ) );
            Assert.Equal( "Ivan (selected)", cmd.Title );
            Assert.Equal( CommandResults.Continue(), result );
        }

        [Fact]
        public void Execute_WhenFighterAlreadySelected_ShouldRemoveParticipantFromBattleEngine()
        {
            // Arrange
            Guid fighterId = Guid.NewGuid();
            Mock<IFighter> fighterMock = new();
            fighterMock.SetupGet( f => f.Id ).Returns( fighterId );
            fighterMock.SetupGet( f => f.Name ).Returns( "Maria" );

            BattleEngine battleEngine = new( _uiMock.Object );
            battleEngine.AddParticipant( fighterMock.Object );

            SelectFighterCommand cmd = new( _uiMock.Object, battleEngine, fighterMock.Object );

            // Act
            CommandResult result = cmd.Execute();

            // Assert
            Assert.False( battleEngine.ContainsParticipant( fighterId ) );
            Assert.Equal( "Maria", cmd.Title );
            Assert.Equal( CommandResults.Continue(), result );
        }
    }
}