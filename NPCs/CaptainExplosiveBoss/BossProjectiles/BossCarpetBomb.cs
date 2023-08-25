using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.NPCs.CaptainExplosiveBoss.BossProjectiles
{
    public class BossCarpetBomb : ModProjectile
    {
        private const int PickPower = 0;
        private float reference;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rocket");
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.tileCollide = true;
            Projectile.width = 46;
            Projectile.height = 18;
            Projectile.aiStyle = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 550;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.scale = 1.2f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();

            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.Kill();
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Projectile.Kill();
        }

        public override void AI()
        {
            //anim
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                //projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
                if (++Projectile.frame >= 3)
                {
                    Projectile.frame = 0;
                }
            }

            //dust
            float num248 = 0f;
            float num249 = 0f;

            Vector2 position71 = new Vector2(Projectile.position.X + 3f + num248, Projectile.position.Y + 3f + num249) - Projectile.velocity * 0.5f;
            int width67 = Projectile.width - 8;
            int height67 = Projectile.height - 8;
            Color newColor = default(Color);
            int num250 = Dust.NewDust(position71, width67, height67, 6, 0f, 0f, 100, newColor, 1f);
            Dust dust3 = Main.dust[num250];
            dust3.scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
            dust3 = Main.dust[num250];
            dust3.velocity *= 0.2f;
            Main.dust[num250].noGravity = true;
            Vector2 position72 = new Vector2(Projectile.position.X + 3f + num248, Projectile.position.Y + 3f + num249) - Projectile.velocity * 0.5f;
            int width68 = Projectile.width - 8;
            int height68 = Projectile.height - 8;
            newColor = default(Color);
            num250 = Dust.NewDust(position72, width68, height68, 31, 0f, 0f, 100, newColor, 0.5f);
            Main.dust[num250].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
            dust3 = Main.dust[num250];
            dust3.velocity *= 0.05f;


            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            //Create Bomb Damage
            ExplosionDamageEnemy(25, Projectile.Center, Projectile.damage, ModContent.NPCType<CaptainExplosiveBoss>());

            //Create Bomb Explosion
            //CreateExplosion(projectile.Center, 2);

            //Create Bomb Dust
            CreateDust(Projectile.Center, 100);
        }

        private void CreateDust(Vector2 position, int amount)
        {
            Dust dust;
            Vector2 updatedPosition;

            for (int i = 0; i <= amount; i++)
            {
                if (Main.rand.NextFloat() < DustAmount)
                {
                    //---Dust 1---
                    if (Main.rand.NextFloat() < .6f)
                    {
                        updatedPosition = new Vector2(position.X - 180 / 2, position.Y - 180 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 180, 180, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 4.539474f)];
                        dust.noGravity = true;
                        dust.fadeIn = 2.5f;
                    }
                    //------------

                    //---Dust 2---
                    if (Main.rand.NextFloat() < 0.48f)
                    {
                        updatedPosition = new Vector2(position.X - 180 / 2, position.Y - 180 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 180, 180, 203, 0f, 0f, 0, new Color(255, 255, 255), 3.026316f)];
                        dust.noGravity = true;
                        dust.noLight = true;
                    }
                    //------------

                    //---Dust 3---
                    if (Main.rand.NextFloat() < 0.8f)
                    {
                        updatedPosition = new Vector2(position.X - 180 / 2, position.Y - 180 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 180, 180, 31, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
                        dust.noGravity = true;
                        dust.noLight = true;
                    }
                    //------------
                }
            }
        }
    }
}