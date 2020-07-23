using ExtraExplosives.Items.Accessories.AnarchistCookbook;
using ExtraExplosives.UI.AnarchistCookbookUI.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace ExtraExplosives.UI.AnarchistCookbookUI
{
    public class ResourcefulNotesPages : Pages
    {
        private bool startUpFlag = false;
        internal UIImage BombBag;
        internal UIImage MysteryBomb;
        internal UIImage BombBagImage;
        internal UIImage BombBag_GreyscaleImage;
        internal UIImage MysteryBombImage;
        internal UIImage MysteryBomb_GreyscaleImage;
        internal UIPanel CaptainExplosiveNPCBox;
        internal UIPanel KoboldBox;
        internal UIPanel KoboldGliderBox;
        internal AnimatedImage CaptainExplosiveNPCAnimated;
        internal AnimatedImage KoboldAnimated;
        internal AnimatedImage KoboldGliderAnimated;
        internal UIImageButton ToggleBombBag;
        internal UIImageButton ToggleMysteryBomb;
        public override void OnInitialize()
        {
            base.OnInitialize();
            
            CookbookHeader BombBagHeader = new CookbookHeader("Bomb Bag");
            BombBagHeader.HAlign = 0.6f;
            BombBagHeader.Top.Pixels = -20;
            BombBagHeader.TextColor = Color.LightGray;
            
            UIText BombBagFlavorText = new UIText("Never Ending Explosives");
            BombBagFlavorText.HAlign = 0.5f;
            BombBagFlavorText.Top.Pixels = 30;
            BombBagFlavorText.TextColor = Color.GreenYellow;
            BombBagHeader.Append(BombBagFlavorText);
            
            UIText BombBagDescription = new UIText("Chance  to  throw a  second\n" +
                                                         "explosive at no addition cost");
            BombBagDescription.Top.Pixels = 40;
            BombBagDescription.HAlign = 0.69f;
            BombBagDescription.TextColor = Color.DarkGray;
            leftPage.Append(BombBagDescription);
            leftPage.Append(BombBagHeader);

            CookbookHeader MysteryBombHeader = new CookbookHeader("Mystery Bomb");
            MysteryBombHeader.HAlign = 0.75f;
            MysteryBombHeader.Top.Pixels = -20;
            MysteryBombHeader.TextColor = Color.LightGray;
            
            UIText MysteryBombFlavorText = new UIText("Strange shape for a bomb");
            MysteryBombFlavorText.HAlign = 0.5f;
            MysteryBombFlavorText.Top.Pixels = 30;
            MysteryBombFlavorText.TextColor = Color.Red;
            MysteryBombHeader.Append(MysteryBombFlavorText);
            
            UIText MysteryBombDescription = new UIText("Chance to not consume explosives");
            MysteryBombDescription.Top.Pixels = 40;
            MysteryBombDescription.HAlign = 0.95f;
            MysteryBombDescription.TextColor = Color.LightGray;
            rightPage.Append(MysteryBombDescription);
            rightPage.Append(MysteryBombHeader);
            
            CaptainExplosiveNPCBox = new UIPanel();
            CaptainExplosiveNPCBox.Height.Pixels = 112;
            CaptainExplosiveNPCBox.Width.Pixels = 80;
            CaptainExplosiveNPCBox.Top.Pixels = 330;
            CaptainExplosiveNPCBox.HAlign = 0.5f;
            CaptainExplosiveNPCBox.Left.Pixels = 10;
            CaptainExplosiveNPCBox.BackgroundColor = new Color(0,0,0,50);
            CaptainExplosiveNPCBox.BorderColor = new Color(0,0,0,75);
            leftPage.Append(CaptainExplosiveNPCBox);
            
            UIText foundBB = new UIText("     Sold By:\nCaptain Explosive");
            foundBB.TextColor = Color.LightGray;
            foundBB.HAlign = 0.5f;
            foundBB.Top.Pixels = -60;
            CaptainExplosiveNPCBox.Append(foundBB);
            
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
            
            KoboldBox = new UIPanel();
            KoboldBox.Height.Pixels = 112;
            KoboldBox.Width.Pixels = 108;
            KoboldBox.Top.Pixels = 330;
            KoboldBox.HAlign = 0.5f;
            KoboldBox.Left.Pixels = -70;
            KoboldBox.BackgroundColor = new Color(0,0,0,50);
            KoboldBox.BorderColor = new Color(0,0,0,75);
            rightPage.Append(KoboldBox);
            
            UIText foundMB1 = new UIText("                Dropped By:\n       Kobold");
            foundMB1.TextColor = Color.LightGray;
            foundMB1.HAlign = 0.5f;
            foundMB1.Top.Pixels = -60;
            foundMB1.Left.Pixels = 30;
            KoboldBox.Append(foundMB1);
            
                UIText foundK = new UIText("  Part of the:\nOld One's Army");
                foundK.TextColor = Color.LightGray;
                foundK.HAlign = 0.5f;
                foundK.Top.Pixels = 110;
                foundK.Left.Pixels = 80;
                KoboldBox.Append(foundK);

                KoboldAnimated = new AnimatedImage("ExtraExplosives/UI/AnarchistCookbookUI/Animations/Kobold/Kobold", 8, 5);
                KoboldAnimated.HAlign = 0.5f;
                KoboldAnimated.VAlign = 0.5f;
                KoboldAnimated.Height.Pixels = 112;
                KoboldAnimated.Width.Pixels = 108;
                KoboldBox.Append(KoboldAnimated);
                
                KoboldGliderBox = new UIPanel();
            KoboldGliderBox.Height.Pixels = 92;
            KoboldGliderBox.Width.Pixels = 156;
            KoboldGliderBox.Top.Pixels = 340;
            KoboldGliderBox.HAlign = 0.5f;
            KoboldGliderBox.Left.Pixels = 90;
            KoboldGliderBox.BackgroundColor = new Color(0,0,0,50);
            KoboldGliderBox.BorderColor = new Color(0,0,0,75);
            rightPage.Append(KoboldGliderBox);
            
            UIText foundMB2 = new UIText("\nKobold Glider");
            foundMB2.TextColor = Color.LightGray;
            foundMB2.HAlign = 0.5f;
            foundMB2.Top.Pixels = -60;
            KoboldGliderBox.Append(foundMB2);

            KoboldGliderAnimated = new AnimatedImage("ExtraExplosives/UI/AnarchistCookbookUI/Animations/Kobold/KoboldGliderPiece", 2, 30);
            KoboldGliderAnimated.HAlign = 0.5f;
            KoboldGliderAnimated.VAlign = 0.5f;
            KoboldGliderAnimated.Height.Pixels = 92;
            KoboldGliderAnimated.Width.Pixels = 156;
            KoboldGliderBox.Append(KoboldGliderAnimated);

            ToggleBombBag = new UIImageButton(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Box"));
                BombBag = new UIImage(ModContent.GetTexture("ExtraExplosives/Items/Accessories/AnarchistCookbook/BombBag"));
                BombBag.VAlign = 0.5f;
                BombBag.HAlign = 0.5f;
                ToggleBombBag.Append(BombBag);    // Image of bomb bag for labeling
            ToggleBombBag.Left.Pixels = 25;
            ToggleBombBag.Top.Pixels = leftPage.Height.Pixels/2 - 100;
            ToggleBombBag.OnClick += new MouseEvent(BombBagToggle);
            leftPage.Append(ToggleBombBag);
            
            ToggleMysteryBomb = new UIImageButton(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Box"));
                MysteryBomb = new UIImage(ModContent.GetTexture("ExtraExplosives/Items/Accessories/AnarchistCookbook/MysteryBomb"));
                MysteryBomb.VAlign = 0.5f;
                MysteryBomb.HAlign = 0.5f;
                ToggleMysteryBomb.Append(MysteryBomb);    // Image of bomb bag for labeling
            ToggleMysteryBomb.Left.Pixels = 50;
            ToggleMysteryBomb.Top.Pixels = leftPage.Height.Pixels/2 - 100;
            ToggleMysteryBomb.OnClick += new MouseEvent(MysteryBombToggle);
            rightPage.Append(ToggleMysteryBomb);
            
            BombBagImage = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Accessories/BombBag"));
            BombBagImage.Left.Pixels = 0;
            BombBagImage.Top.Pixels = 0;
            BombBagImage.ImageScale = 0.8f;
            BombBagImage.OnClick += new MouseEvent(BombBagToggle);
            leftPage.Append(BombBagImage);
            MysteryBombImage = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Accessories/MysteryBomb"));
            MysteryBombImage.Left.Pixels = 20;
            MysteryBombImage.Top.Pixels = -16;
            MysteryBombImage.ImageScale = 0.7f;
            MysteryBombImage.OnClick += new MouseEvent(MysteryBombToggle);
            rightPage.Append(MysteryBombImage);
            BombBag_GreyscaleImage = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Accessories/BombBag_Greyscale"));
            BombBag_GreyscaleImage.Left.Pixels = 0;
            BombBag_GreyscaleImage.Top.Pixels = 0;
            BombBag_GreyscaleImage.ImageScale = 0.8f;
            BombBag_GreyscaleImage.OnClick += new MouseEvent(BombBagToggle);
            leftPage.Append(BombBag_GreyscaleImage);
            MysteryBomb_GreyscaleImage = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/AnarchistCookbookUI/Accessories/MysteryBomb_Greyscale"));
            MysteryBomb_GreyscaleImage.Left.Pixels = 20;
            MysteryBomb_GreyscaleImage.Top.Pixels = -16;
            MysteryBomb_GreyscaleImage.ImageScale = 0.7f;
            MysteryBomb_GreyscaleImage.OnClick += new MouseEvent(MysteryBombToggle);
            rightPage.Append(MysteryBomb_GreyscaleImage);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!startUpFlag)
            {
                if (Main.LocalPlayer.EE().BombBagActive)
                {
                    leftPage.Append(BombBagImage);
                    ToggleBombBag.Append(new Active());
                }
                else
                {
                    leftPage.Append(BombBag_GreyscaleImage);
                    ToggleBombBag.Append(new Inactive());
                }
                if (Main.LocalPlayer.EE().MysteryBombActive)
                {
                    rightPage.Append(MysteryBombImage);
                    ToggleMysteryBomb.Append(new Active());
                }
                else
                {
                    rightPage.Append(MysteryBomb_GreyscaleImage);
                    ToggleMysteryBomb.Append(new Inactive());
                }
                startUpFlag = true;
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
        }
        
        public void BombBagToggle(UIMouseEvent evt, UIElement listeningElement)
        {
            ExtraExplosivesPlayer mp = Main.LocalPlayer.EE();
            mp.BombBagActive = !mp.BombBagActive;
            ToggleBombBag.RemoveAllChildren();
            leftPage.RemoveChild(BombBagImage);
            leftPage.RemoveChild(BombBag_GreyscaleImage);
            leftPage.Append((mp.BombBagActive) ? BombBagImage : BombBag_GreyscaleImage);
            ToggleBombBag.Append(BombBag);
            ToggleBombBag.Append((mp.BombBagActive) ? (UIText)new Active() : (UIText)new Inactive());
        }
        
        public void MysteryBombToggle(UIMouseEvent evt, UIElement listeningElement)
        {
            ExtraExplosivesPlayer mp = Main.LocalPlayer.EE();
            mp.MysteryBombActive = !mp.MysteryBombActive;
            ToggleMysteryBomb.RemoveAllChildren();
            rightPage.RemoveChild(MysteryBombImage);
            rightPage.RemoveChild(MysteryBomb_GreyscaleImage);
            rightPage.Append((mp.MysteryBombActive) ? MysteryBombImage : MysteryBomb_GreyscaleImage);
            ToggleMysteryBomb.Append(MysteryBomb);
            ToggleMysteryBomb.Append((mp.MysteryBombActive) ? (UIText)(new Active()) : (UIText)(new Inactive()));
        }
    }
}