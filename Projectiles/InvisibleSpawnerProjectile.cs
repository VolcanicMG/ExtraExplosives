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
using ExtraExplosives.Items;
using ExtraExplosives;
using System.Timers;
using System.Threading;

namespace ExtraExplosives.Projectiles
{
    public class InvisibleSpawnerProjectile : ModProjectile
    {

        internal static bool CanBreakWalls;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("InvisibleSpawner");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 22;   //This defines the hitbox width
            projectile.height = 22;    //This defines the hitbox height
            projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 150; //The amsadount of time the projectile is alive for
            projectile.Opacity = 0f;
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Main.myPlayer];
            Vector2 position = projectile.Center;
            Dust dust1;
            Dust dust;
            //Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
            //int radius = 2;     //this is the explosion radius, the highter is the value the bigger is the explosion

            //damage part of the bomb
            //ExplosionDamageProjectile.DamageRadius = 2;
            //Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("ExplosionDamageProjectile"), 1, 0, Main.myPlayer, 0.0f, 0);

            if (projectile.knockBack == 1)
            {
                Main.NewText("3!");
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 100 / 2, position.Y - 100 / 2);
                    dust1 = Main.dust[Terraria.Dust.NewDust(position1, 100, 100, 0, 0f, 0f, 171, new Color(33, 0, 255), 4.0f)];
                    dust1.noGravity = true;
                    dust1.noLight = true;
                    dust1.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);
                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 2, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 2)
            {
                Main.NewText("2!");
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 100 / 2, position.Y - 100 / 2);
                    dust1 = Main.dust[Terraria.Dust.NewDust(position1, 100, 100, 0, 0f, 0f, 171, new Color(33, 0, 255), 4.0f)];
                    dust1.noGravity = true;
                    dust1.noLight = true;
                    dust1.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);
                }
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 30 / 2, position.Y - 30 / 2);
                    dust1 = Main.dust[Terraria.Dust.NewDust(position1, 30, 30, 91, 0f, -6.052631f, 0, new Color(255, 255, 255), 1.842105f)];
                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 3, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 3)
            {
                Main.NewText("1!");
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 100 / 2, position.Y - 100 / 2);
                    dust1 = Main.dust[Terraria.Dust.NewDust(position1, 100, 100, 0, 0f, 0f, 171, new Color(33, 0, 255), 4.0f)];
                    dust1.noGravity = true;
                    dust1.noLight = true;
                    dust1.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);
                }
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 30 / 2, position.Y - 30 / 2);
                    dust1 = Main.dust[Terraria.Dust.NewDust(position1, 30, 30, 91, 0f, -6.052631f, 0, new Color(255, 255, 255), 1.842105f)];
                }
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 121 / 2, position.Y - 121 / 2);
                    dust1 = Main.dust[Terraria.Dust.NewDust(position1, 121, 121, 274, 0f, 0.7894742f, 0, new Color(255, 0, 226), 3.815789f)];
                    dust1.shader = GameShaders.Armor.GetSecondaryShader(77, Main.LocalPlayer);

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 4, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 4)
            {
                Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
                float x = 0;
                float y = 0;
                float speedX = -22f;
                float speedY = -22f;
                float[] z = { .1f, .2f, .3f, .4f, .5f, .6f, .7f, .8f };
                int yCntr = 1;
                int xCntr = 1;
                

                for (y = position.Y - 70; y < position.Y + 71; y++)
                {
                    for (x = position.X - 70; x < position.X + 71; x++)
                    {
                        speedX += 5.5f; //Change X Velocity

                        if (speedX < 0f)
                            speedX -= z[Main.rand.Next(7)];

                        if (speedX > 0f)
                            speedX += z[Main.rand.Next(7)];

                        if (yCntr == 7) //Or 7
                        {
                            Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("SmallExplosiveProjectile"), 0, 5, Main.myPlayer, 0.0f, 0);

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

                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 268 / 2, position.Y - 268 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 268, 268, 218, 3.947369f, -6.052631f, 110, new Color(255, 25, 0), 3.618421f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(24, Main.LocalPlayer);

                }
                
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 5, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 5)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 168 / 2, position.Y - 152 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 168, 152, 176, 0f, -10f, 0, new Color(0, 255, 242), 3.421053f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 6, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 6)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 168 / 2, position.Y - 152 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 263, 263, 176, 0f, -1.315789f, 73, new Color(255, 201, 0), 5f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(76, Main.LocalPlayer);

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 7, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 7)
            {
                Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
                float x = 0;
                float y = 0;
                float speedX = -22f;
                float speedY = -22f;
                float[] z = { .1f, .2f, .3f, .4f, .5f, .6f, .7f, .8f };
                int yCntr = 1;
                int xCntr = 1;

                for (y = position.Y - 70; y < position.Y + 71; y++)
                {
                    for (x = position.X - 70; x < position.X + 71; x++)
                    {
                        speedX += 5.5f; //Change X Velocity

                        if (speedX < 0f)
                            speedX -= z[Main.rand.Next(7)];

                        if (speedX > 0f)
                            speedX += z[Main.rand.Next(7)];

                        if (yCntr == 7) //Or 7
                        {
                            Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("BulletBoomProjectile"), 0, 5, Main.myPlayer, 0.0f, 0);
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
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 263 / 2, position.Y - 263 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 263, 263, 155, 0f, -10f, 90, new Color(0, 255, 142), 3.618421f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(50, Main.LocalPlayer);
                    dust.fadeIn = 2.092105f;

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 8, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 8)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 263 / 2, position.Y - 263 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 263, 263, 156, -0.2631581f, -3.157895f, 0, new Color(0, 255, 142), 3.618421f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(24, Main.LocalPlayer);
                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 9, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 9)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 331 / 2, position.Y - 331 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 331, 331, 275, -0.2631581f, -1.052632f, 0, new Color(134, 255, 0), 5f)];
                    dust.noGravity = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 10, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 10)
            {
                int radius = 10;
                for (int x = -radius; x <= radius; x++)
                {
                    for (int y = -radius; y <= radius; y++)
                    {
                        int xPosition = (int)(x + position.X / 16.0f);
                        int yPosition = (int)(y + position.Y / 16.0f);

                        if (Math.Sqrt(x * x + y * y) <= radius + 0.5)   //this make so the explosion radius is a circle
                        {
                            Projectile.NewProjectile(position.X, position.Y , -10, Main.rand.Next(20) - 10, mod.ProjectileType("DynaglowmiteProjectile"), 0, 5, Main.myPlayer, 0.0f, 0);
                        }
                    }
                }

                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 326 / 2, position.Y - 326 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 326, 326, 63, -6.31579f, -7.631579f, 0, new Color(0, 192, 255), 5f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);

                }
                Projectile.NewProjectile(position.X, position.Y - 64, 0, 0, mod.ProjectileType("BigBouncyDynamiteProjectile"), 0, 5, Main.myPlayer, 0.0f, 0);
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 11, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 11)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 263 / 2, position.Y - 263 / 2);

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 12, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 12)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 263 / 2, position.Y - 263 / 2);

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 13, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 13)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 263 / 2, position.Y - 263 / 2);

                }
                Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("BunnyiteProjectile"), 0, 5, Main.myPlayer, 0.0f, 0);
                Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("HydromiteProjectile"), 0, 5, Main.myPlayer, 0.0f, 0);
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 14, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 14)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 263 / 2, position.Y - 263 / 2);

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 15, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 15)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 263 / 2, position.Y - 263 / 2);

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 16, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 16)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 263 / 2, position.Y - 263 / 2);

                }
                Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("DeliquidifierProjectile"), 0, 5, Main.myPlayer, 0.0f, 0);
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 17, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 17)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 263 / 2, position.Y - 263 / 2);

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 18, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 18)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 263 / 2, position.Y - 263 / 2);

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 19, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 19)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 263 / 2, position.Y - 263 / 2);

                }
                Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("ClusterBombProjectile"), 0, 5, Main.myPlayer, 0.0f, 0);
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 20, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 20)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 263 / 2, position.Y - 263 / 2);

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 21, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 21)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 263 / 2, position.Y - 263 / 2);

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 22, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 22)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 263 / 2, position.Y - 263 / 2);

                }
                Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("MegaExplosiveProjectile"), 0, 5, Main.myPlayer, 0.0f, 0);
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 23, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 23)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 263 / 2, position.Y - 263 / 2);

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 24, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 24)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 263 / 2, position.Y - 263 / 2);

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 25, Main.myPlayer, 0.0f, 0);
            }
            else if (projectile.knockBack == 25)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 263 / 2, position.Y - 263 / 2);

                }
                NPC.NewNPC((int)position.X, (int)position.Y + 20, mod.NPCType("CaptainExplosive"), 0, 0f, 0f, 0f, 0f, 255);
                //Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 12, Main.myPlayer, 0.0f, 0);
            }
            //else if (projectile.knockBack == 12)
            //{

            //    Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 13, Main.myPlayer, 0.0f, 0);
            //}
            //else if (projectile.knockBack == 13)
            //{

            //    Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 14, Main.myPlayer, 0.0f, 0);
            //}
            //else if (projectile.knockBack == 14)
            //{

            //    Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 15, Main.myPlayer, 0.0f, 0);
            //}
            //else if (projectile.knockBack == 15)
            //{

            //    Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 16, Main.myPlayer, 0.0f, 0);
            //}
            //else if (projectile.knockBack == 16)
            //{

            //    Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 17, Main.myPlayer, 0.0f, 0);
            //}
            //else if (projectile.knockBack == 17)
            //{

            //    Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 18, Main.myPlayer, 0.0f, 0);
            //}
            //else if (projectile.knockBack == 18)
            //{
            //    //NPC.NewNPC((int)position.X, (int)position.Y + 20, mod.NPCType("CaptainExplosive"), 0, 0f, 0f, 0f, 0f, 255);
            //    //Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 19, Main.myPlayer, 0.0f, 0);
            //}



        }


    }
}