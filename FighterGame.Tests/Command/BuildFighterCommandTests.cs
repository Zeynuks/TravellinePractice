using Xunit;
using Moq;
using FighterGame.Command;
using FighterGame.Domain.Model;
using FighterGame.Domain.Model.Armor;
using FighterGame.Domain.Model.Class;
using FighterGame.Domain.Model.Races;
using FighterGame.Domain.Model.Weapons;
using FighterGame.Domain.Repository;
using Menu.Core;
using Menu.Infrastructure;
using Menu.UI;

namespace FighterGame.Tests.Command
{
    public class BuildFighterCommandTests
    {
        private readonly Mock<IUserInterface> _uiMock;
        private readonly Mock<IMenuRegistry> _registryMock;
        private readonly Mock<IFighterRepository> _fighterRepositoryMock;
        private readonly Mock<IClass> _classMock;
        private readonly Mock<IRace> _raceMock;
        private readonly Mock<IArmor> _armorMock;
        private readonly Mock<IWeapon> _weaponMock;
        private readonly FighterDto _fighterDto;
        private readonly BuildFighterCommand _sut;

        public BuildFighterCommandTests()
        {
            _uiMock = new Mock<IUserInterface>();
            _registryMock = new Mock<IMenuRegistry>();
            _fighterRepositoryMock = new Mock<IFighterRepository>();
            _classMock = new Mock<IClass>();
            _raceMock = new Mock<IRace>();
            _armorMock = new Mock<IArmor>();
            _weaponMock = new Mock<IWeapon>();
            _fighterDto = new FighterDto();
            _sut = new BuildFighterCommand( _uiMock.Object, _registryMock.Object, _fighterRepositoryMock.Object,
                _fighterDto );
        }

        [Fact]
        public void Execute_WhenFighterBuiltSuccessfully_AddsFighterToRepository()
        {
            // Arrange
            Fighter expectedFighter = new(
                _fighterDto.Name,
                _classMock.Object,
                _raceMock.Object,
                _armorMock.Object,
                _weaponMock.Object );

            // Act
            CommandResult result = _sut.Execute();

            // Assert
            _fighterRepositoryMock.Verify( repo => repo.AddFighter(
                It.Is<Fighter>( f => f.Name == expectedFighter.Name ) ), Times.Once );
            _registryMock.Verify( registry => registry.Remove(
                It.Is<string>( id => id == "create-fighter" ) ), Times.Once );
            Assert.Equal( CommandResults.Back(), result );
        }

        [Fact]
        public void Execute_WhenRepositoryThrows_ShouldDisplayErrorAndReturnBack()
        {
            // Arrange
            _fighterRepositoryMock
                .Setup( repo => repo.AddFighter( It.IsAny<Fighter>() ) )
                .Throws( new Exception( "Ошибка при добавлении бойца" ) );

            // Act
            CommandResult result = _sut.Execute();

            // Assert
            _uiMock.Verify( ui => ui.WriteLine( It.Is<string>( msg =>
                msg.Contains( "Ошибка: Ошибка при добавлении бойца" ) ) ), Times.Once );
            Assert.Equal( CommandResults.Back(), result );
        }

        [Fact]
        public void Execute_WhenFighterDtoNameEmpty_ShouldDisplayValidationMessageAndReturnBack()
        {
            // Arrange
            _fighterDto.Name = "";

            // Act
            CommandResult result = _sut.Execute();

            // Assert
            _uiMock.Verify( ui => ui.WriteLine( It.Is<string>( msg =>
                msg.Contains( "Ошибка: Имя не может быть пустым." ) ) ), Times.Once );
            Assert.Equal( CommandResults.Back(), result );
        }
    }
}