using FighterGame.Command;
using FighterGame.Domain.Model;
using FighterGame.Domain.Repository;
using Menu.Core;
using Menu.Infrastructure;
using Menu.Infrastructure.Menu;
using Menu.UI;
using Moq;

namespace FighterGame.Tests.Command
{
    public class PrepareToBattleCommandTests
    {
        private Mock<IUserInterface> _uiMock;
        private Mock<IMenuRegistry> _registryMock;
        private Mock<IFighterRepository> _fighterRepositoryMock;
        private PrepareToBattleCommand _sut;

        [SetUp]
        public void SetUp()
        {
            _uiMock = new Mock<IUserInterface>();
            _registryMock = new Mock<IMenuRegistry>();
            _fighterRepositoryMock = new Mock<IFighterRepository>();
            _sut = new PrepareToBattleCommand( _uiMock.Object, _registryMock.Object, _fighterRepositoryMock.Object );
        }

        [Test]
        public void Execute_ShouldThrowException_WhenNoFightersFound()
        {
            _fighterRepositoryMock.Setup( r => r.GetAllFighters() ).Returns( new List<IFighter>() );

            CommandResult result = _sut.Execute();

            _uiMock.Verify( ui => ui.WriteLine( It.Is<string>( msg =>
                msg.Contains( "Ошибка: Бойцов не обнаружено." ) ) ), Times.Once );
            Assert.That( result, Is.EqualTo( CommandResults.Continue() ) );
        }

        [Test]
        public void Execute_ShouldAddFightersToMenu_WhenFightersExist()
        {
            List<IFighter> fighters =
            [
                new Mock<IFighter>().Object,
                new Mock<IFighter>().Object
            ];
            _fighterRepositoryMock.Setup( r => r.GetAllFighters() ).Returns( fighters );

            CommandResult result = _sut.Execute();

            _registryMock.Verify( r => r.Add( It.IsAny<CommandMenu>() ), Times.Once );
            _uiMock.Verify( u => u.WriteLine( It.IsAny<string>() ), Times.Never );
            Assert.That( result, Is.InstanceOf<CommandResult>() );
        }
    }
}