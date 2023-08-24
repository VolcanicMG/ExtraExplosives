using ExtraExplosives.UI.AnarchistCookbookUI.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
namespace ExtraExplosives.UI.AnarchistCookbookUI
{
	internal class ButtonUI : UIState
	{
		public static bool Visible;
		public DragableUIPanel dragablePanel;
		/*public override void OnInitialize()
		{
			Visible = false;
			dragablePanel = new DragableUIPanel();
			dragablePanel.SetPadding(0);
			dragablePanel.Left.Set(Main.screenWidth - 300, 0f);
			dragablePanel.Top.Set(Main.screenHeight - 100, 0f);
			dragablePanel.Width.Set(50, 0);
			dragablePanel.Height.Set(50, 0f);
			dragablePanel.BackgroundColor = new Color(0, 0, 0, 0);
			//dragablePanel.BorderColor = new Color(0, 0, 0, 0);
			Texture2D cookbookTexture = ModContent.Request<Texture2D>("ExtraExplosives/Items/Accessories/AnarchistCookbook/AnarchistCookbook").Value;
			UIHoverImageButton cookbookButton = new UIHoverImageButton(cookbookTexture, "Anarchist Cookbook");
			cookbookButton.Left.Set(22, 0f);
			cookbookButton.Top.Set(22, 0f);
			cookbookButton.Width.Set(22, 0);
			cookbookButton.Height.Set(22, 0f);
			cookbookButton.OnClick += new MouseEvent(OpenCookbook);
			dragablePanel.Append(cookbookButton);

			Append(dragablePanel);
		}*/

		private void OpenCookbook(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!Main.LocalPlayer.EE().AnarchistCookbook) return;

			Main.playerInventory = false;
			ModContent.GetInstance<ExtraExplosives>().cookbookInterface.SetState(new CookbookUI());
		}

		public override void Update(GameTime gameTime)
		{
			if (Main.playerInventory)
			{
				ModContent.GetInstance<ExtraExplosives>().cookbookInterface.SetState(null);
			}

			base.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			if (!Main.LocalPlayer.EE().AnarchistCookbook)
			{

				spriteBatch.Draw(ModContent.Request<Texture2D>("ExtraExplosives/UI/AnarchistCookbookUI/CookbookButtonLocked").Value, new Vector2(dragablePanel.Left.Pixels + 42, dragablePanel.Top.Pixels + 18), null, Color.White, 0f, Vector2.Zero, .6f, SpriteEffects.None, 0f);
			}

		}
	}

	internal class CookbookButton : UIImageButton
	{
		public ExtraExplosivesConfig EEConfig;

		public DragableUIPanel DragablePanel;

		public CookbookButton(Texture2D texture, string type) : base(null) // Todo this should not be null, it needs to be texture
		{
		}

		public override void OnInitialize()
		{
		}

		private Vector2 offset;
		public bool dragging;

		public override void RightMouseDown(UIMouseEvent evt)
		{
			base.RightMouseDown(evt);
			DragStart(evt);
		}

		public override void RightMouseUp(UIMouseEvent evt)
		{
			base.RightMouseUp(evt);
			DragEnd(evt);
		}

		private void DragStart(UIMouseEvent evt)
		{
			offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
			dragging = true;
		}

		private void DragEnd(UIMouseEvent evt)
		{
			Vector2 end = evt.MousePosition;
			dragging = false;

			Left.Set(end.X - offset.X, 0f);
			Top.Set(end.Y - offset.Y, 0f);

			Recalculate();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (ContainsPoint(Main.MouseScreen))
			{
				Main.LocalPlayer.mouseInterface = true;
			}

			if (dragging)
			{
				Left.Set(Main.mouseX - offset.X, 0f);
				Top.Set(Main.mouseY - offset.Y, 0f);

				Recalculate();
			}

			var parent = Parent.GetDimensions().ToRectangle();
			if (!GetDimensions().ToRectangle().Intersects(parent))
			{
				Left.Pixels = Utils.Clamp(Left.Pixels, 0, parent.Right - Width.Pixels);
				Top.Pixels = Utils.Clamp(Top.Pixels, 0, parent.Bottom - Height.Pixels);

				Recalculate();
				ExtraExplosives.EEConfig.AnarchistCookbookPos = new Vector2(Left.Pixels, Top.Pixels);
			}
		}
	}
}