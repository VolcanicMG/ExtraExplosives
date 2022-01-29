using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;

namespace ExtraExplosives.UI.AnarchistCookbookUI.UI
{
    public class CookbookHeader : UIText
    {
        private string text;
        public CookbookHeader(string text) : base(text)
        {
            this.text = text;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
            string _underline = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (i % 2 == 0) continue;
                _underline += "_";
            }
            UIText underline = new UIText(_underline);
            underline.HAlign = 0.5f;
            underline.VAlign = 1;
            Append(underline);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
        }
    }
}