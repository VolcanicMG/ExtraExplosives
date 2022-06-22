using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class InfinityBombProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "n/a";
        private const int OriginalDamage = 250;
        private int _multiplier = 1;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinity Bomb");
        }

        public override void SafeSetDefaults()
        {
            pickPower = 50;
            radius = 7;
            Projectile.timeLeft = 300;
            //projectile.CloneDefaults(29);
            Projectile.aiStyle = 16;
            Projectile.timeLeft = 120;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
        }
        public override void Kill(int timeLeft)
        {
            _multiplier *= Projectile.damage / OriginalDamage;
            //Create Bomb Sound
            SoundEngine.PlaySound(SoundID.Item14, (int)Projectile.Center.X, (int)Projectile.Center.Y);

            // Have to set these before calling explosion damage to ensure proper values of damage and knockback
            //projectile.damage = (int) Math.Ceiling(100 * projectile.localAI[0]);
            //projectile.knockBack = (int) Math.Ceiling(20 * projectile.localAI[0]);
            //Create Bomb Damage
            ExplosionDamage();
            radius = (int)(radius * _multiplier);
            //Create Bomb Explosion

            //Main.NewText($"Damage {projectile.damage}, Knockback {projectile.knockBack}, Radius {radius}, multiplier {_multiplier}");
            Explosion();
            //Main.NewText(projectile.localAI[0]);
            //Create Bomb Dust
            DustEffects();

        }

        public override void Explosion()
        {

            // x and y are the tile offset of the current tile relative to the player
            // i and j are the true tile cords relative to 0,0 in the world
            Player player = Main.player[Projectile.owner];
            if (pickPower < -1) return;
            if (player.EE().BombardEmblem) return;

            Vector2 position = new Vector2(Projectile.Center.X / 16f, Projectile.Center.Y / 16f);    // Converts to tile cords for convenience

            radius = (int)((radius + player.EE().RadiusBonus) * player.EE().RadiusMulti);

            //Main.NewText(radius);

            for (int x = -radius;
                x <= radius;
                x++)
            {
                for (int y = -radius;
                    y <= radius;
                    y++)
                {
                    int i = (int)(x + position.X);
                    int j = (int)(y + position.Y);
                    if (!WorldGen.InWorld(i, j)) continue;
                    double dist = Math.Sqrt(x * x + y * y);
                    if (dist <= radius + 0.5) //Circle
                    {
                        if (!WorldGen.TileEmpty(i, j))
                        {
                            if (!CanBreakTile(Main.tile[i, j].TileType, pickPower)) continue;
                            if (!CanBreakTiles) continue;
                            // Using KillTile is laggy, use ClearTile when working with larger tile sets    (also stops sound spam)
                            // But it must be done on outside tiles to ensure propper updates so use it only on outermost tiles
                            if (dist <= radius - 0.5) Main.tile[i, j].ClearTile();
                            else WorldGen.KillTile((int)(i), (int)(j), false, false, false);
                        }

                        if (CanBreakWalls)
                        {
                            WorldGen.KillWall((int)(i), (int)(j));
                        }
                    }
                }
            }
        }
    }
}