using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;

namespace ExtraExplosives.UI.AnarchistCookbookUI.UI
{
    public class UIHoverImage : UIImage
    {
        internal string HoverText;

        public UIHoverImage(Texture2D texture, string hoverText) : base(texture)
        {
            HoverText = hoverText;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            if (IsMouseHovering) Main.hoverItemName = HoverText;
        }
    }
}