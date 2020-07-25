using ExtraExplosives.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Items
{
    public class DebugBombItem : ExplosiveItem
    {
        public override string Texture => "ExtraExplosives/Items/Explosives/SmallExplosiveItem";

        public override bool IgnoreDamageModifiers { get; } = true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Debug Explosive");
            Tooltip.SetDefault("It could do nothing or destroy the world\n" +
                               "Just depends on what im testing");
        }

        public override void SafeSetDefaults()
        {
            item.CloneDefaults(167);
            Explosive = true;
            item.shoot = ModContent.ProjectileType<DebugBombProjectile>();
            item.damage = 100;
            item.knockBack = 100;
            item.crit = 100;
        }

        public override bool AltFunctionUse(Player player)
        {
            Main.NewText($"DEBUG INFO {this.Explosive}");
            return false;
        }
    }
}