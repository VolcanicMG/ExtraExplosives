using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class HouseBombProjectile : ExplosiveProjectile
	{
		protected override string explodeSoundsLoc => "Sounds/Custom/Explosives/House_Bomb_";
		protected override string goreFileLoc => "Gores/Explosives/house_gore";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("HouseBomb");
		}

		public override void SafeSetDefaults()
		{
			IgnoreTrinkets = true;
			pickPower = 40;
			projectile.tileCollide = true;
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 1000;

			drawOffsetX = -15;
			drawOriginOffsetY = -15;
			explodeSounds = new LegacySoundStyle[4];
			for (int num = 1; num <= explodeSounds.Length; num++)
            {
				explodeSounds[num - 1] = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, explodeSoundsLoc + num);
            }
		}

		public override bool OnTileCollide(Vector2 old)
		{
			projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
			projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
			projectile.width = 5;
			projectile.height = 5;
			projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
			projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);

			projectile.velocity.X = 0;
			projectile.velocity.Y = 0;
			projectile.aiStyle = 0;
			return true;
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)], (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Damage
			//ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

			//Create Bomb Explosion
			Explosion();

			//Create Bomb Dust
			CreateDust(projectile.Center, 250);

			//Create Bomb Gore
			Vector2 gVel1 = new Vector2(2f, 2f);
			Vector2 gVel2 = new Vector2(-2f, -2f);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "1"), projectile.scale);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "2"), projectile.scale);
		}

		public override void Explosion()
		{
			Vector2 position = projectile.Center;
			
			int x = 0;
			int y = 0;

			int width = 11;
			int height = 7;

			for (x = -5; x < 6; x++)
			{
				for (y = height - 1; y >= 0; y--)
				{
					int xPosition = (int)(x + position.X / 16.0f);
					int yPosition = (int)(-y + position.Y / 16.0f);

					if (WorldGen.InWorld(xPosition, yPosition))
					{
						ushort tile = Main.tile[xPosition, yPosition].type;
						if (!CanBreakTile(tile, pickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
						{
						}
						else //Breakable
						{
							WorldGen.KillTile(xPosition, yPosition, false, false, false); //This destroys Tiles
							WorldGen.KillWall(xPosition, yPosition, false); //This destroys Walls

							Dust.NewDust(position, 22, 22, DustID.Smoke, 0.0f, 0.0f, 120, new Color(), 1f);
						}

						//Destroy water
						Main.tile[xPosition, yPosition].liquid = Tile.Liquid_Water;
						WorldGen.SquareTileFrame(xPosition, yPosition, true);

						//Particle Effects
						Dust.NewDust(position, 22, 22, DustID.Smoke, 0.0f, 0.0f, 120, new Color(), 1f);  //this is the dust that will spawn after the explosion

						//Place House Outline
						if (y == 0 || y == 6)
							WorldGen.PlaceTile(xPosition, yPosition, TileID.WoodBlock);
						if ((x == -5 || x == 5) && (y == 4 || y == 5))
							WorldGen.PlaceTile(xPosition, yPosition, TileID.WoodBlock);

						//Place House Walls
						if ((y == 5 || y == 2 || y == 1) && x != -5 && x != 5)
							WorldGen.PlaceWall(xPosition, yPosition, WallID.Wood);
						if (y == 3 || y == 4)
						{
							if (x == -4 || x == -3 || x == -2 || x == 2 || x == 3 || x == 4)
								WorldGen.PlaceWall(xPosition, yPosition, WallID.Wood);
							if (x == -1 || x == 0 || x == 1)
								WorldGen.PlaceWall(xPosition, yPosition, WallID.Glass);
						}

						//Places House Lights
						if (y == 5)
							if (x == -4 || x == 4)
								WorldGen.PlaceTile(xPosition, yPosition, TileID.Torches);
					}
				}
			}

			for (x = -5; x < 6; x++)
			{
				for (y = 0; y < height; y++)
				{
					int xPosition = (int)(x + position.X / 16.0f);
					int yPosition = (int)(-y + position.Y / 16.0f);

					//Places House Furniture
					if (y == 1)
					{
						if (x == -5 || x == 5) //Door
							WorldGen.PlaceTile(xPosition, yPosition, TileID.ClosedDoor);
						if (x == 0) //Table
							WorldGen.PlaceTile(xPosition, yPosition, TileID.Tables);
						if (x == 2) //Chair
							WorldGen.PlaceTile(xPosition, yPosition, TileID.Chairs);
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
					if (Main.rand.NextFloat() < 1f)
					{
						updatedPosition = new Vector2(position.X - 250 / 2, position.Y - 190 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 250, 190, 263, 0f, 0f, 0, new Color(255, 255, 255), 4.5f)];
						if (Vector2.Distance(dust.position, projectile.Center) > 125) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.noLight = true;
							dust.fadeIn = 1.618421f;
						}
					}
					//------------

					//---Dust 2---
					if (Main.rand.NextFloat() < 1f)
					{
						updatedPosition = new Vector2(position.X - 221 / 2, position.Y - 170 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 221, 170, 232, 0f, 0f, 214, new Color(255, 150, 0), 4.407895f)];
						if (Vector2.Distance(dust.position, projectile.Center) > 110) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.noLight = true;
						}
					}
					//----------------------

					//---Dust 3-------------
					if (Main.rand.NextFloat() < 1f)
					{
						updatedPosition = new Vector2(position.X - 221 / 2, position.Y - 170 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 221, 170, 1, 0f, 0f, 140, new Color(255, 255, 255), 2.5f)];
						if (Vector2.Distance(dust.position, projectile.Center) > 110) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.noLight = true;
						}
					}
					//------------
				}
			}
		}
	}
}