using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace ExtraExplosives.UI
{
    internal class CEBossUINonOwner : UIState
    {
        //Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        //Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

        internal UITextPanel<string> BossText;
        internal UIText Text;
        internal UIText Text2;

        internal UIImageButton ImageButtonYes;
        internal UIImageButton ImageButtonNo;

        internal UIImage Warning;

        internal UIPanel panel;
        internal UIPanel panel2;

        private int screenX = Main.screenWidth / 2;
        private int screenY = (Main.screenHeight / 5);

        internal static bool IsVisible;

        private int opacity = 50;
        private bool SwitchUp = false;
        private bool SwitchDown = false;

        private int cntr;
        private int minus;


        private int DrawPosY = 220;

        private int amount = (Main.screenWidth + 222);
        private int amount2 = -(Main.screenWidth + 222);

        private bool reset;


        public override void OnInitialize()
        {

            //Warning = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/Warning_Sign"));
            //Warning.HAlign = .5f;
            //Warning.VAlign = .5f;
            //Append(Warning);

            //timer
            BossText = new UITextPanel<string>($"You have {cntr / 60} seconds left");
            BossText.HAlign = .5f;
            BossText.VAlign = .1f;

            IsVisible = true;
        }


        //public override void OnDeactivate()
        //{

        //	// Note that in ExamplePerson we call .SetState(new UI.ExamplePersonUI());, thereby creating a new instance of this UIState each time.
        //	// You could go with a different design, keeping around the same UIState instance if you wanted. This would preserve the UIState between opening and closing. Up to you.
        //	IsVisible = false;
        //}

        // Update is called on a UIState while it is the active state of the UserInterface.
        // We use Update to handle automatically closing our UI when the player is no longer talking to our Example Person NPC.
        public override void Update(GameTime gameTime)
        {
            // Don't delete this or the UIElements attached to this UIState will cease to function.
            base.Update(gameTime);

            //if (ExtraExplosives.CheckUIBoss >= 2)
            //{
            //	ModContent.GetInstance<ExtraExplosives>().CEBossInterfaceNonOwner.SetState(null);
            //}

            if (ExtraExplosives.removeUIElements)
            {
                ExtraExplosives.removeUIElements = false;

                ModContent.GetInstance<ExtraExplosives>().CEBossInterfaceNonOwner.SetState(null);
            }



            if (cntr >= 1500)
            {
                Main.NewText("The server owner didn't select anything, defaulting to 'NO'.");
                ModContent.GetInstance<ExtraExplosives>().CEBossInterfaceNonOwner.SetState(null);

            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            // This will hide the crafting menu similar to the reforge menu. For best results this UI is placed before "Vanilla: Inventory" to prevent 1 frame of the craft menu showing.
            Main.HidePlayerCraftingMenu = true;

            Main.LocalPlayer.mouseInterface = true;

            //draw the line
            spriteBatch.Draw(ModContent.GetTexture("ExtraExplosives/UI/Background"), new Rectangle(0, screenY - 50 + DrawPosY, Main.screenWidth, 250), new Color(192, 192, 192, 30));

            //draw the warning sign
            spriteBatch.Draw(ModContent.GetTexture("ExtraExplosives/UI/Warning_Sign"), new Rectangle(screenX - 75, screenY + DrawPosY, 150, 150), new Color(255, 255, 255, opacity));
            spriteBatch.Draw(ModContent.GetTexture("ExtraExplosives/UI/Warning_Sign"), new Rectangle(screenX - 475, screenY + DrawPosY, 150, 150), new Color(255, 255, 255, opacity));
            spriteBatch.Draw(ModContent.GetTexture("ExtraExplosives/UI/Warning_Sign"), new Rectangle(screenX + 325, screenY + DrawPosY, 150, 150), new Color(255, 255, 255, opacity));

            //group 1
            for (int i = 0; i < Main.screenWidth / 222 + 20; i++)
            {

                //draw the lines and make them blink
                if (cntr % 100 >= 0 && cntr % 100 <= 50) //50 is the amount of time between each frame
                {
                    spriteBatch.Draw(ModContent.GetTexture("ExtraExplosives/UI/Warning_Line2"), new Rectangle(amount2 + cntr, screenY + 150 + DrawPosY, 222, 34), new Color(255, 255, 255, opacity));

                }
                else
                {
                    spriteBatch.Draw(ModContent.GetTexture("ExtraExplosives/UI/Warning_Line1"), new Rectangle(amount2 + cntr, screenY + 150 + DrawPosY, 222, 34), new Color(255, 255, 255, opacity)); //screenX - DrawPosX
                }

                amount2 += 222; //spacing

            }

            amount2 = -(Main.screenWidth + 222);

            //draw the text
            //spriteBatch.DrawString(Main.fontMouseText, "50% More Bombs", new Vector2(aline, screenY + 430), new Color(50, 205, 50), 0, new Vector2(), 1.5f, SpriteEffects.None, 0);

            //group 2
            for (int i = 0; i < Main.screenWidth / 222 + 20; i++)
            {

                //draw the lines and make them blink
                if (cntr % 100 >= 0 && cntr % 100 <= 50) //50 is about the amount of frames
                {
                    spriteBatch.Draw(ModContent.GetTexture("ExtraExplosives/UI/Warning_Line2"), new Rectangle(amount - cntr, screenY - 30 + DrawPosY, 222, 34), new Color(255, 255, 255, opacity));

                }
                else
                {
                    spriteBatch.Draw(ModContent.GetTexture("ExtraExplosives/UI/Warning_Line1"), new Rectangle(amount - cntr, screenY - 30 + DrawPosY, 222, 34), new Color(255, 255, 255, opacity)); //screenX - DrawPosX
                }

                amount -= 222; //spacing

            }

            amount = (Main.screenWidth * 2);

            ////draw the line of Warning signs, group 2
            //for (int i = 0; i < Main.screenWidth / 222 + 222; i++)
            //{

            //	//draw the lines and make them blink
            //	if (cntr % 100 >= 0 && cntr % 100 <= 50) //50 is about the amount of frames
            //	{
            //		spriteBatch.Draw(ModContent.GetTexture("ExtraExplosives/UI/Warning_Line2"), new Rectangle(amount + cntr, screenY - DrawPosY, 222, 34), new Color(255, 255, 255, opacity));

            //	}
            //	else
            //	{
            //		spriteBatch.Draw(ModContent.GetTexture("ExtraExplosives/UI/Warning_Line1"), new Rectangle(amount + cntr, screenY - DrawPosY, 222, 34), new Color(255, 255, 255, opacity)); //screenX - DrawPosX
            //	}

            //	amount += 222; //spacing

            //}

            //amount = -50; //the starting position. Needs to be reset each time the for loop is done so it doesn't keep adding 222 to amount.

            cntr++;
            //Main.NewText(amount2);
            //Main.NewText(Main.screenWidth / 222 + 10);

            if (opacity >= 255)
            {
                SwitchDown = true;
                SwitchUp = false;
            }
            else if (opacity <= 50)
            {
                SwitchUp = true;
                SwitchDown = false;
            }

            if (SwitchUp) //set the speed of a opacity change
            {
                opacity += 4;
            }
            else if (SwitchDown)
            {
                opacity -= 4;
            }

            //progress bar to check time left
            if (cntr >= 750)
            {
                BossText.SetText($"You have {-((cntr / 60) - 25)} seconds left");
                Append(BossText);
                spriteBatch.Draw(ModContent.GetTexture("ExtraExplosives/UI/ProgressBar"), new Rectangle(screenX - 375, 50, 750 - minus, 15), new Color(255, 255, 255, opacity));
                spriteBatch.Draw(ModContent.GetTexture("ExtraExplosives/UI/TimeLeft"), new Rectangle(screenX - 375, 50, 752, 17), new Color(255, 255, 255));
                minus++;
            }
            //Main.NewText(cntr);
        }
    }
}