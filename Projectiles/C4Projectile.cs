using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class C4Projectile : ExplosiveProjectile
	{
		//Variables
		protected override string explodeSoundsLoc => "Sounds/Custom/Explosives/C4_";
		protected override string goreFileLoc => "Gores/Explosives/c4_gore";
		private enum C4State
        {
			Airborne,
			Frozen,
			Primed,
			Exploding
        };
		private C4State projState = C4State.Airborne;
		// private bool freeze;
		private ExtraExplosivesPlayer c4Owner;
		private Vector2 positionToFreeze;
		private LegacySoundStyle indicatorSound;
		private LegacySoundStyle primedSound;
		private SoundEffectInstance indicatorSoundInstance;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("C4");
		}

		public override void SafeSetDefaults()
		{
			pickPower = 70;
			radius = 20;
			projectile.tileCollide = true;
			projectile.width = 32;
			projectile.height = 40;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = Int32.MaxValue;
			//projectile.extraUpdates = 1;
			Terraria.ModLoader.SoundType customType = Terraria.ModLoader.SoundType.Custom;
			indicatorSound = mod.GetLegacySoundSlot(customType, explodeSoundsLoc + "timer");
			primedSound = mod.GetLegacySoundSlot(customType, explodeSoundsLoc + "time_to_explode");
			if (!Main.dedServ && indicatorSound != null || primedSound != null) //Checking for nulls might fix the error
			{
				indicatorSound = indicatorSound.WithPitchVariance(0f).WithVolume(0.5f);
				primedSound = primedSound.WithPitchVariance(0f).WithVolume(0.5f);
			}
			else if(indicatorSound != null || primedSound != null)
            {
				indicatorSound = mod.GetLegacySoundSlot(customType, explodeSoundsLoc + "timer");
				primedSound = mod.GetLegacySoundSlot(customType, explodeSoundsLoc + "time_to_explode");
			}
			explodeSounds = new LegacySoundStyle[4];
			for (int num = 1; num <= explodeSounds.Length; num++)
			{
				explodeSounds[num - 1] = mod.GetLegacySoundSlot(customType, explodeSoundsLoc + "Bomb_" + num);
			}
		}

		public override bool OnTileCollide(Vector2 old)
		{
			// if (!freeze)
			if (projState == C4State.Airborne)
			{
				// freeze = true;
				projState = C4State.Frozen;
				positionToFreeze = new Vector2(projectile.position.X, projectile.position.Y);
				projectile.position.X = positionToFreeze.X;
				projectile.position.Y = positionToFreeze.Y;
				projectile.velocity.X = 0;
				projectile.velocity.Y = 0;
				//projectile.rotation = 0;
			}
			
			return false;
		}

		public override void PostAI()
		{
			switch (projState)
            {
				case C4State.Airborne:
					if (projectile.owner == Main.myPlayer && c4Owner == null)
                    {
						c4Owner = Main.player[projectile.owner].GetModPlayer<ExtraExplosivesPlayer>();
					}
					break;
				case C4State.Frozen:
					projectile.position = positionToFreeze;
					projectile.velocity = Vector2.Zero;
					if (indicatorSoundInstance == null)
						indicatorSoundInstance = Main.PlaySound(indicatorSound, (int)projectile.Center.X, (int)projectile.Center.Y);
					else if (indicatorSoundInstance.State != SoundState.Playing)	// else if needed to avoid a NullReferenceException
						indicatorSoundInstance.Play();
					if (c4Owner != null && c4Owner.detonate)
                    {
						projState = C4State.Primed;
						projectile.ai[1] = 55;
						Main.PlaySound(primedSound, (int)projectile.position.X, (int)projectile.position.Y);
                    }
					break;
				case C4State.Primed:
					projectile.ai[1]--;
					if (projectile.ai[1] < 1)
					{
						projState = C4State.Exploding;
					}
					break;
				case C4State.Exploding:
					projectile.Kill();
					break;
            }
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)], (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Dust
			CreateDust(projectile.Center, 550);
			
			Explosion();

			ExplosionDamage();

			//Creating Bomb Gore
			Vector2 gVel1 = new Vector2(-4f, -4f);
			Vector2 gVel2 = new Vector2(4f, -4f);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "1"), projectile.scale);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "2"), projectile.scale);
		}

		public override void Explosion()
		{
			if (Main.player[projectile.owner].EE().BombardEmblem) return;
			Vector2 position = projectile.Center;
			for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left
			{
				for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
				{
					int xPosition = (int)(x + position.X / 16.0f);
					int yPosition = (int)(y + position.Y / 16.0f);

					Tile tile = Framing.GetTileSafely(xPosition, yPosition);

					if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Circle
					{
						ushort tileP = tile.type;
						if (!CanBreakTile(tileP, pickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
						{
						}
						else //Breakable
						{
							if (CanBreakTiles) //User preferences dictates if bombs can break tiles
							{
								if (!TileID.Sets.BasicChest[Main.tile[xPosition, yPosition - 1].type] && !TileLoader.IsDresser(Main.tile[xPosition, yPosition - 1].type))
								{
									tile.ClearTile();
									tile.active(false);
								}
								if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false); //This destroys Walls
							}
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
				if (Main.rand.NextFloat() < DustAmount)
				{
					//---Dust 1---
					if (Main.rand.NextFloat() < 0.3f)
					{
						updatedPosition = new Vector2(position.X - 360 / 2, position.Y - 360 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 360, 360, 0, 0f, 0f, 171, new Color(33, 0, 255), 5.0f)];
						if (Vector2.Distance(dust.position, projectile.Center) > 360 / 2) dust.active = false;
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
						updatedPosition = new Vector2(position.X - 642 / 2, position.Y - 642 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 642, 642, 56, 0f, 0f, 0, new Color(255, 255, 255), 3f)];
						if (Vector2.Distance(dust.position, projectile.Center) > 642 / 2) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.noLight = true;
							dust.shader = GameShaders.Armor.GetSecondaryShader(91, Main.LocalPlayer);
						}
					}
					//------------

					//---Dust 3---
					if (Main.rand.NextFloat() < 0.3f)
					{
						updatedPosition = new Vector2(position.X - 560 / 2, position.Y - 560 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 560, 560, 6, 0f, 0.5263162f, 0, new Color(255, 150, 0), 5f)];
						if (Vector2.Distance(dust.position, projectile.Center) > 560 / 2) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.noLight = true;
							dust.fadeIn = 3f;
						}
					}
					//------------

					//---Dust 4---
					if (Main.rand.NextFloat() < 0.3f)
					{
						updatedPosition = new Vector2(position.X - 157 / 2, position.Y - 157 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 157, 157, 55, 0f, 0f, 0, new Color(255, 100, 0), 3.552631f)];
						if (Vector2.Distance(dust.position, projectile.Center) > 157 / 2) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);
						}
					}
					//------------
				}
			}
		}
	}
}