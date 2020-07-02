using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class HellavatorProjectile : ModProjectile
	{
		private const int PickPower = 40;
		private const string gore = "Gores/Explosives/hellevator_gore";
		private LegacySoundStyle explodeSound;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hellavator Projectile");
		}

		public override void SetDefaults()
		{
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
			explodeSound = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/Explosives/Hellavator_1");
		}

		public override bool OnTileCollide(Vector2 old)
		{
			projectile.Kill();

			return true;
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(explodeSound, (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Damage
			ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

			//Create Bomb Explosion
			CreateExplosion(projectile.Center, 0);

			//Create Bomb Dust
			CreateDust(projectile.Center, 400);

			//Create Bomb Gore
			Vector2 gVel1 = new Vector2(-2f, 2f);
			Vector2 gVel2 = new Vector2(2f, -2f);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(projectile.rotation), mod.GetGoreSlot(gore + "1"), projectile.scale);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(projectile.rotation), mod.GetGoreSlot(gore + "2"), projectile.scale);
		}

		private void CreateExplosion(Vector2 position, int radius)
		{
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
						if (!CanBreakTile(tile, PickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
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
						dust.noGravity = true;
						dust.noLight = true;
						dust.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);
					}
					//------------

					//---Dust 2---
					if (Main.rand.NextFloat() < 0.3f)
					{
						updatedPosition = new Vector2(position.X - 10 / 2, position.Y - 10 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 10, 10, 148, 0f, 0.2631581f, 120, new Color(255, 226, 0), 2.039474f)];
						dust.noGravity = true;
						dust.noLight = true;
						dust.shader = GameShaders.Armor.GetSecondaryShader(111, Main.LocalPlayer);
						dust.fadeIn = 3f;
					}
					//------------
				}
			}
		}
	}
}