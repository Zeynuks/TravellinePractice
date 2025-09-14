using Xunit;
using Moq;
using FighterGame.Domain;
using FighterGame.Domain.Model;
using FighterGame.Domain.Model.Races;
using Menu.UI;

namespace FighterGame.Tests.Domain
{
    public class BattleEngineTests
    {
        private static Mock<IUserInterface> CreateUi()
        {
            Mock<IUserInterface> mockUi = new( MockBehavior.Strict );
            mockUi.Setup( x => x.Clear() );
            mockUi.Setup( x => x.WriteLine( It.IsAny<string>() ) );
            mockUi.Setup( x => x.ReadKey( It.IsAny<bool>() ) ).Returns( ConsoleKey.Enter );
            return mockUi;
        }

        private static Mock<IFighter> CreateFighter(
            string name,
            int initialHealth,
            int rollInitiative,
            int rollHealing,
            int raceMaxHealth,
            Func<IFighter, IFighter, int>? attackLogic = null )
        {
            Mock<IFighter> fighter = new( MockBehavior.Strict );
            fighter.SetupGet( x => x.Id ).Returns( Guid.NewGuid() );
            fighter.SetupGet( x => x.Name ).Returns( name );

            int health = initialHealth;
            fighter.SetupGet( x => x.Health ).Returns( () => health );

            Mock<IRace> race = new( MockBehavior.Strict );
            race.SetupGet( r => r.MaxHealth ).Returns( raceMaxHealth );
            fighter.SetupGet( x => x.Race ).Returns( race.Object );

            fighter.Setup( x => x.RollInitiative() ).Returns( rollInitiative );
            fighter.Setup( x => x.RollHealing() ).Returns( rollHealing );

            fighter.Setup( x => x.Heal( It.IsAny<int>() ) )
                .Callback<int>( h => { health = Math.Min( health + h, raceMaxHealth ); } );

            fighter.Setup( x => x.Attack( It.IsAny<IFighter>() ) )
                .Returns<IFighter>( defender => attackLogic?.Invoke( fighter.Object, defender ) ?? 0 );

            return fighter;
        }

        [Fact]
        public void AddParticipant_WhenFighterIsUnique_ShouldAddSuccessfully()
        {
            Mock<IUserInterface> mockUi = CreateUi();
            BattleEngine battleEngine = new( mockUi.Object );
            Mock<IFighter> fighterAlpha = CreateFighter( "Alpha", 10, 10, 0, 100 );

            battleEngine.AddParticipant( fighterAlpha.Object );

            Assert.True( battleEngine.ContainsParticipant( fighterAlpha.Object.Id ) );
        }

        [Fact]
        public void AddParticipant_WhenFighterAlreadyExists_ShouldThrowInvalidOperationException()
        {
            Mock<IUserInterface> mockUi = CreateUi();
            BattleEngine battleEngine = new( mockUi.Object );
            Mock<IFighter> fighterAlpha = CreateFighter( "Alpha", 10, 10, 0, 100 );
            battleEngine.AddParticipant( fighterAlpha.Object );

            Assert.Throws<InvalidOperationException>( () => battleEngine.AddParticipant( fighterAlpha.Object ) );
        }

        [Fact]
        public void DeleteParticipant_WhenFighterExists_ShouldRemoveSuccessfully()
        {
            Mock<IUserInterface> mockUi = CreateUi();
            BattleEngine battleEngine = new( mockUi.Object );
            Mock<IFighter> fighterAlpha = CreateFighter( "Alpha", 10, 10, 0, 100 );
            battleEngine.AddParticipant( fighterAlpha.Object );
            Assert.True( battleEngine.ContainsParticipant( fighterAlpha.Object.Id ) );

            battleEngine.DeleteParticipant( fighterAlpha.Object );

            Assert.False( battleEngine.ContainsParticipant( fighterAlpha.Object.Id ) );
        }

        [Fact]
        public void DeleteParticipant_WhenFighterNotFound_ShouldThrowInvalidOperationException()
        {
            Mock<IUserInterface> mockUi = CreateUi();
            BattleEngine battleEngine = new( mockUi.Object );
            Mock<IFighter> fighterAlpha = CreateFighter( "Alpha", 10, 10, 0, 100 );

            Assert.Throws<InvalidOperationException>( () => battleEngine.DeleteParticipant( fighterAlpha.Object ) );
        }

        [Fact]
        public void ContainsParticipant_WhenQueried_ShouldReturnTrueOnlyForAddedFighter()
        {
            Mock<IUserInterface> mockUi = CreateUi();
            BattleEngine battleEngine = new( mockUi.Object );
            Mock<IFighter> fighterAlpha = CreateFighter( "Alpha", 10, 10, 0, 100 );
            Mock<IFighter> fighterBravo = CreateFighter( "Bravo", 10, 10, 0, 100 );
            battleEngine.AddParticipant( fighterAlpha.Object );

            bool hasAlpha = battleEngine.ContainsParticipant( fighterAlpha.Object.Id );
            bool hasBravo = battleEngine.ContainsParticipant( fighterBravo.Object.Id );

            Assert.True( hasAlpha );
            Assert.False( hasBravo );
        }

        [Fact]
        public void StartBattle_WhenLessThanTwoFighters_ShouldClearUiAndThrowInvalidOperationException()
        {
            Mock<IUserInterface> mockUi = CreateUi();
            BattleEngine battleEngine = new( mockUi.Object );
            Mock<IFighter> fighterSolo = CreateFighter( "Solo", 10, 10, 0, 100 );
            battleEngine.AddParticipant( fighterSolo.Object );

            Assert.Throws<InvalidOperationException>( () => battleEngine.StartBattle() );
            mockUi.Verify( x => x.Clear(), Times.Once );
        }

        [Fact]
        public void StartBattle_WhenTwoFighters_ShouldRunTournamentAnnounceChampionHealAllAndClearList()
        {
            Mock<IUserInterface> mockUi = CreateUi();
            BattleEngine battleEngine = new( mockUi.Object );

            Mock<IFighter> fighterAlpha = CreateFighter(
                "Alpha",
                10,
                20,
                5,
                100,
                ( _, defender ) =>
                {
                    int newHealth = Math.Max( 0, defender.Health - 15 );
                    Mock.Get( defender ).SetupGet( x => x.Health ).Returns( newHealth );
                    return 15;
                } );

            Mock<IFighter> fighterBravo = CreateFighter(
                "Bravo",
                10,
                10,
                3,
                100,
                ( _, _ ) => 0 );

            battleEngine.AddParticipant( fighterAlpha.Object );
            battleEngine.AddParticipant( fighterBravo.Object );

            battleEngine.StartBattle();

            mockUi.Verify( x => x.Clear(), Times.Once );
            mockUi.Verify( x => x.WriteLine( It.Is<string>( s => s.Contains( "Турнирная таблица" ) ) ),
                Times.AtLeastOnce );
            mockUi.Verify( x => x.WriteLine( It.Is<string>( s => s.Contains( "Alpha vs Bravo" ) ) ),
                Times.AtLeastOnce );
            mockUi.Verify( x => x.WriteLine( It.Is<string>( s =>
                s.Contains( "— победитель турнира!" ) && s.Contains( "Alpha" ) ) ), Times.AtLeastOnce );

            Mock.Get( fighterAlpha.Object ).Verify( x => x.Heal( It.Is<int>( h => h == 100 ) ), Times.Once );
            Mock.Get( fighterBravo.Object ).Verify( x => x.Heal( It.Is<int>( h => h == 100 ) ), Times.Once );
        }

        [Fact]
        public void StartBattle_WhenThreeFighters_ShouldHandleOddBracketAnnounceChampionAndHealAll()
        {
            Mock<IUserInterface> mockUi = CreateUi();
            BattleEngine battleEngine = new( mockUi.Object );

            Mock<IFighter> fighterStrong = CreateFighter(
                "Strong",
                20,
                30,
                4,
                100,
                ( _, defender ) =>
                {
                    int newHealth = Math.Max( 0, defender.Health - 25 );
                    Mock.Get( defender ).SetupGet( x => x.Health ).Returns( newHealth );
                    return 25;
                } );

            Mock<IFighter> fighterMid = CreateFighter(
                "Mid",
                10,
                20,
                2,
                100,
                ( _, _ ) => 0 );

            Mock<IFighter> fighterLow = CreateFighter(
                "Low",
                10,
                10,
                1,
                100,
                ( _, _ ) => 0 );

            battleEngine.AddParticipant( fighterStrong.Object );
            battleEngine.AddParticipant( fighterMid.Object );
            battleEngine.AddParticipant( fighterLow.Object );

            battleEngine.StartBattle();

            mockUi.Verify( x => x.WriteLine( It.Is<string>( s =>
                s.Contains( "получает пропуск" ) || s.Contains( "проходит далее без боя" ) ) ), Times.AtLeastOnce );
            mockUi.Verify( x => x.WriteLine( It.Is<string>( s =>
                s.Contains( "— победитель турнира!" ) && s.Contains( "Strong" ) ) ), Times.AtLeastOnce );

            Mock.Get( fighterStrong.Object ).Verify( x => x.Heal( It.Is<int>( h => h == 100 ) ), Times.Once );
            Mock.Get( fighterMid.Object ).Verify( x => x.Heal( It.Is<int>( h => h == 100 ) ), Times.Once );
            Mock.Get( fighterLow.Object ).Verify( x => x.Heal( It.Is<int>( h => h == 100 ) ), Times.Once );
        }
    }
}