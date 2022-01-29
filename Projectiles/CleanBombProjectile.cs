﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    class CleanBombProjectile : ExplosiveProjectile
    {
        //Variables:
        public static float Radius = 400f;      // Used for Dust particles + Damage radius
        protected override string explodeSoundsLoc => "Sounds/Custom/Explosives/Clean_Bomb_";
        protected override string goreFileLoc => "n/a";
        private int[] dustsToSpawn;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("CleanBomb");
        }

        public override void SafeSetDefaults()
        {
            IgnoreTrinkets = true;
            projectile.tileCollide = true;
            projectile.width = 22;
            projectile.height = 38;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = int.MaxValue - 1;
            projectile.damage = 0;

            //Defining the dusts to spawn
            dustsToSpawn = new int[] {
                158,
                176,
                177,
                177
            };

            //Retrieving the explosion sounds
            explodeSounds = new Terraria.Audio.LegacySoundStyle[4];
            for (int num = 1; num <= explodeSounds.Length; num++)
            {
                explodeSounds[num - 1] = mod.GetLegacySoundSlot(SoundType.Custom, explodeSoundsLoc + num);
            }
        }

        public override bool OnTileCollide(Vector2 old)
        {
            projectile.Kill();
            return true;
        }

        public override void Kill(int timeLeft)
        {
            Vector2 center = projectile.Center;

            //Create bomb sound
            Main.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)], (int)center.X, (int)center.Y);

            //Create bomb dust
            CreateDust(center, (int)(Radius / 3f));

            //Spawning the explosion projectile (actually removes buffs)
            Projectile.NewProjectile(center, Vector2.Zero, ModContent.ProjectileType<CleanBombExplosionProjectile>(), 1, 0, projectile.owner, 0, 0);
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
