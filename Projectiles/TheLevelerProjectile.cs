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
    public class TheLevelerProjectile : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Leveler");
            //Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 10;   //This defines the hitbox width
            projectile.height = 32;    //This defines the hitbox height
            projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 80; //The amount of time the projectile is alive for
            projectile.damage = 0;
        }



        public override void Kill(int timeLeft)
        {
            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);

            //int radius = 100;     //this is the explosion radius, the highter is the value the bigger is the explosion
            //int xPos;

            //int d = -1;
            //bool t = true;
            //int cntr = 1;


            int x = 0;
            int y = 0;

            int width = 60; //Explosion Width
            int height = 10; //Explosion Height

            for (y = 0; y > height; y++)
            {
                for(x = -width; x < width; x++)
                {
                    int xPos = (int) (x + position.X / 16.0f); //converts to world space
                    int yPos = (int) (y + position.Y / 16.0f); //converts to world space

                    WorldGen.KillTile(xPos, yPos, false, false, false);  //this make the explosion destroy tiles  
                    Dust.NewDust(position, width, height, DustID.Smoke, 0.0f, 0.0f, 120, new Color(), 1f);  //this is the dust that will spawn after the explosion

                }
                width++; //Increments width to make stairs on each end
            }



            //for (int x = 0; x <= radius; x++)
            //{
            //    xPos = (int)position.X;

            //    WorldGen.
            //    WorldGen.KillTile(xPos + d, (int)position.Y, false, false, false);  //this make the explosion destroy tiles  
            //    Dust.NewDust(position, 22, 22, DustID.Smoke, 0.0f, 0.0f, 120, new Color(), 1f);  //this is the dust that will spawn after the explosion

            //    d--;
            //    //if (t)
            //    //{
            //    //    d += cntr + 1;
            //    //    t = false;
            //    //}
            //    //else
            //    //{
            //    //    d -= cntr - 1;
            //    //    t = true;
            //    //}
            //    //cntr++;
                
            //}


            //int x = 0;
            //int cntr = 0;

            //for (int y = (int)position.Y; y < position.Y + 30; y++)
            //{
            //    for (x = (int)position.X; x > position.X - 500 ; x--)
            //    {
            //        WorldGen.KillTile(x, y, false, false, false);  //this make the explosion destroy tiles  
            //        Dust.NewDust(position, 22, 22, DustID.Smoke, 0.0f, 0.0f, 120, new Color(), 1f);  //this is the dust that will spawn after the explosion
            //        Main.NewText("xPos: " + x + " yPos: " + y + " cntr: " + cntr++ );
            //    }
            //    x = (int)position.X;
            //}
            Main.NewText("Terrain has been leveled! Would now be a good time to say \"action can't be undone...\"", (byte)30, (byte)255, (byte)10, false);
        }

    }
}