using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;

namespace ExtraExplosives.UI.AnarchistCookbookUI.UI
{
    internal class Active : UIText
    {
        
        
        private static string text = "Active";
        
        public Active() : base(text)
        {
            OnInitialize();
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
            VAlign = 0.5f;
            Left.Pixels = 50;
            TextColor = Color.LimeGreen;
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