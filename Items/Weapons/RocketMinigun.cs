using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ExtraExplosives.Items.Weapons
{
	public class RocketMinigun : ModItem
	{

		private int fireSpeed = 15;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rocket Minigun");
			Tooltip.SetDefault("Uses up a lot of rockets...");

		}

		public override void SetDefaults()
		{

			item.useStyle = 5;
			item.autoReuse = true;
			item.useAnimation = fireSpeed;
			item.useTime = fireSpeed;
			item.useAmmo = AmmoID.Rocket;
			item.width = 66;
			item.height = 36;
			item.shoot = 134;
			item.UseSound = SoundID.Item11;
			item.channel = true;
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

			if (fireSpeed > 5) //change the firespeed
			{
				fireSpeed -= 1;
			}

			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 55f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}

			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;

			return true;

			
		}

		public override void HoldItem(Player player)
		{

			if(player.channel == true)
			{

				item.useTime = fireSpeed;
				item.useAnimation = fireSpeed;

			}
			else if(player.channel == false)
			{
				fireSpeed = 15;
				item.useTime = fireSpeed;
				item.useAnimation = fireSpeed;
				
			}

			base.HoldItem(player);
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