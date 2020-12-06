using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class MagicBombProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "Gores/Explosives/magic_gore";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magic Bomb");
        }

        public override void SafeSetDefaults()
        {
            projectile.CloneDefaults(29);
            pickPower = 0;
            radius = 5;
        }
        public override void Kill(int timeLeft)
        {
            mod.Logger.DebugFormat("Damage {0}", projectile.damage);
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

            //Create Bomb Damage
            ExplosionDamage();

            //Create Bomb Explosion
            Explosion();

            //Create Bomb Dust
            DustEffects();

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(1f, 0f);
            Vector2 gVel2 = new Vector2(1f, 1f);
            Gore.NewGore(projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "1"), projectile.scale);
            Gore.NewGore(projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "2"), projectile.scale);
        }
    }
}