using ExtraExplosives.Items.Explosives;
using ExtraExplosives.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using Item = Terraria.Item;

namespace ExtraExplosives.UI
{
    internal class ExtraExplosivesUI : UIState
    {
        internal UIImage Box;

        internal UIImage combineButton;
        internal UIImage combineButtonTen;
        internal UIImage PlusIcon;
        internal UIImage Indicator;
        internal UIImage Indicator2;

        internal UITextPanel<string> text;

        private bool hoveringOverReforgeButton;
        private bool hoveringOverReforgeButtonTen;

        internal static int ItemAmmo; //projectile
        internal static int ItemProjectile;

        private Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        private Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

        private VanillaItemSlotWrapper _vanillaItemSlot;
        private VanillaItemSlotWrapper _vanillaItemSlot2;

        public override void OnInitialize()
        {
            Box = new UIImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/CombineUI"));
            //Image5.Width.Set(ImageWidth, 0f);
            //Image5.Height.Set(ImageHeight, 0f);

            Box.Left.Set(100, 0f);
            Box.Top.Set(300, 0f);
            Box.ImageScale = 1.2f;
            Append(Box);

            //indicator
            Indicator2 = new UIImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/Indicator"));
            Indicator2.Left.Set(97, 0);
            Indicator2.Top.Set(37, 0);
            Indicator2.ImageScale = 1.1f;
            Box.Append(Indicator2);

            //indicator
            Indicator = new UIImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/Indicator"));
            Indicator.Left.Set(2, 0);
            Indicator.Top.Set(37, 0);
            Indicator.ImageScale = 1.1f;
            Box.Append(Indicator);

            //+1
            Texture2D reforgeTexture = TextureAssets.Reforge[0].Value;
            combineButton = new UIImage(reforgeTexture);
            combineButton.Left.Set(158, 0f);
            combineButton.Top.Set(Box.Height.Pixels / 2 - 7, 0f);
            Box.Append(combineButton);

            //+10
            combineButtonTen = new UIImage(reforgeTexture);
            combineButtonTen.Left.Set(197, 0f);
            combineButtonTen.Top.Set(Box.Height.Pixels / 2 - 7, 0f);
            Box.Append(combineButtonTen);

            PlusIcon = new UIImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/PlusIcon"));
            PlusIcon.HAlign = .29f;
            PlusIcon.VAlign = .6f;
            Box.Append(PlusIcon);

            text = new UITextPanel<string>("Currently Working Explosives: [c/AB40FF:Bullet Boom(Uses empty shells)]");
            //float widthText = ReforgeText.GetDimensions().Width;

            //ReforgeText.Left.Set(screenX - ReforgeText.Width.Pixels, 0f);
            text.Left.Set(-40, 0f);
            text.Top.Set(140, 0f);
            Box.Append(text);

            _vanillaItemSlot = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f, "ExtraExplosives/UI/BombIcon")
            {
                Left = { Pixels = 5 },
                Top = { Pixels = 40 },
                ValidItemFunc = item => item.IsAir || !item.IsAir
            };

            _vanillaItemSlot2 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f, "ExtraExplosives/UI/AmmoIcon")
            {
                Left = { Pixels = 100 },
                Top = { Pixels = 40 },
                ValidItemFunc = item => item.IsAir || !item.IsAir
            };
            // Here we limit the items that can be placed in the slot. We are fine with placing an empty item in or a non-empty item that can be prefixed. Calling Prefix(-3) is the way to know if the item in question can take a prefix or not.
            Box.Append(_vanillaItemSlot);
            Box.Append(_vanillaItemSlot2);
        }

        public override void OnDeactivate()
        // TODO again wrong entitysource
        {
            if (!_vanillaItemSlot.Item.IsAir)
            {
                // QuickSpawnClonedItem will preserve mod data of the item. QuickSpawnItem will just spawn a fresh version of the item, losing the prefix.
                Main.LocalPlayer.QuickSpawnItem(new EntitySource_Parent(Main.LocalPlayer), _vanillaItemSlot.Item, _vanillaItemSlot.Item.stack);
                // Now that we've spawned the item back onto the player, we reset the item by turning it into air.
                _vanillaItemSlot.Item.TurnToAir();
            }
            if (!_vanillaItemSlot2.Item.IsAir)
            {
                // QuickSpawnClonedItem will preserve mod data of the item. QuickSpawnItem will just spawn a fresh version of the item, losing the prefix.
                Main.LocalPlayer.QuickSpawnItem(new EntitySource_Parent(Main.LocalPlayer), _vanillaItemSlot2.Item, _vanillaItemSlot2.Item.stack);
                // Now that we've spawned the item back onto the player, we reset the item by turning it into air.
                _vanillaItemSlot.Item.TurnToAir();
            }

            // Note that in ExamplePerson we call .SetState(new UI.ExamplePersonUI());, thereby creating a new instance of this UIState each time.
            // You could go with a different design, keeping around the same UIState instance if you wanted. This would preserve the UIState between opening and closing. Up to you.
        }

        // Update is called on a UIState while it is the active state of the UserInterface.
        // We use Update to handle automatically closing our UI when the player is no longer talking to our Example Person NPC.
        public override void Update(GameTime gameTime)
        {
            // Don't delete this or the UIElements attached to this UIState will cease to function.
            base.Update(gameTime);

            // talkNPC is the index of the NPC the player is currently talking to. By checking talkNPC, we can tell when the player switches to another NPC or closes the NPC chat dialog.
            if (Main.LocalPlayer.talkNPC == -1 || Main.npc[Main.LocalPlayer.talkNPC].type != NPCType<CaptainExplosive>())
            {
                // When that happens, we can set the state of our UserInterface to null, thereby closing this UIState. This will trigger OnDeactivate above.
                GetInstance<ExtraExplosivesSystem>().ExtraExplosivesUserInterface.SetState(null);
            }
        }

        private bool tickPlayed;

        protected override void DrawSelf(SpriteBatch spriteBatch) //TODO Clean this mess up
        {

            base.DrawSelf(spriteBatch);

            if (_vanillaItemSlot.IsMouseHovering || Indicator.IsMouseHovering)
            {
                Main.hoverItemName = "Explosive";
                Main.LocalPlayer.mouseInterface = true;
            }
            else if (_vanillaItemSlot2.IsMouseHovering || Indicator2.IsMouseHovering)
            {
                Main.hoverItemName = "Bullets (10 required)";
                Main.LocalPlayer.mouseInterface = true;
            }
            else if (combineButton.IsMouseHovering)
            {
                combineButton.SetImage(TextureAssets.Reforge[1].Value);
                hoveringOverReforgeButton = true;
                Main.hoverItemName = "Combine +1";
                Main.LocalPlayer.mouseInterface = true;
            }
            else if (combineButtonTen.IsMouseHovering)
            {
                combineButtonTen.SetImage(TextureAssets.Reforge[1].Value);
                hoveringOverReforgeButtonTen = true;
                Main.hoverItemName = "Combine +10";
                Main.LocalPlayer.mouseInterface = true;
            }
            else { combineButton.SetImage(TextureAssets.Reforge[0].Value); hoveringOverReforgeButton = false; combineButtonTen.SetImage(TextureAssets.Reforge[0].Value); hoveringOverReforgeButtonTen = false; }

            // This will hide the crafting menu similar to the reforge menu. For best results this UI is placed before "Vanilla: Inventory" to prevent 1 frame of the craft menu showing.
            Main.hidePlayerCraftingMenu = true;

            // Here we have a lot of code. This code is mainly adapted from the vanilla code for the reforge option.
            // This code draws "Place an item here" when no item is in the slot and draws the reforge cost and a reforge button when an item is in the slot.
            // This code could possibly be better as different UIElements that are added and removed, but that's not the main point of this example.
            // If you are making a UI, add UIElements in OnInitialize that act on your ItemSlot or other inputs rather than the non-UIElement approach you see below.

            const int slotX = 50;
            const int slotY = 270;

            //string message2 = "Element/Ammo"; //Will be check depending on whats the first slot, for insance, if the first slot has a bullet boom then it will select ammo

            //ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, "Currently Working Explosives: Bullet Boom", new Vector2(slotX, slotY + 80), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);

            Indicator.SetImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/Indicator").Value);
            Indicator2.SetImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/Indicator").Value);

            bool Craftable = false;
            bool CraftTen = false;

            //ammo
            if (!_vanillaItemSlot2.Item.IsAir && _vanillaItemSlot.Item.IsAir)
            {
                if (_vanillaItemSlot2.Item.ammo == AmmoID.Bullet && _vanillaItemSlot2.Item.stack >= 10)
                {
                    Indicator2.SetImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/IndicatorGreen").Value);
                    Indicator.SetImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/IndicatorRed").Value);
                }
                else if (_vanillaItemSlot2.Item.ammo == AmmoID.Bullet)
                {
                    Indicator2.SetImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/IndicatorYellow").Value);

                    if (hoveringOverReforgeButton || hoveringOverReforgeButtonTen)
                    {
                        Main.hoverItemName = $"You need to add {10 - _vanillaItemSlot2.Item.stack} bullets";
                        Main.LocalPlayer.mouseInterface = true;
                    }

                    Indicator.SetImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/IndicatorRed").Value);
                }
                else
                {
                    Indicator2.SetImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/IndicatorRed").Value);

                    if (hoveringOverReforgeButton || hoveringOverReforgeButtonTen)
                    {
                        Main.hoverItemName = "You need to add 10 bullets and an empty shell";
                        Main.LocalPlayer.mouseInterface = true;
                    }

                    Indicator.SetImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/IndicatorRed").Value);
                }
            }

            //explosives
            if (!_vanillaItemSlot.Item.IsAir) //check to see if the slot is air or not
            {
                //bomb
                if (_vanillaItemSlot.Item.type == ModContent.ItemType<BulletBoomEmptyItem>())
                {
                    Indicator.SetImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/IndicatorGreen").Value);
                }
                else
                {
                    Indicator.SetImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/IndicatorRed").Value);
                }

                //ammo
                if (_vanillaItemSlot2.Item.ammo == AmmoID.Bullet && _vanillaItemSlot2.Item.stack >= 10)
                {
                    Indicator2.SetImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/IndicatorGreen").Value);

                    if (hoveringOverReforgeButtonTen && _vanillaItemSlot2.Item.stack <= 100)
                    {
                        Main.hoverItemName = $"You need to add {100 - _vanillaItemSlot2.Item.stack} bullets for +10";
                        Main.LocalPlayer.mouseInterface = true;
                    }
                }
                else if (_vanillaItemSlot2.Item.ammo == AmmoID.Bullet)
                {
                    Indicator2.SetImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/IndicatorYellow").Value);

                    if (hoveringOverReforgeButton || hoveringOverReforgeButtonTen)
                    {
                        Main.hoverItemName = $"You need to add {10 - _vanillaItemSlot2.Item.stack} bullets";
                        Main.LocalPlayer.mouseInterface = true;
                    }
                }
                else
                {
                    Indicator2.SetImage(ModContent.Request<Texture2D>("ExtraExplosives/UI/IndicatorRed").Value);

                    if (hoveringOverReforgeButton || hoveringOverReforgeButtonTen)
                    {
                        Main.hoverItemName = "You need to add 10 bullets";
                        Main.LocalPlayer.mouseInterface = true;
                    }
                }

                //------------------------------------------------------ Indicators above -----------------------------------------------

                if (_vanillaItemSlot.Item.type == ModContent.ItemType<BulletBoomEmptyItem>() && _vanillaItemSlot2.Item.ammo == AmmoID.Bullet) //Check to see if the slot has a bulletboom here and ammo in the ammo slot
                {
                    if (_vanillaItemSlot.Item.stack >= 1 && _vanillaItemSlot2.Item.stack >= 10)
                    {
                        Craftable = true;
                    }
                    if (_vanillaItemSlot.Item.stack >= 10 && _vanillaItemSlot2.Item.stack >= 100)
                    {

                        CraftTen = true;
                    }
                    //if (_vanillaItemSlot.Item.stack >= 1 && _vanillaItemSlot2.Item.stack < 10 && !_vanillaItemSlot2.Item.IsAir)
                    //{
                    //	ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, "Need 10!", new Vector2(slotX + 150, slotY + 50), new Color(255, 0, 0), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
                    //}

                }
                else
                {
                    //if (_vanillaItemSlot.Item.type == ModContent.ItemType<BulletBoomEmptyItem>() && _vanillaItemSlot2.Item.ammo != AmmoID.Bullet && !_vanillaItemSlot2.Item.IsAir) //check to see if the item is a bullet boom and the second slot is not ammo and not air
                    //{
                    //	ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, message2, new Vector2(slotX + 150, slotY + 50), new Color(255, 0, 0), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
                    //}
                    Craftable = false;
                    CraftTen = false;
                }

                if (!_vanillaItemSlot.Item.IsAir && !_vanillaItemSlot2.Item.IsAir && (Craftable || CraftTen)) //if both spots are full summon the combine -----------------------
                {
                    int reforgeX = slotX + 290;
                    int reforgeY = slotY + 40;

                    //hoveringOverReforgeButton = Main.mouseX > reforgeX - 15 && Main.mouseX < reforgeX + 15 && Main.mouseY > reforgeY - 15 && Main.mouseY < reforgeY + 15 && !PlayerInput.IgnoreMouseInterface;

                    //ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, "Combine", new Vector2(slotX + 250, (float)slotY), Color.White, 0f, Vector2.Zero, Vector2.One, -1f, 2f);

                    //Texture2D reforgeTexture = Main.reforgeTexture[hoveringOverReforgeButton ? 1 : 0];

                    //Main.spriteBatch.Draw(reforgeTexture, new Vector2(reforgeX, reforgeY), null, Color.White, 0f, reforgeTexture.Size() / 1.5f, 0.8f, SpriteEffects.None, 0f);
                    if (hoveringOverReforgeButton)
                    {
                        Main.hoverItemName = "Combine";
                        if (!tickPlayed)
                        {
                            //SoundEngine.PlaySound(SoundID.MenuTick);
                        }
                        tickPlayed = true;
                        Main.LocalPlayer.mouseInterface = true;
                        if (Main.mouseLeftRelease && Main.mouseLeft && Craftable) //add a check here to see if its an item that can be combined and if it can produce it here
                        {
                            if (_vanillaItemSlot.Item.type == ModContent.ItemType<BulletBoomEmptyItem>() && _vanillaItemSlot2.Item.ammo == AmmoID.Bullet)
                            {
                                ItemAmmo = _vanillaItemSlot2.Item.type; //get the id for the ammo

                                // Now that we've spawned the item back onto the player, we reset the item by turning it into air.
                                if (_vanillaItemSlot.Item.stack >= 1 && _vanillaItemSlot2.Item.stack >= 10 && Main.netMode != NetmodeID.Server)
                                {
                                    Player player = Main.player[Main.myPlayer];
                                    spriteBatch.End();
                                    bool flag = false;
                                    foreach (Item checkItem in Main.player[Main.myPlayer].inventory)
                                    {
                                        if (checkItem.type == ModContent.ItemType<BulletBoomItem>())
                                        {
                                            BulletBoomItem modCheckItem = (BulletBoomItem)checkItem.ModItem;
                                            if (modCheckItem.Item.shoot == _vanillaItemSlot2.Item.shoot && modCheckItem.overStack <= 998)
                                            {

                                                modCheckItem.overStack++;
                                                // TODO Context is probably wrong
                                                PopupText.NewText(PopupTextContext.ItemReforge, modCheckItem.Item, modCheckItem.overStack, true, false);
                                                flag = true;
                                            }
                                        }
                                    }

                                    if (!flag)
                                    {
                                        string bulletType = "";
                                        StringBuilder sb = new StringBuilder(_vanillaItemSlot2.Item.Name);
                                        sb.Replace(" bullet", "");
                                        sb.Replace(" Bullet", "");
                                        // TODO no clue what this should be, giving it a null, this is wrong fix
                                        int itemInt = Item.NewItem(player.GetSource_ItemUse(null), player.position, ModContent.ItemType<BulletBoomItem>(), 1);
                                        //Main.player[Main.myPlayer].QuickSpawnItem(ModContent.ItemType<TestItem>());
                                        Main.item[itemInt].instanced = true;
                                        Main.item[itemInt].shoot = _vanillaItemSlot2.Item.shoot;
                                        //Main.item[itemInt].modItem.DisplayName.SetDefault(Main.item[itemInt].Name + " " + _vanillaItemSlot2.Item.Name);
                                        Main.item[itemInt]
                                            .SetNameOverride(sb.ToString() + " Bullet Boom");
                                        Main.item[itemInt].damage = _vanillaItemSlot2.Item.damage;
                                        BulletBoomItem tmp = (BulletBoomItem)Main.item[itemInt].ModItem;
                                        tmp.bulletType = _vanillaItemSlot2.Item.Name;
                                        tmp.overStack++;
                                    }

                                    spriteBatch.Begin();
                                    // Removes the correct amount of each item
                                    _vanillaItemSlot.Item.stack = _vanillaItemSlot.Item.stack - 1;
                                    _vanillaItemSlot2.Item.stack = _vanillaItemSlot2.Item.stack - 10;
                                }
                                else
                                {
                                    _vanillaItemSlot.Item.TurnToAir();
                                    _vanillaItemSlot2.Item.TurnToAir();
                                }

                                //ItemLoader.PostReforge(_vanillaItemSlot.Item);
                                //ItemLoader.PostReforge(_vanillaItemSlot2.Item);
                                //SoundEngine.PlaySound(SoundID.Item37);
                            }
                        }
                    }

                    if (hoveringOverReforgeButtonTen && CraftTen)
                    {
                        Main.hoverItemName = "Combine Ten";
                        if (!tickPlayed)
                        {
                            //SoundEngine.PlaySound(SoundID.MenuTick);
                        }
                        tickPlayed = true;
                        Main.LocalPlayer.mouseInterface = true;
                        if (Main.mouseLeftRelease && Main.mouseLeft && CraftTen) //add a check here to see if its an item that can be combined and if it can produce it here
                        {
                            if (_vanillaItemSlot.Item.type == ModContent.ItemType<BulletBoomEmptyItem>() && _vanillaItemSlot2.Item.ammo == AmmoID.Bullet)
                            {
                                ItemAmmo = _vanillaItemSlot2.Item.type; //get the id for the ammo

                                // Now that we've spawned the item back onto the player, we reset the item by turning it into air.
                                if (_vanillaItemSlot.Item.stack >= 10 && _vanillaItemSlot2.Item.stack >= 100 && Main.netMode != NetmodeID.Server)
                                {
                                    Player player = Main.player[Main.myPlayer];
                                    spriteBatch.End();
                                    bool flag = false;
                                    foreach (Item checkItem in Main.player[Main.myPlayer].inventory)
                                    {
                                        if (checkItem.type == ModContent.ItemType<BulletBoomItem>())
                                        {
                                            BulletBoomItem modCheckItem = (BulletBoomItem)checkItem.ModItem;
                                            if (modCheckItem.Item.shoot == _vanillaItemSlot2.Item.shoot && modCheckItem.overStack <= 998)
                                            {

                                                modCheckItem.overStack += 10;
                                                // TODO Wrong context
                                                PopupText.NewText(PopupTextContext.ItemReforge, modCheckItem.Item, modCheckItem.overStack, true, false);
                                                flag = true;
                                            }
                                        }
                                    }

                                    if (!flag)
                                    {
                                        string bulletType = "";
                                        StringBuilder sb = new StringBuilder(_vanillaItemSlot2.Item.Name);
                                        sb.Replace(" bullet", "");
                                        sb.Replace(" Bullet", "");
                                        // TODO null
                                        int itemInt = Item.NewItem(player.GetSource_ItemUse(null), player.position, ModContent.ItemType<BulletBoomItem>(),
                                            1);
                                        //Main.player[Main.myPlayer].QuickSpawnItem(ModContent.ItemType<TestItem>());
                                        Main.item[itemInt].instanced = true;
                                        Main.item[itemInt].shoot = _vanillaItemSlot2.Item.shoot;
                                        //Main.item[itemInt].modItem.DisplayName.SetDefault(Main.item[itemInt].Name + " " + _vanillaItemSlot2.Item.Name);
                                        Main.item[itemInt]
                                            .SetNameOverride(sb.ToString() + " Bullet Boom");
                                        Main.item[itemInt].damage = _vanillaItemSlot2.Item.damage;
                                        BulletBoomItem tmp = (BulletBoomItem)Main.item[itemInt].ModItem;
                                        tmp.bulletType = _vanillaItemSlot2.Item.Name;
                                        tmp.overStack += 10;
                                    }

                                    spriteBatch.Begin();
                                    // Removes the correct amount of each item
                                    _vanillaItemSlot.Item.stack = _vanillaItemSlot.Item.stack - 10;
                                    _vanillaItemSlot2.Item.stack = _vanillaItemSlot2.Item.stack - 100;
                                }
                                else
                                {
                                    _vanillaItemSlot.Item.TurnToAir();
                                    _vanillaItemSlot2.Item.TurnToAir();
                                }

                                //ItemLoader.PostReforge(_vanillaItemSlot.Item);
                                //ItemLoader.PostReforge(_vanillaItemSlot2.Item);
                                //SoundEngine.PlaySound(SoundID.Item37);
                            }
                        }
                    }

                }
            }
            //else
            //{
            //	string message = "Explosive";
            //	ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, message, new Vector2(slotX + 60, slotY + 170), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
            //}

            //if (!_vanillaItemSlot2.Item.IsAir)
            //{
            //}
            //else
            //{
            //	ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, message2, new Vector2(slotX + 150, slotY + 50), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
            //}
        }
    }
}
