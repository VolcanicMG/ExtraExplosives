using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using static ExtraExplosives.GlobalMethods;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class HydromiteProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "ExtraExplosives/Assets/Sounds/Custom/Explosives/Hydromite_";
        protected override string goreFileLoc => "Gores/Explosives/lavamite-hydromite_gore";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydromite");
        }

        public override void SafeSetDefaults()
        {
            radius = 10;
            Projectile.tileCollide = true;
            Projectile.width = 10;
            Projectile.height = 32;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100;
            explodeSounds = new SoundStyle[3];
            for (int num = 1; num <= explodeSounds.Length; num++)
            {
                explodeSounds[num - 1] = new SoundStyle(explodeSoundsLoc + num);
            }
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            //SoundEngine.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)]);

            //Create Bomb Damage
            //ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

            //Create Bomb Explosion
            Explosion();

            //Create Bomb Dust
            DustEffects(type: 1, shake: false, dustType: 188, color: new Color(0, 42, 255));

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(-2f, -2f);
            Vector2 gVel2 = new Vector2(0f, 2f);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "1").Type, Projectile.scale);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "2").Type, Projectile.scale);
        }

        public override void Explosion()
        {
            Vector2 position = Projectile.Center;
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
                            //Main.tile[xPosition, yPosition].LiquidType = 0;
                            Main.tile[xPosition, yPosition].LiquidAmount = 128;
                            WorldGen.SquareTileFrame(xPosition, yPosition, true);
                        }
                    }
                }
            }
        }
    }
}