using ExtraExplosives.UI.AnarchistCookbookUI.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace ExtraExplosives.UI.AnarchistCookbookUI
{
    internal class ButtonUI : UIState
    {
        public static bool Visible;
        internal CookbookButton _cookbookButton;
        
        public DragableUIPanel DragablePanel;
        
        public override void OnInitialize()
        {
            Visible = false;
            DragablePanel = new DragableUIPanel();
            DragablePanel.SetPadding(0);
            DragablePanel.Left.Set(Main.screenWidth - 300, 0f);
            DragablePanel.Top.Set(Main.screenHeight - 100,0f);
            DragablePanel.Width.Set(50,0);
            DragablePanel.Height.Set(50,0f);
            DragablePanel.BackgroundColor = new Color(0,0,0,0);
            DragablePanel.BorderColor = new Color(0,0,0,0);
            
            _cookbookButton = new CookbookButton(ModContent.GetTexture("ExtraExplosives/Items/Accessories/AnarchistCookbook/AnarchistCookbook"), "CookbookButton");
            Texture2D button1 = ModContent.GetTexture("ExtraExplosives/Items/Accessories/AnarchistCookbook/AnarchistCookbook");
            UIHoverImageButton CookbookButton = new UIHoverImageButton(button1, "Anarchist Cookbook");
            CookbookButton.Left.Set(22, 0f);
            CookbookButton.Top.Set(22,0f);
            CookbookButton.Width.Set(22,0);
            CookbookButton.Height.Set(22,0f);
            CookbookButton.OnClick += new MouseEvent(OpenCookbook);
            DragablePanel.Append(CookbookButton);
            
            
            Append(DragablePanel);
        }
        
        private void OpenCookbook(UIMouseEvent evt, UIElement listeningElement)
        {
            if (!Main.LocalPlayer.EE().AnarchistCookbook) return;

            Main.playerInventory = false;
            ModContent.GetInstance<ExtraExplosives>().cookbookInterface.SetState(new CookbookUI());
        }

        public override void Update(GameTime gameTime)
        {
            if (Main.playerInventory)
            {
                ModContent.GetInstance<ExtraExplosives>().cookbookInterface.SetState(null);
            }

            base.Update(gameTime);
        }
    }
    
    internal class CookbookButton : UIImageButton
    {
        

        public ExtraExplosivesConfig EEConfig;
        
        public DragableUIPanel DragablePanel;

        public CookbookButton(Texture2D texture, string type) : base(texture)
        {
            
        }

        public override void OnInitialize()
        {
           
            
        }
        
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
                ExtraExplosives.EEConfig.AnarchistCookbookPos = new Vector2(Left.Pixels, Top.Pixels);
            }
        }
    }
}