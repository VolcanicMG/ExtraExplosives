﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class HydromiteProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "Sounds/Custom/Explosives/Hydromite_";
        protected override string goreFileLoc => "Gores/Explosives/lavamite-hydromite_gore";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydromite");
        }

        public override void SafeSetDefaults()
        {
            radius = 10;
            projectile.tileCollide = true;
            projectile.width = 10;
            projectile.height = 32;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 100;
            explodeSounds = new LegacySoundStyle[3];
            for (int num = 1; num <= explodeSounds.Length; num++)
            {
                explodeSounds[num - 1] = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, explodeSoundsLoc + num);
            }
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)], (int)projectile.Center.X, (int)projectile.Center.Y);

            //Create Bomb Damage
            //ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

            //Create Bomb Explosion
            Explosion();

            //Create Bomb Dust
            DustEffects(type: 1, shake: false, dustType: 188, color: new Color(0, 42, 255));

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(-2f, -2f);
            Vector2 gVel2 = new Vector2(0f, 2f);
            Gore.NewGore(projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "1"), projectile.scale);
            Gore.NewGore(projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "2"), projectile.scale);
        }

        public override void Explosion()
        {
            Vector2 position = projectile.Center;
            for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left
            {
                for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Circle
                    {
                        if (WorldGen.TileEmpty((int)(x + position.X / 16.0f), (int)(y + position.Y / 16.0f)))
                        {
                            Main.tile[xPosition, yPosition].liquidType(0);
                            Main.tile[xPosition, yPosition].liquid = 128;
                            WorldGen.SquareTileFrame(xPosition, yPosition, true);
                        }
                    }
                }
            }
        }
    }
}