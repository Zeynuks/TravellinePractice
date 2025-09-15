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

        private readonly Fighter _attacker;

        public FighterTests()
        {
            _classMock = new Mock<IClass>();
            _raceMock = new Mock<IRace>();
            _armorMock = new Mock<IArmor>();
            _weaponMock = new Mock<IWeapon>();
            _healDiceMock = new Mock<IDice>();
            Mock<IDice> weaponDamageDiceMock = new();

            _classMock.SetupGet( x => x.DamageModify ).Returns( 1.0 );

            _raceMock.SetupGet( x => x.MaxHealth ).Returns( 100 );
            _raceMock.SetupGet( x => x.HealDice ).Returns( _healDiceMock.Object );
            _raceMock.SetupGet( x => x.DamageResist ).Returns( DamageType.Necrotic );

            _armorMock.SetupGet( x => x.ArmorClass ).Returns( 10 );

            weaponDamageDiceMock.Setup( x => x.Roll() ).Returns( 8 );
            _weaponMock.SetupGet( x => x.Damage ).Returns( weaponDamageDiceMock.Object );
            _weaponMock.SetupGet( x => x.DamageType ).Returns( DamageType.Fire );

            _attacker = new Fighter(
                "Attacker",
                _classMock.Object,
                _raceMock.Object,
                _armorMock.Object,
                _weaponMock.Object );
        }

        [Fact]
        public void Constructor_WhenNameIsWhitespace_ShouldThrowInvalidOperationException()
        {
            // Arrange
            const string invalidName = "  ";

            // Act
            Action act = () => new Fighter(
                invalidName,
                _classMock.Object,
                _raceMock.Object,
                _armorMock.Object,
                _weaponMock.Object );

            // Assert
            Assert.Throws<InvalidOperationException>( act );
        }

        [Fact]
        public void Constructor_WhenDataValid_ShouldInitializeProperties()
        {
            // Arrange
            const string name = "Hero";

            // Act
            Fighter fighter = new( name, _classMock.Object, _raceMock.Object, _armorMock.Object, _weaponMock.Object );

            // Assert
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
            // Arrange
            _healDiceMock.Setup( x => x.Roll() ).Returns( 13 );

            // Act
            int heal = _attacker.RollHealing();

            // Assert
            Assert.Equal( 13, heal );
        }

        [Fact]
        public void TakeDamage_WhenNegative_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            const int negativeDamage = -1;

            // Act
            Action act = () => _attacker.TakeDamage( negativeDamage );

            // Assert
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>( act );
            Assert.Contains( "Урон не может быть отрицательным.", ex.Message );
        }

        [Fact]
        public void TakeDamage_WhenDamageExceedsHealth_ShouldReduceToZeroNotNegative()
        {
            // Arrange
            int startHealth = _attacker.Health;

            // Act
            _attacker.TakeDamage( 30 );
            _attacker.TakeDamage( 80 );

            // Assert
            Assert.Equal( 0, _attacker.Health );
            Assert.True( startHealth > _attacker.Health );
        }

        [Fact]
        public void Heal_WhenNegative_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            const int negativeHeal = -5;

            // Act
            Action act = () => _attacker.Heal( negativeHeal );

            // Assert
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>( act );
            Assert.Contains( "Значение лечения не может быть отрицательным.", ex.Message );
        }

        [Fact]
        public void Heal_WhenHealingExceedsMax_ShouldNotExceedMaxHealth()
        {
            // Arrange
            _attacker.TakeDamage( 60 );

            // Act
            _attacker.Heal( 30 );
            _attacker.Heal( 1000 );

            // Assert
            Assert.Equal( _attacker.Race.MaxHealth, _attacker.Health );
        }
    }
}