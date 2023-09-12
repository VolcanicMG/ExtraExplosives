using ExtraExplosives.Items.Explosives;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class PrismBomb : ExplosiveItem
    {
        public override void SafeSetDefaults()
        {
            Item.damage = 250;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 16;
            Item.useTime = 22;
            Item.shootSpeed = 15f;
            Item.knockBack = 6.5f;
            Item.width = 30;
            Item.height = 36;
            Item.maxStack = 99;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.buyPrice(0, 1, 0, 50);
            Item.consumable = true;

            Item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
            Item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
            Item.autoReuse = false; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

            Item.shoot = ModContent.ProjectileType<Projectiles.PrismBomb.PrismBomb>();
        }

        public override void AddRecipes()//whatever the recipe is
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LargeDiamond, 1);
            recipe.AddIngredient(ItemID.LargeAmethyst, 1);
            recipe.AddIngredient(ItemID.LargeEmerald, 1);
            recipe.AddIngredient(ItemID.LargeRuby, 1);
            recipe.AddIngredient(ItemID.FallenStar, 10);
            recipe.AddIngredient(ModContent.ItemType<MediumExplosiveItem>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}