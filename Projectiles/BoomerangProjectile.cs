using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class BoomerangProjectile : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BOOMerang");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.EnchantedBoomerang);
            projectile.damage = 50;
            projectile.damage = 46;
            projectile.friendly = true;
            aiType = ProjectileID.EnchantedBoomerang;
        }

        //public override bool OnTileCollide(Vector2 oldVelocity)
        //{
        //    projectile.Kill();
        //    return base.OnTileCollide(oldVelocity);
        //}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.Kill();
        }


        public override void Kill(int timeLeft)
        {

            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

            //Create Bomb Damage
            ExplosionDamage(3f, projectile.Center, 50, 20, projectile.owner);
            ExplosionDamage(3f, projectile.Center, 46, 20, projectile.owner);

            //Create Bomb Dust
            CreateDust(projectile.Center, 10);

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
                    if (Main.rand.NextFloat() < 1f)
                    {
                        updatedPosition = new Vector2(position.X - 100 / 2, position.Y - 100 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 100, 100, 6, 0f, 0.5f, 0, new Color(255, 0, 0), 4f)];
                        dust.noGravity = true;
                        dust.fadeIn = 0f;
                        dust.noLight = true;
                    }
                    //------------
                }
            }
        }
    }
}


