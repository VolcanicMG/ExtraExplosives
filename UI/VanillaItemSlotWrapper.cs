using ExtraExplosives.Sounds.Item;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;

namespace ExtraExplosives.UI
{
	internal class VanillaItemSlotWrapper : UIElement
	{
		internal Item Item;
		private readonly int _context;
		private readonly float _scale;
		internal Func<Item, bool> ValidItemFunc;
		internal string TextureString = "";

		internal bool Full;

		public VanillaItemSlotWrapper(int context = ItemSlot.Context.BankItem, float scale = 1f, string TextureFindString = "")
		{
			_context = context;
			_scale = scale;
			Item = new Item();
			Item.SetDefaults(0);

			Width.Set(Main.inventoryBack9Texture.Width * scale, 0f);
			Height.Set(Main.inventoryBack9Texture.Height * scale, 0f);

			TextureString = TextureFindString;
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			float oldScale = Main.inventoryScale;
			Main.inventoryScale = _scale;
			Rectangle rectangle = GetDimensions().ToRectangle();

			if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
			{
				Main.LocalPlayer.mouseInterface = true;
				if (ValidItemFunc == null || ValidItemFunc(Main.mouseItem))
				{
					// Handle handles all the click and hover actions based on the context.
					ItemSlot.Handle(ref Item, _context);

					Full = true;
				}
				
			}

			if(!this.Item.IsAir)
			{
				Full = true;
			}
			else
			{
				Full = false;
			}

			// Draw draws the slot itself and Item. Depending on context, the color will change, as will drawing other things like stack counts.
			ItemSlot.Draw(spriteBatch, ref Item, _context, rectangle.TopLeft());

			//draw the texture
			if (!TextureString.Equals("") && Full != true)
			{
				spriteBatch.Draw(ModContent.GetTexture(TextureString), new Rectangle(rectangle.X + (ModContent.GetTexture(TextureString).Width / 2), rectangle.Y + (ModContent.GetTexture(TextureString).Height / 2), ModContent.GetTexture(TextureString).Width, ModContent.GetTexture(TextureString).Height), new Color(255, 255, 255, 130));
			}

			Main.inventoryScale = oldScale;
		}
	}
}