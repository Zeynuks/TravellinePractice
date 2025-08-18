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
using Moq;

namespace FighterGame.Tests.Command
{
    [TestFixture]
    public class BuildFighterCommandTests
    {
        private Mock<IUserInterface> _uiMock;
        private Mock<IMenuRegistry> _registryMock;
        private Mock<IFighterRepository> _fighterRepositoryMock;
        private Mock<IClass> _classMock;
        private Mock<IRace> _raceMock;
        private Mock<IArmor> _armorMock;
        private Mock<IWeapon> _weaponMock;
        private FighterDto _fighterDto;
        private BuildFighterCommand _sut;

        [SetUp]
        public void SetUp()
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

        [Test]
        public void Execute_FighterIsBuiltSuccessfully_ShouldAddFighterToRepository()
        {
            Fighter expectedFighter = new(
                _fighterDto.Name,
                _classMock.Object,
                _raceMock.Object,
                _armorMock.Object,
                _weaponMock.Object );

            CommandResult result = _sut.Execute();

            _fighterRepositoryMock.Verify( repo => repo.AddFighter(
                It.Is<Fighter>( f => f.Name == expectedFighter.Name ) ), Times.Once );
            _registryMock.Verify( registry => registry.Remove(
                It.Is<string>( id => id == "create-fighter" ) ), Times.Once );
            Assert.That( result, Is.EqualTo( CommandResults.Back() ) );
        }

        [Test]
        public void Execute_ExceptionThrown_ShouldDisplayErrorMessage()
        {
            _fighterRepositoryMock.Setup( repo => repo.AddFighter( It.IsAny<Fighter>() ) )
                .Throws( new Exception( "Ошибка при добавлении бойца" ) );

            CommandResult result = _sut.Execute();

            _uiMock.Verify( ui => ui.WriteLine( It.Is<string>( msg =>
                msg.Contains( "Ошибка: Ошибка при добавлении бойца" ) ) ), Times.Once );
            Assert.That( result, Is.EqualTo( CommandResults.Back() ) );
        }

        [Test]
        public void Execute_FighterDtoHasInvalidData_ShouldThrowException()
        {
            _fighterDto.Name = "";

            CommandResult result = _sut.Execute();

            _uiMock.Verify( ui => ui.WriteLine( It.Is<string>( msg =>
                msg.Contains( "Ошибка: Имя не может быть пустым." ) ) ), Times.Once );
            Assert.That( result, Is.EqualTo( CommandResults.Back() ) );
        }
    }
}