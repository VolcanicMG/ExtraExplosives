using ExtraExplosives.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Buffs
{
	public class ExtraExplosivesSurstrommingBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Fuse-Eel Surstromming");
			Description.SetDefault("Explosive damage up!");
			Main.debuff[Type] = false;
			Main.buffNoSave[Type] = false;
			Main.buffNoTimeDisplay[Type] = false;
			canBeCleared = true;
			longerExpertDebuff = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{

			player.GetModPlayer<ExtraExplosivesPlayer>().surstromming = true;
		}
	}
}