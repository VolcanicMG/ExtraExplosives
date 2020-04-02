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
    public class TorchBombProjectile : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Torch Bomb");
            //Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 40;   //This defines the hitbox width
            projectile.height = 40;    //This defines the hitbox height
            projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 45; //The amount of time the projectile is alive for
            projectile.damage = 0;
        }



        public override void Kill(int timeLeft)
        {
            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);

            float x = 0;
            float y = 0;
            float width = 20;
            float height = 20;

            int randomChance = 10;

            for(x = -width; x < width; x++)
            {
                for (y = -height; y < height; y++)
                {
                    if(Main.rand.Next(randomChance) == 1)
                        if(WorldGen.TileEmpty((int)(x + position.X / 16.0f), (int)(y + position.Y / 16.0f)))
                            WorldGen.PlaceTile((int)(x + position.X / 16.0f), (int)(y + position.Y / 16.0f), TileID.Torches, false, false, -1, 0);
                }
                x = x + 2;
            }


            //float x = 0;
            //float y = 0;
            //float speedX = -22f;
            //float speedY = -22f;
            //float[] z = { .1f, .2f, .3f, .4f, .5f, .6f, .7f, .8f };
            //int yCntr = 1;
            //int xCntr = 1;

            //for (y = position.Y - 70; y < position.Y + 71; y++)
            //{
            //    for (x = position.X - 70; x < position.X + 71; x++)
            //    {
            //        speedX += 5.5f; //Change X Velocity

            //        if (speedX < 0f)
            //            speedX -= z[Main.rand.Next(7)];

            //        if (speedX > 0f)
            //            speedX += z[Main.rand.Next(7)];

            //        if (yCntr == 1 || yCntr == 7)
            //            Projectile.NewProjectile(x, y, speedX, speedY, ProjectileID.StickyGlowstick, 0, 0, Main.myPlayer, 0.0f, 0); //Spawns in the glowsticks in square

            //        if ((xCntr == 1 || xCntr == 7) && (yCntr != 1 || yCntr != 7))
            //            Projectile.NewProjectile(x, y, speedX, speedY, ProjectileID.StickyGlowstick, 0, 0, Main.myPlayer, 0.0f, 0); //Spawns in the glowsticks in square

            //        x = x + 20;
            //        xCntr++;
            //    }
            //    y = y + 20;
            //    speedY += 5.5f; //Change Y Velocity
            //    speedX = -22f; //Reset X Velocity
            //    xCntr = 1;
            //    yCntr++;
            //}

        }

    }
}