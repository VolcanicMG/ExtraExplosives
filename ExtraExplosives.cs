using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using ExtraExplosives.Projectiles;
using ExtraExplosives.NPCs;
using ExtraExplosives.UI;

namespace ExtraExplosives
{
	public class ExtraExplosives : Mod
	{
		//move the first 4 over to player????
		internal static ModHotKey TriggerExplosion;
		internal static bool detonate = false;
		internal static Player playerProjectileOwner;
		internal static Player playerProjectileOwnerInvis;
		internal static float dustAmount;
		internal UserInterface ExtraExplosivesUserInterface;
		

		public ExtraExplosives()
		{



		}

		public override void UpdateUI(GameTime gameTime)
		{

			ExtraExplosivesUserInterface?.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));

			int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
			if (inventoryIndex != -1)
			{
				layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
					"ExtraExplosives: UI",
					delegate {
						// If the current UIState of the UserInterface is null, nothing will draw. We don't need to track a separate .visible value.
						ExtraExplosivesUserInterface.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
			}
		}
		public override void Load()
		{
			Logger.InfoFormat("{0} Extra Explosives logger", Name);

			ExtraExplosivesUserInterface = new UserInterface();
			TriggerExplosion = RegisterHotKey("Explode", "Mouse2");

		}
	}
}