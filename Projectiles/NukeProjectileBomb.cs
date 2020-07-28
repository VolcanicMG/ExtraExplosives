using ExtraExplosives.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class NukeProjectileBomb : ExplosiveProjectile
	{
		//Variables
		protected override string explodeSoundsLoc => "n/a";
		protected override string goreFileLoc => "n/a";
		private bool firstTick;

		private SoundEffectInstance sound;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("NukeExplosive");
		}

		public override void SafeSetDefaults()
		{
			pickPower = 250;
			radius = 150;
			projectile.tileCollide = false;
			projectile.width = 66;
			projectile.height = 112;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 10000;
			projectile.netImportant = true;
			//projectile.scale = 1.5f;
		}

		public override void AI()
		{
			//send the projectiles position to the player's camera and set NukeActive to true
			ExtraExplosives.NukePos = projectile.Center;
			ExtraExplosives.NukeActive = true; //since the projectile is active set it active in the player class

			if (!firstTick)
			{
				if (Main.netMode != NetmodeID.Server) // This all needs to happen client-side!
				{
					sound = Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/wizz"));
				}

				firstTick = true;
			}

			if ((projectile.position.Y / 16) > Main.maxTilesY - 100) //check abd see if the projectile is in the underworld if so destroy at maxtilesy - 100
			{
				Kill(0);
			}

			if ((projectile.position.Y / 16) > Main.worldSurface * 0.35)
			{
				//Main.NewText("Set");
				projectile.tileCollide = true;
			}
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Kill(0);
			return base.OnTileCollide(oldVelocity);
		}

		public override void Kill(int timeLeft)
		{
            Main.NewText("Kill");
			Player player = Main.player[projectile.owner];
			
			Main.screenPosition = player.Center;
			//Stop the sound
			if (Main.netMode != NetmodeID.Server) // This all needs to happen client-side!
			{
				sound.Stop();
			}

			player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " was killed by a nuclear explosion"), 5000, 1);

			//Set the shader
			ExtraExplosives.NukeHit = true;
			if (Main.netMode != NetmodeID.Server) // This all needs to happen client-side!
			{
				Filters.Scene.Activate("BigBang", player.Center).GetShader().UseColor(255, 255, 255).UseOpacity(0.1f);
				//float progress = 0f;
				//Filters.Scene["Bang"].GetShader().UseProgress(progress).UseOpacity(0);
			}

			Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Explosion"));


			//Create Bomb Damage
			ExplosionDamage();

			//Create Bomb Explosion
			Explosion();

			//for (int x = 0; x < 50; x++)
			//{
			//	SpawnProjectileSynced(new Vector2(projectile.position.X, projectile.position.Y), new Vector2(Main.rand.Next(15) - 7, Main.rand.Next(15) - 7), ModContent.ProjectileType<InvisibleNukeProjectile>(), 0, 0, 0, 0, projectile.owner);
			//	//Projectile.NewProjectile(position.X, position.Y, Main.rand.Next(40) + 10, Main.rand.Next(40) + 10, ModContent.ProjectileType<InvisibleNukeProjectile>(), 0, 0, projectile.owner, 0.0f, 0); //Spawns in the glowsticks in square
			//}
			projectile.timeLeft = 0;
			
			ExtraExplosives.NukeActive = false;
			ExtraExplosives.NukeActivated = false;
			ExtraExplosives.NukeHit = false;
			projectile.Kill();
		}

		public override void Explosion()
		{
			// x and y are the tile offset of the current tile relative to the player
            // i and j are the true tile cords relative to 0,0 in the world
            Player player = Main.player[projectile.owner];

            Vector2 position = new Vector2(projectile.Center.X / 16f, projectile.Center.Y / 16f);    // Converts to tile cords for convenience

            radius = (int)((radius + player.EE().RadiusBonus) * player.EE().RadiusMulti);
            for (int x = -radius;
                x <= radius;
                x++)
            {
                //int x = (int)(i + position.X);
                for (int y = -radius;
                    y <= radius;
                    y++)
                {
                    //int y = (int)(j + position.Y);
                    int i = (int) (x + position.X);
                    int j = (int) (y + position.Y);
                    if (!WorldGen.InWorld(i, j)) continue;
                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5) //Circle
                    {
                        //Main.NewText($"({i}, {j})");
                        //Dust dust = Dust.NewDustDirect(new Vector2(i, j), 1, 1, 54);
                        //dust.noGravity = true;
                        if (!WorldGen.TileEmpty(i, j))
                        {
                            if (!CanBreakTile(Main.tile[i, j].type, pickPower)) continue;
                            if (!CanBreakTiles) continue;
                            // Using KillTile is laggy, use ClearTile when working with larger tile sets    (also stops sound spam)
                            // But KillTile must be used tiles sitting on the radius to ensure propper updates so use it only on outermost tiles
                            // Using cleartile on the edge of the explosion will cause update failures when breaking tiles (especially multitiles) which is buggy and bad
                            if (Math.Abs(x) == radius || Math.Abs(y) == radius)
                                WorldGen.KillTile((int) (i), (int) (j), false, false, false);
                            else Main.tile[i,j].ClearTile();   
                            //
                        }
                        
                        if (CanBreakWalls)
                        {
                            //WorldGen.KillWall((int) (i), (int) (j));
                        }
                    }
                }
            }

			position = projectile.Center;
			
			int depth = 10; //Sets the depth of the waste

			for (int x = depth + 1; x > 0; x--)
			{
				if (x == depth + 1) AddNuclearWaste(radius++, position, x, ModContent.TileType<NuclearWasteSurfaceTile>(), ModContent.TileType<NuclearWasteSurfaceTile>());
				else AddNuclearWaste(radius++, position, x, ModContent.TileType<NuclearWasteSurfaceTile>(), ModContent.TileType<NuclearWasteSubSurfaceTile>());
			}
		}

		private void AddNuclearWaste(int radius, Vector2 position, int spawnChance, int surfaceTile, int subSurfaceTile)
		{
			for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left
			{
				for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
				{
					int i = (int)(x + position.X / 16.0f);
					int j = (int)(y + position.Y / 16.0f);

					if (Math.Sqrt(x * x + y * y) <= radius - 1 + 0.5 && (WorldGen.InWorld(i, j))) //Circle
					{
						Main.tile[i, j].liquid = Tile.Liquid_Water;
						WorldGen.SquareTileFrame(i, j, true);
					}
					else if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(i, j))) //Circle
					{
						Main.tile[i, j].liquid = Tile.Liquid_Water;
						WorldGen.SquareTileFrame(i, j, true);

						ushort tile = Main.tile[i, j].type;
						if (!CanBreakTile(tile, pickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
						{
							if (!WorldGen.TileEmpty(i, j)) //Runs when a tile is not equal empty
							{
								if (Main.rand.Next(10) < spawnChance)
								{
									WorldGen.KillTile(i, j, false, false, true);
									if (WorldGen.TileEmpty(i + 1, j) || WorldGen.TileEmpty(i - 1, j) || WorldGen.TileEmpty(i, j + 1) || WorldGen.TileEmpty(i, j - 1))
									{
										WorldGen.PlaceTile(i, j, surfaceTile);
									}
									else
									{
										WorldGen.PlaceTile(i, j, subSurfaceTile);
									}
								}
							}
						}
						else //Breakable
						{
							if (!WorldGen.TileEmpty(i, j)) //Runs when a tile is not equal empty
							{
								if (Main.rand.Next(10) < spawnChance)
								{
									WorldGen.KillTile(i, j, false, false, true);
									if (WorldGen.TileEmpty(i + 1, j) || WorldGen.TileEmpty(i - 1, j) || WorldGen.TileEmpty(i, j + 1) || WorldGen.TileEmpty(i, j - 1))
									{
										WorldGen.PlaceTile(i, j, surfaceTile);
									}
									else
									{
										WorldGen.PlaceTile(i, j, subSurfaceTile);
									}
								}
							}
						}
					}
				}
			}
		}
	}
}