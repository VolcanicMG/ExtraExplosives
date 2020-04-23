using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using ExtraExplosives.UI;

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
                            //if (++cntr <= 100) Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, pid, 75, 20, projectile.owner, 0.0f, 0);
                            if (++cntr <= 100)
                            {
                                if (projectile.knockBack == 1) //Musket Ball
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 14, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 2) //Meteor Shot
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 36, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 3) //Silver Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 14, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 4) //Crystal Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 89, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 5) //Cursed Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 104, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 6) //Chlorophyte Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 207, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 7) //High Velocity Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 242, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 8) //Ichor Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 279, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 9) //Venom Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 283, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 10) //Party Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 284, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 11) //Nano Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 285, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 12) //Exploding Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 286, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 13) //Golden Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 287, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 14) //Luminite Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 638, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else  //Defaults To Musket Ball
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 14, 75, 20, projectile.owner, 0.0f, 0);
                                }
                            }
                        }
                        else
                        {

                            spedX = Main.rand.Next(15) - 7;
                            spedY = Main.rand.Next(15) - 7;
                            if (spedX == 0) spedX = 1;
                            if (spedY == 0) spedY = 1;
                            if (++cntr <= 100)
                            {
                                if (projectile.knockBack == 1) //Musket Ball
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 14, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 2) //Meteor Shot
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 36, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 3) //Silver Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 14, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 4) //Crystal Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 89, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 5) //Cursed Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 104, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 6) //Chlorophyte Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 207, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 7) //High Velocity Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 242, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 8) //Ichor Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 279, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 9) //Venom Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 283, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 10) //Party Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 284, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 11) //Nano Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 285, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 12) //Exploding Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 286, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 13) //Golden Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 287, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else if (projectile.knockBack == 14) //Luminite Bullet
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 638, 75, 20, projectile.owner, 0.0f, 0);
                                }
                                else  //Defaults To Musket Ball
                                {
                                    Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, 14, 75, 20, projectile.owner, 0.0f, 0);
                                }
                            }
                        }

                    }
                }
            }

            Dust dust1;
            Dust dust2;
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            for (int i = 0; i < 100; i++)
            {
                if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
                {
                    Vector2 position1 = new Vector2(position.X - 100 / 2, position.Y - 100 / 2);
                    dust1 = Main.dust[Terraria.Dust.NewDust(position1, 100, 100, 0, 0f, 0f, 171, new Color(33, 0, 255), 4.0f)];
                    dust1.noGravity = true;
                    dust1.noLight = true;
                    dust1.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);
                }

            }

            for (int i = 0; i < 100; i++)
            {
                if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
                {
                    Vector2 position2 = new Vector2(position.X - 80 / 2, position.Y - 80 / 2);
                    dust2 = Main.dust[Terraria.Dust.NewDust(position2, 80, 80, 6/*35*/, 0f, 0f, 0, new Color(240, 240, 240), 4.0f)];
                    dust2.noGravity = true;
                }
            }
        }
    }
}