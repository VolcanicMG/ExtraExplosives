using ExtraExplosives.Projectiles;
using ExtraExplosives.Projectiles.RegenBomb;
using Terraria.ModLoader;

namespace ExtraExplosives.Items
{
    
    // In development, here only so i dont lose it in the merge
    // More or less completely ignore this and its projectile for now it doesnt work
    public class RegenBombItem : ExplosiveItem
    {
        public override bool Autoload(ref string name)
        {
            return false;
        }

        public override void SafeSetDefaults()
        {
            item.consumable = false;
            item.width = 20;
            item.height = 20;
            item.damage = 0;
            item.knockBack = 0;
            item.shoot = ModContent.ProjectileType<RegenBombProjectile>();
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;
        }
    }
}