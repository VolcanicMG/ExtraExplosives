using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace ExtraExplosives.UI
{
    internal class CEBossUI : UIState
    {
        //Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        //Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

        internal UITextPanel<string> BossText;
        internal UIText Text;
        internal UIText Text2;

        internal UIImageButton ImageButtonYes;
        internal UIImageButton ImageButtonNo;

        internal UIImage Warning;
        internal UIImage PanelSkin;
        internal UIImage Panel2Skin;

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

        private float ScreenWidth = 1920f; //Resolution for the average monitor and what the UI was build for
        private float FocusedScreenWidth = 0f;
        private float ScreenAdjustments = 0f;
        private float ScaleUI = Main.UIScale;

        private int DrawPosY = 220;

        private int amount = (Main.screenWidth + 222);
        private int amount2 = -(Main.screenWidth + 222);

        private bool reset;


        public override void OnInitialize()
        {
            FocusedScreenWidth = Main.screenWidth;
            ScreenAdjustments = FocusedScreenWidth / ScreenWidth;

            panel = new UIPanel();
            panel.Height.Set(200 * ScreenAdjustments / ScaleUI, 0);
            panel.Width.Set(400 * ScreenAdjustments / ScaleUI, 0);
            panel.Left.Set((ScreenWidth / 10) * ScreenAdjustments / ScaleUI, 0);
            //panel.HAlign = .2f;
            panel.VAlign = .85f;
            panel.BackgroundColor = new Color(192, 192, 192, 0);
            panel.Recalculate();
            Append(panel);

            //Skin for the panel
            PanelSkin = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/ReforgeUI"));
            PanelSkin.ImageScale = 2.2f * ScreenAdjustments / ScaleUI;
            PanelSkin.HAlign = .5f;
            PanelSkin.VAlign = .5f;
            panel.Append(PanelSkin);

            panel2 = new UIPanel();
            panel2.Height.Set(200 * ScreenAdjustments / ScaleUI, 0);
            panel2.Width.Set(400 * ScreenAdjustments / ScaleUI, 0);

            float Find = (FocusedScreenWidth < ScreenWidth) ? .9f : 1f;
            panel2.Left.Set((ScreenWidth - panel.Left.Pixels - panel2.Width.Pixels) * Math.Abs(ScreenAdjustments * Find) / ScaleUI, 0);
            panel2.VAlign = .85f;
            panel2.BackgroundColor = new Color(192, 192, 192, 0);
            panel2.Recalculate();
            Append(panel2);

            //Skin for the second panel
            Panel2Skin = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/ReforgeUI"));
            Panel2Skin.ImageScale = 2.2f * ScreenAdjustments / ScaleUI;
            Panel2Skin.HAlign = .5f;
            Panel2Skin.VAlign = .5f;
            panel2.Append(Panel2Skin);

            //The green button
            ImageButtonYes = new UIImageButton(ModContent.GetTexture("ExtraExplosives/UI/YesButton"));
            ImageButtonYes.Width.Set(150 / ScaleUI, 0f);
            ImageButtonYes.Height.Set(150 / ScaleUI, 0f);
            ImageButtonYes.Left.Set((panel.Left.Pixels + panel.Width.Pixels + 50), 0);
            ImageButtonYes.VAlign = .86f;
            ImageButtonYes.OnClick += new MouseEvent(ButtonClickedYes);
            Append(ImageButtonYes);

            //The red button
            ImageButtonNo = new UIImageButton(ModContent.GetTexture("ExtraExplosives/UI/NoButton"));
            ImageButtonNo.Width.Set(150 / ScaleUI, 0f);
            ImageButtonNo.Height.Set(150 / ScaleUI, 0f);
            ImageButtonNo.VAlign = .86f;
            ImageButtonNo.Left.Set((panel2.Left.Pixels - 190), 0f);
            ImageButtonNo.OnClick += new MouseEvent(ButtonClickedNo);
            Append(ImageButtonNo);

            //Warning = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/Warning_Sign"));
            //Warning.HAlign = .5f;
            //Warning.VAlign = .5f;
            //Append(Warning);

            //timer
            BossText = new UITextPanel<string>($"You have {cntr / 60} seconds left");
            BossText.HAlign = .5f;
            BossText.VAlign = .1f;

            //text for the green button
            Text = new UIText("+50% Increase in bomb drops.\n" +
                              "Pressing the green button will\n" +
                              "turn block breaking on.\n" +
                              "[c/FF0000:May lag servers]", .4f, true);
            Text.TextColor = new Color(50, 205, 50);
            //Text.HAlign = .24f;
            Text.VAlign = .3f;
            Text.HAlign = .2f;
            panel.Append(Text);

            //text for the red button
            Text2 = new UIText("[c/FF0000:Turn off block breaking.]\n\n" +
                               "The fight will be easier\n" +
                               "and won't lag on servers.", .4f, true);
            Text2.TextColor = new Color(50, 205, 50);
            //Text.HAlign = .24f;
            Text2.VAlign = .3f;
            float FindAline = (FocusedScreenWidth < ScreenWidth) ? .2f : 1f;
            Text2.HAlign = 1 * FindAline;
            panel2.Append(Text2);

            IsVisible = true;
        }

        //When the button it clicked do this
        private void ButtonClickedYes(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.PlaySound(SoundID.Coins);
            ExtraExplosives.CheckUIBoss = 2;
            ExtraExplosives.CheckBossBreak = true;
            Main.NewText("You selected 'Yes'");

            //set the boss to move for all of the players.
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket myPacket = ExtraExplosives.Instance.GetPacket(); // not going through for some reason
                myPacket.Write((byte)ExtraExplosives.EEMessageTypes.checkBossUIYes);
                myPacket.Send();
            }

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket myPacket = ExtraExplosives.Instance.GetPacket(); // not going through for some reason
                myPacket.Write((byte)ExtraExplosives.EEMessageTypes.removeUI);
                myPacket.Send();
            }
        }

        private void ButtonClickedNo(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.PlaySound(SoundID.Coins);
            ExtraExplosives.CheckUIBoss = 2;
            ExtraExplosives.CheckBossBreak = false;
            Main.NewText("You selected 'No'");

            //set the boss to move for all of the players.
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket myPacket = ExtraExplosives.Instance.GetPacket(); // not going through for some reason
                myPacket.Write((byte)ExtraExplosives.EEMessageTypes.checkBossUINo);
                myPacket.Send();
            }

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket myPacket = ExtraExplosives.Instance.GetPacket(); // not going through for some reason
                myPacket.Write((byte)ExtraExplosives.EEMessageTypes.removeUI);
                myPacket.Send();
            }

            //Main.NewText(ExtraExplosives.Instance);
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

            if (ExtraExplosives.CheckUIBoss == 2)
            {
                ModContent.GetInstance<ExtraExplosives>().CEBossInterface.SetState(null);
            }

            if (cntr >= 1500)
            {
                ExtraExplosives.CheckUIBoss = 2;
                Main.NewText("You didn't select anything, defaulting to 'NO'.");

                //set the boss to move for all of the players.
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    ModPacket myPacket = ExtraExplosives.Instance.GetPacket(); // not going through for some reason
                    myPacket.Write((byte)ExtraExplosives.EEMessageTypes.checkBossUINo);
                    myPacket.Send();
                }

                ModContent.GetInstance<ExtraExplosives>().CEBossInterface.SetState(null);
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