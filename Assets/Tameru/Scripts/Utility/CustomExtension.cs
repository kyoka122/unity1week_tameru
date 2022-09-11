using System;
using Tameru.Entity;

namespace Tameru.Utility
{
    public static class CustomExtension
    {
        public static SeType ConvertSeType(this MagicType type)
        {
            return type switch
            {
                MagicType.SmallBall    => SeType.UseSmallBall,
                MagicType.MediumBall   => SeType.UseMediumBall,
                MagicType.LargeBall    => SeType.UseLargeBall,
                MagicType.SmallBullet  => SeType.UseSmallBullet,
                MagicType.MediumBullet => SeType.UseMediumBullet,
                MagicType.LargeBullet  => SeType.UseLargeBullet,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}