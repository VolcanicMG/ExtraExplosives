using ExtraExplosives.UI.AnarchistCookbookUI.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

// BUG Loading is unreliable

namespace ExtraExplosives.UI.AnarchistCookbookUI
{
    public class HandyNotesPages : Pages
    {
        private bool? toggle = null;
        private bool startUpFlag = false;
        internal UIImage StickyGunpowder;
        internal UIImage LightweightBombshells;
        internal UIImage StickyGunpowderImage;
        internal UIImage StickyGunpowder_GreyscaleImage;
        internal UIImage LightweightBombshellsImage;
        internal UIImage LightweightBombshells_GreyscaleImage;
        internal UIPanel CaptainExplosiveNPCBox;
        internal UIPanel StickyGunpowderBox1;
        internal UIPanel StickyGunpowderBox2;
        internal AnimatedImage CaptainExplosiveNPCAnimated;
        internal UIImage StickyGunpowderAnimated1;
        internal UIImage StickyGunpowderAnimated2;
        internal UIImageButton Toggle;
        internal UIToggleImage Check;
        internal SliderBar VelocitySliderBar;
        internal UIText VelocityOut;
        internal UIImageButton ToggleLightweightBombshells;
        internal UIImageButton ToggleStickyGunpowder;

        public override void OnInitialize()
        {
            base.OnInitialize();

            CookbookHeader StickyGunpowderHeader = new CookbookHeader("Sticky Gunpowder");
            StickyGunpowderHeader.HAlign = 0.6f;
            StickyGunpowderHeader.Top.Pixels = -20;
            StickyGunpowderHeader.TextColor = Color.LightGray;

            UIText StickyGunpowderFlavorText = new UIText("Gorillas and Ducks");
            StickyGunpowderFlavorText.HAlign = 0.5f;
            StickyGunpowderFlavorText.Top.Pixels = 30;
            StickyGunpowderFlavorText.TextColor = Color.AliceBlue;
            StickyGunpowderHeader.Append(StickyGunpowderFlavorText);

            UIText StickyGunpowderDescription = new UIText("   Explosives stick to surfaces\n" +
                                                                 "Works with bombs and grenades");
            StickyGunpowderDescription.Top.Pixels = 40;
            StickyGunpowderDescription.HAlign = 0.7f;
            StickyGunpowderDescription.TextColor = Color.LightGray;
            leftPage.Append(StickyGunpowderDescription);
            leftPage.Append(StickyGunpowderHeader);

            CookbookHeader LightweightBombshellsHeader = new CookbookHeader("Lightweight Bombshells");
            LightweightBombshellsHeader.HAlign = 0.75f;
            LightweightBombshellsHeader.Top.Pixels = -20;
            LightweightBombshellsHeader.TextColor = Color.LightGray;

            UIText LightweightBombshellsFlavorText = new UIText("100% more helium");
            LightweightBombshellsFlavorText.HAlign = 0.5f;
            LightweightBombshellsFlavorText.Top.Pixels = 30;
            LightweightBombshellsFlavorText.TextColor = Color.DarkGray;
            LightweightBombshellsHeader.Append(LightweightBombshellsFlavorText);

            UIText LightweightBombshellsDescription = new UIText("Allows you to throw explosives\n" +
                                                                       " further than ever before.");
            LightweightBombshellsDescription.Top.Pixels = 40;
            LightweightBombshellsDescription.HAlign = 0.85f;
            LightweightBombshellsDescription.TextColor = Color.LightGray;
            rightPage.Append(LightweightBombshellsDescription);
            rightPage.Append(LightweightBombshellsHeader);

            StickyGunpowderBox1 = new UIPanel();
            StickyGunpowderBox1.Height.Pixels = 100;
            StickyGunpowderBox1.Width.Pixels = 100;
            StickyGunpowderBox1.Top.Pixels = 340;
            StickyGunpowderBox1.HAlign = 0.5f;
            StickyGunpowderBox1.Left.Pixels = -140;
            StickyGunpowderBox1.BackgroundColor = new Color(0, 0, 0, 50);
            StickyGunpowderBox1.BorderColor = new Color(0, 0, 0, 75);
            leftPage.Append(StickyGunpowderBox1);

            StickyGunpowderAnimated1 = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Animations/StickyGunpowder/Gel"));
            StickyGunpowderAnimated1.HAlign = 0.5f;
            StickyGunpowderAnimated1.VAlign = 0.5f;
            StickyGunpowderAnimated1.Top.Pixels = -10;
            StickyGunpowderAnimated1.Height.Pixels = 54;
            StickyGunpowderAnimated1.Width.Pixels = 78;
            StickyGunpowderBox1.Append(StickyGunpowderAnimated1);

            StickyGunpowderBox2 = new UIPanel();
            StickyGunpowderBox2.Height.Pixels = 100;
            StickyGunpowderBox2.Width.Pixels = 100;
            StickyGunpowderBox2.Top.Pixels = 330;
            StickyGunpowderBox2.HAlign = 0.5f;
            StickyGunpowderBox2.Left.Pixels = 90;
            StickyGunpowderBox2.BackgroundColor = new Color(0, 0, 0, 50);
            StickyGunpowderBox2.BorderColor = new Color(0, 0, 0, 75);
            leftPage.Append(StickyGunpowderBox2);

            UIText foundCW2 = new UIText("               Crafted with:\n" +
                                        "    Gel           and       Explosive Powder");
            foundCW2.TextColor = Color.LightGray;
            foundCW2.HAlign = 0.5f;
            foundCW2.Left.Pixels = -100;
            foundCW2.Top.Pixels = -60;
            StickyGunpowderBox2.Append(foundCW2);

            UIText foundMat = new UIText("At any Anvil");
            foundMat.TextColor = Color.LightGray;
            foundMat.HAlign = 0.5f;
            foundMat.Top.Pixels = 110;
            foundMat.Left.Pixels = -115;
            StickyGunpowderBox2.Append(foundMat);

            StickyGunpowderAnimated2 = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Animations/StickyGunpowder/ExplosivePowder"));
            StickyGunpowderAnimated2.HAlign = 0.5f;
            StickyGunpowderAnimated2.VAlign = 0.5f;
            StickyGunpowderAnimated2.Top.Pixels = 10;
            StickyGunpowderAnimated2.Height.Pixels = 100;
            StickyGunpowderAnimated2.Width.Pixels = 100;
            StickyGunpowderBox2.Append(StickyGunpowderAnimated2);

            CaptainExplosiveNPCBox = new UIPanel();
            CaptainExplosiveNPCBox.Height.Pixels = 112;
            CaptainExplosiveNPCBox.Width.Pixels = 80;
            CaptainExplosiveNPCBox.Top.Pixels = 330;
            CaptainExplosiveNPCBox.HAlign = 0.5f;
            CaptainExplosiveNPCBox.Left.Pixels = 10;
            CaptainExplosiveNPCBox.BackgroundColor = new Color(0, 0, 0, 50);
            CaptainExplosiveNPCBox.BorderColor = new Color(0, 0, 0, 75);
            rightPage.Append(CaptainExplosiveNPCBox);

            UIText foundLB = new UIText("     Sold By:\nCaptain Explosive");
            foundLB.TextColor = Color.LightGray;
            foundLB.HAlign = 0.5f;
            foundLB.Top.Pixels = -60;
            CaptainExplosiveNPCBox.Append(foundLB);

            UIText foundCENPC = new UIText("Always");
            foundCENPC.TextColor = Color.LightGray;
            foundCENPC.HAlign = 0.5f;
            foundCENPC.Top.Pixels = 110;
            CaptainExplosiveNPCBox.Append(foundCENPC);

            CaptainExplosiveNPCAnimated = new AnimatedImage("ExtraExplosives/UI/AnarchistCookbookUI/Animations/CaptainExplosiveNPC/CaptainExplosiveNPC", 13, 5);
            CaptainExplosiveNPCAnimated.HAlign = 0.5f;
            CaptainExplosiveNPCAnimated.VAlign = 0.5f;
            CaptainExplosiveNPCAnimated.Height.Pixels = 112;
            CaptainExplosiveNPCAnimated.Width.Pixels = 80;
            CaptainExplosiveNPCBox.Append(CaptainExplosiveNPCAnimated);

            ToggleStickyGunpowder = new UIHoverImageButton(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Box"), "Sticky Gunpowder");
            StickyGunpowder = new UIImage(ModContent.GetTexture("ExtraExplosives/Items/Accessories/AnarchistCookbook/StickyGunpowder"));
            StickyGunpowder.VAlign = 0.5f;
            StickyGunpowder.HAlign = 0.5f;
            ToggleStickyGunpowder.Append(StickyGunpowder);    // Image of Sticky Gunpowder for labeling
            ToggleStickyGunpowder.Left.Pixels = 25;
            ToggleStickyGunpowder.Top.Pixels = leftPage.Height.Pixels / 2 - 100;
            ToggleStickyGunpowder.OnClick += new MouseEvent(StickyGunpowderToggle);
            leftPage.Append(ToggleStickyGunpowder);

            ToggleLightweightBombshells = new UIHoverImageButton(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Box"), "Lightweight Bombshells");
            LightweightBombshells = new UIImage(ModContent.GetTexture("ExtraExplosives/Items/Accessories/AnarchistCookbook/LightweightBombshells"));
            LightweightBombshells.VAlign = 0.5f;
            LightweightBombshells.HAlign = 0.5f;
            ToggleLightweightBombshells.Append(LightweightBombshells);    // Image of Sticky Gunpowder for labeling
            ToggleLightweightBombshells.Left.Pixels = 50;
            ToggleLightweightBombshells.Top.Pixels = rightPage.Height.Pixels / 2 - 100;
            ToggleLightweightBombshells.OnClick += new MouseEvent(LightweightBombshellsToggle);
            rightPage.Append(ToggleLightweightBombshells);

            VelocitySliderBar = new SliderBar("Bombshell Weight");
            VelocitySliderBar.HAlign = 0.1f;
            VelocitySliderBar.VAlign = 0.5f;
            VelocitySliderBar.Left.Pixels = 200;
            VelocitySliderBar.Top.Pixels = rightPage.Height.Pixels / 2 - 325;
            VelocitySliderBar.Width.Pixels = 220;
            VelocitySliderBar.Height.Pixels = 20;
            rightPage.Append(VelocitySliderBar);

            VelocityOut = new UIText("null");
            VelocityOut.VAlign = 1;
            VelocityOut.HAlign = 0.5f;
            VelocityOut.Top.Pixels = 25;
            VelocitySliderBar.Append(VelocityOut);

            StickyGunpowderImage = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Accessories/StickyGunpowder"));
            StickyGunpowderImage.Left.Pixels = 0;
            StickyGunpowderImage.Top.Pixels = 0;
            StickyGunpowderImage.ImageScale = 0.8f;
            StickyGunpowderImage.OnClick += new MouseEvent(StickyGunpowderToggle);
            leftPage.Append(StickyGunpowderImage);
            LightweightBombshellsImage = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Accessories/LightweightBombshells"));
            LightweightBombshellsImage.Left.Pixels = 20;
            LightweightBombshellsImage.Top.Pixels = -16;
            LightweightBombshellsImage.ImageScale = 0.7f;
            LightweightBombshellsImage.OnClick += new MouseEvent(LightweightBombshellsToggle);
            rightPage.Append(LightweightBombshellsImage);
            StickyGunpowder_GreyscaleImage = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Accessories/StickyGunpowder_Greyscale"));
            StickyGunpowder_GreyscaleImage.Left.Pixels = 0;
            StickyGunpowder_GreyscaleImage.Top.Pixels = 0;
            StickyGunpowder_GreyscaleImage.ImageScale = 0.8f;
            StickyGunpowder_GreyscaleImage.OnClick += new MouseEvent(StickyGunpowderToggle);
            leftPage.Append(StickyGunpowder_GreyscaleImage);
            LightweightBombshells_GreyscaleImage = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Accessories/LightweightBombshells_Greyscale"));
            LightweightBombshells_GreyscaleImage.Left.Pixels = 20;
            LightweightBombshells_GreyscaleImage.Top.Pixels = -16;
            LightweightBombshells_GreyscaleImage.ImageScale = 0.7f;
            LightweightBombshells_GreyscaleImage.OnClick += new MouseEvent(LightweightBombshellsToggle);
            rightPage.Append(LightweightBombshells_GreyscaleImage);
        }

        public override void Update(GameTime gameTime)
        {
            if (!startUpFlag)
            {
                if (Main.LocalPlayer.EE().StickyGunpowderActive)
                {
                    leftPage.Append(StickyGunpowderImage);
                    ToggleStickyGunpowder.Append(new Active());
                }
                else
                {
                    leftPage.Append(StickyGunpowder_GreyscaleImage);
                    ToggleStickyGunpowder.Append(new Inactive());
                }
                if (Main.LocalPlayer.EE().LightweightBombshellsActive)
                {
                    rightPage.Append(LightweightBombshellsImage);
                    ToggleLightweightBombshells.Append(new Active());
                }
                else
                {
                    rightPage.Append(LightweightBombshells_GreyscaleImage);
                    ToggleLightweightBombshells.Append(new Inactive());
                }
                VelocitySliderBar.SetSlider((int)(Main.LocalPlayer.EE().LightweightBombshellVelocity));
                startUpFlag = true;
            }

            if (VelocitySliderBar != null && startUpFlag)
            {
                Main.LocalPlayer.EE().LightweightBombshellVelocity = VelocitySliderBar.value;
            }

            if (VelocitySliderBar.value != null)
            {
                VelocitySliderBar.RemoveChild(VelocityOut);
                VelocityOut = new UIText($"Current Weight: {1 + (VelocitySliderBar.value) / 12}kg");
                VelocityOut.VAlign = 1;
                VelocityOut.HAlign = 0.5f;
                VelocityOut.Top.Pixels = 25;
                VelocitySliderBar.Append(VelocityOut);
            }
            base.Update(gameTime);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
        }

        public void StickyGunpowderToggle(UIMouseEvent evt, UIElement listeningElement)
        {
            ExtraExplosivesPlayer mp = Main.LocalPlayer.EE();
            mp.StickyGunpowderActive = !mp.StickyGunpowderActive;
            ToggleStickyGunpowder.RemoveAllChildren();
            leftPage.RemoveChild(StickyGunpowderImage);
            leftPage.RemoveChild(StickyGunpowder_GreyscaleImage);
            leftPage.Append(mp.StickyGunpowderActive ? StickyGunpowderImage : StickyGunpowder_GreyscaleImage);
            ToggleStickyGunpowder.Append(StickyGunpowder);
            ToggleStickyGunpowder.Append(mp.StickyGunpowderActive ? (UIText)new Active() : (UIText)new Inactive());
        }

        public void LightweightBombshellsToggle(UIMouseEvent evt, UIElement listeningElement)
        {
            ExtraExplosivesPlayer mp = Main.LocalPlayer.EE();
            mp.LightweightBombshellsActive = !mp.LightweightBombshellsActive;
            ToggleLightweightBombshells.RemoveAllChildren();
            rightPage.RemoveChild(LightweightBombshellsImage);
            rightPage.RemoveChild(LightweightBombshells_GreyscaleImage);
            rightPage.Append(mp.LightweightBombshellsActive ? LightweightBombshellsImage : LightweightBombshells_GreyscaleImage);
            ToggleLightweightBombshells.Append(LightweightBombshells);
            ToggleLightweightBombshells.Append(mp.LightweightBombshellsActive ? (UIText)(new Active()) : (UIText)(new Inactive()));
        }
    }
}