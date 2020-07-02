using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class PhaseBombProjectile : ModProjectile
	{
		private Mod CalamityMod = ModLoader.GetMod("CalamityMod");
		private Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

		internal static bool CanBreakWalls;
		private const int PickPower = 50;
		private const string gore = "Gores/Explosives/phase_gore";
		private LegacySoundStyle phaseSound;
		private SoundEffectInstance phaseSoundInstance = null;
		private LegacySoundStyle[] explodeSounds;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("PhaseBomb");
			//Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
			Main.projFrames[projectile.type] = 10;
		}

		public override void SetDefaults()
		{
			projectile.tileCollide = false; //checks to see if the projectile can go through tiles
			projectile.width = 22;   //This defines the hitbox width
			projectile.height = 22;	//This defines the hitbox height
			projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
			projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
			projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
			projectile.timeLeft = 1000; //The amount of time the projectile is alive for
			phaseSound = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/Explosives/Phase_Bomb").WithVolume(0.5f);
			explodeSounds = new LegacySoundStyle[3];
			for (int num = 1; num <= explodeSounds.Length; num++)
            {
				explodeSounds[num - 1] = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/Explosives/Phase_Bomb_Explode_" + num);
            }
		}



		public override void AI()
		{
			if (phaseSoundInstance == null)
				phaseSoundInstance = Main.PlaySound(phaseSound, (int)projectile.Center.X, (int)projectile.Center.Y);

			if (phaseSoundInstance.State != SoundState.Playing)
				phaseSoundInstance.Play();

			Player player = Main.player[projectile.owner];
			if ((player.releaseLeft && Main.mouseLeft) == false)
			{
				phaseSoundInstance.Stop(true);
				projectile.Kill();
			}


			if (++projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				//projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
				if (++projectile.frame >= 10)
				{
					projectile.frame = 0;
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)], (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Dust
			CreateDust(projectile.Center, 500);

			//Create Bomb Damage
			ExplosionDamage(20f * 2f, projectile.Center, 450, 40, projectile.owner);

			//Create Bomb Explosion
			CreateExplosion(projectile.Center, 20);

			//Create Bomb Gore
			Vector2 gVel1 = new Vector2(0.0f, 3.0f);
			Vector2 gVel2 = new Vector2(0.0f, -3.0f);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(projectile.rotation), mod.GetGoreSlot(gore + "1"), projectile.scale);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(projectile.rotation), mod.GetGoreSlot(gore + "2"), projectile.scale);
		}

		private void CreateExplosion(Vector2 position, int radius)
		{
			for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left
			{
				for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
				{
					int xPosition = (int)(x + position.X / 16.0f);
					int yPosition = (int)(y + position.Y / 16.0f);

					if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Circle
					{
						ushort tile = Main.tile[xPosition, yPosition].type;
						if (!CanBreakTile(tile, PickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
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
		}

		private void CreateDust(Vector2 position, int amount)
		{
			Dust dust;
			//Vector2 updatedPosition;

			for (int i = 0; i <= amount; i++)
			{
				if (Main.rand.NextFloat() < DustAmount)
				{
					//---Dust 1---
					if (Main.rand.NextFloat() < 0.2f)
					{
						Vector2 position1 = new Vector2(position.X - 600 / 2, position.Y - 600 / 2);
						// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
						dust = Main.dust[Terraria.Dust.NewDust(position1, 600, 600, 155, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
						dust.noGravity = true;
						dust.shader = GameShaders.Armor.GetSecondaryShader(105, Main.LocalPlayer);

						Dust dust2;
						Vector2 position2 = new Vector2(position.X - 650 / 2, position.Y - 650 / 2);
						// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
						dust2 = Main.dust[Terraria.Dust.NewDust(position2, 600, 600, 49, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
						dust2.noGravity = true;
						dust2.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);
					}
					//------------
				}
			}
		}
	}
}