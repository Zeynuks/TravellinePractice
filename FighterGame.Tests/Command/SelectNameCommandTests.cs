using Xunit;
using Moq;
using FighterGame.Command;
using FighterGame.Domain.Model;
using Menu.Core;
using Menu.UI;

namespace FighterGame.Tests.Command
{
    public class SelectNameCommandTests
    {
        private readonly Mock<IUserInterface> _uiMock;
        private readonly FighterDto _fighterDto;
        private readonly SelectNameCommand _sut;

        public SelectNameCommandTests()
        {
            _uiMock = new Mock<IUserInterface>();
            _fighterDto = new FighterDto();
            _sut = new SelectNameCommand( _uiMock.Object, _fighterDto );
        }

        [Fact]
        public void Execute_WhenNameInputEmpty_ShouldKeepPreviousNameAndContinue()
        {
            // Arrange
            string lastName = _fighterDto.Name;
            _uiMock.Setup( ui => ui.ReadLine( It.IsAny<string>() ) ).Returns( string.Empty );

            // Act
            CommandResult result = _sut.Execute();

            // Assert
            Assert.Equal( lastName, _fighterDto.Name );
            Assert.Equal( CommandResults.Continue(), result );
        }

        [Fact]
        public void Execute_WhenNameInputWhitespace_ShouldKeepPreviousNameAndContinue()
        {
            // Arrange
            string lastName = _fighterDto.Name;
            const string whitespaceName = "         ";
            _uiMock.Setup( ui => ui.ReadLine( It.IsAny<string>() ) ).Returns( whitespaceName );

            // Act
            CommandResult result = _sut.Execute();

            // Assert
            Assert.Equal( lastName, _fighterDto.Name );
            Assert.Equal( CommandResults.Continue(), result );
        }

        [Fact]
        public void Execute_WhenValidNameProvided_ShouldSetNameAndContinue()
        {
            // Arrange
            const string validName = "Test Name";
            _uiMock.Setup( ui => ui.ReadLine( It.IsAny<string>() ) ).Returns( validName );

            // Act
            CommandResult result = _sut.Execute();

            // Assert
            Assert.Equal( validName, _fighterDto.Name );
            Assert.Equal( CommandResults.Continue(), result );
        }
    }
}