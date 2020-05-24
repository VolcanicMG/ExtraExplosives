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
    public class ExtraExplosives_ProjectileTemplate : ModProjectile
    {
        public override string Texture => "ExtraExplosives/Projectiles/BulletBoomProjectile"; //DELETE ME********************************

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ExtraExplosives_ProjectileTemplate");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true;
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 150;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

            //Create Bomb Damage
            ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

            //Create Bomb Explosion
            CreateExplosion(projectile.Center, 2);

            //Create Bomb Dust
            CreateDust(projectile.Center, 10);

        }

        /// <summary>
        /// This function will create an explosion - All explosion related things happen in here
        /// </summary>
        /// <param name="position"> Stores the center point of the explosion - Try: projectile.Center </param>
        /// <param name="radius"> Stores the radius of the explosion </param>
        private void CreateExplosion(Vector2 position, int radius)
        {
            for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left 
            {
                for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5) //Circle
                    {
                        if (CheckForUnbreakableTiles(Main.tile[xPosition, yPosition].type)) //Unbreakable
                        {
                            
                        }
                        else //Breakable
                        {
                            //-----===THIS IS WHERE THE BOMBS UNIQUE CODE GOES===-----\\

                            //-----===########################################===-----\\
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This function will create dust at a set point
        /// </summary>
        /// <param name="position"> Stores the center point of the explosion - Try: projectile.Center </param>
        /// <param name="amount"> Stores max intended amount of dust, this will be overridden by user preferences </param>
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
                        updatedPosition = new Vector2(position.X - 100 / 2, position.Y - 100 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 100, 100, 6, 0f, 0.5f, 0, new Color(255, 0, 0), 4f)];
                        dust.noGravity = true;
                        dust.fadeIn = 0f;
                        dust.noLight = true;
                    }
                    //------------
                }
            }
        }
    }
}


