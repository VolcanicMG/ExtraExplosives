using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ExtraExplosives.Projectiles;
using static Terraria.ModLoader.ModContent;
using ExtraExplosives.Projectiles.Rockets;
using Microsoft.Xna.Framework;

namespace ExtraExplosives.Items.Rockets
{
   public class Rocket0Point5 : ExplosiveItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rocket 0.5");
            Tooltip.SetDefault("'Half a rocket?'");
        }

        public override void SafeSetDefaults()
        {
            item.CloneDefaults(ItemID.RocketI);
            item.width = 26;
            item.height = 14;
            item.value = Item.buyPrice(0, 0, 0, 25);
            item.rare = ItemRarityID.Blue;
            item.damage = 30;
            item.shoot = ModContent.ProjectileType<Rocket0Point5Projectile>();
        }

        public override void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {
            type = item.shoot;
        }
    }
}

