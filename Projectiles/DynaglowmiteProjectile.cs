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

namespace ExtraExplosives.Projectiles
{
    public class DynaglowmiteProjectile : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dynaglowmite");
            //Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 16;   //This defines the hitbox width
            projectile.height = 32;    //This defines the hitbox height
            projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 45; //The amount of time the projectile is alive for
            projectile.damage = 0;
            //projectile.light = .9f;
            //projectile.glowMask = 2;
            
        }

        public override void PostAI()
        {
            Lighting.AddLight(projectile.position, new Vector3(.1f, 1f, 2.2f));
            Lighting.maxX = 10;
            Lighting.maxY = 10;
        
            
        }



        public override void Kill(int timeLeft)
        {
            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
            float x = 0;
            float y = 0;
            float speedX = -22f;
            float speedY = -22f;
            float[] z = { .1f, .2f, .3f, .4f, .5f, .6f, .7f, .8f };
            int yCntr = 1;
            int xCntr = 1;
            Dust dust;

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
                        {
                            Projectile.NewProjectile(x, y, speedX, speedY, ProjectileID.StickyGlowstick, 0, 0, projectile.owner, 0.0f, 0); //Spawns in the glowsticks in square

                        }
                        if ((xCntr == 1 || xCntr == 7) && (yCntr != 1 || yCntr != 7))
                        {
                            Projectile.NewProjectile(x, y, speedX, speedY, ProjectileID.StickyGlowstick, 0, 0, projectile.owner, 0.0f, 0); //Spawns in the glowsticks in square

                        }
                    
                    x = x + 20;
                    xCntr++;
                }
                y = y + 20;
                speedY += 5.5f; //Change Y Velocity
                speedX = -22f; //Reset X Velocity
                xCntr = 1;
                yCntr++;
            }

            for (int i = 0; i < 100; i++)
            {
                if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
                {
                    Vector2 glowPosition = new Vector2(position.X - 200, position.Y - 200);
                    dust = Terraria.Dust.NewDustDirect(glowPosition, 400, 400, 91, 0f, 0f, 157, new Color(0, 142, 255), 2.565789f);
                    dust.noGravity = true;
                    dust.fadeIn = 1.460526f;
                }
            }

            for (int i = 0; i < 50; i++)
            {
                if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
                {
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 glowPosition2 = new Vector2(position.X - 40, position.Y - 40);
                    dust = Terraria.Dust.NewDustDirect(glowPosition2, 80, 80, 197, 0f, 0f, 157, new Color(0, 67, 255), 2.565789f);
                    dust.noGravity = true;
                    dust.fadeIn = 2.486842f;
                }
            }

        }

    }
}