using ExtraExplosives.Buffs;
using ExtraExplosives.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Pets
{
    public class BombBuddyDetector : ModProjectile
    {
        private bool hit;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bomb Buddy Follow");
        }

        public override void SetDefaults()
        {
            Projectile.tileCollide = false;
            Projectile.width = 30;   //This defines the hitbox width
            Projectile.height = 40; //This defines the hitbox height
            Projectile.aiStyle = 0;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            Projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            Projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            Projectile.timeLeft = 50000; //The amount of time the projectile is alive for
            Projectile.width = 38;
            Projectile.height = 40;
            Projectile.Opacity = 0f;
        }

        public override string Texture => "ExtraExplosives/Projectiles/BulletBoomProjectile";

        //public override bool PreAI()
        //{
        //	Player player = Main.player[projectile.owner];
        //	//player.BabyFaceMonster = false; // Relic from aiType
        //	return true;
        //}

        public override void AI()
        {
            Projectile.timeLeft = 5;
            Player player = Main.player[Projectile.owner];
            ExtraExplosivesPlayer modPlayer = player.GetModPlayer<ExtraExplosivesPlayer>();
            Projectile.position = modPlayer.BuddyPos;
            //Main.NewText(projectile.position);

            if (!player.HasBuff(ModContent.BuffType<BombBuddyBuff>()))
            {
                Projectile.Kill();
            }

            //check for a hit
            if (hit)
            {
                Projectile.ai[1]++;
            }

            //check for the end of the delay
            if (Projectile.ai[1] >= 60)
            {
                Projectile.Kill();
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!hit)
            {
                hit = true;

                Vector2 position = Projectile.Center;
                //SoundEngine.PlaySound(SoundID.Item14, position);
                int radius = 5;  //this is the explosion radius, the highter is the value the bigger is the explosion

                ExplosionDamageProjectile.DamageRadius = (float)(radius * 1.5f);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, 0, 0, Mod.Find<ModProjectile>("ExplosionDamageProjectile").Type, 100, 40, Projectile.owner, 0.0f, 0);

                for (int i = 0; i <= 10; i++)
                {
                    Dust dust;
                    Vector2 vev = new Vector2(position.X - (78 / 2), position.Y - (78 / 2));
                    if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
                    {
                        if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
                        {
                            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                            dust = Main.dust[Terraria.Dust.NewDust(vev, 78, 78, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 4.539474f)];
                            dust.noGravity = true;
                            dust.fadeIn = 2.486842f;
                        }

                        if (Main.rand.NextFloat() < 0.5921053f)
                        {
                            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                            dust = Main.dust[Terraria.Dust.NewDust(vev, 78, 78, 203, 0f, 0f, 0, new Color(255, 255, 255), 3.026316f)];
                            dust.noGravity = true;
                            dust.noLight = true;
                        }

                        if (Main.rand.NextFloat() < 0.2763158f)
                        {
                            Vector2 vev2 = new Vector2(position.X - (100 / 2), position.Y - (100 / 2));
                            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                            dust = Main.dust[Terraria.Dust.NewDust(vev2, 100, 100, 31, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
                            dust.noGravity = true;
                            dust.noLight = true;
                        }
                    }
                }
            }
        }
    }
}