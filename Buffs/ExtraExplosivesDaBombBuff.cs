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
using System.Text;
using System.Threading.Tasks;
using ExtraExplosives.Projectiles;

namespace ExtraExplosives.Buffs
{
		public class ExtraExplosivesDaBombBuff : ModBuff
		{
			public override void SetDefaults()
			{
				DisplayName.SetDefault("You DA BOMB!");
				Description.SetDefault("You feel like you could explode\n" +
					"Defense Up");
				Main.debuff[Type] = true;
				Main.buffNoSave[Type] = true;
				Main.buffNoTimeDisplay[Type] = true;
				canBeCleared = false;
			}

			public override void Update(Player player, ref int buffIndex)
			{

				player.statDefense += 100;

				
			}

	}

}
