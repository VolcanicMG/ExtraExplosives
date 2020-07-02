using Microsoft.Xna.Framework;
using System;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class DynaglowmiteProjectile : ModProjectile
	{
		private const string gore = "Gores/Explosives/Dynaglowmite_Gore";
		private LegacySoundStyle[] explodeSounds;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dynaglowmite");
		}

		public override void SetDefaults()
		{
			projectile.tileCollide = true;
			projectile.width = 16;
			projectile.height = 32;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 45;
			projectile.damage = 0;
			//projectile.light = .9f;
			//projectile.glowMask = 2;
			explodeSounds = new LegacySoundStyle[4];
			for (int num = 1; num <= explodeSounds.Length; num++)
            {
				explodeSounds[num - 1] = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/Explosives/Dynaglowmite_" + num);
            }
		}

		public override void PostAI()
		{
			Lighting.AddLight(projectile.position, new Vector3(.1f, 1f, 2.2f));
			Lighting.maxX = 10;
			Lighting.maxY = 10;
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)], (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Damage
			//ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

			//Create Bomb Explosion
			CreateExplosion(projectile.Center, 0);

			//Create Bomb Dust
			CreateDust(projectile.Center, 100);

			//Create Bomb Gore
			int goreType = Main.rand.Next(2);
			Vector2 gVel = Vector2.One.RotatedByRandom(Math.PI * 2) * 2;
			if (goreType == 0)
				for (int num = 0; num < 2; num++)
				{
					Gore.NewGore(projectile.position + Vector2.Normalize(gVel), gVel.RotatedBy(projectile.rotation), mod.GetGoreSlot(gore + "1"), projectile.scale);
					gVel = gVel.RotatedByRandom(Math.PI * 2);
				}
			else
				Gore.NewGore(projectile.position + Vector2.Normalize(gVel), gVel.RotatedBy(projectile.rotation), mod.GetGoreSlot(gore + "2"), projectile.scale);

		}

		private void CreateExplosion(Vector2 position, int radius)
		{
			float x = 0;
			float y = 0;
			float speedX = -22f;
			float speedY = -22f;
			float[] z = { .1f, .2f, .3f, .4f, .5f, .6f, .7f, .8f };
			int yCntr = 1;
			int xCntr = 1;

			for (y = position.Y - 70; y < position.Y + 71; y++)
			{
				for (x = position.X - 70; x < position.X + 71; x++)
				{
					speedX += 5.5f; //Change X Velocity

					if (speedX < 0f)
						speedX -= z[Main.rand.Next(7)];

					if (speedX > 0f)
						speedX += z[Main.rand.Next(7)];

					if (yCntr == 1 || yCntr == 7)
						Projectile.NewProjectile(x, y, speedX, speedY, ProjectileID.StickyGlowstick, 0, 0, projectile.owner, 0.0f, 0); //Spawns in the glowsticks in square

					if ((xCntr == 1 || xCntr == 7) && (yCntr != 1 || yCntr != 7))
						Projectile.NewProjectile(x, y, speedX, speedY, ProjectileID.StickyGlowstick, 0, 0, projectile.owner, 0.0f, 0); //Spawns in the glowsticks in square

					x = x + 20;
					xCntr++;
				}

				y = y + 20;
				speedY += 5.5f; //Change Y Velocity
				speedX = -22f; //Reset X Velocity
				xCntr = 1;
				yCntr++;
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
						updatedPosition = new Vector2(position.X - 400 / 2, position.Y - 400 / 2);

						dust = Terraria.Dust.NewDustDirect(updatedPosition, 400, 400, 91, 0f, 0f, 157, new Color(0, 142, 255), 2.565789f);
						dust.noGravity = true;
						dust.fadeIn = 1.460526f;
					}
					//------------

					//---Dust 2---
					if (Main.rand.NextFloat() < 0.5f)
					{
						updatedPosition = new Vector2(position.X - 80 / 2, position.Y - 80 / 2);

						dust = Terraria.Dust.NewDustDirect(updatedPosition, 80, 80, 197, 0f, 0f, 157, new Color(0, 67, 255), 2.565789f);
						dust.noGravity = true;
						dust.fadeIn = 2.486842f;
						//------------
					}
				}
			}
		}
	}
}