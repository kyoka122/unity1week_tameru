using Tameru.Entity;

namespace Tameru.View
{
    public class WeakMagicView:BaseMagicView
    {
        public override MagicType magicType { get; } = MagicType.Weak;
    }
}