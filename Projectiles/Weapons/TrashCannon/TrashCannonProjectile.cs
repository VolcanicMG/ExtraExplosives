using Microsoft.Xna.Framework;
using Terraria;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles.Weapons.TrashCannon
{
    //TODO Add 1-4 varients
    public class TrashCannonProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc { get; } = "n/a";    // TBD
        protected override string goreFileLoc { get; } = "n/a";         // TBD

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Garbage");    // Name used in death messages
            Main.projFrames[projectile.type] = 12;
        }

        public override void SafeSetDefaults()
        {
            radius = 2;
            pickPower = -2;    // No blocks broken
            projectile.height = 11;
            projectile.width = 11;
            projectile.aiStyle = 16;
            projectile.timeLeft = 300;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.frame = Main.rand.Next(13);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return true;
        }

        public override void Kill(int timeLeft)
        {
            Explosion();
            ExplosionDamage();
            CreateDust(projectile.Center, 25);
            base.Kill(timeLeft);
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
                    if (Main.rand.NextFloat() < 0.5f)
                    {
                        updatedPosition = new Vector2(position.X - 55 / 2, position.Y - 55 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 55, 55, 6, 0f, 0.5263162f, 0, new Color(255, 255, 255), 2)];
                        if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                        }
                    }
                    //------------

                    //---Dust 2---
                    if (Main.rand.NextFloat() < 0.5f)
                    {
                        updatedPosition = new Vector2(position.X - 55 / 2, position.Y - 55 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 55, 55, 203, 0f, 0f, 0, new Color(255, 255, 255), 2)];
                        if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                        }
                    }
                    //------------

                    //---Dust 3---
                    if (Main.rand.NextFloat() < 0.5f)
                    {
                        updatedPosition = new Vector2(position.X - 55 / 2, position.Y - 55 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 55, 55, 31, 0f, 0f, 0, new Color(255, 255, 255), 2)];
                        if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                        }
                    }

                    //------------
                }
            }
        }
    }
}