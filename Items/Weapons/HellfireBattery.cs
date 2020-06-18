using ExtraExplosives.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
	public class HellfireBattery : ModItem
	{
		private int fireSpeed = 6;
		private int mode = 0;

		public static bool homing;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hellfire Battery");
			Tooltip.SetDefault("The rocket minigun's older bigger brother");
		}

		public override void SetDefaults()
		{
			item.useStyle = 5;
			item.autoReuse = true;
			item.useAnimation = fireSpeed;
			item.useTime = fireSpeed;
			item.useAmmo = AmmoID.Rocket;
			item.width = 78;
			item.height = 42;
			item.shoot = 134;
			item.UseSound = SoundID.Item11;
			item.channel = true;
			item.damage = 250;
			item.shootSpeed = 10f;
			item.noMelee = true;
			item.value = Item.buyPrice(0, 15, 0, 50);
			item.knockBack = 4f;
			item.rare = 10;
			item.ranged = true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{

			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 56f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}

			if (mode == 0)
			{
				//Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
				//speedX = perturbedSpeed.X;
				//speedY = perturbedSpeed.Y;
				homing = false;
				Projectile.NewProjectile(new Vector2(position.X, position.Y - 20), new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(25)), type, damage, knockBack, player.whoAmI);
				Projectile.NewProjectile(new Vector2(position.X, position.Y - 10), new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(25)), type, damage, knockBack, player.whoAmI);
				Projectile.NewProjectile(new Vector2(position.X, position.Y), new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(25)), type, damage, knockBack, player.whoAmI);

			}
			else if (mode == 1)
			{
				Projectile.NewProjectile(new Vector2(position.X, position.Y - 20), new Vector2(speedX, speedY), ModContent.ProjectileType<FollowRocketProjectile>(), damage + 400, knockBack, player.whoAmI);
				Projectile.NewProjectile(new Vector2(position.X, position.Y - 10), new Vector2(speedX, speedY), ModContent.ProjectileType<FollowRocketProjectile>(), damage + 400, knockBack, player.whoAmI);
				Projectile.NewProjectile(new Vector2(position.X, position.Y), new Vector2(speedX, speedY), ModContent.ProjectileType<FollowRocketProjectile>(), damage + 400, knockBack, player.whoAmI);
			}
			else if (mode == 2)
			{
				Projectile.NewProjectile(new Vector2(position.X, position.Y - 20), new Vector2(speedX, speedY), ModContent.ProjectileType<HomingRocketProjectile>(), damage + 300, knockBack, player.whoAmI);
				Projectile.NewProjectile(new Vector2(position.X, position.Y - 10), new Vector2(speedX, speedY), ModContent.ProjectileType<HomingRocketProjectile>(), damage + 300, knockBack, player.whoAmI);
				Projectile.NewProjectile(new Vector2(position.X, position.Y), new Vector2(speedX, speedY), ModContent.ProjectileType<HomingRocketProjectile>(), damage + 300, knockBack, player.whoAmI);
			}

			return false;
		}

		public override void HoldItem(Player player)
		{
			if(Main.mouseRight && Main.mouseRightRelease)
			{
				mode++;

				if (mode == 1)
				{
					Main.NewText("Precision Mode");

					item.useAnimation = 35;
					item.useTime = 35;
				}

				if (mode == 3)
				{
					Main.NewText("Spread Mode");

					item.useAnimation = 6;
					item.useTime = 6;

					mode = 0;
				}

				if (mode == 2)
				{
					Main.NewText("Homing Mode");

					item.useAnimation = 16;
					item.useTime = 16;

				}

			}

		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-20, -10);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.RocketLauncher, 1);
			recipe.AddIngredient(ItemID.ChainGun, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}