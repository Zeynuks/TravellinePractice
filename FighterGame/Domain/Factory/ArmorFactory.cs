using FighterGame.Domain.Model.Armor;
using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Factory
{
    public class ArmorFactory
    {
        public IArmor CreateArmor( ArmorType armorType )
        {
            return armorType switch
            {
                ArmorType.ChainMail => new ChainMail(),
                ArmorType.LeatherArmor => new LeatherArmor(),
                ArmorType.NullArmor => new NullArmor(),
                ArmorType.PlateArmor => new PlateArmor(),
                ArmorType.PaddedArmor => new PaddedArmor(),
                _ => throw new ArgumentOutOfRangeException( nameof( armorType ), armorType, null )
            };
        }
    }
}