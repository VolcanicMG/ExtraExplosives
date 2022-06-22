using ExtraExplosives.Projectiles.Rockets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Rockets
{
    public class Rocket0 : ExplosiveItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rocket 0");
            Tooltip.SetDefault("'Mildly explosive'");
        }

        public override void SafeSetDefaults()
        {
            Item.CloneDefaults(ItemID.RocketI);
            Item.width = 26;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 0, 0, 15);
            Item.rare = ItemRarityID.Blue;
            Item.damage = 20;
            Item.shoot = ModContent.ProjectileType<Rocket0Projectile>();
        }

        public override void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {
            type = Item.shoot;
        }

        public override void AddRecipes()
        {
            Recipe modRecipe = CreateRecipe(25);
            modRecipe.AddIngredient(ItemID.IronBar, 1);
            modRecipe.AddIngredient(ItemID.Gel, 10);
            modRecipe.AddTile(TileID.WorkBenches);
            modRecipe.anyIronBar = true;
            modRecipe.Register();
        }
    }
}
