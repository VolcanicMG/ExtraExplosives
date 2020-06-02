using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class TheLevelerProjectile : ModProjectile
    {
        Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

        internal static bool CanBreakWalls;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Leveler");
            //Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 10;   //This defines the hitbox width
            projectile.height = 10;    //This defines the hitbox height
            projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 120; //The amount of time the projectile is alive for
            projectile.damage = 0;

            drawOffsetX = -15;
            drawOriginOffsetY = -15;
        }

        public override void AI()
        {
            projectile.rotation = 0;
        }

        public override bool OnTileCollide(Vector2 old)
        {

            projectile.Kill();
            return true;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

            //Create Bomb Dust
            CreateDust(projectile.Center, 700);

            //Create Bomb Damage
            //ExplosionDamage(20f * 2f, projectile.Center, 450, 40, projectile.owner);

            //Create Bomb Explosion
            CreateExplosion(projectile.Center, 20);


        }

        private void CreateExplosion(Vector2 position, int radius)
        {
            int x = 0;
            int y = 0;

            int width = 100; //Explosion Width
            int height = 20; //Explosion Height

            for (y = height - 1; y >= 0; y--)
            {
                for (x = -width; x < width; x++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(-y + position.Y / 16.0f);

                    if (WorldGen.InWorld(xPosition, yPosition)) //Circle
                    {
                        if (CheckForUnbreakableTiles(Main.tile[xPosition, yPosition].type)) //Unbreakable
                        {
                            if (Main.tile[xPosition, yPosition].type == TileID.DesertFossil)
                            {
                                WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this makes the explosion destroy tiles  
                            }
                        }
                        else //Breakable
                        {
                            WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this makes the explosion destroy tiles
                            if (CanBreakWalls)
                            {
                                WorldGen.KillWall(xPosition, yPosition, false);
                                WorldGen.KillWall(xPosition + 1, yPosition + 1, false); //get the last bit
                            }
                          
                        }
                    }
                }
                width++; //Increments width to make stairs on each end
            }
        }

        private void CreateDust(Vector2 position, int amount)
        {
            //Vector2 updatedPosition;

            for (int i = 0; i <= amount; i++)
            {
                if (Main.rand.NextFloat() < DustAmount)
                {
                    //---Dust 1---
                    if (Main.rand.NextFloat() < 0.3f)
                    {
                        Dust dust1;
                        Dust dust2;

                        Vector2 position1 = new Vector2(position.X - 2000 / 2, position.Y - 320);
                        dust1 = Main.dust[Terraria.Dust.NewDust(position1, 2000, 320, 0, 0f, 0f, 171, new Color(33, 0, 255), 5.0f)];
                        dust1.noGravity = true;
                        dust1.noLight = true;
                        dust1.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);

                        Vector2 position2 = new Vector2(position.X - 2000 / 2, position.Y - 320);
                        dust2 = Main.dust[Terraria.Dust.NewDust(position2, 2000, 320, 148, 0f, 0.2631581f, 120, new Color(255, 226, 0), 2.039474f)];
                        dust2.noGravity = true;
                        dust2.noLight = true;
                        dust2.shader = GameShaders.Armor.GetSecondaryShader(111, Main.LocalPlayer);
                        dust2.fadeIn = 3f;
                    }
                    //------------
                }
            }
        }

    }
}