using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class MediumExplosiveProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "Gores/Explosives/basic-explosive_gore";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("MediumExplosive");
        }

        public override void SafeSetDefaults()
        {
            pickPower = 45;
            radius = 10;
            projectile.tileCollide = true;
            projectile.width = 32;
            projectile.height = 30;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

            //Create Bomb Dust

            Explosion();
            ExplosionDamage();

            DustEffects();

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(-1.5f, 0f);
            Vector2 gVel2 = new Vector2(0f, -1.5f);
            Gore.NewGore(projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "1"), projectile.scale * 1.25f);
            Gore.NewGore(projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "2"), projectile.scale * 1.25f);
        }
    }
}