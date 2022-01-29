using ExtraExplosives.UI.AnarchistCookbookUI.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace ExtraExplosives.UI.AnarchistCookbookUI
{
    public class UtilityNotesPages : Pages
    {
        private bool startUpFlag = false;
        internal UIImage CrossedWires;
        internal UIImage GlowingCompound;
        internal UIImage CrossedWiresImage;
        internal UIImage CrossedWires_GreyscaleImage;
        internal UIImage GlowingCompoundImage;
        internal UIImage GlowingCompound_GreyscaleImage;
        internal UIPanel GlowingCrystalBox;
        internal UIPanel CrossedWiresBox1;
        internal UIPanel CrossedWiresBox2;
        internal AnimatedImage GlowingCrystalAnimated;
        internal AnimatedImage CrossedWiresAnimated1;
        internal UIImage CrossedWiresAnimated2;
        internal UIImageButton ToggleGlowingCompound;
        internal UIImageButton ToggleCrossedWires;
        public override void OnInitialize()
        {
            base.OnInitialize();

            CookbookHeader CrossedWiresHeader = new CookbookHeader("Crossed Wires");
            CrossedWiresHeader.HAlign = 0.6f;
            CrossedWiresHeader.Top.Pixels = -20;
            CrossedWiresHeader.TextColor = Color.LightGray;

            UIText CrossedWiresFlavorText = new UIText("Min-Maxing has never been so fun");
            CrossedWiresFlavorText.HAlign = 0.5f;
            CrossedWiresFlavorText.Top.Pixels = 30;
            CrossedWiresFlavorText.TextColor = Color.Gold;
            CrossedWiresHeader.Append(CrossedWiresFlavorText);

            UIText CrossedWiresDescription = new UIText("Increases explosive damage by 15%");
            CrossedWiresDescription.Top.Pixels = 40;
            CrossedWiresDescription.HAlign = 0.7f;
            CrossedWiresDescription.TextColor = Color.LightGray;
            leftPage.Append(CrossedWiresDescription);
            leftPage.Append(CrossedWiresHeader);

            CookbookHeader GlowingCompoundHeader = new CookbookHeader("Glowing Compound");
            GlowingCompoundHeader.HAlign = 0.75f;
            GlowingCompoundHeader.Top.Pixels = -20;
            GlowingCompoundHeader.TextColor = Color.LightGray;

            UIText GlowingCompoundFlavorText = new UIText("Signs of something more to come...");
            GlowingCompoundFlavorText.HAlign = 0.5f;
            GlowingCompoundFlavorText.Top.Pixels = 30;
            GlowingCompoundFlavorText.TextColor = Color.Chartreuse;
            GlowingCompoundHeader.Append(GlowingCompoundFlavorText);

            UIText GlowingCompoundDescription = new UIText("Bombs leave behind a glowing aura");
            GlowingCompoundDescription.Top.Pixels = 40;
            GlowingCompoundDescription.HAlign = 0.95f;
            GlowingCompoundDescription.TextColor = Color.LightGray;
            rightPage.Append(GlowingCompoundDescription);
            rightPage.Append(GlowingCompoundHeader);

            GlowingCrystalBox = new UIPanel();
            GlowingCrystalBox.Height.Pixels = 72;
            GlowingCrystalBox.Width.Pixels = 72;
            GlowingCrystalBox.Top.Pixels = 330;
            GlowingCrystalBox.HAlign = 0.5f;
            GlowingCrystalBox.Left.Pixels = 10;
            GlowingCrystalBox.BackgroundColor = new Color(0, 0, 0, 50);
            GlowingCrystalBox.BorderColor = new Color(0, 0, 0, 75);
            rightPage.Append(GlowingCrystalBox);

            UIText foundGC = new UIText("   Found In: The Hallow");
            foundGC.TextColor = Color.LightGray;
            foundGC.HAlign = 0.5f;
            foundGC.Top.Pixels = -60;
            GlowingCrystalBox.Append(foundGC);

            UIText foundRH = new UIText("When the radiation finds\n  its way into the crystals\nwhich naturally grow there");
            foundRH.TextColor = Color.LightGray;
            foundRH.HAlign = 0.5f;
            foundRH.Top.Pixels = 90;
            GlowingCrystalBox.Append(foundRH);

            GlowingCrystalAnimated = new AnimatedImage("ExtraExplosives/UI/AnarchistCookbookUI/Animations/GlowingCrystal/GlowingCrystal", 18, 60);
            GlowingCrystalAnimated.HAlign = 0.5f;
            GlowingCrystalAnimated.VAlign = 0.5f;
            GlowingCrystalAnimated.Height.Pixels = 72;
            GlowingCrystalAnimated.Width.Pixels = 72;
            GlowingCrystalBox.Append(GlowingCrystalAnimated);

            CrossedWiresBox1 = new UIPanel();
            CrossedWiresBox1.Height.Pixels = 72;
            CrossedWiresBox1.Width.Pixels = 90;
            CrossedWiresBox1.Top.Pixels = 340;
            CrossedWiresBox1.HAlign = 0.5f;
            CrossedWiresBox1.Left.Pixels = -140;
            CrossedWiresBox1.BackgroundColor = new Color(0, 0, 0, 50);
            CrossedWiresBox1.BorderColor = new Color(0, 0, 0, 75);
            leftPage.Append(CrossedWiresBox1);

            CrossedWiresAnimated1 = new AnimatedImage("ExtraExplosives/UI/AnarchistCookbookUI/Animations/CrossedWires/CrossedWires", 2, 120);
            CrossedWiresAnimated1.HAlign = 0.5f;
            CrossedWiresAnimated1.VAlign = 0.5f;
            CrossedWiresAnimated1.Height.Pixels = 72;
            CrossedWiresAnimated1.Width.Pixels = 90;
            CrossedWiresBox1.Append(CrossedWiresAnimated1);

            CrossedWiresBox2 = new UIPanel();
            CrossedWiresBox2.Height.Pixels = 100;
            CrossedWiresBox2.Width.Pixels = 100;
            CrossedWiresBox2.Top.Pixels = 330;
            CrossedWiresBox2.HAlign = 0.5f;
            CrossedWiresBox2.Left.Pixels = 90;
            CrossedWiresBox2.BackgroundColor = new Color(0, 0, 0, 50);
            CrossedWiresBox2.BorderColor = new Color(0, 0, 0, 75);
            leftPage.Append(CrossedWiresBox2);

            UIText foundCW2 = new UIText("                        Crafted with:\n" +
                                        "      Copper or Tin     and           Gel");
            foundCW2.TextColor = Color.LightGray;
            foundCW2.HAlign = 0.5f;
            foundCW2.Top.Pixels = -60;
            foundCW2.Left.Pixels = -150;
            CrossedWiresBox2.Append(foundCW2);

            UIText foundMat = new UIText("At any Anvil");
            foundMat.TextColor = Color.LightGray;
            foundMat.HAlign = 0.5f;
            foundMat.Top.Pixels = 110;
            foundMat.Left.Pixels = -120;
            CrossedWiresBox2.Append(foundMat);

            CrossedWiresAnimated2 = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Animations/CrossedWires/Gel"));
            CrossedWiresAnimated2.HAlign = 0.5f;
            CrossedWiresAnimated2.VAlign = 0.5f;
            CrossedWiresAnimated2.Height.Pixels = 100;
            CrossedWiresAnimated2.Width.Pixels = 100;
            CrossedWiresBox2.Append(CrossedWiresAnimated2);

            ToggleCrossedWires = new UIImageButton(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Box"));
            CrossedWires = new UIImage(ModContent.GetTexture("ExtraExplosives/Items/Accessories/AnarchistCookbook/CrossedWires"));
            CrossedWires.VAlign = 0.5f;
            CrossedWires.HAlign = 0.5f;
            ToggleCrossedWires.Append(CrossedWires);    // Image of bomb bag for labeling
            ToggleCrossedWires.Left.Pixels = 25;
            ToggleCrossedWires.Top.Pixels = leftPage.Height.Pixels / 2 - 100;
            ToggleCrossedWires.OnClick += new MouseEvent(CrossedWiresToggle);
            leftPage.Append(ToggleCrossedWires);

            ToggleGlowingCompound = new UIImageButton(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Box"));
            GlowingCompound = new UIImage(ModContent.GetTexture("ExtraExplosives/Items/Accessories/AnarchistCookbook/GlowingCompound"));
            GlowingCompound.VAlign = 0.5f;
            GlowingCompound.HAlign = 0.5f;
            ToggleGlowingCompound.Append(GlowingCompound);    // Image of bomb bag for labeling
            ToggleGlowingCompound.Left.Pixels = 50;
            ToggleGlowingCompound.Top.Pixels = leftPage.Height.Pixels / 2 - 100;
            ToggleGlowingCompound.OnClick += new MouseEvent(GlowingCompoundToggle);
            rightPage.Append(ToggleGlowingCompound);

            CrossedWiresImage = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Accessories/CrossedWires"));
            CrossedWiresImage.Left.Pixels = 0;
            CrossedWiresImage.Top.Pixels = 0;
            CrossedWiresImage.ImageScale = 0.8f;
            CrossedWiresImage.OnClick += new MouseEvent(CrossedWiresToggle);
            leftPage.Append(CrossedWiresImage);
            GlowingCompoundImage = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Accessories/GlowingCompound"));
            GlowingCompoundImage.Left.Pixels = 20;
            GlowingCompoundImage.Top.Pixels = -16;
            GlowingCompoundImage.ImageScale = 0.7f;
            GlowingCompoundImage.OnClick += new MouseEvent(GlowingCompoundToggle);
            rightPage.Append(GlowingCompoundImage);
            CrossedWires_GreyscaleImage = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Accessories/CrossedWires_Greyscale"));
            CrossedWires_GreyscaleImage.Left.Pixels = 0;
            CrossedWires_GreyscaleImage.Top.Pixels = 0;
            CrossedWires_GreyscaleImage.ImageScale = 0.8f;
            CrossedWires_GreyscaleImage.OnClick += new MouseEvent(CrossedWiresToggle);
            leftPage.Append(CrossedWires_GreyscaleImage);
            GlowingCompound_GreyscaleImage = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Accessories/GlowingCompound_Greyscale"));
            GlowingCompound_GreyscaleImage.Left.Pixels = 20;
            GlowingCompound_GreyscaleImage.Top.Pixels = -16;
            GlowingCompound_GreyscaleImage.ImageScale = 0.7f;
            GlowingCompound_GreyscaleImage.OnClick += new MouseEvent(GlowingCompoundToggle);
            rightPage.Append(GlowingCompound_GreyscaleImage);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!startUpFlag)
            {
                if (Main.LocalPlayer.EE().CrossedWiresActive)
                {
                    leftPage.Append(CrossedWiresImage);
                    ToggleCrossedWires.Append(new Active());
                }
                else
                {
                    leftPage.Append(CrossedWires_GreyscaleImage);
                    ToggleCrossedWires.Append(new Inactive());
                }

                if (Main.LocalPlayer.EE().GlowingCompoundActive)
                {
                    rightPage.Append(GlowingCompoundImage);
                    ToggleGlowingCompound.Append(new Active());
                }
                else
                {
                    rightPage.Append(GlowingCompound_GreyscaleImage);
                    ToggleGlowingCompound.Append(new Inactive());
                }
                startUpFlag = true;
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
        }

        public void CrossedWiresToggle(UIMouseEvent evt, UIElement listeningElement)
        {
            ExtraExplosivesPlayer mp = Main.LocalPlayer.EE();
            mp.CrossedWiresActive = !mp.CrossedWiresActive;
            ToggleCrossedWires.RemoveAllChildren();
            leftPage.RemoveChild(CrossedWiresImage);
            leftPage.RemoveChild(CrossedWires_GreyscaleImage);
            leftPage.Append((mp.CrossedWiresActive) ? CrossedWiresImage : CrossedWires_GreyscaleImage);
            ToggleCrossedWires.Append(CrossedWires);
            ToggleCrossedWires.Append((mp.CrossedWiresActive) ? (UIText)new Active() : (UIText)new Inactive());
        }

        public void GlowingCompoundToggle(UIMouseEvent evt, UIElement listeningElement)
        {
            ExtraExplosivesPlayer mp = Main.LocalPlayer.EE();
            mp.GlowingCompoundActive = !mp.GlowingCompoundActive;
            ToggleGlowingCompound.RemoveAllChildren();
            rightPage.RemoveChild(GlowingCompoundImage);
            rightPage.RemoveChild(GlowingCompound_GreyscaleImage);
            rightPage.Append((mp.GlowingCompoundActive) ? GlowingCompoundImage : GlowingCompound_GreyscaleImage);
            ToggleGlowingCompound.Append(GlowingCompound);
            ToggleGlowingCompound.Append((mp.GlowingCompoundActive) ? (UIText)(new Active()) : (UIText)(new Inactive()));
        }
    }
}