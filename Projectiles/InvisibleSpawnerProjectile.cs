using ExtraExplosives.NPCs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

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

        //public override void PostAI()
        //{
        //    DrawDusts();
        //    base.PostAI();
        //}

        //private void DrawDusts()
        //{
        //    Vector2 position = projectile.Center;
        //    Vector2 position1 = new Vector2(position.X - 100 / 2, position.Y - 100 / 2);
        //    int num95 = Dust.NewDust(position1, 100, 100, 66, 0f, -10f, 150, Color.Transparent, 0.85f);
        //    Main.dust[num95].color = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.5f);
        //    Main.dust[num95].noGravity = true;
        //    Dust obj17 = Main.dust[num95];
        //    obj17.velocity /= 2f;
        //}

        public override void SetDefaults()
        {
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 22;   //This defines the hitbox width
            projectile.height = 22;    //This defines the hitbox height
            projectile.aiStyle = 0; //16  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 100; //The amsadount of time the projectile is alive for
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
                float xVel = 10; //Use as X Velocity
                float yVel = 10; //Use as Y Velocity
                for (int q = 0; q < 50; q++)
                {
                    for (int i = 0; i < 80; i++) //This loop spawns Dust at a give point and shoots it in a circle
                    {
                        if (i > -1 && i < 10) { xVel--; }
                        if (i == 9) { xVel = 0; }
                        if (i > 9 && i < 20) { xVel--; }
                        if (i == 19) { xVel = -10; }
                        if (i > 19 && i < 30) { yVel--; }
                        if (i == 29) { yVel = 0; }
                        if (i > 29 && i < 40) { yVel--; }
                        if (i == 39) { yVel = -10; }
                        if (i > 39 && i < 50) { xVel++; }
                        if (i == 49) { xVel = 0; }
                        if (i > 49 && i < 60) { xVel++; }
                        if (i == 59) { xVel = 10; }
                        if (i > 59 && i < 70) { yVel++; }
                        if (i == 69) { yVel = 0; }
                        if (i > 69 && i < 80) { yVel++; }
                        if (i == 79) { yVel = 10; }

                        //- - - - - - - -
                        //Spawn Dust Here
                        Vector2 position2 = new Vector2(position.X - 47 / 2, position.Y - 47 / 2);
                        dust = Main.dust[Terraria.Dust.NewDust(position2, 47, 47, 28, xVel, yVel, 0, new Color(255, 0, 0), 1.842105f)];
                        dust.shader = GameShaders.Armor.GetSecondaryShader(88, Main.LocalPlayer);
                        dust.noLight = false;
                        //- - - - - - - -

                    }
                }

                Vector2 position1 = new Vector2(position.X - 100 / 2, position.Y - 100 / 2);
                Main.NewText("3!");
                for (int i = 0; i < 100; i++)
                {
                    dust1 = Main.dust[Terraria.Dust.NewDust(position1, 100, 100, 0, 0f, 0f, 171, new Color(33, 0, 255), 4.0f)];
                    dust1.noGravity = true;
                    dust1.noLight = true;
                    dust1.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);
                    Dust.NewDust(position1, 100, 100, DustID.Vortex, 0.0f, 0.0f, 120, new Color(), 2f);  //this is the dust that will spawn after the explosion
                    Dust.NewDust(position1, 100, 100, DustID.Fireworks, 0.0f, 0.0f, 120, new Color(), 1f);  //this is the dust that will spawn after the explosion
                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 2, projectile.owner, 0.0f, 0);
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
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 3, projectile.owner, 0.0f, 0);
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
                    dust1.noLight = true;
                }
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 121 / 2, position.Y - 121 / 2);
                    dust1 = Main.dust[Terraria.Dust.NewDust(position1, 121, 121, 274, 0f, 0.7894742f, 0, new Color(255, 0, 226), 3.815789f)];
                    dust1.shader = GameShaders.Armor.GetSecondaryShader(77, Main.LocalPlayer);
                    dust1.noLight = true;

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("MegaExplosiveProjectile"), 0, 0, projectile.owner, 0.0f, 0);
                Main.NewText("[c/FF0000: Stand Back!]");
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 4, projectile.owner, 0.0f, 0);
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
                            Projectile.NewProjectile(x, y, speedX, speedY, mod.ProjectileType("SmallExplosiveProjectile"), 0, 5, projectile.owner, 0.0f, 0);

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

                float xVel = 10; //Use as X Velocity
                float yVel = 10; //Use as Y Velocity
                for (int q = 0; q < 50; q++)
                {
                    for (int i = 0; i < 80; i++) //This loop spawns Dust at a give point and shoots it in a circle
                    {
                        if (i > -1 && i < 10) { xVel--; }
                        if (i == 9) { xVel = 0; }
                        if (i > 9 && i < 20) { xVel--; }
                        if (i == 19) { xVel = -10; }
                        if (i > 19 && i < 30) { yVel--; }
                        if (i == 29) { yVel = 0; }
                        if (i > 29 && i < 40) { yVel--; }
                        if (i == 39) { yVel = -10; }
                        if (i > 39 && i < 50) { xVel++; }
                        if (i == 49) { xVel = 0; }
                        if (i > 49 && i < 60) { xVel++; }
                        if (i == 59) { xVel = 10; }
                        if (i > 59 && i < 70) { yVel++; }
                        if (i == 69) { yVel = 0; }
                        if (i > 69 && i < 80) { yVel++; }
                        if (i == 79) { yVel = 10; }

                        //- - - - - - - -
                        //Spawn Dust Here
                        Vector2 position1 = new Vector2(position.X - 47 / 2, position.Y - 47 / 2);
                        dust = Main.dust[Terraria.Dust.NewDust(position1, 47, 47, 28, xVel, yVel, 0, new Color(0, 242, 255), 1.842105f)];
                        dust.shader = GameShaders.Armor.GetSecondaryShader(88, Main.LocalPlayer);
                        dust.noLight = false;
                        //- - - - - - - -

                    }
                }
                //for (int i = 0; i < 50; i++)
                //{
                //    Vector2 position1 = new Vector2(position.X - 268 / 2, position.Y - 268 / 2);
                //    dust = Main.dust[Terraria.Dust.NewDust(position1, 268, 268, 218, 3.947369f, -6.052631f, 110, new Color(255, 25, 0), 3.618421f)];
                //    dust.shader = GameShaders.Armor.GetSecondaryShader(24, Main.LocalPlayer);

                //    dust = Main.dust[Terraria.Dust.NewDust(position1, 268, 268, 262, 0f, -7.368421f, 0, new Color(255, 176, 0), 3.355263f)];
                //    dust.noGravity = true;

                //}

                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 5, projectile.owner, 0.0f, 0);
            }
            else if (projectile.knockBack == 5)
            {
                for (int i = 0; i < 100; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 168 / 2, position.Y - 152 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 168, 152, 176, 0f, -10f, 0, new Color(0, 255, 242), 3.421053f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);

                    dust = Main.dust[Terraria.Dust.NewDust(position1, 205, 205, 132, 0f, 10f, 0, new Color(0, 142, 255), 3.355263f)];
                    dust.noLight = true;

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 6, projectile.owner, 0.0f, 0);
            }
            else if (projectile.knockBack == 6)
            {
                for (int i = 0; i < 100; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 168 / 2, position.Y - 152 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 263, 263, 176, 0f, -1.315789f, 73, new Color(255, 201, 0), 5f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(76, Main.LocalPlayer);


                    dust = Main.dust[Terraria.Dust.NewDust(position1, 263, 263, 29, 0f, 0f, 0, new Color(84, 0, 255), 4.144737f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(103, Main.LocalPlayer);

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 7, projectile.owner, 0.0f, 0);
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
                            Projectile.NewProjectile(x, y, speedX, speedY, mod.ProjectileType("BulletBoomProjectile"), 0, 5, projectile.owner, 0.0f, 0);
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

                    dust = Main.dust[Terraria.Dust.NewDust(position1, 263, 263, 15, 0f, 0f, 0, new Color(109, 255, 0), 4.144737f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);


                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 8, projectile.owner, 0.0f, 0);
            }
            else if (projectile.knockBack == 8)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 263 / 2, position.Y - 263 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 263, 263, 156, -0.2631581f, -3.157895f, 0, new Color(0, 255, 142), 3.618421f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(24, Main.LocalPlayer);
                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 9, projectile.owner, 0.0f, 0);
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
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 10, projectile.owner, 0.0f, 0);
            }
            else if (projectile.knockBack == 10)
            {
                int radius = 1; //2
                for (int x = -radius; x <= radius; x++)
                {
                    for (int y = -radius; y <= radius; y++)
                    {
                        int xPosition = (int)(x + position.X / 16.0f);
                        int yPosition = (int)(y + position.Y / 16.0f);

                        if (Math.Sqrt(x * x + y * y) <= radius + 0.5)   //this make so the explosion radius is a circle
                        {
                            Projectile.NewProjectile(position.X, position.Y, 0, Main.rand.Next(20) - 10, mod.ProjectileType("DynaglowmiteProjectile"), 0, 5, projectile.owner, 0.0f, 0);
                            Vector2 position1 = new Vector2(position.X - 400 / 2, position.Y - 163 / 2);
                            dust = Main.dust[Terraria.Dust.NewDust(position1, 400, 163, 226, 0f, -10f, 0, new Color(8, 0, 255), 0.5263158f)];
                            dust.shader = GameShaders.Armor.GetSecondaryShader(62, Main.LocalPlayer);
                            dust.fadeIn = 3f;

                        }
                    }
                }

                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 326 / 2, position.Y - 326 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 326, 326, 63, -6.31579f, -7.631579f, 0, new Color(0, 192, 255), 5f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);

                    dust = Main.dust[Terraria.Dust.NewDust(position1, 326, 326, 231, 0f, 0.2631581f, 0, new Color(255, 255, 0), 3.157895f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(106, Main.LocalPlayer);

                }
                Main.NewText("[c/FF0000: BOOM BOOM MUAHHAAHAHAH!]");
                Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("BigBouncyDynamiteProjectile"), 0, 5, projectile.owner, 0.0f, 0);
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 11, projectile.owner, 0.0f, 0);
            }
            else if (projectile.knockBack == 11)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 163 / 2, position.Y - 218 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 157, 163, 218, 0f, -10f, 0, new Color(255, 255, 255), 3.618421f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(54, Main.LocalPlayer);
                    dust.fadeIn = 3f;

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 12, projectile.owner, 0.0f, 0);
            }
            else if (projectile.knockBack == 12)
            {
                for (int i = 0; i < 70; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 157 / 2, position.Y - 163 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 157, 163, 219, 0f, -10f, 0, new Color(255, 0, 0), 0.5263158f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(54, Main.LocalPlayer);
                    dust.fadeIn = 3f;

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 13, projectile.owner, 0.0f, 0);
            }
            else if (projectile.knockBack == 13)
            {

                Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("BunnyiteProjectile"), 0, 5, projectile.owner, 0.0f, 0);
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("HydromiteProjectile"), 0, 5, projectile.owner, 0.0f, 0);
                for (int i = 0; i < 70; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 400 / 2, position.Y - 163 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 400, 163, 220, 0f, -10f, 0, new Color(33, 0, 255), 0.5263158f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(54, Main.LocalPlayer);
                    dust.fadeIn = 3f;
                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 14, projectile.owner, 0.0f, 0);
            }
            else if (projectile.knockBack == 14)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 400 / 2, position.Y - 247 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 400, 247, 226, 0f, 0.2631581f, 0, new Color(0, 217, 255), 5f)];
                    dust.noGravity = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(94, Main.LocalPlayer);
                    dust.fadeIn = 0.7105263f;

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 15, projectile.owner, 0.0f, 0);
            }
            else if (projectile.knockBack == 15)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 205 / 2, position.Y - 173 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 205, 173, 61, 0f, 0.2631581f, 0, new Color(255, 125, 0), 4.276316f)];
                    dust.noGravity = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(27, Main.LocalPlayer);

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 16, projectile.owner, 0.0f, 0);
            }
            else if (projectile.knockBack == 16)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 231 / 2, position.Y - 247 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 231, 247, 230, 0f, 0.2631581f, 0, new Color(209, 0, 255), 5f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(106, Main.LocalPlayer);

                }
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
                            Projectile.NewProjectile(x, y, speedX, speedY, mod.ProjectileType("BulletBoomProjectile"), 0, 5, projectile.owner, 0.0f, 0);
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
                Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("DeliquidifierProjectile"), 0, 5, projectile.owner, 0.0f, 0);
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 17, projectile.owner, 0.0f, 0);
            }
            else if (projectile.knockBack == 17)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 336 / 2, position.Y - 336 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 336, 336, 49, 0f, 0.2631581f, 174, new Color(0, 217, 255), 3.157895f)];
                    dust.noGravity = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(18, Main.LocalPlayer);

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 18, projectile.owner, 0.0f, 0);
            }
            else if (projectile.knockBack == 18)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 263 / 2, position.Y - 263 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 263, 263, 56, 0f, 10f, 174, new Color(255, 0, 0), 3.157895f)];
                    dust.noGravity = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(67, Main.LocalPlayer);

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 19, projectile.owner, 0.0f, 0);
            }
            else if (projectile.knockBack == 19)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 336 / 2, position.Y - 326 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 336, 326, 234, 0f, -5.263158f, 0, new Color(209, 255, 0), 3.157895f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(108, Main.LocalPlayer);

                }
                Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
                Projectile.NewProjectile(position.X - 16, position.Y - 32, 0, -7, mod.ProjectileType("ClusterBombProjectile"), 0, 5, projectile.owner, 0.0f, 0);
                Projectile.NewProjectile(position.X + 16, position.Y - 32, 0, -7, mod.ProjectileType("ClusterBombProjectile"), 0, 5, projectile.owner, 0.0f, 0);
                Main.NewText("[c/FF0000: Watch out, HAHAHAHAH]");
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 20, projectile.owner, 0.0f, 0);
                if (CaptainExplosive.CaptianIsDed == true)
                {
                    Main.NewText("[c/FF0000: He's coming...]");
                }
            }
            else if (projectile.knockBack == 20)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 205 / 2, position.Y - 173 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 205, 173, 65, 6.842105f, 0.2631581f, 0, new Color(255, 176, 0), 4.276316f)];
                    dust.noGravity = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(91, Main.LocalPlayer);

                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 21, projectile.owner, 0.0f, 0);
            }
            else if (projectile.knockBack == 21)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 205 / 2, position.Y - 173 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 205, 173, 68, -0.2631581f, 0.2631581f, 140, new Color(255, 0, 0), 4.276316f)];
                    dust.noGravity = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(21, Main.LocalPlayer);

                    Vector2 position2 = new Vector2(position.X - 400 / 2, position.Y - 400 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position2, 400, 400, 77, -0.2631581f, -10f, 0, new Color(84, 0, 255), 5f)];
                    dust.noGravity = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(85, Main.LocalPlayer);
                    dust.fadeIn = 3f;
                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 22, projectile.owner, 0.0f, 0);
            }
            else if (projectile.knockBack == 22)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 205 / 2, position.Y - 205 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 205, 205, 75, -0.2631581f, 0.2631581f, 140, new Color(255, 251, 0), 5f)];
                    dust.noGravity = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(50, Main.LocalPlayer);
                    dust.fadeIn = 2.289474f;

                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    dust = Terraria.Dust.NewDustDirect(position1, 205, 205, 132, 0f, 10f, 0, new Color(255, 176, 0), 3.355263f);

                }
                Projectile.NewProjectile(position.X, position.Y, 0, -5, mod.ProjectileType("GiganticExplosiveProjectile"), 0, 5, projectile.owner, 0.0f, 0);
                Main.NewText("[c/FF0000: RUN!!!]");
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 23, projectile.owner, 0.0f, 0);
            }
            else if (projectile.knockBack == 23)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 300 / 2, position.Y - 300 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 300, 300, 76, -0.2631581f, 0.2631581f, 0, new Color(209, 255, 0), 4.539474f)];
                    dust.noGravity = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(76, Main.LocalPlayer);

                    dust = Main.dust[Terraria.Dust.NewDust(position1, 300, 300, 217, 0f, 0f, 0, new Color(84, 0, 255), 5f)];
                    dust.noGravity = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);
                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 24, projectile.owner, 0.0f, 0);
            }
            else if (projectile.knockBack == 24)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 205 / 205, position.Y - 263 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position, 205, 205, 77, -0.2631581f, -0.2631581f, 0, new Color(234, 255, 0), 4.539474f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(82, Main.LocalPlayer);

                    dust = Main.dust[Terraria.Dust.NewDust(position1, 205, 205, 176, 0f, 0f, 0, new Color(0, 255, 117), 5f)];
                    dust.noGravity = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(114, Main.LocalPlayer);
                }
                Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 25, projectile.owner, 0.0f, 0);
            }
            else if (projectile.knockBack == 25)
            {
                for (int i = 0; i < 100; i++)
                {
                    Vector2 position1 = new Vector2(position.X - 263 / 2, position.Y - 263 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position, 263, 263, 80, -0.2631581f, -7.631579f, 0, new Color(234, 255, 0), 4.539474f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(112, Main.LocalPlayer);

                    Dust.NewDust(position1, 263, 263, DustID.Confetti, 0.0f, 0.0f, 120, new Color(), 1f);  //this is the dust that will spawn after the explosion

                    dust = Main.dust[Terraria.Dust.NewDust(position1, 263, 263, 148, 0f, 0f, 0, new Color(0, 255, 117), 5f)];
                    dust.noGravity = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(114, Main.LocalPlayer);
                }

                if (CaptainExplosive.CaptianIsDed == true)
                {
                    NPC.NewNPC((int)position.X, (int)position.Y + 20, mod.NPCType("CaptainExplosive"), 0, 0f, 0f, 0f, 0f, 255);
                    //Projectile.NewProjectile(position.X, position.Y - 16, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 12, Main.myPlayer, 0.0f, 0);
                    CaptainExplosive.CaptianIsDed = false;
                }
                else
                {
                    Main.NewText("Captain Explosive: Hey! I'm already here. If you want something, just come ask. You dont have to keep blowing up your world...");
                }
            }




        }
    }
}



//dust = Main.dust[Terraria.Dust.NewDust(position, 400, 400, 43, 0.2631581f, -10f, 0, new Color(255, 255, 255), 5f)];
//	dust.noGravity = true;
//	dust.shader = GameShaders.Armor.GetSecondaryShader(53, Main.LocalPlayer);
//	dust.fadeIn = 3f;


//dust = Main.dust[Terraria.Dust.NewDust(position, 30, 30, 43, 0f, -4.210526f, 197, new Color(255, 255, 255), 3.289474f)];
//	dust.fadeIn = 3f;


//int num95 = Dust.NewDust(rectangle.TopLeft(), rectangle.Width, rectangle.Height, 66, 0f, 0f, 150, Color.Transparent, 0.85f);
//Main.dust[num95].color = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.5f);
//						Main.dust[num95].noGravity = true;
//						Dust obj17 = Main.dust[num95];
//obj17.velocity /= 2f;