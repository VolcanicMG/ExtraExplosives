using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.UI;

namespace ExtraExplosives.UI.AnarchistCookbookUI.UI
{
    public class AnimatedImage : UIElement
    {
        private int timer = 0;
        private int timePerFrame;
        private int frame = 1;
        private int frameCount;
        private string filePath;

        public AnimatedImage(string filePath, int frameCount, int timePerFrame)
        {
            this.frameCount = frameCount;
            this.timePerFrame = timePerFrame;
            this.filePath = filePath;
            Texture2D tempTexture = ModContent.GetTexture(filePath + 1);
            //Height.Pixels = tempTexture.Height;
            // Width.Pixels = tempTexture.Width;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            timer++;
            if (timer % timePerFrame == 0)
            {
                frame++;
                if (frame > frameCount) frame = 1;
            }

            Texture2D currentFrame = ModContent.GetTexture(filePath + "" + frame);
            CalculatedStyle dimensions = base.GetInnerDimensions();
            Rectangle rectangle = dimensions.ToRectangle();
            spriteBatch.Draw(currentFrame, rectangle, Color.White);
        }
    }
}