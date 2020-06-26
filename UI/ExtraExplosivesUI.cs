
  
using System;
using System.Collections.Generic;
using System.Text;
using ExtraExplosives.Items;
using ExtraExplosives.Items.Explosives;
using ExtraExplosives.NPCs;
using ExtraExplosives.Projectiles;
using log4net.Repository.Hierarchy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.UI;
using Terraria.UI.Chat;
using static Terraria.ModLoader.ModContent;
using Item = Terraria.Item;

namespace ExtraExplosives.UI
{
	internal class ExtraExplosivesUI : UIState
	{

		internal static int ItemAmmo; //projectile
		internal static int ItemProjectile;

		private Mod CalamityMod = ModLoader.GetMod("CalamityMod");
		private Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

		internal UIImage Box;
		internal UIImage combineButton;
		internal UIImage Image;
		internal UIImage Image2;
		internal UITextPanel<string> text;
		internal UITextPanel<string> BombText;
		internal UITextPanel<string> itemText;
		private VanillaItemSlotWrapper _vanillaItemSlot;
		private VanillaItemSlotWrapper _vanillaItemSlot2;

		private bool hoveringOverReforgeButton;
		public override void OnInitialize()
		{
			Box = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/ReforgeUI"));
			//Image5.Width.Set(ImageWidth, 0f);
			//Image5.Height.Set(ImageHeight, 0f);
			
			Box.Left.Set(100, 0f);
			Box.Top.Set(300, 0f);
			Box.ImageScale = 1.2f;
			Append(Box);
			Texture2D reforgeTexture = Main.reforgeTexture[0];
			combineButton = new UIImage(reforgeTexture);
			combineButton.Left.Set(170, 0f);
			combineButton.Top.Set(22, 0f);

			Box.Append(combineButton);
			_vanillaItemSlot = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f)
			{
				Left = { Pixels = 8 },
				Top = { Pixels = 20 },
				ValidItemFunc = item => item.IsAir || !item.IsAir
			};

			_vanillaItemSlot2 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f)
			{
				Left = { Pixels = 80 },
				Top = { Pixels = 20 },
				ValidItemFunc = item => item.IsAir || !item.IsAir
			};
			// Here we limit the items that can be placed in the slot. We are fine with placing an empty item in or a non-empty item that can be prefixed. Calling Prefix(-3) is the way to know if the item in question can take a prefix or not.
			Box.Append(_vanillaItemSlot);
			Box.Append(_vanillaItemSlot2);
			Image = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/UICheckRed"));
			//Image.Width.Set(ImageWidth, 0f);
			//Image.Height.Set(ImageHeight, 0f);
			
			Image.Left.Set(25, 0f);
			Image.Top.Set(75, 0f);
			Image.ImageScale = 1.5f;
			Box.Append(Image);

			Image2 = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/UICheckRed"));
			//Image2.Width.Set(ImageWidth, 0f);
			//Image2.Height.Set(ImageHeight, 0f);
			
			Image2.Left.Set(97, 0f);
			Image2.Top.Set(75, 0f);
			Image2.ImageScale = 1.5f;
			Box.Append(Image2);

			text = new UITextPanel<string>("Currently Working Explosives: Bullet Boom");
			//float widthText = ReforgeText.GetDimensions().Width;

			//ReforgeText.Left.Set(screenX - ReforgeText.Width.Pixels, 0f);
			text.Left.Set(-40, 0f);
			text.Top.Set(140, 0f);
			Box.Append(text);
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

		protected override void DrawSelf(SpriteBatch spriteBatch) //TODO Clean this mess up
		{

			base.DrawSelf(spriteBatch);

			if (!_vanillaItemSlot.Item.IsAir)
			{
				Image.SetImage(ModContent.GetTexture("ExtraExplosives/UI/UICheckGreen"));

			}
			else if (_vanillaItemSlot.Item.IsAir)
			{
				Image.SetImage(ModContent.GetTexture("ExtraExplosives/UI/UICheckRed"));

			}
			if (!_vanillaItemSlot2.Item.IsAir)
			{
				Image2.SetImage(ModContent.GetTexture("ExtraExplosives/UI/UICheckGreen"));

			}
			else if (_vanillaItemSlot2.Item.IsAir )
			{
				Image2.SetImage(ModContent.GetTexture("ExtraExplosives/UI/UICheckRed"));

			}
			
			// This will hide the crafting menu similar to the reforge menu. For best results this UI is placed before "Vanilla: Inventory" to prevent 1 frame of the craft menu showing.
			Main.HidePlayerCraftingMenu = true;

			// Here we have a lot of code. This code is mainly adapted from the vanilla code for the reforge option.
			// This code draws "Place an item here" when no item is in the slot and draws the reforge cost and a reforge button when an item is in the slot.
			// This code could possibly be better as different UIElements that are added and removed, but that's not the main point of this example.
			// If you are making a UI, add UIElements in OnInitialize that act on your ItemSlot or other inputs rather than the non-UIElement approach you see below.

			const int slotX = 50;
			const int slotY = 270;

			string message2 = "Element/Ammo"; //Will be check depending on whats the first slot, for insance, if the first slot has a bullet boom then it will select ammo

			if (_vanillaItemSlot.IsMouseHovering)
			{
				Main.hoverItemName = "Explosive";
				Main.LocalPlayer.mouseInterface = true;
			}
			else if (_vanillaItemSlot2.IsMouseHovering)
			{
				Main.hoverItemName = "Bullets (10 required)";
				Main.LocalPlayer.mouseInterface = true;
			}
			else if (combineButton.IsMouseHovering)
			{
				combineButton.SetImage(Main.reforgeTexture[1]);
				hoveringOverReforgeButton = true;
				Main.hoverItemName = "Combine";
				Main.LocalPlayer.mouseInterface = true;
			}
			else { combineButton.SetImage(Main.reforgeTexture[0]); hoveringOverReforgeButton = false; }

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
					
					
				
					

					

					
					
					if (hoveringOverReforgeButton)
					{
						
						if (!tickPlayed)
						{
							Main.PlaySound(SoundID.MenuTick, -1, -1, 1, 1f, 0f);
						}
						tickPlayed = true;
						Main.LocalPlayer.mouseInterface = true;
						if (Main.mouseLeftRelease && Main.mouseLeft && Craftable == true) //add a check here to see if its an item that can be combined and if it can produce it here
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
											BulletBoomItem modCheckItem = (BulletBoomItem)checkItem.modItem;
											if (modCheckItem.item.shoot == _vanillaItemSlot2.Item.shoot && modCheckItem.overStack <= 998)
											{
												modCheckItem.overStack++;
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
										int itemInt = Item.NewItem(player.position, ModContent.ItemType<BulletBoomItem>(),
											1);
										//Main.player[Main.myPlayer].QuickSpawnItem(ModContent.ItemType<TestItem>());
										Main.item[itemInt].instanced = true;
										Main.item[itemInt].shoot = _vanillaItemSlot2.Item.shoot;
										//Main.item[itemInt].modItem.DisplayName.SetDefault(Main.item[itemInt].Name + " " + _vanillaItemSlot2.Item.Name);
										Main.item[itemInt]
											.SetNameOverride(sb.ToString() + " Bullet Boom");
										Main.item[itemInt].damage = _vanillaItemSlot2.Item.damage;
										BulletBoomItem tmp = (BulletBoomItem)Main.item[itemInt].modItem;
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
								Main.PlaySound(SoundID.Item37, -1, -1);
							}
						}
					}
				}
			}
			

			
		}
	}
}
