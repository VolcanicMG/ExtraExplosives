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

namespace ExtraExplosives
{
	public class ExtraExplosivesPlayer : ModPlayer
	{
		
		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			Player player = Main.player[Main.myPlayer];

			if (ExtraExplosives.TriggerExplosion.JustReleased && ExtraExplosives.playerProjectileOwner == player)
			{
				ExtraExplosives.detonate = true;
				//Main.NewText("Detonate", (byte)30, (byte)255, (byte)10, false);
			}
			else
			{
				ExtraExplosives.detonate = false;
			}


		
		}

		


	}
}