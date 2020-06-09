using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System;

namespace ExtraExplosives.Items.Weapons
{
	public class AirStrike : ModItem
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Air Strike");
			Tooltip.SetDefault("Send them down!");

		}

		public override void SetDefaults()
		{

			item.useStyle = 5;
			item.autoReuse = true;
			item.useAnimation = 140;
			item.useTime = 140;
			item.useAmmo = AmmoID.Rocket;
			item.width = 66;
			item.height = 36;
			item.shoot = ProjectileID.RocketI;
			item.UseSound = SoundID.Item11;
			//item.channel = true;
			item.damage = 200;
			item.shootSpeed = 10f;
			item.noMelee = true;
			item.value = Item.buyPrice(0, 30, 0, 50);
			item.knockBack = 4f;
			item.rare = 9;
			item.ranged = true;

		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{

			int numberProjectiles = 15; // shoots 6 projectiles
			for (int index = 0; index < numberProjectiles; ++index)
			{
				Vector2 vector2_1 = new Vector2((float)((double)player.position.X + (double)player.width * 0.5 + (double)(Main.rand.Next(201) * -player.direction) + ((double)Main.mouseX + (double)Main.screenPosition.X - (double)player.position.X)), (float)((double)player.position.Y + (double)player.height * 0.5 - 600.0));   //this defines the projectile width, direction and position
				vector2_1.X = (float)(((double)vector2_1.X + (double)player.Center.X) / 2.0) + (float)Main.rand.Next(-200, 201);
				vector2_1.Y -= (float)(100 * index);
				float num12 = (float)Main.mouseX + Main.screenPosition.X - vector2_1.X;
				float num13 = (float)Main.mouseY + Main.screenPosition.Y - vector2_1.Y;
				if ((double)num13 < 0.0) num13 *= -1f;
				if ((double)num13 < 20.0) num13 = 20f;
				float num14 = (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
				float num15 = item.shootSpeed / num14;
				float num16 = num12 * num15;
				float num17 = num13 * num15;
				float SpeedX = num16 + (float)Main.rand.Next(-40, 41) * 0.02f; //change the Main.rand.Next here to, for example, (-10, 11) to reduce the spread. Change this to 0 to remove it altogether
				float SpeedY = num17 + (float)Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(vector2_1.X, vector2_1.Y, SpeedX, SpeedY, type, damage, knockBack, Main.myPlayer, 0.0f, (float)Main.rand.Next(5));
			}
			return false;
			
		}

		public override void HoldItem(Player player)
		{

			if(player.channel == true)
			{

				

			}
			else if(player.channel == false)
			{
				
				
			}

			base.HoldItem(player);
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(0, 0);
		}


		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.RocketLauncher, 1);
			recipe.AddIngredient(ItemID.DaedalusStormbow, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

}