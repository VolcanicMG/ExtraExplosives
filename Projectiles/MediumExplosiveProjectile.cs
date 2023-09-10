using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class MediumExplosiveProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreName => "basic-explosive_gore";

        public override void SafeSetDefaults()
        {
            pickPower = 45;
            radius = 10;
            Projectile.tileCollide = true;
            Projectile.width = 32;
            Projectile.height = 30;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            //Create Bomb Dust

            ExplosionTileDamage();
            ExplosionEntityDamage();

            DustEffects();

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(-1.5f, 0f);
            Vector2 gVel2 = new Vector2(0f, -1.5f);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}1").Type, Projectile.scale * 1.25f);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}2").Type, Projectile.scale * 1.25f);
        }
    }
}