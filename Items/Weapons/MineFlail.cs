using ExtraExplosives.Projectiles.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class MineFlail : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mine Flail");
            Tooltip.SetDefault("InDev");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.value = Item.sellPrice(silver: 5);
            item.rare = ItemRarityID.White;
            item.noMelee = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 40;
            item.useTime = 40;
            item.knockBack = 4f;
            item.damage = 9;
            item.noUseGraphic = true;
            item.shoot = ModContent.ProjectileType<MineFlailProjectile>();
            item.shootSpeed = 15.1f;
            item.UseSound = SoundID.Item1;
            item.melee = true;
            item.crit = 9;
            item.channel = true;
        }
    }
}