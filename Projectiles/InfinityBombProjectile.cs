using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class InfinityBombProjectile : ModProjectile
    {
	    private int _pickPower = 1;
	    
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinity Bomb Projectile");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(29);
            projectile.timeLeft = 120;
            projectile.width = 20;
            projectile.height = 20;
            projectile.tileCollide = true;
        }
        public override void Kill(int timeLeft)
        {
	        mod.Logger.Debug(projectile.ai[0]);
	        
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

            //Create Bomb Dust
            CreateDust(projectile.Center, 10);

            //Create Bomb Damage
            ExplosionDamage(5f, projectile.Center, (int)Math.Ceiling(100 * projectile.ai[0]), (int)Math.Ceiling(20 * projectile.ai[0]), projectile.owner);

            //Create Bomb Explosion
            CreateExplosion(projectile.Center, (int)Math.Ceiling(2 * projectile.ai[0]));
        }
        
        private void CreateExplosion(Vector2 position, int radius)
        		{
        			for (int x = -radius; x <= radius; x++)
        			{
        				for (int y = -radius; y <= radius; y++)
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
        
        		private void CreateDust(Vector2 position, int amount)
        		{
        			Dust dust;
        			Vector2 updatedPosition;
        
        			for (int i = 0; i <= amount; i++)
        			{
        				if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
        				{
        					//Dust 1
        					if (Main.rand.NextFloat() < 0.9f)
        					{
        						updatedPosition = new Vector2(position.X - 78 / 2, position.Y - 78 / 2);
        
        						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 78, 78, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 4.539474f)];
        						dust.noGravity = true;
        						dust.fadeIn = 2.5f;
        					}
        
        					//Dust 2
        					if (Main.rand.NextFloat() < 0.6f)
        					{
        						updatedPosition = new Vector2(position.X - 78 / 2, position.Y - 78 / 2);
        
        						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 78, 78, 203, 0f, 0f, 0, new Color(255, 255, 255), 3.026316f)];
        						dust.noGravity = true;
        						dust.noLight = true;
        					}
        
        					//Dust 3
        					if (Main.rand.NextFloat() < 0.3f)
        					{
        						updatedPosition = new Vector2(position.X - 100 / 2, position.Y - 100 / 2);
        
        						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 100, 100, 31, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
        						dust.noGravity = true;
        						dust.noLight = true;
        					}
        				}
        			}
        		}
    }
}