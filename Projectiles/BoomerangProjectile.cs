using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class BoomerangProjectile : ExplosiveProjectile      //TODO Recode so it works with the Bombedier class
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "n/a";
        private bool HitSomeThing;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("BOOMerang");
        }

        public override void DangerousSetDefaults()
        {
            radius = 5;
            Projectile.CloneDefaults(ProjectileID.EnchantedBoomerang);
            Projectile.damage = 46;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.minion = false;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            AIType = ProjectileID.EnchantedBoomerang;
        }

        //public override bool OnTileCollide(Vector2 oldVelocity)
        //{
        //	projectile.Kill();
        //	return base.OnTileCollide(oldVelocity);
        //}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            HitSomeThing = true;

            //Create Bomb Sound
            //SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);


            //Create Bomb Dust
            DustEffects();

            ExplosionDamage();

            //projectile.Kill();
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];

            if (Main.rand.NextFloat() < .2f && HitSomeThing == false)
            {
                //Create Bomb Sound
                //SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

                //Create Bomb Damage
                if (!player.EE().BlastShielding &&
                    !player.EE().BlastShieldingActive)
                {
                    player.Hurt(PlayerDeathReason.ByProjectile(player.whoAmI, Projectile.whoAmI), (int)(Projectile.damage * (crit ? 1.5 : 1)), Projectile.direction);
                }

                //Create Bomb Dust
                DustEffects();

                Projectile.Kill();
            }
        }

        //private void CreateDust(Vector2 position, int amount)
        //{
        //    Dust dust;
        //    Vector2 updatedPosition;

        //    for (int i = 0; i <= amount; i++)
        //    {
        //        if (Main.rand.NextFloat() < DustAmount)
        //        {
        //            //---Dust 1---
        //            if (Main.rand.NextFloat() < 1f)
        //            {
        //                updatedPosition = new Vector2(position.X - radius * 8, position.Y - radius * 8);

        //                dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, radius * 16, radius * 16, 6, 0f, 0.5f, 0, new Color(255, 0, 0), 4f)];
        //                if (Vector2.Distance(dust.position, projectile.Center) > radius * 8) dust.active = false;
        //                else
        //                {
        //                    dust.noGravity = true;
        //                    dust.fadeIn = 0f;
        //                    dust.noLight = true;
        //                }
        //            }
        //            //------------
        //        }
        //    }
        //}
    }
}