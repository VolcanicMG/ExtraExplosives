using ExtraExplosives.Projectiles;
using ExtraExplosives.Sounds.Custom;
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
			Tooltip.SetDefault("The rocket minigun's older bigger brother\n" +
				"[c/AB40FF:Right click to swap between 3 different modes:]\n" +
				"Spread mode\n" +
				"Precision mode\n" +
				"Homing mode");
		}

		public override void SetDefaults()
		{
			item.useStyle = 5;
			item.autoReuse = true;
			item.useAnimation = fireSpeed;
			item.useTime = fireSpeed;
			item.useAmmo = AmmoID.Rocket;
			item.width = 78;
			item.crit = 35;
			item.height = 42;
			item.shoot = 134;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Hellfire");
			item.channel = true;
			item.damage = 250;
			item.shootSpeed = 13f;
			item.noMelee = true;
			item.value = Item.buyPrice(10, 1, 0, 50);
			item.knockBack = 4f;
			item.rare = 11;
			item.ranged = true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{

			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 56f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}

			if (mode == 0) //spread
			{
				//Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
				//speedX = perturbedSpeed.X;
				//speedY = perturbedSpeed.Y;
				homing = false;
				Projectile.NewProjectile(new Vector2(position.X, position.Y - 20), new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(25)), type, damage, knockBack, player.whoAmI);
				Projectile.NewProjectile(new Vector2(position.X, position.Y - 10), new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(25)), type, damage, knockBack, player.whoAmI);
				Projectile.NewProjectile(new Vector2(position.X, position.Y), new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(25)), type, damage, knockBack, player.whoAmI);

			}
			else if (mode == 1) //precision
			{
				Projectile.NewProjectile(new Vector2(position.X, position.Y), new Vector2(speedX, speedY), ModContent.ProjectileType<FollowRocketProjectile>(), damage + 3000, knockBack, player.whoAmI);
			}
			else if (mode == 2) //homing
			{
				Projectile.NewProjectile(new Vector2(position.X, position.Y + Main.rand.NextFloat(10, -20)), new Vector2(speedX, speedY), ModContent.ProjectileType<HomingRocketProjectile>(), damage + 100, knockBack, player.whoAmI);
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
					Main.NewText("[c/AC7988:Precision Mode]");

					item.useAnimation = 50;
					item.useTime = 50;
				}

				if (mode == 3)
				{
					Main.NewText("[c/AC7988:Spread Mode]");

					item.useAnimation = 6;
					item.useTime = 6;

					mode = 0;
				}

				if (mode == 2)
				{
					Main.NewText("[c/AC7988:Homing Mode]");

					item.useAnimation = 12;
					item.useTime = 12;

				}

			}

		}

		public override bool CanUseItem(Player player)
		{
			if (mode == 1)
			{
				// Ensures no more than one spear can be thrown out, use this when using autoReuse
				return player.ownedProjectileCounts[ModContent.ProjectileType<FollowRocketProjectile>()] < 1;
			}
			else
			{
				return true;
			}
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-20, -10);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<RocketMinigun>(), 1);
			//recipe.AddIngredient(ModContent.ItemType<EndlessRocketLoader>(), 1);
			recipe.AddIngredient(ItemID.SDMG, 1);
			recipe.AddIngredient(ItemID.FragmentSolar, 25);
			recipe.AddIngredient(ItemID.FragmentNebula, 25);
			recipe.AddIngredient(ItemID.FragmentStardust, 25);
			recipe.AddIngredient(ItemID.FragmentVortex, 25);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}