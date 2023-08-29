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
        protected override string goreName => "n/a";
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

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            ManualExplode(SoundID.Item14);

            return base.OnTileCollide(oldVelocity);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            HitSomeThing = true;

            ManualExplode(SoundID.Item14);
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];

            if (Main.rand.NextFloat() < .2f && HitSomeThing == false)
            {
                //Create Bomb Damage
                if (!player.EE().BlastShielding &&
                    !player.EE().BlastShieldingActive)
                {
                    player.Hurt(PlayerDeathReason.ByProjectile(player.whoAmI, Projectile.whoAmI), (int)(Projectile.damage * (crit ? 1.5 : 1)), Projectile.direction);
                }

                SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

                //Create Bomb Dust
                DustEffects();
            }
        }
    }
}