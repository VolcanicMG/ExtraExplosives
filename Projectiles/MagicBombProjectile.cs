using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;
using Terraria.ModLoader;
using System;

namespace ExtraExplosives.Projectiles
{
    public class MagicBombProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreName => "magic_gore";

        public override void SafeSetDefaults()
        {
            Projectile.CloneDefaults(29);
            pickPower = 0;
            Projectile.timeLeft = 200;
        }

        public override void Kill(int timeLeft)
        {
            //Set the radius
            radius = (explosionDamage <= 100) ? 5 : (explosionDamage / 50) + 5;

            //Create Bomb Sound
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            //Create Bomb Damage
            ExplosionEntityDamage();

            //Create Bomb Explosion
            ExplosionTileDamage();

            //Create Bomb Dust
            DustEffects();

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(1f, 0f);
            Vector2 gVel2 = new Vector2(1f, 1f);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}1").Type, Projectile.scale);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}2").Type, Projectile.scale);
        }
    }
}