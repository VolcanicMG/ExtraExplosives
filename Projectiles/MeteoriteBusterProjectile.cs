using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class MeteoriteBusterProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "Gores/Explosives/meteorite-buster_gore";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("MeteoriteBuster");
        }

        public override void SafeSetDefaults()
        {
            pickPower = 50;
            radius = 30;
            projectile.tileCollide = true;
            projectile.width = 28;
            projectile.height = 30;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 400;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

            Explosion();

            //Create Bomb Dust
            CreateDust(projectile.Center, 600);

            ExplosionDamage();
            //Create Bomb Damage
            //ExplosionDamage(30f * 20f, projectile.Center, 450, 40, projectile.owner);

            //Create Bomb Explosion
            //CreateExplosion(projectile.Center, 30);

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(2.0f, -2.0f);
            Vector2 gVel2 = new Vector2(-2.0f, 2.0f);
            Gore.NewGore(projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "1"), projectile.scale);
            Gore.NewGore(projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "2"), projectile.scale);
        }

        public override void Explosion()
        {
            Vector2 position = projectile.Center;
            for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left
            {
                for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Circle
                    {
                        ushort tile = Main.tile[xPosition, yPosition].type;
                        if (!CanBreakTile(tile, pickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
                        {
                            if (Main.tile[xPosition, yPosition].type == TileID.Meteorite)
                            {
                                WorldGen.KillTile(xPosition, yPosition, false, false, false);  //This make the explosion destroy tiles
                            }
                        }
                        else //Breakable
                        {
                            if (Main.tile[xPosition, yPosition].type == TileID.Meteorite)
                            {
                                WorldGen.KillTile(xPosition, yPosition, false, false, false);  //This make the explosion destroy tiles
                            }
                        }
                    }
                }
            }
        }

        private void CreateDust(Vector2 position, int amount)
        {
            Dust dust;
            Vector2 updatedPosition;

            for (int i = 0; i <= amount; i++)
            {
                if (Main.rand.NextFloat() < DustAmount)
                {
                    //---Dust 1---
                    if (Main.rand.NextFloat() < 0.2f)
                    {
                        updatedPosition = new Vector2(position.X - 550 / 2, position.Y - 550 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 550, 550, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 10f)];
                        if (Vector2.Distance(dust.position, projectile.Center) > 275) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.fadeIn = 2.486842f;
                        }
                    }
                    //------------

                    //---Dust 2---
                    if (Main.rand.NextFloat() < 0.2f)
                    {
                        updatedPosition = new Vector2(position.X - 550 / 2, position.Y - 550 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 550, 550, 203, 0f, 0f, 0, new Color(255, 255, 255), 10f)];
                        if (Vector2.Distance(dust.position, projectile.Center) > 275) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.noLight = true;
                        }
                    }
                    //------------

                    //---Dust 3---
                    if (Main.rand.NextFloat() < 0.2f)
                    {
                        updatedPosition = new Vector2(position.X - 550 / 2, position.Y - 550 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 550, 550, 31, 0f, 0f, 0, new Color(255, 255, 255), 10f)];
                        if (Vector2.Distance(dust.position, projectile.Center) > 275) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.noLight = true;
                        }
                    }
                    //------------
                }
            }
        }
    }
}