using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Buffs
{
    public class ExtraExplosivesSurstrommingBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = false;
            Main.buffNoTimeDisplay[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().surstromming = true;
        }
    }
}