using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
	public class RocketMinigun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rocket Minigun");
			Tooltip.SetDefault("Uses up a lot of rockets...");
		}

		public override void SetDefaults()
		{

			item.useStyle = 5;
			item.autoReuse = true;
			item.useAnimation = 5;
			item.useTime = 5;
			item.useAmmo = AmmoID.Rocket;
			item.width = 30;
			item.height = 20;
			item.shoot = 134;
			item.UseSound = SoundID.Item11;
			item.damage = 200;
			item.shootSpeed = 10f;
			item.noMelee = true;
			item.value = Item.buyPrice(0, 5, 0, 50);
			item.knockBack = 4f;
			item.rare = 9;
			item.ranged = true;

		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{

			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}

			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}


		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.RocketLauncher, 1);
			recipe.AddIngredient(ItemID.ChainGun, 1);
			recipe.anyWood = true;
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

}