using ExtraExplosives.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Buffs
{
	public class RadiatedDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Radiated");
			Description.SetDefault("Your flesh is deteriorating");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = false;
			canBeCleared = false;
			longerExpertDebuff = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			Vector2 NPCPos = npc.Center; //npc pos

			npc.GetGlobalNPC<ExtraExplosivesGlobalNPC>().Radiated = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			Vector2 PlayerPos = player.Center; //player pos

			player.GetModPlayer<ExtraExplosivesPlayer>().RadiatedDebuff = true;
		}
	}
}