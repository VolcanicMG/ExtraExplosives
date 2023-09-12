using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    class CleanBombProjectile : ExplosiveProjectile
    {
        //Variables:
        public static float Radius = 400f;      // Used for Dust particles + Damage radius
        protected override string explodeSoundsLoc => "ExtraExplosives/Assets/Sounds/Custom/Explosives/Clean_Bomb_";
        protected override string goreName => "n/a";
        private int[] dustsToSpawn;

        public override void SafeSetDefaults()
        {
            IgnoreTrinkets = true;
            Projectile.tileCollide = true;
            Projectile.width = 22;
            Projectile.height = 38;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = int.MaxValue - 1;
            Projectile.damage = 0;

            //Defining the dusts to spawn
            dustsToSpawn = new int[] {
                158,
                176,
                177,
                177
            };

            //Retrieving the explosion sounds
            explodeSounds = new SoundStyle[4];
            for (int num = 1; num <= explodeSounds.Length; num++)
            {
                explodeSounds[num - 1] = new SoundStyle(explodeSoundsLoc + num);
            }
        }

        public override bool OnTileCollide(Vector2 old)
        {
            Projectile.Kill();
            return true;
        }

        public override void Kill(int timeLeft)
        {
            Vector2 center = Projectile.Center;

            //Create bomb sound
            SoundEngine.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)]);

            //Create bomb dust
            CreateDust(center, (int)(Radius / 3f));

            //Spawning the explosion projectile (actually removes buffs)
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), center, Vector2.Zero, ModContent.ProjectileType<CleanBombExplosionProjectile>(), 1, 0, Projectile.owner, 0, 0);
        }

        private void CreateDust(Vector2 position, int amount)
        {
            //Defining reused objects / data types
            Dust dust;
            Vector2 spawnRegionCorner = new Vector2(position.X - Radius / 2, position.Y - Radius / 2); ;
            int currentDust = dustsToSpawn[0];

            //Calculating the amount of dust to spawn based on the Dust Amount user setting
            amount = (int)Math.Floor(amount * DustAmount);

            for (int i = 0; i < amount; i++)
            {
                //Calculating which dust particle to spawn
                currentDust = dustsToSpawn[Main.rand.Next(dustsToSpawn.Length)];

                //Spawning the dust particle
                dust = Main.dust[Dust.NewDust(spawnRegionCorner, (int)Radius, (int)Radius, currentDust, 0f, 0.5f, 1, Color.LightGreen, 1f)];
                if (Vector2.Distance(dust.position, position) > Radius) dust.active = false;
            }
        }
    }
}
