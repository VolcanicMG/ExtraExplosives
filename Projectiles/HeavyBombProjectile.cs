using IL.Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	//TODO Possible make Explosion() and ExplosionDamage() unique to cut down on radius and damage resets
	public class HeavyBombProjectile : ExplosiveProjectile
	{
		protected override string explodeSoundsLoc => "n/a";
		protected override string goreFileLoc => "Gores/Explosives/heavy_gore";
		
		//Used to track when a tile can be destroyed
		private float counter
		{
			get => projectile.localAI[0];
			set => projectile.localAI[0] = value;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("HeavyBomb");
		}

		public override void SafeSetDefaults()
		{
			pickPower = 50;
			radius = 2;
			projectile.tileCollide = true;
			projectile.width = 13;
			projectile.height = 19;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 1000;
		}

		public override void PostAI()
		{
			if (counter > 0)
			{
				counter--;
				return;
			}

			counter = 5; // Timer so dust doesnt get spammed
		}

		public override bool OnTileCollide(Vector2 old)
		{
			Vector2 positionS = new Vector2(projectile.Center.X / 16f, projectile.Center.Y / 16f);    // Converts to tile cords for convenience
			Tile tileS = Framing.GetTileSafely((int)positionS.X, (int)positionS.Y + 1);

			if (tileS.active() && CanBreakTile(tileS.type, pickPower))
			{
				projectile.velocity.Y = -0.8f * old.Y;
				if (projectile.velocity.Y > 10) projectile.velocity.Y = 10;
				//Create Bomb Sound
				Main.PlaySound(SoundID.Item37, (int)projectile.Center.X, (int)projectile.Center.Y);

				//Create Bomb Damage
				ExplosionDamage();

				//Create Bomb Explosion
				Vector2 position = projectile.Center;

				if (!Main.player[projectile.owner].EE().BombardEmblem)  // Skip this if the emblem is equipped
				{
					for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left
					{
						for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
						{
							int xPosition = (int)(x + position.X / 16.0f);
							int yPosition = (int)(y + position.Y / 16.0f);

							if (Math.Sqrt(x * x + y * y) <= radius + 0.5 &&
								(WorldGen.InWorld(xPosition, yPosition))) //Circle
							{
								ushort tile = Main.tile[xPosition, yPosition].type;
								if (!CanBreakTile(tile, pickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
								{
								}
								else //Breakable
								{
									WorldGen.KillTile(xPosition, yPosition, false, false, false); 
								}
							}
						}
					}
				}

				if (counter >= 1) return false; // if the dust counter hasn't reset, dont spawn dust

				//Create Bomb Dust
				for (int i = 0; i < 10; i++)
				{
					if (Main.rand.NextFloat() < DustAmount)
					{
						Dust dust = Main.dust[
							Terraria.Dust.NewDust(new Vector2(position.X - (30 / 2), position.Y - 0), 30, 30, 1, 0f, 0f, 0,
								new Color(255, 255, 255), 1f)];
					}
				}
			}

			return false;
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(SoundID.Item14, (int) projectile.Center.X, (int) projectile.Center.Y);


			radius = 20;
			projectile.damage = 500; // Done because two different damage values are required and there is not clean way to alter then besdies this
			
			//Create Bomb Dust
			CreateDust(projectile.Center, 500);
			
			Explosion();
			ExplosionDamage();
			//Create Bomb Damage
			//ExplosionDamage(20f * 1.5f, projectile.Center, 500, 40, projectile.owner);

			//Create Bomb Explosion
			//CreateExplosion(projectile.Center, 20);

			//Create Bomb Gore
			Vector2 gVel1 = new Vector2(0f, 2f);
			Vector2 gVel2 = new Vector2(-2f, 2f);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "1"), projectile.scale);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "2"), projectile.scale);
		}

		/*public override void Explosion()
		{
			Vector2 position = projectile.Center;
			for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left
			{
				for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
				{
					int xPosition = (int)(x + position.X / 16.0f);
					int yPosition = (int)(y + position.Y / 16.0f);

					if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Circle
					{
						ushort tile = Main.tile[xPosition, yPosition].type;
						if (!CanBreakTile(tile, pickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
						{
						}
						else //Breakable
						{
							WorldGen.KillTile(xPosition, yPosition, false, false, false); //This destroys Tiles
							if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false); //This destroys Walls
						}
					}
				}
			}
		}*/

		private void CreateDust(Vector2 position, int amount)
		{
			Dust dust;
			Vector2 updatedPosition;

			for (int i = 0; i <= amount; i++)
			{
				if (Main.rand.NextFloat() < DustAmount)
				{
					//---Dust 1---
					if (Main.rand.NextFloat() < 0.5f)
					{
						updatedPosition = new Vector2(position.X - radius * 8, position.Y - radius * 8);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, radius * 16, radius * 16, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 10f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 8) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.fadeIn = 2.486842f;
						}
					}
					//------------

					//---Dust 2---
					if (Main.rand.NextFloat() < 0.5f)
					{
						updatedPosition = new Vector2(position.X - radius * 8, position.Y - radius * 8);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, radius * 16, radius * 16, 203, 0f, 0f, 0, new Color(255, 255, 255), 10f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 8) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.noLight = true;
						}
					}
					//------------

					//---Dust 3---
					if (Main.rand.NextFloat() < 0.5f)
					{
						updatedPosition = new Vector2(position.X - radius * 8, position.Y - radius * 8);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, radius * 16, radius * 16, 31, 0f, 0f, 0, new Color(255, 255, 255), 10f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 8) dust.active = false;
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