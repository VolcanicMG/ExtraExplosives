using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class BasicExplosiveProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreName => "basic-explosive_gore";


        public override void SafeSetDefaults()
        {
            pickPower = 40;
            radius = 3;
            Projectile.tileCollide = true;
            Projectile.width = 26;
            Projectile.height = 22;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            //projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 150;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            //Create Bomb Dust
            DustEffects();

            //Create Bomb Explosion
            ExplosionTileDamage();

            //Create Bomb Damage
            ExplosionEntityDamage();

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(-1f, 0f);
            Vector2 gVel2 = new Vector2(0f, -1f);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}1").Type, Projectile.scale);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}2").Type, Projectile.scale);
        }
    }
}