using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using static ExtraExplosives.GlobalMethods;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class BreakenTheBankenProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "Sounds/Custom/Explosives/Breaken_The_Banken_";
        protected override string goreFileLoc => "Gores/Explosives/breaken-the-banken_gore";
        private const int PickPower = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BreakenTheBanken");
        }

        public override void SafeSetDefaults()
        {
            IgnoreTrinkets = true;
            radius = 20;
            pickPower = -2;
            Projectile.tileCollide = true;
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = 20;
            Projectile.timeLeft = 140;
            explodeSounds = new LegacySoundStyle[4];
            for (int num = 1; num <= explodeSounds.Length; num++)
                explodeSounds[num - 1] = Mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, explodeSoundsLoc + num);
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            SoundEngine.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)], (int)Projectile.Center.X, (int)Projectile.Center.Y);

            //Create Bomb Damage
            //ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

            //Create Bomb Explosion
            Explosion();

            ExplosionDamage();

            //Create Bomb Dust
            //CreateDust(projectile.Center, 10);

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(-3f, 3f);
            Vector2 gVel2 = new Vector2(3f, 0f);
            Gore.NewGore(Projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "1").Type, Projectile.scale);
            Gore.NewGore(Projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "2").Type, Projectile.scale);
        }

        public override void Explosion()
        {
            Vector2 position = Projectile.Center;
            int cntr = 0; //Tracks how many coins have spawned in

            for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left
            {
                for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Circle
                    {
                        ushort tile = Main.tile[xPosition, yPosition].TileType;
                        if (!CanBreakTile(tile, PickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
                        {
                        }
                        else //Breakable
                        {
                            if (WorldGen.TileEmpty(xPosition, yPosition))
                            {
                                if (++cntr <= 50) Projectile.NewProjectile(position.X + x, position.Y + y, Main.rand.Next(10) - 5, Main.rand.Next(10) - 5, Mod.Find<ModProjectile>("BreakenTheBankenChildProjectile").Type, 100, 20, Projectile.owner, 0.0f, 0);
                            }
                            else
                            {
                                if (++cntr <= 50) Projectile.NewProjectile(position.X, position.Y, Main.rand.Next(10) - 5, Main.rand.Next(10) - 5, Mod.Find<ModProjectile>("BreakenTheBankenChildProjectile").Type, 100, 20, Projectile.owner, 0.0f, 0);
                            }
                        }
                    }
                }
            }
        }

        public override void ExplosionDamage()
        {
            return;
        }
    }
}