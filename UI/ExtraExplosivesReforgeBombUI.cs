using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace ExtraExplosives.UI
{
	internal class ExtraExplosivesReforgeBombUI : UIState
	{

		//Mod CalamityMod = ModLoader.GetMod("CalamityMod");
		//Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

		private VanillaItemSlotWrapper _vanillaItemSlot;
		private VanillaItemSlotWrapper _vanillaItemSlot2;
		private VanillaItemSlotWrapper _vanillaItemSlot3;

		private int screenX = Main.screenWidth / 2;
		private int screenY = (Main.screenHeight / 5) - 40;

		internal static bool reforge = false;

		internal static bool IsVisible;
		public override void OnInitialize()
		{


			_vanillaItemSlot = new VanillaItemSlotWrapper(ItemSlot.Context.PrefixItem, 0.85f)
			{
				Left = { Pixels = screenX - 90 },
				Top = { Pixels = screenY },
				ValidItemFunc = item => item.IsAir || !item.IsAir && item.Prefix(-3),

			};

			_vanillaItemSlot2 = new VanillaItemSlotWrapper(ItemSlot.Context.PrefixItem, 0.85f)
			{
				Left = { Pixels = screenX - 30 },
				Top = { Pixels = screenY },
				ValidItemFunc = item => item.IsAir || !item.IsAir && item.Prefix(-3)
			};

			_vanillaItemSlot3 = new VanillaItemSlotWrapper(ItemSlot.Context.PrefixItem, 0.85f)
			{
				Left = { Pixels = screenX + 30 },
				Top = { Pixels = screenY },
				ValidItemFunc = item => item.IsAir || !item.IsAir && item.Prefix(-3)
			};
			// Here we limit the items that can be placed in the slot. We are fine with placing an empty item in or a non-empty item that can be prefixed. Calling Prefix(-3) is the way to know if the item in question can take a prefix or not.
			Append(_vanillaItemSlot);
			Append(_vanillaItemSlot2);
			Append(_vanillaItemSlot3);

			IsVisible = true;

		}

		public override void OnDeactivate()
		{
			if (!_vanillaItemSlot.Item.IsAir)
			{
				// QuickSpawnClonedItem will preserve mod data of the item. QuickSpawnItem will just spawn a fresh version of the item, losing the prefix.
				//Main.LocalPlayer.QuickSpawnClonedItem(_vanillaItemSlot.Item, _vanillaItemSlot.Item.stack);
				// Now that we've spawned the item back onto the player, we reset the item by turning it into air.
				Main.LocalPlayer.QuickSpawnClonedItem(_vanillaItemSlot.Item, _vanillaItemSlot.Item.stack);
				// Now that we've spawned the item back onto the player, we reset the item by turning it into air.
				_vanillaItemSlot.Item.TurnToAir();
			}

			if (!_vanillaItemSlot2.Item.IsAir)
			{
				// QuickSpawnClonedItem will preserve mod data of the item. QuickSpawnItem will just spawn a fresh version of the item, losing the prefix.
				Main.LocalPlayer.QuickSpawnClonedItem(_vanillaItemSlot2.Item, _vanillaItemSlot2.Item.stack);
				// Now that we've spawned the item back onto the player, we reset the item by turning it into air.
				_vanillaItemSlot2.Item.TurnToAir();
			}

			if (!_vanillaItemSlot3.Item.IsAir)
			{
				// QuickSpawnClonedItem will preserve mod data of the item. QuickSpawnItem will just spawn a fresh version of the item, losing the prefix.
				Main.LocalPlayer.QuickSpawnClonedItem(_vanillaItemSlot3.Item, _vanillaItemSlot3.Item.stack);
				// Now that we've spawned the item back onto the player, we reset the item by turning it into air.
				_vanillaItemSlot3.Item.TurnToAir();
			}
			// Note that in ExamplePerson we call .SetState(new UI.ExamplePersonUI());, thereby creating a new instance of this UIState each time. 
			// You could go with a different design, keeping around the same UIState instance if you wanted. This would preserve the UIState between opening and closing. Up to you.
			IsVisible = false;
		}

		// Update is called on a UIState while it is the active state of the UserInterface.
		// We use Update to handle automatically closing our UI when the player is no longer talking to our Example Person NPC.
		public override void Update(GameTime gameTime)
		{
			// Don't delete this or the UIElements attached to this UIState will cease to function.
			base.Update(gameTime);

			// talkNPC is the index of the NPC the player is currently talking to. By checking talkNPC, we can tell when the player switches to another NPC or closes the NPC chat dialog.
		}


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
			ItemSlot.DrawSavings(Main.spriteBatch, slotX + 130, Main.instance.invBottom, true);

			if (reforge)
			{

				Main.LocalPlayer.mouseInterface = true;

				if (!_vanillaItemSlot.Item.IsAir)
				{
					if (ItemLoader.PreReforge(_vanillaItemSlot.Item))
					{
						bool favorited = _vanillaItemSlot.Item.favorited;
						int stack = _vanillaItemSlot.Item.stack;
						Item reforgeItem = new Item();
						reforgeItem.netDefaults(_vanillaItemSlot.Item.netID);
						reforgeItem = reforgeItem.CloneWithModdedDataFrom(_vanillaItemSlot.Item);
						// This is the main effect of this slot. Giving the Awesome prefix 90% of the time and the ReallyAwesome prefix the other 10% of the time. All for a constant 1 gold. Useless, but informative.
						reforgeItem.Prefix(-2);
						_vanillaItemSlot.Item = reforgeItem.Clone();
						_vanillaItemSlot.Item.position.X = Main.LocalPlayer.position.X + (float)(Main.LocalPlayer.width / 2) - (float)(_vanillaItemSlot.Item.width / 2);
						_vanillaItemSlot.Item.position.Y = Main.LocalPlayer.position.Y + (float)(Main.LocalPlayer.height / 2) - (float)(_vanillaItemSlot.Item.height / 2);
						_vanillaItemSlot.Item.favorited = favorited;
						_vanillaItemSlot.Item.stack = stack;
						ItemLoader.PostReforge(_vanillaItemSlot.Item);
						ItemText.NewText(_vanillaItemSlot.Item, _vanillaItemSlot.Item.stack, true, false);
						Main.PlaySound(SoundID.Item37, -1, -1);

					}
				}

				if (!_vanillaItemSlot2.Item.IsAir)
				{
					if (ItemLoader.PreReforge(_vanillaItemSlot2.Item))
					{
						bool favorited = _vanillaItemSlot2.Item.favorited;
						int stack = _vanillaItemSlot2.Item.stack;
						Item reforgeItem = new Item();
						reforgeItem.netDefaults(_vanillaItemSlot2.Item.netID);
						reforgeItem = reforgeItem.CloneWithModdedDataFrom(_vanillaItemSlot2.Item);
						// This is the main effect of this slot. Giving the Awesome prefix 90% of the time and the ReallyAwesome prefix the other 10% of the time. All for a constant 1 gold. Useless, but informative.
						reforgeItem.Prefix(-2);
						_vanillaItemSlot2.Item = reforgeItem.Clone();
						_vanillaItemSlot2.Item.position.X = Main.LocalPlayer.position.X + (float)(Main.LocalPlayer.width / 2) - (float)(_vanillaItemSlot2.Item.width / 2);
						_vanillaItemSlot2.Item.position.Y = Main.LocalPlayer.position.Y + (float)(Main.LocalPlayer.height / 2) - (float)(_vanillaItemSlot2.Item.height / 2);
						_vanillaItemSlot2.Item.favorited = favorited;
						_vanillaItemSlot2.Item.stack = stack;
						ItemLoader.PostReforge(_vanillaItemSlot2.Item);
						ItemText.NewText(_vanillaItemSlot2.Item, _vanillaItemSlot2.Item.stack, true, false);
						Main.PlaySound(SoundID.Item37, -1, -1);

					}
				}

				if (!_vanillaItemSlot3.Item.IsAir)
				{
					if (reforge && ItemLoader.PreReforge(_vanillaItemSlot3.Item))
					{
						bool favorited = _vanillaItemSlot3.Item.favorited;
						int stack = _vanillaItemSlot3.Item.stack;
						Item reforgeItem = new Item();
						reforgeItem.netDefaults(_vanillaItemSlot3.Item.netID);
						reforgeItem = reforgeItem.CloneWithModdedDataFrom(_vanillaItemSlot3.Item);
						// This is the main effect of this slot. Giving the Awesome prefix 90% of the time and the ReallyAwesome prefix the other 10% of the time. All for a constant 1 gold. Useless, but informative.
						reforgeItem.Prefix(-2);
						_vanillaItemSlot3.Item = reforgeItem.Clone();
						_vanillaItemSlot3.Item.position.X = Main.LocalPlayer.position.X + (float)(Main.LocalPlayer.width / 2) - (float)(_vanillaItemSlot3.Item.width / 2);
						_vanillaItemSlot3.Item.position.Y = Main.LocalPlayer.position.Y + (float)(Main.LocalPlayer.height / 2) - (float)(_vanillaItemSlot3.Item.height / 2);
						_vanillaItemSlot3.Item.favorited = favorited;
						_vanillaItemSlot3.Item.stack = stack;
						ItemLoader.PostReforge(_vanillaItemSlot3.Item);
						ItemText.NewText(_vanillaItemSlot3.Item, _vanillaItemSlot3.Item.stack, true, false);
						Main.PlaySound(SoundID.Item37, -1, -1);

					}
				}

				reforge = false; //only reforge once

				if(_vanillaItemSlot.Item.IsAir && _vanillaItemSlot2.Item.IsAir && _vanillaItemSlot3.Item.IsAir)
				{
					Main.NewText("Nothing to reforge...");
				}
			}
			else
			{

				string message = "Place an item in one(or all) of the slots to reforge";
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, message, new Vector2(screenX - 200, screenY - 40), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);

			}


		}
	}
}
