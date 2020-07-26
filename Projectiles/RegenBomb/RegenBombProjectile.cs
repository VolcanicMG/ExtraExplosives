using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles.RegenBomb
{
    public abstract class RegenBombProjectile : ExplosiveProjectile
    {
        //public override bool CloneNewInstances { get; } = true;
        internal float velocity = 0.01f;
        private bool complete = false;
        internal int countdown = 120;
        protected override string explodeSoundsLoc { get; } = "n/a";
        protected override string goreFileLoc { get; } = "n/a";
        private int minX { get; set; }
        private int maxX { get; set; }
        private int minY { get; set; }
        private int maxY { get; set; }
        private int seed { get; set; }

        public abstract void RegenDefaults();
        public override void SafeSetDefaults()
        {
            pickPower = -2;
            projectile.tileCollide = false;
            projectile.timeLeft = 1000000;
            projectile.aiStyle = 0;
            RegenDefaults();
            countdown *= projectile.extraUpdates;
        }

        public virtual void SetRadius(int radius)
        {
            this.radius = radius;
        }

        private int[] currentTile = new int[2];
        public sealed override bool PreAI()
        {
            //projectile.velocity.X *= 0.99f;
            //if (projectile.velocity.Y < 0) projectile.velocity *= 1 - velocity;
            //else if (projectile.velocity.Y == 0) projectile.velocity.Y += 0.1f;
            //else projectile.velocity *= 1 + velocity;
            if (countdown == 0)
            {
                countdown--;
                minX = (int)(projectile.Center.X / 16 - radius);
                maxX = (int)(projectile.Center.X / 16 + radius);
                minY = (int)(projectile.Center.Y / 16 - radius);
                maxY = (int)(projectile.Center.Y / 16 + radius);
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

        public sealed override void AI()
        {
            try
            {
                int _ = ContentInstance<ExtraExplosivesWorld>.Instance.originalWorldState[currentTile[0],
                    currentTile[1]];
            }
            catch (Exception e)
            {
                Main.NewText("The world doesn't have the required information to be regenerated\n" +
                             "This may be due to using a world which was generated before installing this version of EE\n" +
                             "or by installing a newer version of EE over the version this world was originally generated in\n" +
                             "The regen bomb will not work in this world, please generate a new world and try again");
                projectile.timeLeft = 0;
                complete = true;
                projectile.Kill();
            }
            projectile.velocity = Vector2.Zero;

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
            }
            
            currentTile[0]++;
            if (currentTile[0] > maxX)
            {
                currentTile[0] = minX;
                currentTile[1]++;
            }
            if (currentTile[1] > maxY)
            {
                currentTile[1] = minY;
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

        public override bool PreKill(int timeLeft)
        {
            if (complete) return true;
            return false;
        }
    }
}