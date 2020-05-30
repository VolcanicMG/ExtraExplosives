using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.UI.Chat;
using ExtraExplosives;
using ExtraExplosives.UI;
using ExtraExplosives.Projectiles;
using ExtraExplosives.Items;

namespace ExtraExplosives.UI
{
	internal class ExtraExplosivesReforgeBombUI : UIState
	{

		//Mod CalamityMod = ModLoader.GetMod("CalamityMod");
		//Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

		private VanillaItemSlotWrapper _vanillaItemSlot;
		private VanillaItemSlotWrapper _vanillaItemSlot2;
		private VanillaItemSlotWrapper _vanillaItemSlot3;

		internal UIPanel Box;

		internal UITextPanel<string> ReforgeText;

		internal UIImage Image;
		internal UIImage Image2;
		internal UIImage Image3;
		internal UIImage Image4;

		private int screenX = Main.screenWidth / 2;
		private int screenY = (Main.screenHeight / 5);

		//internal static bool reforge = false;
		private static bool reforgeCheck = false;
		private static bool reforgeCheck2 = false;
		private static bool reforgeCheck3 = false;

		internal static bool IsVisible;
		public override void OnInitialize()
		{
			//panel box
			Box = new UIPanel();
			int width = 200;
			int height = 80;
			Box.SetPadding(0);
			Box.Left.Set(screenX - width / 2 - (width / 27), 0f);
			Box.Top.Set(screenY - height / 2 + (height / 8), 0f);
			Box.Width.Set(width, 0f);
			Box.Height.Set(height, 0f);
			Box.BackgroundColor = new Color(47, 83, 136, 200);
			Append(Box);

			//images
			int ImageWidth = 24;
			int ImageHeight = 24;
			Image = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/UICheckRed"));
			Image.Width.Set(ImageWidth, 0f);
			Image.Height.Set(ImageHeight, 0f);
			Image.Left.Set(width / 5 - ImageWidth / 2, 0f);
			Image.Top.Set(height / 2 - ImageHeight * 1.5f, 0f);
			//Image.ImageScale = 2.5f;
			Box.Append(Image);

			Image2 = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/UICheckRed"));
			Image2.Width.Set(ImageWidth, 0f);
			Image2.Height.Set(ImageHeight, 0f);
			Image2.Left.Set(width / 2 - ImageWidth / 2, 0f);
			Image2.Top.Set(height / 2 - ImageHeight * 1.5f, 0f);
			//Image.ImageScale = 2.5f;
			Box.Append(Image2);

			Image3 = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/UICheckRed"));
			Image3.Width.Set(ImageWidth, 0f);
			Image3.Height.Set(ImageHeight, 0f);
			Image3.Left.Set(width / 1.5f + ImageWidth / 1.7f, 0f);
			Image3.Top.Set(height / 2 - ImageHeight * 1.5f, 0f);
			//Image.ImageScale = 2.5f;
			Box.Append(Image3);

			Image4 = new UIImage(ModContent.GetTexture("ExtraExplosives/UI/Reforge"));
			Image4.Width.Set(ImageWidth, 0f);
			Image4.Height.Set(ImageHeight, 0f);
			Image4.Left.Set(width / 2 - ImageWidth / 2 - 3, 0f);
			Image4.Top.Set(height / 2 + ImageHeight + 20, 0f);
			//Image.ImageScale = 2.5f;
			Box.Append(Image4);

			ReforgeText = new UITextPanel<string>("Place an item in one(or all) of the slots to reforge");
			//float widthText = ReforgeText.GetDimensions().Width;
			ReforgeText.Left.Set(screenX - 215, 0f);
			ReforgeText.Top.Set(screenY - 80, 0f);
			Append(ReforgeText);

			//slots
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

			//image 1
			if(!_vanillaItemSlot.Item.IsAir && reforgeCheck == false)
			{
				Image.SetImage(ModContent.GetTexture("ExtraExplosives/UI/UICheckEmpty"));

			}
			else if (_vanillaItemSlot.Item.IsAir && reforgeCheck == false)
			{
				Image.SetImage(ModContent.GetTexture("ExtraExplosives/UI/UICheckRed"));

			}
			else if(!_vanillaItemSlot.Item.IsAir && reforgeCheck == true)
			{
				Image.SetImage(ModContent.GetTexture("ExtraExplosives/UI/UICheckGreen"));
				
			}

			//image 2
			if (!_vanillaItemSlot2.Item.IsAir && reforgeCheck2 == false)
			{
				Image2.SetImage(ModContent.GetTexture("ExtraExplosives/UI/UICheckEmpty"));

			}
			else if (_vanillaItemSlot2.Item.IsAir && reforgeCheck2 == false)
			{
				Image2.SetImage(ModContent.GetTexture("ExtraExplosives/UI/UICheckRed"));

			}
			else if (!_vanillaItemSlot2.Item.IsAir && reforgeCheck2 == true)
			{
				Image2.SetImage(ModContent.GetTexture("ExtraExplosives/UI/UICheckGreen"));

			}

			//image 3
			if (!_vanillaItemSlot3.Item.IsAir && reforgeCheck3 == false)
			{
				Image3.SetImage(ModContent.GetTexture("ExtraExplosives/UI/UICheckEmpty"));

			}
			else if (_vanillaItemSlot3.Item.IsAir && reforgeCheck3 == false)
			{
				Image3.SetImage(ModContent.GetTexture("ExtraExplosives/UI/UICheckRed"));

			}
			else if (!_vanillaItemSlot3.Item.IsAir && reforgeCheck3 == true)
			{
				Image3.SetImage(ModContent.GetTexture("ExtraExplosives/UI/UICheckGreen"));

			}

			if (_vanillaItemSlot.Item.IsAir && reforgeCheck == true)
			{
				reforgeCheck = false;
			}

			if (_vanillaItemSlot2.Item.IsAir && reforgeCheck2 == true)
			{
				reforgeCheck2 = false;
			}

			if (_vanillaItemSlot3.Item.IsAir && reforgeCheck3 == true)
			{
				reforgeCheck3 = false;
			}

			if (ExtraExplosivesPlayer.reforgePub)
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
					if (ItemLoader.PreReforge(_vanillaItemSlot3.Item))
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

				ExtraExplosivesPlayer.reforgePub = false; //Only reforge once
				reforgeCheck = true; //Check to see if the reforge went off
				reforgeCheck2 = true;
				reforgeCheck3 = true;

				if (_vanillaItemSlot.Item.IsAir && _vanillaItemSlot2.Item.IsAir && _vanillaItemSlot3.Item.IsAir)
				{
					Main.NewText("Nothing to reforge...");
				}
			}
			else
			{

				//string message = "Place an item in one(or all) of the slots to reforge";
				//ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, message, new Vector2(screenX - 200, screenY - 55), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);

			}


		}
	}
}
