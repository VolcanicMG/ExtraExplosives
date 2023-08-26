using ExtraExplosives.NPCs;
using Terraria;

namespace ExtraExplosives
{
    public static class StaticMethods
    {

        // Static Calls for Global things and the EE player
        // Added because I've called eeplayer alot and i hate the code

        public static ExtraExplosivesPlayer EE(this Player player) => (ExtraExplosivesPlayer)player.GetModPlayer<ExtraExplosivesPlayer>();
        public static ExtraExplosivesGlobalItem EE(this Item item) => (ExtraExplosivesGlobalItem)item.GetGlobalItem<ExtraExplosivesGlobalItem>();
        public static ExtraExplosivesGlobalProjectile EE(this Projectile projectile) => (ExtraExplosivesGlobalProjectile)projectile.GetGlobalProjectile<ExtraExplosivesGlobalProjectile>();
        public static ExtraExplosivesGlobalNPC EE(this NPC npc) => (ExtraExplosivesGlobalNPC)npc.GetGlobalNPC<ExtraExplosivesGlobalNPC>();

    }
}