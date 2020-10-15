using ExtraExplosives.UI.AnarchistCookbookUI.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace ExtraExplosives.UI.AnarchistCookbookUI
{
    public class SafetyNotesPages : Pages
    {
        private int animationCounterSC { get; set; } = 0;
        private bool startUpFlag = false;
        internal UIImage BlastShielding;
        internal UIImage ReactivePlating;
        internal UIImage BlastShieldingImage;
        internal UIImage BlastShielding_GreyscaleImage;
        internal UIImage ReactivePlatingImage;
        internal UIImage ReactivePlating_GreyscaleImage;
        internal UIPanel SkeletonCommandoBox;
        internal UIPanel TacticalSkeletonBox;
        internal AnimatedImage SkeletonCommandoAnimated;
        internal AnimatedImage TacticalSkeletonAnimated;
        internal UIImageButton ToggleBlastShielding;
        internal UIImageButton ToggleReactivePlating;
        public override void OnInitialize()
        {
            base.OnInitialize();
            
            CookbookHeader BlastShieldHeader = new CookbookHeader("Blast Shielding");
            BlastShieldHeader.HAlign = 0.6f;
            BlastShieldHeader.Top.Pixels = -20;
            BlastShieldHeader.TextColor = Color.LightGray;
            
            UIText BlastShieldFlavorText = new UIText("Caution: Radioactive");
            BlastShieldFlavorText.HAlign = 0.5f;
            BlastShieldFlavorText.Top.Pixels = 30;
            BlastShieldFlavorText.TextColor = Color.OliveDrab;
            BlastShieldHeader.Append(BlastShieldFlavorText);
            
            UIText BlastShieldDescription = new UIText("Prevents  the  user  from taking\n" +
                                                             "damage from their own explosives");
            BlastShieldDescription.Top.Pixels = 40;
            BlastShieldDescription.HAlign = 0.7f;
            BlastShieldDescription.TextColor = Color.LightGray;
            leftPage.Append(BlastShieldDescription);
            leftPage.Append(BlastShieldHeader);

            SkeletonCommandoBox = new UIPanel();
            SkeletonCommandoBox.Height.Pixels = 100;
            SkeletonCommandoBox.Width.Pixels = 100;
            SkeletonCommandoBox.Top.Pixels = 330;
            SkeletonCommandoBox.HAlign = 0.5f;
            SkeletonCommandoBox.Left.Pixels = -10;
            SkeletonCommandoBox.BackgroundColor = new Color(0,0,0,50);
            SkeletonCommandoBox.BorderColor = new Color(0,0,0,75);
            leftPage.Append(SkeletonCommandoBox);
            
            UIText foundBS = new UIText("     Dropped by:\nSkeleton Commandos");
            foundBS.TextColor = Color.LightGray;
            foundBS.HAlign = 0.5f;
            foundBS.Top.Pixels = -60;
            SkeletonCommandoBox.Append(foundBS);
            
            UIText foundSC = new UIText("1% Chance");
            foundSC.TextColor = Color.LightGray;
            foundSC.HAlign = 0.5f;
            foundSC.Top.Pixels = 100;
            SkeletonCommandoBox.Append(foundSC);

            SkeletonCommandoAnimated = new AnimatedImage("ExtraExplosives/UI/AnarchistCookbookUI/Animations/SkeletonCommando/SkeletonCommando", 14, 5);
            SkeletonCommandoAnimated.HAlign = 0.5f;
            SkeletonCommandoAnimated.VAlign = 0.5f;
            SkeletonCommandoAnimated.Height.Pixels = 100;
            SkeletonCommandoAnimated.Width.Pixels = 100;
            SkeletonCommandoBox.Append(SkeletonCommandoAnimated);
            
            TacticalSkeletonBox = new UIPanel();
            TacticalSkeletonBox.Height.Pixels = 100;
            TacticalSkeletonBox.Width.Pixels = 100;
            TacticalSkeletonBox.Top.Pixels = 330;
            TacticalSkeletonBox.HAlign = 0.5f;
            TacticalSkeletonBox.BackgroundColor = new Color(0,0,0,50);
            TacticalSkeletonBox.BorderColor = new Color(0,0,0,75);
            rightPage.Append(TacticalSkeletonBox);
            
            UIText foundRP = new UIText("    Dropped by: \nTactical Skeletons");
            foundRP.TextColor = Color.LightGray;
            foundRP.HAlign = 0.5f;
            foundRP.Top.Pixels = -60;
            TacticalSkeletonBox.Append(foundRP);
            
            UIText foundTS = new UIText("1% Chance");
            foundTS.TextColor = Color.LightGray;
            foundTS.HAlign = 0.5f;
            foundTS.Top.Pixels = 100;
            TacticalSkeletonBox.Append(foundTS);

            TacticalSkeletonAnimated = new AnimatedImage("ExtraExplosives/UI/AnarchistCookbookUI/Animations/TacticalSkeleton/TacticalSkeleton", 14, 5);
            TacticalSkeletonAnimated.HAlign = 0.5f;
            TacticalSkeletonAnimated.VAlign = 0.5f;
            TacticalSkeletonAnimated.Height.Pixels = 100;
            TacticalSkeletonAnimated.Width.Pixels = 100;
            TacticalSkeletonBox.Append(TacticalSkeletonAnimated);
            
            CookbookHeader ReactivePlatingHeader = new CookbookHeader("Reactive Plating");
            ReactivePlatingHeader.HAlign = 0.75f;
            ReactivePlatingHeader.Top.Pixels = -20;
            ReactivePlatingHeader.TextColor = Color.LightGray;
            
            UIText ReactivePlatingFlavorText = new UIText("Made of Depleted Uranium");
            ReactivePlatingFlavorText.HAlign = 0.5f;
            ReactivePlatingFlavorText.Top.Pixels = 30;
            ReactivePlatingFlavorText.TextColor = Color.Crimson;
            ReactivePlatingHeader.Append(ReactivePlatingFlavorText);
            
            UIText ReactivePlatingDescription = new UIText("Increases  damage  output;\n" +
                                                                 "Decreases   damage  taken;");
            ReactivePlatingDescription.Top.Pixels = 40;
            ReactivePlatingDescription.HAlign = 0.82f;
            ReactivePlatingDescription.Left.Pixels = 10;
            ReactivePlatingDescription.TextColor = Color.LightGray;
            rightPage.Append(ReactivePlatingDescription);
            rightPage.Append(ReactivePlatingHeader);
            
            ToggleBlastShielding = new UIImageButton(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Box"));
                BlastShielding = new UIImage(ModContent.GetTexture("ExtraExplosives/Items/Accessories/AnarchistCookbook/BlastShielding"));
                BlastShielding.VAlign = 0.5f;
                BlastShielding.HAlign = 0.5f;
                ToggleBlastShielding.Append(BlastShielding);    // Image of bomb bag for labeling
            ToggleBlastShielding.Left.Pixels = 25;
            ToggleBlastShielding.Top.Pixels = leftPage.Height.Pixels/2 - 100;
            ToggleBlastShielding.OnClick += new MouseEvent(BlastShieldingToggle);
            leftPage.Append(ToggleBlastShielding);
            
            ToggleReactivePlating = new UIImageButton(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Box"));
                ReactivePlating = new UIImage(ModContent.GetTexture("ExtraExplosives/Items/Accessories/AnarchistCookbook/ReactivePlating"));
                ReactivePlating.VAlign = 0.5f;
                ReactivePlating.HAlign = 0.5f;
                ToggleReactivePlating.Append(ReactivePlating);    // Image of bomb bag for labeling
            ToggleReactivePlating.Left.Pixels = 50;
            ToggleReactivePlating.Top.Pixels = rightPage.Height.Pixels/2 - 100;
            ToggleReactivePlating.OnClick += new MouseEvent(ReactivePlatingToggle);
            rightPage.Append(ToggleReactivePlating);
            
            BlastShieldingImage = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Accessories/BlastShielding"));
            BlastShieldingImage.Left.Pixels = 0;
            BlastShieldingImage.Top.Pixels = 0;
            BlastShieldingImage.ImageScale = 0.8f;
            BlastShieldingImage.OnClick += new MouseEvent(BlastShieldingToggle);
            //leftPage.Append(BlastShieldingImage);
            ReactivePlatingImage = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Accessories/ReactivePlating"));
            ReactivePlatingImage.Left.Pixels = 20;
            ReactivePlatingImage.Top.Pixels = -16;
            ReactivePlatingImage.ImageScale = 0.7f;
            ReactivePlatingImage.OnClick += new MouseEvent(ReactivePlatingToggle);
            //rightPage.Append(ReactivePlatingImage);
            BlastShielding_GreyscaleImage = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Accessories/BlastShielding_Greyscale"));
            BlastShielding_GreyscaleImage.Left.Pixels = 0;
            BlastShielding_GreyscaleImage.Top.Pixels = 0;
            BlastShielding_GreyscaleImage.ImageScale = 0.8f;
            BlastShielding_GreyscaleImage.OnClick += new MouseEvent(BlastShieldingToggle);
            //leftPage.Append(BlastShielding_GreyscaleImage);
            ReactivePlating_GreyscaleImage = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Accessories/ReactivePlating_Greyscale"));
            ReactivePlating_GreyscaleImage.Left.Pixels = 20;
            ReactivePlating_GreyscaleImage.Top.Pixels = -16;
            ReactivePlating_GreyscaleImage.ImageScale = 0.7f;
            ReactivePlating_GreyscaleImage.OnClick += new MouseEvent(ReactivePlatingToggle);
            //rightPage.Append(ReactivePlating_GreyscaleImage);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            if (!startUpFlag)
            {
                if (Main.LocalPlayer.EE().ReactivePlatingActive)
                {
                    leftPage.Append(BlastShieldingImage);
                    ToggleBlastShielding.Append(new Active());
                }
                else
                {
                    leftPage.Append(BlastShielding_GreyscaleImage);
                    ToggleBlastShielding.Append(new Inactive());
                }
                if (Main.LocalPlayer.EE().BlastShieldingActive)
                {
                    rightPage.Append(ReactivePlatingImage);
                    ToggleReactivePlating.Append(new Active());
                }
                else
                {
                    rightPage.Append(ReactivePlating_GreyscaleImage);
                    ToggleReactivePlating.Append(new Inactive());
                }
                startUpFlag = true;
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
        }
        public void BlastShieldingToggle(UIMouseEvent evt, UIElement listeningElement)
        {
            ExtraExplosivesPlayer mp = Main.LocalPlayer.EE();
            mp.BlastShieldingActive = !mp.BlastShieldingActive;
            ToggleBlastShielding.RemoveAllChildren();
            leftPage.RemoveChild(BlastShieldingImage);
            leftPage.RemoveChild(BlastShielding_GreyscaleImage);
            leftPage.Append((mp.BlastShieldingActive) ? BlastShieldingImage : BlastShielding_GreyscaleImage);
            ToggleBlastShielding.Append(BlastShielding);
            ToggleBlastShielding.Append((mp.BlastShieldingActive) ? (UIText)new Active() : (UIText)new Inactive());
        }
        
        public void ReactivePlatingToggle(UIMouseEvent evt, UIElement listeningElement)
        {
            ExtraExplosivesPlayer mp = Main.LocalPlayer.EE();
            mp.ReactivePlatingActive = !mp.ReactivePlatingActive;
            ToggleReactivePlating.RemoveAllChildren();
            rightPage.RemoveChild(ReactivePlatingImage);
            rightPage.RemoveChild(ReactivePlating_GreyscaleImage);
            rightPage.Append((mp.ReactivePlatingActive) ? ReactivePlatingImage : ReactivePlating_GreyscaleImage);
            ToggleReactivePlating.Append(ReactivePlating);
            ToggleReactivePlating.Append((mp.ReactivePlatingActive) ? (UIText)(new Active()) : (UIText)(new Inactive()));
        }
    }
}