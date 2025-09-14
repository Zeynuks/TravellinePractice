using Xunit;
using Moq;
using FighterGame.Domain.Dice;
using FighterGame.Domain.Model;
using FighterGame.Domain.Model.Armor;
using FighterGame.Domain.Model.Class;
using FighterGame.Domain.Model.Races;
using FighterGame.Domain.Model.Types;
using FighterGame.Domain.Model.Weapons;

namespace FighterGame.Tests.Domain.Model
{
    public class FighterTests
    {
        private readonly Mock<IClass> _classMock;
        private readonly Mock<IRace> _raceMock;
        private readonly Mock<IArmor> _armorMock;
        private readonly Mock<IWeapon> _weaponMock;
        private readonly Mock<IDice> _healDiceMock;
        private readonly Mock<IDice> _weaponDamageDiceMock;

        private readonly Fighter _attacker;
        private readonly Fighter _defender;

        public FighterTests()
        {
            _classMock = new Mock<IClass>();
            _raceMock = new Mock<IRace>();
            _armorMock = new Mock<IArmor>();
            _weaponMock = new Mock<IWeapon>();
            _healDiceMock = new Mock<IDice>();
            _weaponDamageDiceMock = new Mock<IDice>();

            _classMock.SetupGet( x => x.DamageModify ).Returns( 1.0 );

            _raceMock.SetupGet( x => x.MaxHealth ).Returns( 100 );
            _raceMock.SetupGet( x => x.HealDice ).Returns( _healDiceMock.Object );
            _raceMock.SetupGet( x => x.DamageResist ).Returns( DamageType.Necrotic );

            _armorMock.SetupGet( x => x.ArmorClass ).Returns( 10 );

            _weaponDamageDiceMock.Setup( x => x.Roll() ).Returns( 8 );
            _weaponMock.SetupGet( x => x.Damage ).Returns( _weaponDamageDiceMock.Object );
            _weaponMock.SetupGet( x => x.DamageType ).Returns( DamageType.Fire );

            _attacker = new Fighter(
                "Attacker",
                _classMock.Object,
                _raceMock.Object,
                _armorMock.Object,
                _weaponMock.Object );

            _defender = new Fighter(
                "Defender",
                _classMock.Object,
                _raceMock.Object,
                _armorMock.Object,
                _weaponMock.Object );
        }

        [Fact]
        public void Ctor_WhenNameIsWhitespace_ShouldThrowInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>( () =>
                new Fighter( "  ", _classMock.Object, _raceMock.Object, _armorMock.Object, _weaponMock.Object ) );
        }

        [Fact]
        public void Ctor_WhenDataValid_ShouldInitializeProperties()
        {
            const string name = "Hero";

            Fighter fighter = new( name, _classMock.Object, _raceMock.Object, _armorMock.Object, _weaponMock.Object );

            Assert.NotEqual( Guid.Empty, fighter.Id );
            Assert.Equal( name, fighter.Name );
            Assert.Same( _classMock.Object, fighter.Class );
            Assert.Same( _raceMock.Object, fighter.Race );
            Assert.Same( _armorMock.Object, fighter.Armor );
            Assert.Same( _weaponMock.Object, fighter.Weapon );
            Assert.Equal( 100, fighter.Health );
        }

        [Fact]
        public void RollHealing_WhenHealDiceReturnsValue_ShouldReturnThatValue()
        {
            _healDiceMock.Setup( x => x.Roll() ).Returns( 13 );

            int heal = _attacker.RollHealing();

            Assert.Equal( 13, heal );
        }

        [Fact]
        public void TakeDamage_WhenNegative_ShouldThrowArgumentOutOfRangeException()
        {
            ArgumentOutOfRangeException ex =
                Assert.Throws<ArgumentOutOfRangeException>( () => _attacker.TakeDamage( -1 ) );
            Assert.Contains( "Урон не может быть отрицательным.", ex.Message );
        }

        [Fact]
        public void TakeDamage_WhenDamageExceedsHealth_ShouldReduceToZeroNotNegative()
        {
            int start = _attacker.Health;

            _attacker.TakeDamage( 30 );
            _attacker.TakeDamage( 80 );

            Assert.Equal( 0, _attacker.Health );
            Assert.True( start > _attacker.Health );
        }

        [Fact]
        public void Heal_WhenNegative_ShouldThrowArgumentOutOfRangeException()
        {
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>( () => _attacker.Heal( -5 ) );
            Assert.Contains( "Значение лечения не может быть отрицательным.", ex.Message );
        }

        [Fact]
        public void Heal_WhenHealingExceedsMax_ShouldNotExceedMaxHealth()
        {
            _attacker.TakeDamage( 60 );

            _attacker.Heal( 30 );
            _attacker.Heal( 1000 );

            Assert.Equal( _attacker.Race.MaxHealth, _attacker.Health );
        }
    }
}