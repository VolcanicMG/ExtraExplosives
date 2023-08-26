using ExtraExplosives.UI.AnarchistCookbookUI.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace ExtraExplosives.UI.AnarchistCookbookUI
{
    internal class CookbookUI : UIState
    {
        internal float horizontalScale = Main.screenHeight / 1017f;    // Current height divided by optimal height
        internal float verticalScale = Main.screenWidth / 1920f;    // Current width by optimal width
        //internal UIPanel baseLayer;
        internal UIImage cookbookSkin;
        internal HandyNotesPages handyNotesPages;
        internal RandomNotesPages randomNotesPages;
        internal ResourcefulNotesPages resourcefulNotesPages;
        internal SafetyNotesPages safetyNotesPages;
        internal UtilityNotesPages utilityNotesPages;
        internal UIPanel leftPage;
        internal UIPanel rightPage;
        internal UIHoverImage HandyNotes;
        internal UIHoverImage RandomNotes;
        internal UIHoverImage ResourcefulNotes;
        internal UIHoverImage SafetyNotes;
        internal UIHoverImage UtilityNotes;
        internal UIImage HandyNotesImg;

        internal UIElement tabs;

        internal UIPanel test;
        //internal UIImage RandomNotesImg;
        //internal UIImage ResourcefulNotesImg;
        //internal UIImage SafetyNotesImg;
        //internal UIImage UtilityNotesImg;
        //
        private bool originalDrawUpdate = false;


        //public static bool Visible;

        public override void OnInitialize()
        {
            //Visible = false;

            /*
            baseLayer = new UIPanel();
            baseLayer.HAlign = 0.5f;
            baseLayer.VAlign = 0.5f;
            baseLayer.Width.Set(1358 * horizontalScale,0);
            baseLayer.Height.Set(622 * verticalScale,0);
            baseLayer.BackgroundColor = new Color(0,0,0,100);
            baseLayer.BorderColor = new Color(0,0,0,0);
            */
            cookbookSkin = new UIImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/AnarchistCookbookUI/CookbookSkin"));
            //cookbookSkin.ImageScale = horizontalScale;
            cookbookSkin.Width.Pixels = 1358;// * horizontalScale;
            cookbookSkin.Height.Pixels = 622;// * verticalScale;
            cookbookSkin.Left.Pixels = Main.screenWidth / 2 - cookbookSkin.Width.Pixels / 2;
            cookbookSkin.Top.Pixels = Main.screenHeight / 2 - cookbookSkin.Height.Pixels / 2;
            //cookbookSkin.ImageScale = horizontalScale;
            //baseLayer.Append(cookbookSkin);


            tabs = new UIElement();
            tabs.Width.Pixels = 300;
            tabs.Height.Pixels = cookbookSkin.Width.Pixels;
            test = new UIPanel();
            test.BackgroundColor = Color.Black;
            test.Width = tabs.Width;
            test.Height = tabs.Height;
            tabs.VAlign = 0.5f;
            //tabs.Top.Pixels = -32 * verticalScale;
            tabs.Left.Pixels = cookbookSkin.Width.Pixels - 180;
            //tabs.Append(test);
            //tabs.Left.Pixels = (cookbookSkin.Width.Pixels - 180) * horizontalScale;
            //tabs.Left.Pixels = Main.screenWidth / 2;// + cookbookSkin.Width.Pixels / 2;
            //cookbookSkin.Append(tabs);

            handyNotesPages = new HandyNotesPages();
            handyNotesPages.BackgroundColor = new Color(100, 100, 100);
            cookbookSkin.Append(handyNotesPages);

            randomNotesPages = new RandomNotesPages();
            randomNotesPages.BackgroundColor = new Color(100, 100, 100);
            cookbookSkin.Append(randomNotesPages);

            resourcefulNotesPages = new ResourcefulNotesPages();
            resourcefulNotesPages.BackgroundColor = new Color(100, 100, 100);
            cookbookSkin.Append(resourcefulNotesPages);

            safetyNotesPages = new SafetyNotesPages();
            safetyNotesPages.BackgroundColor = new Color(100, 100, 100);
            cookbookSkin.Append(safetyNotesPages);

            utilityNotesPages = new UtilityNotesPages();
            utilityNotesPages.BackgroundColor = new Color(100, 100, 100);
            cookbookSkin.Append(utilityNotesPages);

            HandyNotes = new UIHoverImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/AnarchistCookbookUI/HandyNotesTab").Value, "Handy Notes");
            HandyNotes.Left.Set(8, 0);
            HandyNotes.Top.Set(-24, 0);
            HandyNotes.VAlign = 1 / 6f;
            HandyNotes.Width.Set(60 * horizontalScale, 0);
            HandyNotes.Height.Set(100 * verticalScale, 0);
            HandyNotes.OnLeftClick += new MouseEvent(MoveToHandyNotesPage);
            tabs.Append(HandyNotes);

            RandomNotes = new UIHoverImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/AnarchistCookbookUI/RandomNotesTab").Value, "Random Notes");
            RandomNotes.Left.Set(0, 0);
            RandomNotes.Top.Set(-12, 0);
            RandomNotes.VAlign = 2 / 6f;
            RandomNotes.Width.Set(60 * horizontalScale, 0);
            RandomNotes.Height.Set(100 * verticalScale, 0);
            RandomNotes.OnLeftClick += new MouseEvent(MoveToRandomNotesPage);
            tabs.Append(RandomNotes);

            ResourcefulNotes = new UIHoverImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/AnarchistCookbookUI/ResourcefulNotesTab").Value, "Resourceful Notes");
            ResourcefulNotes.Left.Set(4, 0);
            ResourcefulNotes.Top.Set(0, 0);
            ResourcefulNotes.VAlign = 3 / 6f;
            ResourcefulNotes.Width.Set(60 * horizontalScale, 0);
            ResourcefulNotes.Height.Set(100 * verticalScale, 0);
            ResourcefulNotes.OnLeftClick += new MouseEvent(MoveToResourcefulNotesPage);
            tabs.Append(ResourcefulNotes);

            SafetyNotes = new UIHoverImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/AnarchistCookbookUI/SafetyNotesTab").Value, "Safety Notes");
            SafetyNotes.Left.Set(16, 0);
            SafetyNotes.Top.Set(12, 0);
            SafetyNotes.VAlign = 4 / 6f;
            SafetyNotes.Width.Set(60 * horizontalScale, 0);
            SafetyNotes.Height.Set(100 * verticalScale, 0);
            SafetyNotes.OnLeftClick += new MouseEvent(MoveToSafetyNotesPage);
            tabs.Append(SafetyNotes);

            UtilityNotes = new UIHoverImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/AnarchistCookbookUI/UtilityNotesTab").Value, "Utility Notes");
            UtilityNotes.Left.Set(12, 0);
            UtilityNotes.Top.Set(24, 0);
            UtilityNotes.VAlign = 5 / 6f;
            UtilityNotes.Width.Set(60 * horizontalScale, 0);
            UtilityNotes.Height.Set(100 * verticalScale, 0);
            UtilityNotes.OnLeftClick += new MouseEvent(MoveToUtilityNotesPage);
            tabs.Append(UtilityNotes);
            Append(cookbookSkin);
            Append(tabs);
        }

        public override void Update(GameTime gameTime)
        {
            if (Main.LocalPlayer.dead)
            {
                ModContent.GetInstance<ExtraExplosives>().cookbookInterface.SetState(null);
            }

            horizontalScale = Main.screenWidth / 1920f;
            verticalScale = Main.screenHeight / 1017f;

            // Cookbook texture alignment
            //cookbookSkin.ImageScale = horizontalScale;
            //Main.NewText(cookbookSkin.Width.Pixels + " " + cookbookSkin.Height.Pixels);
            //cookbookSkin.Width.Pixels = 1358;// * horizontalScale;
            //cookbookSkin.Height.Pixels = 622;// * verticalScale;
            cookbookSkin.Left.Pixels = (Main.screenWidth / 2 - cookbookSkin.Width.Pixels / 2);// * horizontalScale;
            //cookbookSkin.Left.Pixels = (1920 / 2 - cookbookSkin.Width.Pixels / 2);// * horizontalScale;
            cookbookSkin.Top.Pixels = (Main.screenHeight / 2 - cookbookSkin.Height.Pixels / 2);// * verticalScale;
                                                                                               //cookbookSkin.Top.Pixels = (1017 / 2 - cookbookSkin.Height.Pixels / 2);// * verticalScale;
                                                                                               //cookbookSkin.ImageScale = horizontalScale;
                                                                                               //Main.NewText($"{cookbookSkin.Width.Pixels}, {cookbookSkin.Height.Pixels}");


            // Tabs alignment
            //tabs.HAlign = 1.1f;
            //tabs.Left.Pixels = Main.screenWidth * 0.62f;
            //Main.NewText(1358 * horizontalScale);
            //tabs.VAlign = 0.5f;
            //tabs.Width.Pixels = 300 * horizontalScale;
            //tabs.Height.Pixels = cookbookSkin.Height.Pixels;// * verticalScale;
            //tabs.Height.Pixels = cookbookSkin.Width.Pixels * 2;

            test.Width = tabs.Width;
            test.Height = tabs.Height;
            /*cookbookSkin.ImageScale = horizontalScale;
            cookbookSkin.Height.Pixels = 622 * verticalScale;
            cookbookSkin.Width.Pixels = 1358 * horizontalScale;
            cookbookSkin.HAlign = 0;
            cookbookSkin.VAlign = 0;
            cookbookSkin.Left.Pixels = 0;
            cookbookSkin.Top.Pixels = 0;*/

            // baseLayer.RemoveChild(cookbookSkin);
            //baseLayer.Append(cookbookSkin);
            //Main.NewText(Main.screenWidth + " : " + Main.screenHeight);
            //cookbookSkin.Left.Pixels = baseLayer.Width.Pixels/2;
            //cookbookSkin.Top.Pixels = baseLayer.Height.Pixels/2;
            //cookbookSkin.Height.Pixels = 648 * verticalScale;
            //cookbookSkin.Width.Pixels = 1358 * horizontalScale;

            //HandyNotes.ImageScale = verticalScale;
            //RandomNotes.ImageScale = verticalScale;
            //ResourcefulNotes.ImageScale = verticalScale;
            //SafetyNotes.ImageScale = verticalScale;
            //UtilityNotes.ImageScale = verticalScale;

            /*HandyNotes.Top.Set(((cookbookSkin.Height.Pixels/12) * 1),0);
            RandomNotes.Top.Set(((cookbookSkin.Height.Pixels/12) * 3),0);
            ResourcefulNotes.Top.Set(((cookbookSkin.Height.Pixels/12) * 5),0);
            SafetyNotes.Top.Set(((cookbookSkin.Height.Pixels/12) * 7),0);
            UtilityNotes.Top.Set(((cookbookSkin.Height.Pixels/12) * 9),0);*/

            HandyNotes.VAlign = 1 / 6f;
            RandomNotes.VAlign = 2 / 6f;
            ResourcefulNotes.VAlign = 3 / 6f;
            SafetyNotes.VAlign = 4 / 6f;
            UtilityNotes.VAlign = 5 / 6f;

            if (!originalDrawUpdate)
            {
                originalDrawUpdate = true;
                cookbookSkin.RemoveAllChildren();
                cookbookSkin.Append(handyNotesPages);
                cookbookSkin.Append(tabs);
                handyNotesPages.MakeVisible();
            }


            //tabs.Left.Pixels = cookbookSkin.Width.Pixels - 180;

            //--------------------------------------------------------------------------------- Look into this if we need to change how the UI state is set.

            if (cookbookSkin.ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            base.Update(gameTime);
        }

        public void MoveToHandyNotesPage(UIMouseEvent evt, UIElement listeningElement)
        {
            cookbookSkin.RemoveAllChildren();
            cookbookSkin.Append(handyNotesPages);
            cookbookSkin.Append(tabs);
            handyNotesPages.MakeVisible();
        }
        public void MoveToRandomNotesPage(UIMouseEvent evt, UIElement listeningElement)
        {
            cookbookSkin.RemoveAllChildren();
            cookbookSkin.Append(randomNotesPages);
            cookbookSkin.Append(tabs);
            randomNotesPages.MakeVisible();
        }

        public void MoveToResourcefulNotesPage(UIMouseEvent evt, UIElement listeningElement)
        {
            cookbookSkin.RemoveAllChildren();
            cookbookSkin.Append(resourcefulNotesPages);
            cookbookSkin.Append(tabs);
            resourcefulNotesPages.MakeVisible();
        }

        public void MoveToSafetyNotesPage(UIMouseEvent evt, UIElement listeningElement)
        {
            cookbookSkin.RemoveAllChildren();
            cookbookSkin.Append(safetyNotesPages);
            cookbookSkin.Append(tabs);
            safetyNotesPages.MakeVisible();
        }

        public void MoveToUtilityNotesPage(UIMouseEvent evt, UIElement listeningElement)
        {
            cookbookSkin.RemoveAllChildren();
            cookbookSkin.Append(utilityNotesPages);
            cookbookSkin.Append(tabs);
            utilityNotesPages.MakeVisible();
        }
    }
}