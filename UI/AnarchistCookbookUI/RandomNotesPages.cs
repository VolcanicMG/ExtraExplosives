using System;
using ExtraExplosives.UI.AnarchistCookbookUI.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace ExtraExplosives.UI.AnarchistCookbookUI
{
    
    //Controls the Random fuel and Short Fuze
    // Will allow for control over the effect used and fuze time
    // Fuze time can only be lowered to 1/2 the original to 2 times the total
    public class RandomNotesPages : Pages
    {
        private bool startUpFlag = false;
        internal UIImage RandomFuel;
        internal UIImage OnFire;
        internal UIImage Frostburn;
        internal UIImage Confused;
        internal UIImage ShortFuze;
        internal UIImage RandomFuelImage;
        internal UIImage RandomFuel_GreyscaleImage;
        internal UIImage ShortFuzeImage;
        internal UIImage ShortFuze_GreyscaleImage;
        internal UIPanel CaptainExplosiveNPCBox;
        internal UIPanel CaptainExplosiveBossBox;
        internal AnimatedImage CaptainExplosiveNPCAnimated;
        internal AnimatedImage CaptainExplosiveBossAnimated;
        internal UIImageButton ToggleRandomFuel;
        internal UIImageButton ToggleOnFire;
        internal UIImageButton ToggleFrostburn;
        internal UIImageButton ToggleConfused;
        internal UIImageButton ToggleShortFuze;
        internal SliderBar FuseSliderBar;
        internal UIText FuseOut;
        internal SliderBar FuzeSlider;
        public override void OnInitialize()
        {
            base.OnInitialize();    // Leave this or it wont work
            
            CookbookHeader RandomFuelHeader = new CookbookHeader("Random Fuel");
            RandomFuelHeader.HAlign = 0.6f;
            RandomFuelHeader.Top.Pixels = -20;
            RandomFuelHeader.TextColor = Color.LightGray;
            
            UIText RandomFuelFlavorText = new UIText("Kerosene and Nitroglycerin");
            RandomFuelFlavorText.HAlign = 0.5f;
            RandomFuelFlavorText.Top.Pixels = 30;
            RandomFuelFlavorText.TextColor = Color.Yellow;
            RandomFuelHeader.Append(RandomFuelFlavorText);
            
            UIText RandomFuelDescription = new UIText("Explosives are infused with\n" +
                                                            "one of three status effects");
            RandomFuelDescription.Top.Pixels = 40;
            RandomFuelDescription.HAlign = 0.7f;
            RandomFuelDescription.Left.Pixels = -10;
            RandomFuelDescription.TextColor = Color.LightGray;
            leftPage.Append(RandomFuelDescription);
            leftPage.Append(RandomFuelHeader);

            CookbookHeader ShortFuzeHeader = new CookbookHeader("Short Fuze");
            ShortFuzeHeader.HAlign = 0.65f;
            ShortFuzeHeader.Top.Pixels = -20;
            ShortFuzeHeader.TextColor = Color.LightGray;
            
            UIText ShortFuzeFlavorText = new UIText("As short as my temper");
            ShortFuzeFlavorText.HAlign = 0.5f;
            ShortFuzeFlavorText.Top.Pixels = 30;
            ShortFuzeFlavorText.TextColor = Color.Orange;
            ShortFuzeHeader.Append(ShortFuzeFlavorText);
            
            UIText ShortFuzeDescription = new UIText("Allows for chaning the fuze\n" +
                                                           "length for most  explosives\n");
            ShortFuzeDescription.Top.Pixels = 40;
            ShortFuzeDescription.HAlign = 0.85f;
            ShortFuzeDescription.Left.Pixels = -20;
            ShortFuzeDescription.TextColor = Color.LightGray;
            rightPage.Append(ShortFuzeDescription);
            rightPage.Append(ShortFuzeHeader);
            
            CaptainExplosiveBossBox = new UIPanel();
            CaptainExplosiveBossBox.Height.Pixels = 140;
            CaptainExplosiveBossBox.Width.Pixels = 200;
            CaptainExplosiveBossBox.Top.Pixels = 330;
            CaptainExplosiveBossBox.HAlign = 0.5f;
            CaptainExplosiveBossBox.Left.Pixels = -20;
            CaptainExplosiveBossBox.BackgroundColor = new Color(0,0,0,50);
            CaptainExplosiveBossBox.BorderColor = new Color(0,0,0,75);
            leftPage.Append(CaptainExplosiveBossBox);
            
            UIText foundRF = new UIText("    Dropped by: \nCaptain Explosive");
            foundRF.TextColor = Color.LightGray;
            foundRF.HAlign = 0.5f;
            foundRF.Top.Pixels = -60;
            CaptainExplosiveBossBox.Append(foundRF);
            
            UIText foundCEB = new UIText("13.33% Chance");
            foundCEB.TextColor = Color.LightGray;
            foundCEB.HAlign = 0.5f;
            foundCEB.Top.Pixels = 140;
            CaptainExplosiveBossBox.Append(foundCEB);

            CaptainExplosiveBossAnimated = new AnimatedImage("ExtraExplosives/UI/AnarchistCookbookUI/Animations/CaptainExplosiveBoss/CaptainExplosiveBoss", 4, 5);
            CaptainExplosiveBossAnimated.HAlign = 0.5f;
            CaptainExplosiveBossAnimated.VAlign = 0.5f;
            CaptainExplosiveBossAnimated.Height.Pixels = 140;
            CaptainExplosiveBossAnimated.Width.Pixels = 200;
            CaptainExplosiveBossBox.Append(CaptainExplosiveBossAnimated);
            
            CaptainExplosiveNPCBox = new UIPanel();
            CaptainExplosiveNPCBox.Height.Pixels = 112;
            CaptainExplosiveNPCBox.Width.Pixels = 80;
            CaptainExplosiveNPCBox.Top.Pixels = 330;
            CaptainExplosiveNPCBox.HAlign = 0.5f;
            CaptainExplosiveNPCBox.Left.Pixels = 10;
            CaptainExplosiveNPCBox.BackgroundColor = new Color(0,0,0,50);
            CaptainExplosiveNPCBox.BorderColor = new Color(0,0,0,75);
            rightPage.Append(CaptainExplosiveNPCBox);
            
            UIText foundSF = new UIText("     Sold By:\nCaptain Explosive");
            foundSF.TextColor = Color.LightGray;
            foundSF.HAlign = 0.5f;
            foundSF.Top.Pixels = -60;
            CaptainExplosiveNPCBox.Append(foundSF);
            
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
            
            ToggleRandomFuel = new UIImageButton(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Box"));
                RandomFuel = new UIImage(ModContent.GetTexture("ExtraExplosives/Items/Accessories/AnarchistCookbook/RandomFuel"));
                RandomFuel.VAlign = 0.5f;
                RandomFuel.HAlign = 0.5f;
                ToggleRandomFuel.Append(RandomFuel);    // Image of Random fuel for labeling
            ToggleRandomFuel.Left.Pixels = 50;
            ToggleRandomFuel.Top.Pixels = rightPage.Height.Pixels/2 - 100;
            ToggleRandomFuel.OnClick += new MouseEvent(RandomFuelToggle);
            leftPage.Append(ToggleRandomFuel);
            
            /*ToggleOnFire = new UIImageButton(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Box"));
                OnFire = new UIImage(ModContent.GetTexture("Terraria/Buff_24"));
                OnFire.VAlign = 0.5f;
                OnFire.HAlign = 0.5f;
                ToggleOnFire.Append(OnFire);    // Image of Random fuel for labeling
            ToggleOnFire.Left.Pixels = 25;
            ToggleOnFire.Top.Pixels = leftPage.Height.Pixels/2 - 75;
            ToggleOnFire.OnClick += new MouseEvent(OnFireToggle);
            leftPage.Append(ToggleOnFire);
            
            ToggleFrostburn = new UIImageButton(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Box"));
                Frostburn = new UIImage(ModContent.GetTexture("Terraria/Buff_44"));
                Frostburn.VAlign = 0.5f;
                Frostburn.HAlign = 0.5f;
                    ToggleFrostburn.Append(Frostburn);    // Image of Random fuel for labeling
            ToggleFrostburn.Left.Pixels = 250;
            ToggleFrostburn.Top.Pixels = leftPage.Height.Pixels/2 - 125;
            ToggleFrostburn.OnClick += new MouseEvent(FrostburnToggle);
            leftPage.Append(ToggleFrostburn);
            
            ToggleConfused = new UIImageButton(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Box"));
                Confused = new UIImage(ModContent.GetTexture("Terraria/Buff_31"));
                Confused.VAlign = 0.5f;
                Confused.HAlign = 0.5f;
                ToggleConfused.Append(Confused);    // Image of Random fuel for labeling
            ToggleConfused.Left.Pixels = 250;
            ToggleConfused.Top.Pixels = leftPage.Height.Pixels/2 - 75;
            ToggleConfused.OnClick += new MouseEvent(ConfusedToggle);
            leftPage.Append(ToggleConfused);*/
            
            ToggleShortFuze = new UIImageButton(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Box"));
                ShortFuze = new UIImage(ModContent.GetTexture("ExtraExplosives/Items/Accessories/AnarchistCookbook/ShortFuse"));
                ShortFuze.VAlign = 0.5f;
                ShortFuze.HAlign = 0.5f;
                ToggleShortFuze.Append(ShortFuze);    // Image of Random fuel for labeling
            ToggleShortFuze.Left.Pixels = 50;
            ToggleShortFuze.Top.Pixels = rightPage.Height.Pixels/2 - 100;
            ToggleShortFuze.OnClick += new MouseEvent(ShortFuzeToggle);
            rightPage.Append(ToggleShortFuze);
            
            FuseSliderBar = new SliderBar("Fuze Length");
            FuseSliderBar.HAlign = 0.1f;
            FuseSliderBar.VAlign = 0.5f;
            FuseSliderBar.Left.Pixels = 200;
            FuseSliderBar.Top.Pixels = rightPage.Height.Pixels/2 - 325;
            FuseSliderBar.Width.Pixels = 220;
            FuseSliderBar.Height.Pixels = 20;
            rightPage.Append(FuseSliderBar);
            
            FuseOut = new UIText("null");
            FuseOut.VAlign = 1;
            FuseOut.HAlign = 0.5f;
            FuseOut.Top.Pixels = 25;
            FuseSliderBar.Append(FuseOut);
            
            RandomFuelImage = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Accessories/RandomFuel"));
            RandomFuelImage.Left.Pixels = 0;
            RandomFuelImage.Top.Pixels = 0;
            RandomFuelImage.ImageScale = 0.8f;
            RandomFuelImage.OnClick += new MouseEvent(RandomFuelToggle);
            leftPage.Append(RandomFuelImage);
            
            ShortFuzeImage = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Accessories/ShortFuse"));
            ShortFuzeImage.Left.Pixels = 20;
            ShortFuzeImage.Top.Pixels = -16;
            ShortFuzeImage.ImageScale = 0.7f;
            ShortFuzeImage.OnClick += new MouseEvent(ShortFuzeToggle);
            rightPage.Append(ShortFuzeImage);
            
            RandomFuel_GreyscaleImage = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Accessories/RandomFuel_Greyscale"));
            RandomFuel_GreyscaleImage.Left.Pixels = 0;
            RandomFuel_GreyscaleImage.Top.Pixels = 0;
            RandomFuel_GreyscaleImage.ImageScale = 0.8f;
            RandomFuel_GreyscaleImage.OnClick += new MouseEvent(RandomFuelToggle);
            leftPage.Append(RandomFuel_GreyscaleImage);
            
            ShortFuze_GreyscaleImage = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Accessories/ShortFuse_Greyscale"));
            ShortFuze_GreyscaleImage.Left.Pixels = 20;
            ShortFuze_GreyscaleImage.Top.Pixels = -16;
            ShortFuze_GreyscaleImage.ImageScale = 0.7f;
            ShortFuze_GreyscaleImage.OnClick += new MouseEvent(ShortFuzeToggle);
            rightPage.Append(ShortFuze_GreyscaleImage);
        }

        public override void Update(GameTime gameTime)
        {
            if (!startUpFlag)
            {
                if (Main.LocalPlayer.EE().RandomFuelActive)
                {
                    leftPage.Append(RandomFuelImage);
                    ToggleRandomFuel.Append(new Active());
                }
                else
                {
                    leftPage.Append(RandomFuel_GreyscaleImage);
                    ToggleRandomFuel.Append(new Inactive());
                }

                /*
                if (Main.LocalPlayer.EE().RandomFuelOnFire)
                {
                    ToggleOnFire.Append(new Active());
                }
                else
                {
                    ToggleOnFire.Append(new Inactive());
                }

                if (Main.LocalPlayer.EE().RandomFuelFrostburn)
                {
                    ToggleFrostburn.Append(new Active());
                }
                else
                {
                    ToggleFrostburn.Append(new Inactive());
                }

                if (Main.LocalPlayer.EE().RandomFuelConfused)
                {
                    ToggleConfused.Append(new Active());
                }
                else
                {
                    ToggleConfused.Append(new Inactive());
                }
                */

                if (Main.LocalPlayer.EE().ShortFuseActive)
                {
                    rightPage.Append(ShortFuzeImage);
                    ToggleShortFuze.Append(new Active());
                }
                else
                {
                    rightPage.Append(ShortFuze_GreyscaleImage);
                    ToggleShortFuze.Append(new Inactive());
                }
                if(Main.LocalPlayer.EE().ShortFuseTime != Single.NaN && Main.LocalPlayer.EE().ShortFuseTime != null) FuseSliderBar.SetSlider((int)(Main.LocalPlayer.EE().ShortFuseTime));
                else
                {
                    Main.NewText("Something went wrong while setting up UI values");
                    Main.NewText("Please notify the Extra Explosives Devs (if this is a test build just @Charlie)");
                    FuseSliderBar.SetSlider(1);
                }
                startUpFlag = true;
            }

            if (FuseSliderBar != null && startUpFlag)
            {
                Main.LocalPlayer.EE().ShortFuseTime = FuseSliderBar.value;
            }

            if (FuseSliderBar.value != null)
            {
                FuseSliderBar.RemoveChild(FuseOut);
                float bounds = (float)Math.Round((FuseSliderBar.value - 5) / 87.5f + 0.5f, 2);
                if (bounds > 2) bounds = 2;
                string text = (bounds < 1 ? " shorter" : " longer");
                FuseOut = new UIText($"Fuze is {bounds} times" + text);
                FuseOut.VAlign = 1f;
                FuseOut.HAlign = 0.5f;
                FuseOut.Top.Pixels = 25;
                Main.LocalPlayer.EE().ShortFuseTime = bounds;
                FuseSliderBar.Append(FuseOut);
                //FuseSliderBar.SetSlider(1);
            }
            base.Update(gameTime);
            
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
        }
        
        public void RandomFuelToggle(UIMouseEvent evt, UIElement listeningElement)
        {
            ExtraExplosivesPlayer mp = Main.LocalPlayer.EE();
            mp.RandomFuelActive = !mp.RandomFuelActive;
            ToggleRandomFuel.RemoveAllChildren();
            leftPage.RemoveChild(RandomFuelImage);
            leftPage.RemoveChild(RandomFuel_GreyscaleImage);
            leftPage.Append((mp.RandomFuelActive) ? RandomFuelImage : RandomFuel_GreyscaleImage);
            ToggleRandomFuel.Append(RandomFuel);
            ToggleRandomFuel.Append((mp.RandomFuelActive) ? (UIText)(new Active()) : (UIText)(new Inactive()));
        }
        
        
        public void OnFireToggle(UIMouseEvent evt, UIElement listeningElement)
        {
            ExtraExplosivesPlayer mp = Main.LocalPlayer.EE();
            mp.RandomFuelOnFire = !mp.RandomFuelOnFire;
            ToggleOnFire.RemoveAllChildren();
            ToggleOnFire.Append(OnFire);
            ToggleOnFire.Append((mp.RandomFuelOnFire) ? (UIText)(new Active()) : (UIText)(new Inactive()));
        }
        
        public void FrostburnToggle(UIMouseEvent evt, UIElement listeningElement)
        {
            ExtraExplosivesPlayer mp = Main.LocalPlayer.EE();
            mp.RandomFuelFrostburn = !mp.RandomFuelFrostburn;
            ToggleFrostburn.RemoveAllChildren();
            ToggleFrostburn.Append(Frostburn);
            ToggleFrostburn.Append((mp.RandomFuelFrostburn) ? (UIText)(new Active()) : (UIText)(new Inactive()));
        }
        
        public void ConfusedToggle(UIMouseEvent evt, UIElement listeningElement)
        {
            ExtraExplosivesPlayer mp = Main.LocalPlayer.EE();
            mp.RandomFuelConfused = !mp.RandomFuelConfused;
            ToggleConfused.RemoveAllChildren();
            ToggleConfused.Append(Confused);
            ToggleConfused.Append((mp.RandomFuelConfused) ? (UIText)(new Active()) : (UIText)(new Inactive()));
        }

        public void ShortFuzeToggle(UIMouseEvent evt, UIElement listeningElement)
        {
            ExtraExplosivesPlayer mp = Main.LocalPlayer.EE();
            mp.ShortFuseActive = !mp.ShortFuseActive;
            ToggleShortFuze.RemoveAllChildren();
            rightPage.RemoveChild(ShortFuzeImage);
            rightPage.RemoveChild(ShortFuze_GreyscaleImage);
            rightPage.Append((mp.ShortFuseActive) ? ShortFuzeImage : ShortFuze_GreyscaleImage);
            ToggleShortFuze.Append(ShortFuze);
            ToggleShortFuze.Append((mp.ShortFuseActive) ? (UIText)(new Active()) : (UIText)(new Inactive()));
        }
    }
}