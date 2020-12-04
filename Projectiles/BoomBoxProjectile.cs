using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace ExtraExplosives.Projectiles
{
    public class BoomBoxProjectile : ExplosiveProjectile
    {
        private bool setToTrue = false;
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "Gores/Explosives/basic-explosive_gore"; //TODO - needs gores

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boom Box");
        }

        public override void SafeSetDefaults()
        {
            projectile.tileCollide = true;
            projectile.width = 40;
            projectile.height = 28;
            projectile.aiStyle = 16;
            projectile.friendly = false;
            projectile.hostile = false;
            //projectile.penetrate = -1;
            projectile.timeLeft = 150;
            projectile.timeLeft = 10000000;
        }
        public override void AI()
        {
            if (!ExtraExplosives.boomBoxMusic && setToTrue)
            {
                projectile.active = false;
                //Create Bomb Sound
                Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

                //Create Bomb Dust
                CreateDust(projectile.Center, 10);

                //Create Bomb Gore
                Vector2 gVel1 = new Vector2(-1f, 0f);
                Vector2 gVel2 = new Vector2(0f, -1f);
                Gore.NewGore(projectile.position, gVel1.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "1"), projectile.scale);
                Gore.NewGore(projectile.position, gVel2.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "2"), projectile.scale);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (!setToTrue)
            {
                ExtraExplosives.boomBoxMusic = true;
                ExtraExplosives.randomMusicID = (Main.rand.Next(41) + 1);
                setToTrue = true;
            }
            return true;
        }

        private void CreateDust(Vector2 position, int amount)
        {
            Dust dust;
            Vector2 updatedPosition;

            for (int i = 0; i <= amount; i++)
            {
                if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
                {
                    //Dust 1
                    if (Main.rand.NextFloat() < 0.9f)
                    {
                        //updatedPosition = new Vector2(position.X, position.Y);

                        updatedPosition = projectile.Center;
                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 78, 78, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 4.539474f)];
                        if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.fadeIn = 2.5f;
                        }
                    }

                    //Dust 2
                    if (Main.rand.NextFloat() < 0.6f)
                    {
                        //updatedPosition = new Vector2(position.X - 78 / 2, position.Y - 78 / 2);

                        updatedPosition = projectile.Center;
                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 78, 78, 203, 0f, 0f, 0, new Color(255, 255, 255), 3.026316f)];
                        if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.noLight = true;
                        }
                    }

                    //Dust 3
                    if (Main.rand.NextFloat() < 0.3f)
                    {
                        //updatedPosition = new Vector2(position.X - 100 / 2, position.Y - 100 / 2);
                        updatedPosition = projectile.Center;
                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 100, 100, 31, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
                        if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.noLight = true;
                        }
                    }
                }
            }
        }
    }
}