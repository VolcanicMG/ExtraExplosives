using ExtraExplosives.Items.Explosives;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
			item.damage = 250;
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
			item.value = Item.buyPrice(0, 1, 0, 50);
			item.consumable = true;

			item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			item.autoReuse = false; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

			item.shoot = mod.ProjectileType("PrismBomb");
		}

		public override void AddRecipes()//whatever the recipe is
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LargeDiamond, 1);
			recipe.AddIngredient(ItemID.LargeAmethyst, 1);
			recipe.AddIngredient(ItemID.LargeEmerald, 1);
			recipe.AddIngredient(ItemID.LargeRuby, 1);
			recipe.AddIngredient(ItemID.FallenStar, 10);
			recipe.AddIngredient(ModContent.ItemType<MediumExplosiveItem>(), 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}