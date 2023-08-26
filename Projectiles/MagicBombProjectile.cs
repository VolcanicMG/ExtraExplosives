using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class MagicBombProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "Gores/Explosives/magic_gore";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Magic Bomb");
        }

        public override void SafeSetDefaults()
        {
            Projectile.CloneDefaults(29);
            pickPower = 0;
            radius = 5;
        }
        public override void Kill(int timeLeft)
        {
            Mod.Logger.DebugFormat("Damage {0}", Projectile.damage);
            //Create Bomb Sound
            //SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            //Create Bomb Damage
            ExplosionDamage();

            //Create Bomb Explosion
            Explosion();

            //Create Bomb Dust
            DustEffects();

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(1f, 0f);
            Vector2 gVel2 = new Vector2(1f, 1f);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "1").Type, Projectile.scale);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "2").Type, Projectile.scale);
        }
    }
}