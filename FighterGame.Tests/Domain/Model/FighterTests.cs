using FighterGame.Domain.Dice;
using FighterGame.Domain.Model;
using FighterGame.Domain.Model.Armor;
using FighterGame.Domain.Model.Class;
using FighterGame.Domain.Model.Races;
using FighterGame.Domain.Model.Types;
using FighterGame.Domain.Model.Weapons;
using Moq;

namespace FighterGame.Tests.Domain.Model
{
    [TestFixture]
    public class FighterTests
    {
        private Mock<IClass> _classMock;
        private Mock<IRace> _raceMock;
        private Mock<IArmor> _armorMock;
        private Mock<IWeapon> _weaponMock;
        private Mock<IDice> _healDiceMock;
        private Mock<IDice> _weaponDamageDiceMock;

        private Fighter _attacker;
        private Fighter _defender;

        [SetUp]
        public void SetUp()
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

        [Test]
        public void Ctor_EmptyName_ShouldThrow()
        {
            Assert.That(
                () => new Fighter( "  ", _classMock.Object, _raceMock.Object, _armorMock.Object, _weaponMock.Object ),
                Throws.InvalidOperationException.With.Message.Contains( "Имя не может быть пустым." ) );
        }

        [Test]
        public void Ctor_ValidData_ShouldInitializeProperties()
        {
            const string name = "Hero";

            Fighter fighter = new( name, _classMock.Object, _raceMock.Object, _armorMock.Object, _weaponMock.Object );

            Assert.Multiple( () =>
            {
                Assert.That( fighter.Id, Is.Not.EqualTo( Guid.Empty ) );
                Assert.That( fighter.Name, Is.EqualTo( name ) );
                Assert.That( fighter.Class, Is.SameAs( _classMock.Object ) );
                Assert.That( fighter.Race, Is.SameAs( _raceMock.Object ) );
                Assert.That( fighter.Armor, Is.SameAs( _armorMock.Object ) );
                Assert.That( fighter.Weapon, Is.SameAs( _weaponMock.Object ) );
                Assert.That( fighter.Health, Is.EqualTo( 100 ) );
            } );
        }

        [Test]
        [Repeat( 100 )]
        public void RollInitiative_ShouldReturnInRange1To20()
        {
            int roll = _attacker.RollInitiative();

            Assert.That( roll, Is.InRange( 1, 20 ) );
        }

        [Test]
        public void RollHealing_ShouldReturnRaceHealDiceRoll()
        {
            _healDiceMock.Setup( x => x.Roll() ).Returns( 13 );

            int heal = _attacker.RollHealing();

            Assert.That( heal, Is.EqualTo( 13 ) );
        }

        [Test]
        public void TakeDamage_Negative_ShouldThrow()
        {
            Assert.That( () => _attacker.TakeDamage( -1 ),
                Throws.TypeOf<ArgumentOutOfRangeException>().With.Message
                    .Contains( "Урон не может быть отрицательным." ) );
        }

        [Test]
        public void TakeDamage_ShouldDecreaseHealth_ButNotBelowZero()
        {
            int start = _attacker.Health;

            _attacker.TakeDamage( 30 );
            _attacker.TakeDamage( 80 );

            Assert.Multiple( () =>
            {
                Assert.That( _attacker.Health, Is.EqualTo( 0 ) );
                Assert.That( start, Is.GreaterThan( _attacker.Health ) );
            } );
        }

        [Test]
        public void Heal_Negative_ShouldThrow()
        {
            Assert.That( () => _attacker.Heal( -5 ),
                Throws.TypeOf<ArgumentOutOfRangeException>().With.Message
                    .Contains( "Значение лечения не может быть отрицательным." ) );
        }

        [Test]
        public void Heal_ShouldIncreaseHealth_ButNotExceedMax()
        {
            _attacker.TakeDamage( 60 );

            _attacker.Heal( 30 );
            _attacker.Heal( 1000 );

            Assert.That( _attacker.Health, Is.EqualTo( _attacker.Race.MaxHealth ) );
        }

        [Test]
        public void Attack_AgainstVeryHighArmor_ShouldEventuallyReturnZeroOnMiss()
        {
            Mock<IArmor> hardArmor = new();
            hardArmor.SetupGet( x => x.ArmorClass ).Returns( 25 );
            Fighter tank = new( "Tank", _classMock.Object, _raceMock.Object, hardArmor.Object, _weaponMock.Object );

            bool sawMissZero = false;

            for ( int i = 0; i < 200 && !sawMissZero; i++ )
            {
                int dmg = _attacker.Attack( tank );
                if ( dmg == 0 )
                {
                    sawMissZero = true;
                }

                if ( _attacker.Health == 0 )
                {
                    break;
                }
            }

            Assert.That( sawMissZero, Is.True, "Ожидался хотя бы один промах с уроном 0 против высокого AC." );
        }

        [Test]
        public void Attack_CriticalMiss_ShouldReturnNegativeAndDamageSelf_EventuallyObserved()
        {
            int initialHealth = _attacker.Health;
            bool sawCriticalMiss = false;
            int observedNegative = 0;

            for ( int i = 0; i < 400 && !sawCriticalMiss; i++ )
            {
                int dmg = _attacker.Attack( _defender );
                if ( dmg < 0 )
                {
                    sawCriticalMiss = true;
                    observedNegative = dmg;
                }

                if ( _attacker.Health == 0 )
                {
                    break;
                }
            }

            Assert.Multiple( () =>
            {
                Assert.That( sawCriticalMiss, Is.True, "Ожидался хотя бы один крит-промах с отрицательным уроном." );
                Assert.That( observedNegative, Is.LessThan( 0 ) );
                Assert.That( _attacker.Health, Is.LessThan( initialHealth ) );
            } );
        }

        [Test]
        public void Attack_OnHit_ShouldReduceTargetHealth()
        {
            Mock<IArmor> weakArmor = new();
            weakArmor.SetupGet( x => x.ArmorClass ).Returns( 1 );
            Fighter weakTarget = new( "Weak", _classMock.Object, _raceMock.Object, weakArmor.Object,
                _weaponMock.Object );

            int initialTargetHealth = weakTarget.Health;
            bool sawPositiveDamage = false;
            int observedDamage = 0;

            for ( int i = 0; i < 200 && !sawPositiveDamage; i++ )
            {
                int dmg = _attacker.Attack( weakTarget );
                if ( dmg > 0 )
                {
                    sawPositiveDamage = true;
                    observedDamage = dmg;
                }

                if ( _attacker.Health == 0 )
                {
                    break;
                }
            }

            Assert.Multiple( () =>
            {
                Assert.That( sawPositiveDamage, Is.True,
                    "Ожидался хотя бы один успешный удар с положительным уроном." );
                Assert.That( observedDamage, Is.GreaterThan( 0 ) );
                Assert.That( weakTarget.Health, Is.EqualTo( initialTargetHealth - observedDamage ) );
            } );
        }
    }
}