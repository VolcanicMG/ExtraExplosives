using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class ClusterBombProjectile : ExplosiveProjectile
	{
		private Mod CalamityMod = ModLoader.GetMod("CalamityMod");
		private Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

		internal static bool CanBreakWalls;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("ClusterBomb");
			//Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
		}

		public override void SafeSetDefaults()
		{
			pickPower = 50;
			radius = 14;
			projectile.tileCollide = true; //checks to see if the projectile can go through tiles
			projectile.width = 40;   //This defines the hitbox width
			projectile.height = 40;	//This defines the hitbox height
			projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
			projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
			projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
			projectile.timeLeft = 150; //The amount of time the projectile is alive for
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Dust
			CreateDust(projectile.Center, 10);

			Explosion();
			ExplosionDamage();
			
			//Create Bomb Damage
			//ExplosionDamage(14f * 1.5f, projectile.Center, 450, 20, projectile.owner);

			//Create Bomb Explosion
			//CreateExplosion(projectile.Center, 14);

			Vector2 vel;
			Vector2 pos;

			for (int i = 0; i <= 50; i++)
			{
				if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
				{
					int Hw = 550;
					float scale = 10f;

					Dust dust;
					// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
					Vector2 vev = new Vector2(projectile.position.X - (Hw / 2), projectile.position.Y - (Hw / 2));
					dust = Main.dust[Terraria.Dust.NewDust(vev, Hw, Hw, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), scale)];
					if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
					else
					{
						dust.noGravity = true;
						dust.fadeIn = 2.486842f;
					}

					// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
					dust = Main.dust[Terraria.Dust.NewDust(vev, Hw, Hw, 203, 0f, 0f, 0, new Color(255, 255, 255), scale)];
					if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
					else
					{
						dust.noGravity = true;
						dust.noLight = true;
					}

					// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
					dust = Main.dust[Terraria.Dust.NewDust(vev, Hw, Hw, 31, 0f, 0f, 0, new Color(255, 255, 255), scale)];
					if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
					else
					{
						dust.noGravity = true;
						dust.noLight = true;
					}
				}
			}
		}

		public override void Explosion()
		{
			Vector2 position = projectile.Center;
			for (int x = -radius; x <= radius; x++)
			{
				for (int y = -radius; y <= radius; y++)
				{
					int i = (int)(x + position.X / 16.0f);
					int j = (int)(y + position.Y / 16.0f);

					if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(i, j))) //Circle
					{
						ushort tile = Main.tile[i, j].type;
						if (!CanBreakTile(tile, pickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
						{
						}
						else //Breakable
						{
							if (Main.rand.Next(100) == 1) Projectile.NewProjectile(position.X + x, position.Y + y, Main.rand.NextFloat(-15, 15), Main.rand.NextFloat(-15,5), ModContent.ProjectileType<ClusterBombChildProjectile>(), 150, 10, projectile.owner);
							if (Main.player[projectile.owner].EE().BombardEmblem) continue;
							Main.tile[i, j].ClearTile();
							//WorldGen.KillTile(xPosition, yPosition, false, false, false); //This destroys Tiles
							if (CanBreakWalls) WorldGen.KillWall(i, j, false); //This destroys Walls
						}
					}
				}
			}
		}

		private void CreateDust(Vector2 position, int amount)
		{
			Dust dust;
			Vector2 updatedPosition;

			for (int i = 0; i <= amount; i++)
			{
				if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
				{
					//Dust 1
					if (Main.rand.NextFloat() < 0.9f)
					{
						updatedPosition = new Vector2(position.X - 78 / 2, position.Y - 78 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 78, 78, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 4.539474f)];
						dust.noGravity = true;
						dust.fadeIn = 2.5f;
					}

					//Dust 2
					if (Main.rand.NextFloat() < 0.6f)
					{
						updatedPosition = new Vector2(position.X - 78 / 2, position.Y - 78 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 78, 78, 203, 0f, 0f, 0, new Color(255, 255, 255), 3.026316f)];
						dust.noGravity = true;
						dust.noLight = true;
					}

					//Dust 3
					if (Main.rand.NextFloat() < 0.3f)
					{
						updatedPosition = new Vector2(position.X - 100 / 2, position.Y - 100 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 100, 100, 31, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
						dust.noGravity = true;
						dust.noLight = true;
					}
				}
			}
		}
	}
}