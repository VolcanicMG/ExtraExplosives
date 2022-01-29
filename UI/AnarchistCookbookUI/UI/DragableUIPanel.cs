using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace ExtraExplosives.UI.AnarchistCookbookUI.UI
{
    public class DragableUIPanel : UIPanel
    {
        private Vector2 offset;
        public bool dragging;

        public override void RightMouseDown(UIMouseEvent evt)
        {
            base.RightMouseDown(evt);
            DragStart(evt);
        }

        public override void RightMouseUp(UIMouseEvent evt)
        {
            base.RightMouseUp(evt);
            DragEnd(evt);
        }

        private void DragStart(UIMouseEvent evt)
        {
            offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
            dragging = true;
        }

        private void DragEnd(UIMouseEvent evt)
        {
            Vector2 end = evt.MousePosition;
            dragging = false;

            Left.Set(end.X - offset.X, 0f);
            Top.Set(end.Y - offset.Y, 0f);

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
                Top.Set(Main.mouseY - offset.Y, 0f);

                Recalculate();
            }

            var parent = Parent.GetDimensions().ToRectangle();
            if (!GetDimensions().ToRectangle().Intersects(parent))
            {
                Left.Pixels = Utils.Clamp(Left.Pixels, 0, parent.Right - Width.Pixels);
                Top.Pixels = Utils.Clamp(Top.Pixels, 0, parent.Bottom - Height.Pixels);

                Recalculate();
            }
        }
    }
}