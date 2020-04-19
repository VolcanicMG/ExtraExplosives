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

namespace ExtraExplosives
{
	public class ExtraExplosives : Mod
	{
		internal static ModHotKey TriggerExplosion;
		internal static bool detonate = false;
		internal static Player playerProjectileOwner;

		public ExtraExplosives()
		{



		}

		public override void Load()
		{

			TriggerExplosion = RegisterHotKey("Explode", "Mouse2");

		}
	}
}