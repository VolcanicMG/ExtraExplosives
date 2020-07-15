using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class HellavatorProjectile : ExplosiveProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hellavator Projectile");
		}

		public override void SafeSetDefaults()
		{
			radius = 0;
			pickPower = 40;
			projectile.tileCollide = true;
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 120;
			projectile.damage = 0;

			drawOffsetX = -15;
			drawOriginOffsetY = -15;
		}

		public override bool OnTileCollide(Vector2 old)
		{
			projectile.Kill();

			return true;
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Damage
			//ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

			//Create Bomb Explosion
			//CreateExplosion(projectile.Center, 0);

			Explosion();
			ExplosionDamage();
			
			//Create Bomb Dust
			CreateDust(projectile.Center, 400);
		}

		public override void Explosion()
		{
			Vector2 position = projectile.Center;
			int width = 3; //Explosion Width
			int height = Main.maxTilesY; //Explosion Height

			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				return;
			}

			Main.NewText(height);

			for (int x = -width; x < width; x++)
			{
				for (int y = 0; y <= height; y++)
				{
					int xPosition = (int)(x + position.X / 16.0f);
					int yPosition = (int)(y + position.Y / 16.0f);

					if (WorldGen.InWorld(xPosition, yPosition))
					{
						ushort tile = Main.tile[xPosition, yPosition].type;
						if (!CanBreakTile(tile, pickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
						{
						}
						else //Breakable
						{
							WorldGen.KillTile(xPosition, yPosition, false, false, false); //This destroys Tiles
							if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false); //This destroys Walls
							if (CanBreakWalls && y - 1 != height) WorldGen.KillWall(xPosition + 1, yPosition + 1, false); //Break the last bit of wall
							NetMessage.SendTileSquare(-1, xPosition, yPosition, 1);
						}

						Main.tile[xPosition, yPosition].liquid = Tile.Liquid_Water; //This destroys liquids
						WorldGen.SquareTileFrame(xPosition, yPosition, true); //Updates Area
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
				if (Main.rand.NextFloat() < DustAmount)
				{
					//---Dust 1---
					if (Main.rand.NextFloat() < 0.3f)
					{
						updatedPosition = new Vector2(position.X - 10 / 2, position.Y - 10 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 10, 10, 0, 0f, 0f, 171, new Color(33, 0, 255), 5.0f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.noLight = true;
							dust.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);
						}
					}
					//------------

					//---Dust 2---
					if (Main.rand.NextFloat() < 0.3f)
					{
						updatedPosition = new Vector2(position.X - 10 / 2, position.Y - 10 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 10, 10, 148, 0f, 0.2631581f, 120, new Color(255, 226, 0), 2.039474f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.noLight = true;
							dust.shader = GameShaders.Armor.GetSecondaryShader(111, Main.LocalPlayer);
							dust.fadeIn = 3f;
						}
					}
					//------------
				}
			}
		}
	}
}