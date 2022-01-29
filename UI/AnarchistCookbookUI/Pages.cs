using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;

namespace ExtraExplosives.UI.AnarchistCookbookUI
{
    public class Pages : UIPanel        // Template File
    {                                    // All changes will propagate to other Pages
        public static bool Visible { get; set; }
        internal string foundCraft { get; set; } = "Found In:";
        internal UIPanel leftPage;
        internal UIPanel rightPage;
        internal UIImage leftDivider;
        internal UIImage rightDivider;
        internal int padding = 25;
        internal Color color = new Color(0, 0, 0, 0);

        public Pages()
        {
        }

        public override void OnInitialize()
        {
            VAlign = 0.5f;
            HAlign = 0.5f;
            Width.Pixels = Main.screenWidth / 2 + padding * 4 - 60;
            Height.Pixels = Main.screenHeight / 2 + padding * 4 - 44;
            BackgroundColor = new Color(0, 0, 0, 0);
            BorderColor = color;

            leftPage = new UIPanel();
            leftPage.HAlign = 0.05f;
            //leftPage.Left.Pixels = padding;
            leftPage.VAlign = 0.5f;
            //leftPage.Top.Pixels = padding;
            leftPage.Width.Pixels = Width.Pixels / 2 - (2 * padding);
            leftPage.Height.Pixels = Height.Pixels - (2 * padding);
            leftPage.BackgroundColor = color;
            leftPage.BorderColor = color;
            Append(leftPage);

            leftDivider = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Divider"));
            leftDivider.VAlign = 0.5f;
            leftDivider.HAlign = 0.5f;
            //leftDivider.Left.Pixels = -10;
            leftPage.Append(leftDivider);

            rightPage = new UIPanel();
            rightPage.HAlign = 0.95f;
            //rightPage.Left.Pixels = Width.Pixels / 2 + padding;
            rightPage.VAlign = 0.5f;
            //rightPage.Top.Pixels = padding;
            rightPage.Width.Pixels = Width.Pixels / 2 - (2 * padding);
            rightPage.Height.Pixels = Height.Pixels - (2 * padding);
            rightPage.BackgroundColor = color;
            rightPage.BorderColor = color;
            Append(rightPage);

            rightDivider = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Divider"));
            rightDivider.VAlign = 0.5f;
            rightDivider.HAlign = 0.5f;
            rightPage.Append(rightDivider);
        }

        public void MakeVisible() => Visible = true;
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