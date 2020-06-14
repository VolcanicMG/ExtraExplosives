using System.Collections.Generic;
using ExtraExplosives.Items.Explosives;
using ExtraExplosives.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using static Terraria.ModLoader.ModContent;

namespace ExtraExplosives.UI
{
	internal class ExtraExplosivesUI : UIState
	{
		public IDictionary<int, int> AmmoItemIDToItem = ExtraExplosives.mapItemToItemID;		// Needed for crafting bullet booms
		
		internal static int ItemAmmo; //projectile
		internal static int ItemProjectile;

		private Mod CalamityMod = ModLoader.GetMod("CalamityMod");
		private Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

		private VanillaItemSlotWrapper _vanillaItemSlot;
		private VanillaItemSlotWrapper _vanillaItemSlot2;

		public override void OnInitialize()
		{
			_vanillaItemSlot = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f)
			{
				Left = { Pixels = 50 },
				Top = { Pixels = 270 },
				ValidItemFunc = item => item.IsAir || !item.IsAir
			};

			_vanillaItemSlot2 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f)
			{
				Left = { Pixels = 200 },
				Top = { Pixels = 270 },
				ValidItemFunc = item => item.IsAir || !item.IsAir
			};
			// Here we limit the items that can be placed in the slot. We are fine with placing an empty item in or a non-empty item that can be prefixed. Calling Prefix(-3) is the way to know if the item in question can take a prefix or not.
			Append(_vanillaItemSlot);
			Append(_vanillaItemSlot2);
		}

		public override void OnDeactivate()
		{
			if (!_vanillaItemSlot.Item.IsAir)
			{
				// QuickSpawnClonedItem will preserve mod data of the item. QuickSpawnItem will just spawn a fresh version of the item, losing the prefix.
				Main.LocalPlayer.QuickSpawnClonedItem(_vanillaItemSlot.Item, _vanillaItemSlot.Item.stack);
				// Now that we've spawned the item back onto the player, we reset the item by turning it into air.
				_vanillaItemSlot.Item.TurnToAir();
			}
			if (!_vanillaItemSlot2.Item.IsAir)
			{
				// QuickSpawnClonedItem will preserve mod data of the item. QuickSpawnItem will just spawn a fresh version of the item, losing the prefix.
				Main.LocalPlayer.QuickSpawnClonedItem(_vanillaItemSlot2.Item, _vanillaItemSlot2.Item.stack);
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
				GetInstance<ExtraExplosives>().ExtraExplosivesUserInterface.SetState(null);
			}
		}

		private bool tickPlayed;

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);

			// This will hide the crafting menu similar to the reforge menu. For best results this UI is placed before "Vanilla: Inventory" to prevent 1 frame of the craft menu showing.
			Main.HidePlayerCraftingMenu = true;

			// Here we have a lot of code. This code is mainly adapted from the vanilla code for the reforge option.
			// This code draws "Place an item here" when no item is in the slot and draws the reforge cost and a reforge button when an item is in the slot.
			// This code could possibly be better as different UIElements that are added and removed, but that's not the main point of this example.
			// If you are making a UI, add UIElements in OnInitialize that act on your ItemSlot or other inputs rather than the non-UIElement approach you see below.

			const int slotX = 50;
			const int slotY = 270;

			string message2 = "Element/Ammo"; //Will be check depending on whats the first slot, for insance, if the first slot has a bullet boom then it will select ammo

			ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, "Currently Working Explosives: Bullet Boom", new Vector2(slotX, slotY + 80), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);

			bool Craftable = false;
			if (!_vanillaItemSlot.Item.IsAir) //check to see if the slot is air or not
			{
				if (_vanillaItemSlot.Item.type == ModContent.ItemType<BulletBoomEmptyItem>()) message2 = "Bullet";

				if (_vanillaItemSlot.Item.type == ModContent.ItemType<BulletBoomEmptyItem>() && _vanillaItemSlot2.Item.ammo == AmmoID.Bullet) //Check to see if the slot has a bulletboom here and ammo in the ammo slot
				{
					if (_vanillaItemSlot.Item.stack >= 1 && _vanillaItemSlot2.Item.stack >= 10)
					{
						Craftable = true;
					}
					if (_vanillaItemSlot.Item.stack >= 1 && _vanillaItemSlot2.Item.stack < 10 && !_vanillaItemSlot2.Item.IsAir)
					{
						ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, "Need 10!", new Vector2(slotX + 150, slotY + 50), new Color(255, 0, 0), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
					}
				}
				else
				{
					if (_vanillaItemSlot.Item.type == ModContent.ItemType<BulletBoomEmptyItem>() && _vanillaItemSlot2.Item.ammo != AmmoID.Bullet && !_vanillaItemSlot2.Item.IsAir) //check to see if the item is a bullet boom and the second slot is not ammo and not air
					{
						ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, message2, new Vector2(slotX + 150, slotY + 50), new Color(255, 0, 0), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
					}
					Craftable = false;
				}

				if (!_vanillaItemSlot.Item.IsAir && !_vanillaItemSlot2.Item.IsAir && Craftable == true) //if both spots are full summon the combine -----------------------
				{
					int reforgeX = slotX + 290;
					int reforgeY = slotY + 40;
					bool hoveringOverReforgeButton = Main.mouseX > reforgeX - 15 && Main.mouseX < reforgeX + 15 && Main.mouseY > reforgeY - 15 && Main.mouseY < reforgeY + 15 && !PlayerInput.IgnoreMouseInterface;

					ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, "Combine", new Vector2(slotX + 250, (float)slotY), Color.White, 0f, Vector2.Zero, Vector2.One, -1f, 2f);

					Texture2D reforgeTexture = Main.reforgeTexture[hoveringOverReforgeButton ? 1 : 0];

					Main.spriteBatch.Draw(reforgeTexture, new Vector2(reforgeX, reforgeY), null, Color.White, 0f, reforgeTexture.Size() / 1.5f, 0.8f, SpriteEffects.None, 0f);
					if (hoveringOverReforgeButton)
					{
						Main.hoverItemName = "Combine";
						if (!tickPlayed)
						{
							Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						}
						tickPlayed = true;
						Main.LocalPlayer.mouseInterface = true;

						AmmoItemIDToItem = ExtraExplosives.mapItemToItemID;
                        if (Main.mouseLeftRelease && Main.mouseLeft && Craftable == true) //add a check here to see if its an item that can be combined and if it can produce it here
                        {
                            if (_vanillaItemSlot.Item.type == ModContent.ItemType<BulletBoomEmptyItem>() && AmmoItemIDToItem.ContainsKey(_vanillaItemSlot2.Item.type))
                            {
                                ItemAmmo = _vanillaItemSlot2.Item.type; //get the id for the ammo
                                
                                
                                
                                //Main.NewText(ItemAmmo);

                                // Now that we've spawned the item back onto the player, we reset the item by turning it into air.
                                if (_vanillaItemSlot.Item.stack >= 1 && _vanillaItemSlot2.Item.stack >= 10)
                                {
                                    // Gets the correct Bullet Boom item id from the ammo item id being used
                                    // Since the type of ammo has already been confirmed to be registered, no checking is needed
                                    Main.LocalPlayer.QuickSpawnItem(AmmoItemIDToItem[ItemAmmo], 1);
                                    
                                    
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
                                Main.PlaySound(SoundID.Item37, -1, -1);
                            }
                            
                        }
					}
				}
			}
			else
			{
				string message = "Explosive";
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, message, new Vector2(slotX, slotY + 50), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
			}

			if (!_vanillaItemSlot2.Item.IsAir)
			{
			}
			else
			{
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, message2, new Vector2(slotX + 150, slotY + 50), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
			}
		}
	}
}