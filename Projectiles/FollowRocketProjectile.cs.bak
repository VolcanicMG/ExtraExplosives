using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class FollowRocketProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "n/a";
        private float reference;
        private bool dust2;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Follow Rocket");
            Main.projFrames[projectile.type] = 3;
        }

        public override void SafeSetDefaults()
        {
            pickPower = 0;
            radius = 12;
            InflictDamageSelf = false;
            projectile.knockBack = 20;
            projectile.tileCollide = true;
            projectile.width = 18;
            projectile.height = 18;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = 1;
            projectile.timeLeft = 550;
            projectile.ranged = true;
            projectile.scale = 1.2f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();

            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.Kill();
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            projectile.Kill();
        }

        public override void AI()
        {
            //anim
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                //projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
                if (++projectile.frame >= 3)
                {
                    projectile.frame = 0;
                }
            }

            //dust
            float num248 = 0f;
            float num249 = 0f;

            Vector2 position71 = new Vector2(projectile.position.X + 3f + num248, projectile.position.Y + 3f + num249) - projectile.velocity * 0.5f;
            int width67 = projectile.width - 8;
            int height67 = projectile.height - 8;
            Color newColor = default(Color);
            int num250 = Dust.NewDust(position71, width67, height67, 6, 0f, 0f, 100, newColor, 1f);
            Dust dust3 = Main.dust[num250];
            dust3.scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
            dust3 = Main.dust[num250];
            dust3.velocity *= 0.2f;
            Main.dust[num250].noGravity = true;
            Vector2 position72 = new Vector2(projectile.position.X + 3f + num248, projectile.position.Y + 3f + num249) - projectile.velocity * 0.5f;
            int width68 = projectile.width - 8;
            int height68 = projectile.height - 8;
            newColor = default(Color);
            num250 = Dust.NewDust(position72, width68, height68, 31, 0f, 0f, 100, newColor, 0.5f);
            Main.dust[num250].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
            dust3 = Main.dust[num250];
            dust3.velocity *= 0.05f;


            if (Main.myPlayer == projectile.owner && projectile.ai[0] <= 0f)
            {
                if (Main.player[projectile.owner].channel)
                {
                    float num114 = 12f;

                    Vector2 vector10 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                    float num115 = (float)Main.mouseX + Main.screenPosition.X - vector10.X;
                    float num116 = (float)Main.mouseY + Main.screenPosition.Y - vector10.Y;
                    if (Main.player[projectile.owner].gravDir == -1f)
                    {
                        num116 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector10.Y;
                    }
                    float num117 = (float)Math.Sqrt((double)(num115 * num115 + num116 * num116));
                    num117 = (float)Math.Sqrt((double)(num115 * num115 + num116 * num116));
                    if (projectile.ai[0] < 0f)
                    {
                        reference = projectile.ai[0];
                        reference += 1f;
                    }

                    else if (num117 > num114)
                    {
                        num117 = num114 / num117;
                        num115 *= num117;
                        num116 *= num117;
                        int num118 = (int)(num115 * 1000f);
                        int num119 = (int)(projectile.velocity.X * 1000f);
                        int num120 = (int)(num116 * 1000f);
                        int num121 = (int)(projectile.velocity.Y * 1000f);
                        if (num118 != num119 || num120 != num121)
                        {
                            projectile.netUpdate = true;
                        }

                        else
                        {
                            projectile.velocity.X = num115;
                            projectile.velocity.Y = num116;
                        }
                    }
                    else
                    {
                        int num122 = (int)(num115 * 1000f);
                        int num123 = (int)(projectile.velocity.X * 1000f);
                        int num124 = (int)(num116 * 1000f);
                        int num125 = (int)(projectile.velocity.Y * 1000f);
                        if (num122 != num123 || num124 != num125)
                        {
                            projectile.netUpdate = true;
                        }
                    }

                    projectile.velocity.X = num115;
                    projectile.velocity.Y = num116;

                    if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
                    {
                        dust2 = true;
                        projectile.Kill();
                    }

                    projectile.timeLeft = 250;
                }
                else if (projectile.ai[0] <= 0f)
                {
                    projectile.netUpdate = true;
                    float num126 = 12f;
                    Vector2 vector11 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                    float num127 = (float)Main.mouseX + Main.screenPosition.X - vector11.X;
                    float num128 = (float)Main.mouseY + Main.screenPosition.Y - vector11.Y;
                    if (Main.player[projectile.owner].gravDir == -1f)
                    {
                        num128 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector11.Y;
                    }
                    float num129 = (float)Math.Sqrt((double)(num127 * num127 + num128 * num128));
                    if (num129 == 0f || projectile.ai[0] < 0f)
                    {
                        vector11 = new Vector2(Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2), Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2));
                        num127 = projectile.position.X + (float)projectile.width * 0.5f - vector11.X;
                        num128 = projectile.position.Y + (float)projectile.height * 0.5f - vector11.Y;
                        num129 = (float)Math.Sqrt((double)(num127 * num127 + num128 * num128));
                    }
                    num129 = num126 / num129;
                    num127 *= num129;
                    num128 *= num129;
                    projectile.velocity.X = num127;
                    projectile.velocity.Y = num128;
                    if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
                    {
                        projectile.Kill();
                    }

                    projectile.ai[0] = 1f;
                }
                else
                {
                    projectile.timeLeft = 550;
                }
            }


            if (projectile.velocity.X != 0f || projectile.velocity.Y != 0f)
            {
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) - 2.355f;
            }
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }

            projectile.rotation = projectile.velocity.ToRotation();

        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

            //Create Bomb Damage
            //ExplosionDamage(10f, projectile.Center, projectile.damage, 20f, projectile.owner);

            //Create Bomb Explosion
            //CreateExplosion(projectile.Center, 2);

            //Explosion();
            ExplosionDamage();

            //Create Bomb Dust
            if(dust2) DustEffects();
            else DustEffectsRockets(projectile.oldVelocity);
            
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
                    if (Main.rand.NextFloat() < 1f)
                    {
                        updatedPosition = new Vector2(position.X - radius * 8, position.Y - radius * 8);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, radius * 16, radius * 16, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 4.539474f)];
                        if (Vector2.Distance(dust.position, projectile.Center) > (radius + 6) * 8) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.fadeIn = 2.5f;
                        }
                    }
                    //------------

                    //---Dust 2---
                    if (Main.rand.NextFloat() < 0.48f)
                    {
                        updatedPosition = new Vector2(position.X - radius * 8, position.Y - radius * 8);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, radius * 16, radius * 16, 203, 0f, 0f, 0, new Color(255, 255, 255), 3.026316f)];
                        if (Vector2.Distance(dust.position, projectile.Center) > (radius + 6) * 8) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.noLight = true;
                        }
                    }
                    //------------

                    //---Dust 3---
                    if (Main.rand.NextFloat() < 0.8f)
                    {
                        updatedPosition = new Vector2(position.X - radius * 8, position.Y - radius * 8);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, radius * 16, radius * 16, 31, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
                        if (Vector2.Distance(dust.position, projectile.Center) > (radius + 6) * 8) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.noLight = true;
                        }
                    }
                    //------------
                }
            }
        }
    }
}