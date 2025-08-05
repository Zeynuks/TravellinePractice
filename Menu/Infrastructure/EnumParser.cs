using System.ComponentModel;
using System.Reflection;

namespace Menu.Infrastructure
{
    public static class EnumParser
    {
        /// <summary>
        /// Получает описание из атрибута Description, если он есть.
        /// </summary>
        public static string? GetEnumDescription<TEnum>( TEnum enumValue )
        {
            FieldInfo? field = enumValue?.GetType().GetField( enumValue.ToString() ?? string.Empty );
            if ( field == null )
            {
                return null;
            }

            DescriptionAttribute? attribute =
                ( DescriptionAttribute? )Attribute.GetCustomAttribute( field, typeof( DescriptionAttribute ) );

            return attribute?.Description;
        }
    }
}