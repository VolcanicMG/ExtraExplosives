using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles.Testing
{
    public class TestingExplosive : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "Gores/Explosives/basic-explosive_gore";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Testing Explosive");
        }

        public override void SafeSetDefaults()
        {
            pickPower = 0;
            radius = 30;
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

            Explosion();

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(-1f, 0f);
            Vector2 gVel2 = new Vector2(0f, -1f);
            // TODO probably not correct IEntitySource
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "1").Type, Projectile.scale);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "2").Type, Projectile.scale);
        }


    }
}