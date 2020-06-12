
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ExtraExplosives.Items.Weapons
{
    public class PrismBomb : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Explodes into a rainbow prism that damages enemies");
        }

        public override void SetDefaults()
        {
            item.damage = 100;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 16;
            item.useTime = 22;
            item.shootSpeed = 15f;
            item.knockBack = 6.5f;
            item.width = 30;
            item.height = 36;
            item.maxStack = 99;
            item.scale = 1f;
            item.rare = ItemRarityID.Yellow;
            item.value = 30000;
            item.consumable = true;
            
            item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
            item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
            item.autoReuse = false; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

            
            item.shoot = mod.ProjectileType("PrismBomb");
        }
        public override void AddRecipes()//whatever the reipe is
        {
            
        }

    }
}