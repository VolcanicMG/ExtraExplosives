using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace ExtraExplosives.Projectiles
{
    public class RegenBombProjectile : ExplosiveProjectile
    {
        private bool complete = false;
        private int countdown = 120;
        protected override string explodeSoundsLoc { get; } = "n/a";
        protected override string goreFileLoc { get; } = "n/a";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Regen Bomb");
        }

        public override void SafeSetDefaults()
        {
            radius = 10;
            pickPower = -2;
            projectile.tileCollide = false;
            projectile.timeLeft = Int32.MaxValue;
            projectile.aiStyle = 16;
        }

        public override string Texture { get; } = "ExtraExplosives/Projectiles/LargeExplosiveProjectile";
        
        private int[] currentTile = new int[2];
        private int minX, maxX, minY, maxY;
        public override bool PreAI()
        {
            //Main.NewText(countdown);
            if (countdown == 0)
            {
                countdown--;
                int minX = (int)(projectile.Center.X / 16 - radius / 2);
                int maxX = (int)(projectile.Center.X / 16 + radius / 2);
                int minY = (int)(projectile.Center.Y / 16 - radius / 2);
                int maxY = (int)(projectile.Center.Y / 16 + radius / 2);
                if (minX < 0) minX = 0;
                if (maxX > Main.mapMaxX) maxX = Main.mapMaxX;
                if (minY < 0) minY = 0;
                if (maxY > Main.mapMaxX) maxY = Main.mapMaxY;
                currentTile[0] = minX;
                currentTile[1] = minY;
            }
            else if (countdown > 0)
            {
                countdown--;
                base.AI();
                return false;
            }
            return true;
        }

        private int stage = 0;
        public override void AI()
        {
            projectile.velocity = Vector2.Zero;
            switch (stage)
            {
                case 0:
                    int x = (int)(currentTile[0] - projectile.Center.X);
                    int y = (int)(currentTile[1] - projectile.Center.Y);
                    if (Math.Sqrt(x * x + y * y) > radius + 0.5)
                    {
                        // Do things if needed
                    }
                    else if (WorldGen.TileEmpty(currentTile[0], currentTile[1]))
                        WorldGen.PlaceTile(currentTile[0], currentTile[1], TileID.Dirt);
                    currentTile[0]++;
                    if (currentTile[0] > maxX)
                    {
                        currentTile[0] = minX;
                        currentTile[1]++;
                        if (currentTile[1] > maxY)
                        {
                            currentTile[1] = minY;
                            Main.NewText("Finsihed placing dirt");
                            stage = 1;
                        }
                    }
                    return;
                default:
                    Kill(0);
                    return;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            return;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            return;
        }

        public override void Explosion()
        {
            base.Explosion();
        }

        public override bool PreKill(int timeLeft)
        {
            if (complete) return true;
            return false;
        }
    }
}