using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class BreakenTheBankenProjectile : ExplosiveProjectile
	{
		private const int PickPower = 0;
		private const string gore = "Gores/Explosives/breaken-the-banken_gore";
		private const string sounds = "Sounds/Custom/Explosives/Breaken_The_Banken_";
		private LegacySoundStyle[] explodeSounds;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("BreakenTheBanken");
		}

		public override void SafeSetDefaults()
		{
			radius = 20;
			pickPower = -2;
			projectile.tileCollide = true;
			projectile.width = 22;
			projectile.height = 22;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.penetrate = 20;
			projectile.timeLeft = 140;
			explodeSounds = new LegacySoundStyle[4];
			for (int num = 1; num <= explodeSounds.Length; num++)
				explodeSounds[num - 1] = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, sounds + num);
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)], (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Damage
			//ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

			//Create Bomb Explosion
			Explosion();
			
			ExplosionDamage();

			//Create Bomb Dust
			//CreateDust(projectile.Center, 10);

			//Create Bomb Gore
			Vector2 gVel1 = new Vector2(-3f, 3f);
			Vector2 gVel2 = new Vector2(3f, 0f);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(projectile.rotation), mod.GetGoreSlot(gore + "1"), projectile.scale);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(projectile.rotation), mod.GetGoreSlot(gore + "2"), projectile.scale);
		}

		public override void Explosion()
		{
			Vector2 position = projectile.Center;
			int cntr = 0; //Tracks how many coins have spawned in

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
							if (WorldGen.TileEmpty(xPosition, yPosition))
							{
								if (++cntr <= 50) Projectile.NewProjectile(position.X + x, position.Y + y, Main.rand.Next(10) - 5, Main.rand.Next(10) - 5, mod.ProjectileType("BreakenTheBankenChildProjectile"), 100, 20, projectile.owner, 0.0f, 0);
							}
							else
							{
								if (++cntr <= 50) Projectile.NewProjectile(position.X, position.Y, Main.rand.Next(10) - 5, Main.rand.Next(10) - 5, mod.ProjectileType("BreakenTheBankenChildProjectile"), 100, 20, projectile.owner, 0.0f, 0);
							}
						}
					}
				}
			}
		}

		public override void ExplosionDamage()
		{
			return;
		}
	}
}