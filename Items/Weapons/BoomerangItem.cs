using ExtraExplosives.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
	public class BoomerangItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("BOOMerang");
			Tooltip.SetDefault("It’s coming back!!\n" +
				"[c/FF0000:Has a 1/5 chance of damaging you]");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.EnchantedBoomerang);
			item.shoot = ModContent.ProjectileType<BoomerangProjectile>();
			item.damage = 50;
<<<<<<< HEAD
			item.knockBack = 20;
			item.rare = ItemRarityID.Green;
=======
			item.rare = ItemRarityID.Lime;
>>>>>>> ade5cb7c462e2a26ee38cb48d28cf4313244afbb
			item.value = Item.buyPrice(0, 1, 0, 0);
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Flamarang, 1);
			recipe.AddIngredient(ItemID.Dynamite, 2);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool CanUseItem(Player player)
		{
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			return player.ownedProjectileCounts[item.shoot] < 1;
		}
	}
}