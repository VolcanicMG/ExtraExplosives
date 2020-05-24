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
    public class LavamiteProjectile : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lavamite");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 10;   //This defines the hitbox width
            projectile.height = 32;    //This defines the hitbox height
            projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 100; //The amount of time the projectile is alive for
        }

        public override void PostAI()
        {
            Lighting.AddLight(projectile.position, new Vector3(2.2f, 1f, .1f));
            Lighting.maxX = 10;
            Lighting.maxY = 10;
        }

        public override void Kill(int timeLeft)
        {
            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
            int radius = 10;     //this is the explosion radius, the highter is the value the bigger is the explosion

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5)   //this make so the explosion radius is a circle
                    {
                        if (WorldGen.TileEmpty((int)(x + position.X / 16.0f), (int)(y + position.Y / 16.0f)))
                        {
                            Main.tile[xPosition, yPosition].liquidType(1);
                            Main.tile[xPosition, yPosition].liquid = 128;
                            WorldGen.SquareTileFrame(xPosition, yPosition, true);
                            //Dust.NewDust(position, 22, 22, DustID.Smoke, 0.0f, 0.0f, 120, new Color(), 1f);  //this is the dust that will spawn after the explosion

                        }
                    }
                }
            }

            for (int i = 0; i < 100; i++)
            {
                if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
                {
                    Dust dust;
                    Vector2 position1 = new Vector2(position.X - 168 / 2, position.Y - 168 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 168, 168, 185, 0.2631581f, 0f, 0, new Color(255, 0, 0), 3.815789f)];
                    dust.noGravity = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(58, Main.LocalPlayer);
                }
            }
        }

    }
}

//Main.tile[xPosition, yPosition].liquid = Tile.Liquid_Water Breaks water instead of creating it
// Main.tile[(int)((position.X + i) / 16), (int)((position.Y + j) / 16)].liquid = 1;