using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    internal class FlashbangProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreName => "n/a";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flashbang");
        }

        public override void SafeSetDefaults()
        {
            IgnoreTrinkets = true;
            Projectile.tileCollide = true;
            Projectile.width = 12;
            Projectile.height = 32;
            Projectile.aiStyle = 16;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
            Projectile.damage = 0;
        }

        public override void Kill(int timeLeft)
        {
            //add lighting
            Lighting.AddLight(Projectile.position, new Vector3(255f, 255f, 255f));

            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center); //Sound Effect

            //Projectile.NewProjectile(projectile.Center.X - 450, projectile.Center.Y, 0, 0, ModContent.ProjectileType<InvisFlashbangProjectile>(), 1, 0, projectile.owner, 0.0f, 0); //Left
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<InvisFlashbangProjectile>(), 1, 1, Projectile.owner, 0.0f, 0);
        }
    }
}