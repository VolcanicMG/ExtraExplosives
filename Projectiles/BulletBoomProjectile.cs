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
    public class BulletBoomProjectile : ModProjectile
    {
        internal static bool CanBreakWalls;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BulletBoom");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 22;   //This defines the hitbox width
            projectile.height = 22;    //This defines the hitbox height
            projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = 1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 40; //The amsadount of time the projectile is alive for
        }

        public override bool OnTileCollide(Vector2 old)
        {
            projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
            projectile.width = 20;
            projectile.height = 64;
            projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);

            projectile.velocity.X = 0;
            projectile.velocity.Y = 0;
            projectile.aiStyle = 0;
            return true;
        }

        public override void Kill(int timeLeft)
        {

            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
            int radius = 5;     //this is the explosion radius, the highter is the value the bigger is the explosion

            Vector2 vel;
            int spedX;
            int spedY;
            int cntr = 0;

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5)   //this make so the explosion radius is a circle
                    {
                            if (WorldGen.TileEmpty(xPosition, yPosition))
                            {

                                spedX = Main.rand.Next(15) - 7;
                                spedY = Main.rand.Next(15) - 7;
                                if (spedX == 0) spedX = 1;
                                if (spedY == 0) spedY = 1;
                                if (++cntr <= 100) Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, ProjectileID.ExplosiveBullet, 75, 20, projectile.owner, 0.0f, 0);
                            }
                            else
                            {

                                spedX = Main.rand.Next(15) - 7;
                                spedY = Main.rand.Next(15) - 7;
                                if (spedX == 0) spedX = 1;
                                if (spedY == 0) spedY = 1;
                                if (++cntr <= 100) Projectile.NewProjectile(position.X, position.Y, spedX, spedY, ProjectileID.ExplosiveBullet, 75, 20, projectile.owner, 0.0f, 0);
                            }
                        
                    }
                }
            }

            Dust dust1;
            Dust dust2;
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            for (int i = 0; i < 100; i++)
            {
                Vector2 position1 = new Vector2(position.X - 100 / 2, position.Y - 100 / 2);
                dust1 = Main.dust[Terraria.Dust.NewDust(position1, 100, 100, 0, 0f, 0f, 171, new Color(33, 0, 255), 4.0f)];
                dust1.noGravity = true;
                dust1.noLight = true;
                dust1.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);

            }

            for (int i = 0; i < 100; i++)
            {
                Vector2 position2 = new Vector2(position.X - 80 / 2, position.Y - 80 / 2);
                dust2 = Main.dust[Terraria.Dust.NewDust(position2, 80, 80, 6/*35*/, 0f, 0f, 0, new Color(240, 240, 240), 4.0f)];
                dust2.noGravity = true;
            }
        }
    }
}