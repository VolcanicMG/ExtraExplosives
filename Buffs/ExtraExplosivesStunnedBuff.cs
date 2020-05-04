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
	public class ExtraExplosivesStunnedBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Stunned");
			Description.SetDefault("You can't move\n" +
				"You can't attack\n" +
				"You can't place blocks\n");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = false;
			canBeCleared = false;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			Vector2 NPCPos = npc.Center; //npc pos
			if (npc.boss)
			{
				
			}
			else
			{
				Dust dust1;
				//dust
				Vector2 position1 = new Vector2(NPCPos.X, NPCPos.Y - npc.height + 10);
				dust1 = Main.dust[Terraria.Dust.NewDust(position1, 10, 10, 106, 0f, 0f, 171, new Color(33, 0, 255), 2.0f)];
				dust1.noGravity = true;
				dust1.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);
				dust1.noLight = true;

				//stop the npc if not a boss
				npc.velocity.X = 0;
				npc.velocity.Y = npc.velocity.Y + 0.3f;
			}
			
		}

		public override void Update(Player player, ref int buffIndex)
		{

			Vector2 PlayerPos = player.Center; //player pos

			if (Main.netMode != NetmodeID.Server) // This all needs to happen client-side!
			{

				Filters.Scene.Activate("Bang", player.Center).GetShader().UseColor(255, 255, 255).UseOpacity(0.1f);
				//float progress = 0f;
				//Filters.Scene["Bang"].GetShader().UseProgress(progress).UseOpacity(0);

			}

			//spawn dust
			Dust dust1;
			Vector2 position1 = new Vector2(PlayerPos.X, PlayerPos.Y - player.height + 10);
			dust1 = Main.dust[Terraria.Dust.NewDust(position1, 10, 10, 106, 0f, 0f, 171, new Color(33, 0, 255), 2.0f)];
			dust1.noGravity = true;
			dust1.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);
			dust1.noLight = true;

			//stop the player
			player.controlUseItem = false;
			player.controlUseTile = false;
			player.velocity.X = 0;
			player.velocity.Y = player.velocity.Y + 0.3f;
		}

	}

}
