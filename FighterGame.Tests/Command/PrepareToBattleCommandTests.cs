using Xunit;
using Moq;
using FighterGame.Command;
using FighterGame.Domain.Model;
using FighterGame.Domain.Repository;
using Menu.Core;
using Menu.Infrastructure;
using Menu.Infrastructure.Menu;
using Menu.UI;

namespace FighterGame.Tests.Command
{
    public class PrepareToBattleCommandTests
    {
        private readonly Mock<IUserInterface> _uiMock;
        private readonly Mock<IMenuRegistry> _registryMock;
        private readonly Mock<IFighterRepository> _fighterRepositoryMock;
        private readonly PrepareToBattleCommand _sut;

        public PrepareToBattleCommandTests()
        {
            _uiMock = new Mock<IUserInterface>();
            _registryMock = new Mock<IMenuRegistry>();
            _fighterRepositoryMock = new Mock<IFighterRepository>();
            _sut = new PrepareToBattleCommand( _uiMock.Object, _registryMock.Object, _fighterRepositoryMock.Object );
        }

        [Fact]
        public void Execute_WhenNoFightersFound_ShouldDisplayErrorAndContinue()
        {
            // Arrange
            _fighterRepositoryMock.Setup( r => r.GetAllFighters() ).Returns( new List<IFighter>() );

            // Act
            CommandResult result = _sut.Execute();

            // Assert
            _uiMock.Verify( ui => ui.WriteLine( It.Is<string>( msg =>
                msg.Contains( "Ошибка: Бойцов не обнаружено." ) ) ), Times.Once );
            Assert.Equal( CommandResults.Continue(), result );
        }

        [Fact]
        public void Execute_WhenFightersExist_ShouldAddFightersToMenuAndReturnCommandResult()
        {
            // Arrange
            List<IFighter> fighters =
            [
                new Mock<IFighter>().Object,
                new Mock<IFighter>().Object
            ];
            _fighterRepositoryMock.Setup( r => r.GetAllFighters() ).Returns( fighters );

            // Act
            CommandResult result = _sut.Execute();

            // Assert
            _registryMock.Verify( r => r.Add( It.IsAny<CommandMenu>() ), Times.Once );
            _uiMock.Verify( u => u.WriteLine( It.IsAny<string>() ), Times.Never );
            Assert.IsType<CommandResult>( result );
        }
    }
}