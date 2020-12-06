using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

namespace ExtraExplosives.Projectiles
{
    public class BulletBoomProjectile : ExplosiveProjectile	// Will rebuild this file later
    {
        public override bool CloneNewInstances => true;    // DONT CHANGE
        public override string Texture => "ExtraExplosives/Items/Explosives/BulletBoomItem";    // texture, change if needed
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "n/a";

        // Variables
        private int _projectileID;

        public void SetProjectile(int projectileID)
        {
            _projectileID = projectileID;
        }

        public int GetProjectile() => _projectileID;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bullet Boom");    // internal name only, will not have a space for the projName piece
        }

        public override void SafeSetDefaults()
        {
            radius = 5;
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 22;   //This defines the hitbox width
            projectile.height = 22; //This defines the hitbox height
            projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = 1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 40; //The amsadount of time the projectile is alive for
            projectile.knockBack = _projectileID;
        }

        public override bool OnTileCollide(Vector2 old)
        {
            projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
            projectile.width = 20;
            projectile.height = 64;
            projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);

            projectile.velocity.X = 0;
            projectile.velocity.Y = 0;
            projectile.aiStyle = 0;
            return true;
        }

        public override void Kill(int timeLeft)
        {
            _projectileID = projectile.damage;

            ExtraExplosivesPlayer mp = Main.player[projectile.owner].EE();

            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);

            Vector2 vel;
            int spedX;
            int spedY;
            int cntr = 0;

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5)   //this make so the explosion radius is a circle
                    {
                        //mod.Logger.Debug(projectile.damage);
                        if (WorldGen.TileEmpty(xPosition, yPosition))
                        {
                            spedX = Main.rand.Next(15) - 7;
                            spedY = Main.rand.Next(15) - 7;
                            if (spedX == 0) spedX = 1;
                            if (spedY == 0) spedY = 1;
                            //if (++cntr <= 100) Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, (int)projectile.knockBack, (int)((projectile.damage + Main.player[projectile.owner].EE().DamageBonus) * Main.player[projectile.owner].EE().DamageMulti), 20, projectile.owner, 0.0f, 0);
                            if (++cntr <= 100) Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, (int)projectile.knockBack, (int)((projectile.damage + mp.DamageBonus) * mp.DamageMulti), 20, projectile.owner, 0.0f, 0);
                        }
                        else
                        {
                            spedX = Main.rand.Next(15) - 7;
                            spedY = Main.rand.Next(15) - 7;
                            if (spedX == 0) spedX = 1;
                            if (spedY == 0) spedY = 1;
                            if (++cntr <= 100) Projectile.NewProjectile(position.X + x, position.Y + y, spedX, spedY, (int)projectile.knockBack, (int)((projectile.damage + mp.DamageBonus) * mp.DamageMulti), 20, projectile.owner, 0.0f, 0);
                        }
                    }
                }
            }

            DustEffects();
        }
    }
}