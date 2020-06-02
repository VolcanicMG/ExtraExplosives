using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using ExtraExplosives;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class BigBouncyDynamiteProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BigBouncyDynamite");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true;
            projectile.width = 13;
            projectile.height = 32;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 250;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // This code makes the projectile very bouncy.
            if (projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
            {
                if (projectile.velocity.X >= 10 || projectile.velocity.X < -10)
                    projectile.velocity.X = 10;
                else
                    projectile.velocity.X = oldVelocity.X * -1.2f;
            }
            if (projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
            {
                if (projectile.velocity.Y >= 10 || projectile.velocity.Y < -10)
                    projectile.velocity.Y = 10;
                else
                    projectile.velocity.Y = oldVelocity.Y * -1.2f;
            }
            return false;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);
            
            //Create Bomb Dust
            CreateDust(projectile.Center, 100);
            
            //Create Bomb Damage
            ExplosionDamage(3f, projectile.Center, 300, 30, projectile.owner);

            //Create Bomb Explosion
            CreateExplosion(projectile.Center, 5);

            
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
                        if (CheckForUnbreakableTiles(Main.tile[xPosition, yPosition].type)) //Unbreakable
                        {

                        }
                        else //Breakable
                        {
                            //Spawns in bouncy dynamite
                            Projectile.NewProjectile(position.X + x, position.Y + y, Main.rand.Next(100) - 50, Main.rand.Next(100) - 50, ProjectileID.BouncyDynamite, 0, 0, projectile.owner, 0.0f, 0);

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
                if (Main.rand.NextFloat() < DustAmount)
                {
                    //---Dust 1---
                    if (Main.rand.NextFloat() < 1f)
                    {
                        updatedPosition = new Vector2(position.X - 121 / 2, position.Y - 121 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 121, 121, 216, 0f, 0f, 0, new Color(255, 105, 180), 3.092105f)];
                        dust.noGravity = true;
                    }
                    //------------
                }
            }
        }
    }
}