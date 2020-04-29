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
using ExtraExplosives.Items;

namespace ExtraExplosives.Dusts
{
    public class ReforgeBombDust : ModDust
    {

		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.frame = new Rectangle(0, 0, 26, 24);
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.scale -= 0.01f;
			if (dust.scale < 0.75f)
			{
				dust.active = false;
			}
			return false;
		}
	}
}

