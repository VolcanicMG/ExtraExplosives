using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles.RegenBomb
{
    public class RegenBombProjectile : ExplosiveProjectile
    {
        private bool complete = false;
        private int countdown = 120;
        protected override string explodeSoundsLoc { get; } = "n/a";
        protected override string goreFileLoc { get; } = "n/a";
        private int minX { get; set; }
        private int maxX { get; set; }
        private int minY { get; set; }
        private int maxY { get; set; }
        private int seed { get; set; }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Regen Bomb");
        }

        public override void SafeSetDefaults()
        {
            pickPower = -2;
            projectile.tileCollide = false;
            projectile.timeLeft = Int32.MaxValue;
            projectile.aiStyle = 16;
        }

        public virtual void SetRadius(int radius)
        {
            this.radius = radius;
        }

        public override string Texture { get; } = "ExtraExplosives/Projectiles/SmallExplosiveProjectile";
        
        private int[] currentTile = new int[2];
        public override bool PreAI()
        {
            projectile.velocity.X *= 0.99f;
            if (projectile.velocity.Y < 0) projectile.velocity *= 0.98f;
            else if (projectile.velocity.Y == 0) projectile.velocity.Y += 0.1f;
            else projectile.velocity *= 1.05f;
            //Main.NewText(countdown);
            if (countdown == 0)
            {
                countdown--;
                minX = (int)(projectile.Center.X / 16 - radius);
                maxX = (int)(projectile.Center.X / 16 + radius);
                minY = (int)(projectile.Center.Y / 16 - radius);
                maxY = (int)(projectile.Center.Y / 16 + radius);
                //if (minX < 0) minX = 0;
                //else if (maxX > Main.mapMaxX) maxX = Main.mapMaxX;
                //if (minY < 0) minY = 0;
                //else if (maxY > Main.mapMaxX) maxY = Main.mapMaxY;
                currentTile[0] = minX;
                currentTile[1] = minY;
            }
            else if (countdown > 0)
            {
                countdown--;
                return false;
            }
            return true;
        }
        
        public override void AI()
        {
            projectile.velocity = Vector2.Zero;

            /*for (int i = minX; i < maxX; i++)
            {
                for (int j = minY; j < maxY; j++)
                {
                    if (ContentInstance<ExtraExplosivesWorld>.Instance.originalWorldState[i, j] == -1) continue;
                    WorldGen.PlaceTile(i, j, ContentInstance<ExtraExplosivesWorld>.Instance.originalWorldState[i, j],
                        false, true);
                    Main.NewText($"{i}{j} type {ContentInstance<ExtraExplosivesWorld>.Instance.originalWorldState[i,j]}");
                }
            }*/
                
            int x = (int)(currentTile[0] - projectile.Center.X / 16);
            int y = (int)(currentTile[1] - projectile.Center.Y / 16);
            Main.NewText($"{currentTile[0]}, {currentTile[1]}");
            Main.NewText($"{x}, {y} & {radius}");
            if (Math.Sqrt(x * x + y * y) <= radius + 0.5)
            {
                if( WorldGen.InWorld(currentTile[0], currentTile[1])) Main.NewText("Possibly out of world");
                // Do things if needed
                int type = ContentInstance<ExtraExplosivesWorld>.Instance.originalWorldState[currentTile[0],
                    currentTile[1]];
                if (WorldGen.TileEmpty(currentTile[0], currentTile[1]) &&
                    type != -1)
                {
                    WorldGen.PlaceTile(currentTile[0], currentTile[1], type);
                }
                Main.NewText("placing");
            }
            //else Main.NewText("Skipping");
            
            currentTile[0]++;
            //Main.NewText($"i: {currentTile[0]} @ {minX}|{maxX}, j: {currentTile[1]} @ {minY}|{maxY}");
            if (currentTile[0] > maxX)
            {
                currentTile[0] = minX;
                currentTile[1]++;
            }
            if (currentTile[1] > maxY)
            {
                currentTile[1] = minY;
                Main.NewText("Finsihed placing dirt");
                complete = true;
                projectile.timeLeft = 0;
                projectile.Kill();
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