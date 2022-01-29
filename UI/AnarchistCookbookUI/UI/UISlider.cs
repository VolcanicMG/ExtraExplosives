using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace ExtraExplosives.UI.AnarchistCookbookUI.UI
{
    public class SliderBar : UIElement
    {
        public int value { get; internal set; }
        private static Texture2D texture = ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/UI/HUDSliderBar");
        private Slider _slider;

        public SliderBar(string name)// : base(texture)
        {
        }

        public override void OnInitialize()
        {
            _slider = new Slider();
            _slider.Width.Pixels = 16;
            _slider.Height.Pixels = 36;
            _slider.Left.Pixels = 100;
            _slider.VAlign = .5f;
            Append(_slider);
        }

        public override void Update(GameTime gameTime)
        {
            value = (int)(_slider.Left.Pixels);
            base.Update(gameTime);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = base.GetInnerDimensions();
            Rectangle rectangle = dimensions.ToRectangle();
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public void SetSlider(int value)
        {
            _slider.Left.Pixels = value;
        }
    }

    public class Slider : UIElement
    {
        private Vector2 offset;
        public bool dragging;
        private static Texture2D texture = ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/UI/HUDSlider");

        public Slider()
        {
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
        }

        public override void MouseDown(UIMouseEvent evt)
        {
            base.MouseDown(evt);
            DragStart(evt);
        }

        public override void MouseUp(UIMouseEvent evt)
        {
            base.MouseUp(evt);
            DragEnd(evt);
        }

        private void DragStart(UIMouseEvent evt)
        {
            offset = new Vector2(evt.MousePosition.X - Left.Pixels, Top.Pixels);
            dragging = true;
        }

        private void DragEnd(UIMouseEvent evt)
        {
            Vector2 end = evt.MousePosition;
            dragging = false;
            Left.Set(end.X - offset.X, 0f);

            Recalculate();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            if (dragging)
            {
                Left.Set(Main.mouseX - offset.X, 0f);
                Recalculate();
            }

            var parent = Parent.GetDimensions().ToRectangle();
            if (!GetDimensions().ToRectangle().Intersects(parent))
            {
                Left.Pixels = Utils.Clamp(Left.Pixels, 5, 180);
                Recalculate();
            }

            if (Left.Pixels > 180) Left.Pixels = 180;
            if (Left.Pixels < 5) Left.Pixels = 5;
            //Main.NewText(Left.Pixels);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = base.GetInnerDimensions();
            Rectangle rectangle = dimensions.ToRectangle();
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}