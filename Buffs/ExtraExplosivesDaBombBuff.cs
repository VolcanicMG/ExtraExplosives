using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Buffs
{
    public class ExtraExplosivesDaBombBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("You DA BOMB!");
            Description.SetDefault("You feel like you could explode\n" +
                "Defense Up");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            
            //canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 100;
        }
    }
}