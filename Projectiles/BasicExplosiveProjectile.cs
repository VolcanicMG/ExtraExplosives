using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace ExtraExplosives.Projectiles
{
    public class BasicExplosiveProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "Gores/Explosives/basic-explosive_gore";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BasicExplosive");
        }

        public override void SafeSetDefaults()
        {
            pickPower = 40;
            radius = 3;
            projectile.tileCollide = true;
            projectile.width = 26;
            projectile.height = 22;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            //projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 150;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

            //Create Bomb Dust
            DustEffects();

            //Create Bomb Explosion
            Explosion();

            //Create Bomb Damage
            ExplosionDamage();

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(-1f, 0f);
            Vector2 gVel2 = new Vector2(0f, -1f);
            Gore.NewGore(projectile.position, gVel1.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "1"), projectile.scale);
            Gore.NewGore(projectile.position, gVel2.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "2"), projectile.scale);
        }
    }
}