using ExtraExplosives.Projectiles.Rockets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
            Item.CloneDefaults(ItemID.RocketI);
            Item.width = 26;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 0, 0, 25);
            Item.rare = ItemRarityID.Blue;
            Item.damage = 30;
            Item.shoot = ModContent.ProjectileType<Rocket0Point5Projectile>();
        }

        public override void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {
            type = Item.shoot;
        }
    }
}

