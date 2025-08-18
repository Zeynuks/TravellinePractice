using FighterGame.Domain;
using FighterGame.Domain.Model;
using FighterGame.Domain.Model.Races;
using Menu.UI;
using Moq;

namespace FighterGame.Tests.Domain
{
    [TestFixture]
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
                .Callback<int>( h =>
                {
                    health = Math.Min( health + h, raceMaxHealth );
                } );

            fighter.Setup( x => x.Attack( It.IsAny<IFighter>() ) )
                .Returns<IFighter>( defender => attackLogic?.Invoke( fighter.Object, defender ) ?? 0 );

            return fighter;
        }

        [Test]
        public void AddParticipant_WhenFighterIsUnique_ShouldAddSuccessfully()
        {
            Mock<IUserInterface> mockUi = CreateUi();
            BattleEngine battleEngine = new( mockUi.Object );
            Mock<IFighter> fighterAlpha = CreateFighter( "Alpha", 10, 10, 0, 100 );

            battleEngine.AddParticipant( fighterAlpha.Object );

            Assert.That( battleEngine.ContainsParticipant( fighterAlpha.Object.Id ), Is.True );
        }

        [Test]
        public void AddParticipant_WhenFighterAlreadyExists_ShouldThrow()
        {
            Mock<IUserInterface> mockUi = CreateUi();
            BattleEngine battleEngine = new( mockUi.Object );
            Mock<IFighter> fighterAlpha = CreateFighter( "Alpha", 10, 10, 0, 100 );
            battleEngine.AddParticipant( fighterAlpha.Object );

            Assert.That( Act, Throws.TypeOf<InvalidOperationException>() );
            return;

            void Act() => battleEngine.AddParticipant( fighterAlpha.Object );
        }

        [Test]
        public void DeleteParticipant_WhenFighterExists_ShouldRemoveSuccessfully()
        {
            Mock<IUserInterface> mockUi = CreateUi();
            BattleEngine battleEngine = new( mockUi.Object );
            Mock<IFighter> fighterAlpha = CreateFighter( "Alpha", 10, 10, 0, 100 );
            battleEngine.AddParticipant( fighterAlpha.Object );
            Assert.That( battleEngine.ContainsParticipant( fighterAlpha.Object.Id ), Is.True );

            battleEngine.DeleteParticipant( fighterAlpha.Object );

            Assert.That( battleEngine.ContainsParticipant( fighterAlpha.Object.Id ), Is.False );
        }

        [Test]
        public void DeleteParticipant_WhenFighterNotFound_ShouldThrow()
        {
            Mock<IUserInterface> mockUi = CreateUi();
            BattleEngine battleEngine = new( mockUi.Object );
            Mock<IFighter> fighterAlpha = CreateFighter( "Alpha", 10, 10, 0, 100 );

            Assert.That( Act, Throws.TypeOf<InvalidOperationException>() );
            return;

            void Act() => battleEngine.DeleteParticipant( fighterAlpha.Object );
        }

        [Test]
        public void ContainsParticipant_ShouldReturnTrueOnlyForPreviouslyAddedFighter()
        {
            Mock<IUserInterface> mockUi = CreateUi();
            BattleEngine battleEngine = new( mockUi.Object );
            Mock<IFighter> fighterAlpha = CreateFighter( "Alpha", 10, 10, 0, 100 );
            Mock<IFighter> fighterBravo = CreateFighter( "Bravo", 10, 10, 0, 100 );
            battleEngine.AddParticipant( fighterAlpha.Object );

            bool hasAlpha = battleEngine.ContainsParticipant( fighterAlpha.Object.Id );
            bool hasBravo = battleEngine.ContainsParticipant( fighterBravo.Object.Id );

            Assert.Multiple( () =>
            {
                Assert.That( hasAlpha, Is.True );
                Assert.That( hasBravo, Is.False );
            } );
        }

        [Test]
        public void StartBattle_WhenLessThanTwoFighters_ShouldThrowButClearUiFirst()
        {
            Mock<IUserInterface> mockUi = CreateUi();
            BattleEngine battleEngine = new( mockUi.Object );
            Mock<IFighter> fighterSolo = CreateFighter( "Solo", 10, 10, 0, 100 );
            battleEngine.AddParticipant( fighterSolo.Object );

            Assert.That( Act, Throws.TypeOf<InvalidOperationException>() );
            mockUi.Verify( x => x.Clear(), Times.Once );
            return;

            void Act() => battleEngine.StartBattle();
        }

        [Test]
        public void StartBattle_WithTwoFighters_ShouldRunTournament_AnnounceChampion_HealAllAndClearList()
        {
            Mock<IUserInterface> mockUi = CreateUi();
            BattleEngine battleEngine = new( mockUi.Object );

            Mock<IFighter> fighterAlpha = CreateFighter(
                name: "Alpha",
                initialHealth: 10,
                rollInitiative: 20,
                rollHealing: 5,
                raceMaxHealth: 100,
                attackLogic: ( attacker, defender ) =>
                {
                    int newHealth = Math.Max( 0, defender.Health - 15 );
                    Mock.Get( defender ).SetupGet( x => x.Health ).Returns( newHealth );
                    return 15;
                } );

            Mock<IFighter> fighterBravo = CreateFighter(
                name: "Bravo",
                initialHealth: 10,
                rollInitiative: 10,
                rollHealing: 3,
                raceMaxHealth: 100,
                attackLogic: ( attacker, defender ) => 0 );

            battleEngine.AddParticipant( fighterAlpha.Object );
            battleEngine.AddParticipant( fighterBravo.Object );

            battleEngine.StartBattle();

            mockUi.Verify( x => x.Clear(), Times.Once );
            mockUi.Verify( x =>
                x.WriteLine( It.Is<string>( s => s.Contains( "Турнирная таблица" ) ) ), Times.AtLeastOnce );
            mockUi.Verify( x =>
                x.WriteLine( It.Is<string>( s => s.Contains( "Alpha vs Bravo" ) ) ), Times.AtLeastOnce );
            mockUi.Verify( x => x.WriteLine( It.Is<string>( s =>
                s.Contains( "— победитель турнира!" ) && s.Contains( "Alpha" ) ) ), Times.AtLeastOnce );

            Mock.Get( fighterAlpha.Object ).Verify( x =>
                x.Heal( It.Is<int>( h => h == 100 ) ), Times.Once );
            Mock.Get( fighterBravo.Object ).Verify( x =>
                x.Heal( It.Is<int>( h => h == 100 ) ), Times.Once );
        }

        [Test]
        public void StartBattle_WithThreeFighters_ShouldHandleOddBracket_SkipMessageAndAnnounceChampion()
        {
            Mock<IUserInterface> mockUi = CreateUi();
            BattleEngine battleEngine = new( mockUi.Object );

            Mock<IFighter> fighterStrong = CreateFighter(
                name: "Strong",
                initialHealth: 20,
                rollInitiative: 30,
                rollHealing: 4,
                raceMaxHealth: 100,
                attackLogic: ( attacker, defender ) =>
                {
                    int newHealth = Math.Max( 0, defender.Health - 25 );
                    Mock.Get( defender ).SetupGet( x => x.Health ).Returns( newHealth );
                    return 25;
                } );

            Mock<IFighter> fighterMid = CreateFighter(
                name: "Mid",
                initialHealth: 10,
                rollInitiative: 20,
                rollHealing: 2,
                raceMaxHealth: 100,
                attackLogic: ( attacker, defender ) => 0 );

            Mock<IFighter> fighterLow = CreateFighter(
                name: "Low",
                initialHealth: 10,
                rollInitiative: 10,
                rollHealing: 1,
                raceMaxHealth: 100,
                attackLogic: ( attacker, defender ) => 0 );

            battleEngine.AddParticipant( fighterStrong.Object );
            battleEngine.AddParticipant( fighterMid.Object );
            battleEngine.AddParticipant( fighterLow.Object );

            battleEngine.StartBattle();

            mockUi.Verify( x => x.WriteLine( It.Is<string>( s =>
                s.Contains( "получает пропуск" ) || s.Contains( "проходит далее без боя" ) ) ), Times.AtLeastOnce );
            mockUi.Verify( x => x.WriteLine( It.Is<string>( s =>
                s.Contains( "— победитель турнира!" ) && s.Contains( "Strong" ) ) ), Times.AtLeastOnce );

            Mock.Get( fighterStrong.Object ).Verify( x =>
                x.Heal( It.Is<int>( h => h == 100 ) ), Times.Once );
            Mock.Get( fighterMid.Object ).Verify( x =>
                x.Heal( It.Is<int>( h => h == 100 ) ), Times.Once );
            Mock.Get( fighterLow.Object ).Verify( x =>
                x.Heal( It.Is<int>( h => h == 100 ) ), Times.Once );
        }
    }
}