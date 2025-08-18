using FighterGame.Command;
using FighterGame.Domain.Model;
using Menu.Core;
using Menu.UI;
using Moq;

namespace FighterGame.Tests.Command
{
    public class SelectNameCommandTests
    {
        private Mock<IUserInterface> _uiMock;
        private FighterDto _fighterDto;
        private SelectNameCommand _sut;

        [SetUp]
        public void SetUp()
        {
            _uiMock = new Mock<IUserInterface>();
            _fighterDto = new FighterDto();
            _sut = new SelectNameCommand( _uiMock.Object, _fighterDto );
        }

        [Test]
        public void Execute_ShouldThrowException_WhenNameIsEmpty()
        {
            string lastName = _fighterDto.Name;
            _uiMock.Setup( ui => ui.ReadLine( It.IsAny<string>() ) ).Returns( string.Empty );

            CommandResult result = _sut.Execute();

            Assert.Multiple( () =>
            {
                Assert.That( _fighterDto.Name, Is.EqualTo( lastName ) );
                Assert.That( result, Is.EqualTo( CommandResults.Continue() ) );
            } );
        }


        [Test]
        public void Execute_ShouldThrowException_WhenNameIsWhitespaces()
        {
            string lastName = _fighterDto.Name;
            const string validName = "         ";
            _uiMock.Setup( ui => ui.ReadLine( It.IsAny<string>() ) ).Returns( validName );

            CommandResult result = _sut.Execute();

            Assert.Multiple( () =>
            {
                Assert.That( _fighterDto.Name, Is.EqualTo( lastName ) );
                Assert.That( result, Is.EqualTo( CommandResults.Continue() ) );
            } );
        }

        [Test]
        public void Execute_ShouldSetName_WhenValidNameIsProvided()
        {
            const string validName = "Test Name";
            _uiMock.Setup( ui => ui.ReadLine( It.IsAny<string>() ) ).Returns( validName );

            CommandResult result = _sut.Execute();

            Assert.Multiple( () =>
            {
                Assert.That( _fighterDto.Name, Is.EqualTo( validName ) );
                Assert.That( result, Is.EqualTo( CommandResults.Continue() ) );
            } );
        }
    }
}