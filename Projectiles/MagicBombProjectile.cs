using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
<<<<<<< HEAD
    public class MagicBombProjectile : ModProjectile
    {
	    private int _pickPower = 0;
	    
        public override string Texture => "ExtraExplosives/Projectiles/HotPotatoProjectile";
        public override void SetStaticDefaults()
=======
    public class MagicBombProjectile : ExplosiveProjectile
    {
		protected override string explodeSoundsLoc => "n/a";
		protected override string goreFileLoc => "Gores/Explosives/magic_gore";
		public override void SetStaticDefaults()
>>>>>>> Charlie's-Uploads
        {
            DisplayName.SetDefault("Magic Bomb");
        }

<<<<<<< HEAD
        public override void SetDefaults()
        {
            projectile.CloneDefaults(29);
=======
        public override void SafeSetDefaults()
        {
            projectile.CloneDefaults(29);
            pickPower = 0;
            radius = 5;
>>>>>>> Charlie's-Uploads
        }
        public override void Kill(int timeLeft)
		{
			mod.Logger.DebugFormat("Damage {0}", projectile.damage);
			//Create Bomb Sound
			Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Damage
<<<<<<< HEAD
			ExplosionDamage(5f * 2f, projectile.Center, projectile.damage, 25, projectile.owner);

			//Create Bomb Explosion
			CreateExplosion(projectile.Center, 5);

			//Create Bomb Dust
			CreateDust(projectile.Center, 15);
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
						if (!CanBreakTile(tile, _pickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
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

=======
			ExplosionDamage();

			//Create Bomb Explosion
			Explosion();

			//Create Bomb Dust
			CreateDust(projectile.Center, 15);

			//Create Bomb Gore
			Vector2 gVel1 = new Vector2(1f, 0f);
			Vector2 gVel2 = new Vector2(1f, 1f);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "1"), projectile.scale);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "2"), projectile.scale);
		}
        
>>>>>>> Charlie's-Uploads
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
						updatedPosition = new Vector2(position.X - 120 / 2, position.Y - 120 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 120, 120, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 4.5f)];
<<<<<<< HEAD
						dust.noGravity = true;
						dust.fadeIn = 2.486842f;
=======
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.fadeIn = 2.486842f;
						}
>>>>>>> Charlie's-Uploads
					}
					//------------

					//---Dust 2---
					if (Main.rand.NextFloat() < 0.48f)
					{
						updatedPosition = new Vector2(position.X - 120 / 2, position.Y - 120 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 120, 120, 203, 0f, 0f, 0, new Color(255, 255, 255), 3f)];
<<<<<<< HEAD
						dust.noGravity = true;
						dust.noLight = true;
=======
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.noLight = true;
						}
>>>>>>> Charlie's-Uploads
					}
					//------------

					//---Dust 3---
					if (Main.rand.NextFloat() < 0.8f)
					{
						updatedPosition = new Vector2(position.X - 120 / 2, position.Y - 120 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 120, 120, 31, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
<<<<<<< HEAD
						dust.noGravity = true;
						dust.noLight = true;
=======
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.noLight = true;
						}
>>>>>>> Charlie's-Uploads
					}
					//------------
				}
			}
		}
    }
}