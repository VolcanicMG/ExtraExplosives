using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class LavamiteProjectile : ModProjectile
	{
		private const string gore = "Gores/Explosives/lavamite-hydromite_gore";
		private LegacySoundStyle[] explodeSounds;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lavamite");
		}

		public override void SetDefaults()
		{
			projectile.tileCollide = true;
			projectile.width = 10;
			projectile.height = 32;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 100;
			explodeSounds = new LegacySoundStyle[3];
			for (int num = 1; num <= explodeSounds.Length; num++)
            {
				explodeSounds[num - 1] = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/Explosives/Lavamite_" + num);
            }
		}

		public override void PostAI()
		{
			Lighting.AddLight(projectile.position, new Vector3(2.2f, 1f, .1f));
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
			CreateExplosion(projectile.Center, 10);

			//Create Bomb Dust
			CreateDust(projectile.Center, 100);

			//Create Bomb Gore
			Vector2 gVel1 = new Vector2(-2f, -2f);
			Vector2 gVel2 = new Vector2(0f, 2f);
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
						if (WorldGen.TileEmpty((int)(x + position.X / 16.0f), (int)(y + position.Y / 16.0f)))
						{
							Main.tile[xPosition, yPosition].liquidType(1);
							Main.tile[xPosition, yPosition].liquid = 128;
							WorldGen.SquareTileFrame(xPosition, yPosition, true);
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
					if (Main.rand.NextFloat() < 1f)
					{
						updatedPosition = new Vector2(position.X - 168 / 2, position.Y - 168 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 168, 168, 185, 0.2631581f, 0f, 0, new Color(255, 0, 0), 3.815789f)];
						dust.noGravity = true;
						dust.shader = GameShaders.Armor.GetSecondaryShader(58, Main.LocalPlayer);
					}
					//------------
				}
			}
		}
	}
}

//Main.tile[xPosition, yPosition].liquid = Tile.Liquid_Water Breaks water instead of creating it
// Main.tile[(int)((position.X + i) / 16), (int)((position.Y + j) / 16)].liquid = 1;