using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;
using Terraria.ModLoader;

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
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SafeSetDefaults()
        {
            pickPower = 0;
            radius = 12;
            InflictDamageSelf = false;
            Projectile.knockBack = 20;
            Projectile.tileCollide = true;
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
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


            if (Main.myPlayer == Projectile.owner && Projectile.ai[0] <= 0f)
            {
                if (Main.player[Projectile.owner].channel)
                {
                    float num114 = 12f;

                    Vector2 vector10 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                    float num115 = (float)Main.mouseX + Main.screenPosition.X - vector10.X;
                    float num116 = (float)Main.mouseY + Main.screenPosition.Y - vector10.Y;
                    if (Main.player[Projectile.owner].gravDir == -1f)
                    {
                        num116 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector10.Y;
                    }
                    float num117 = (float)Math.Sqrt((double)(num115 * num115 + num116 * num116));
                    num117 = (float)Math.Sqrt((double)(num115 * num115 + num116 * num116));
                    if (Projectile.ai[0] < 0f)
                    {
                        reference = Projectile.ai[0];
                        reference += 1f;
                    }

                    else if (num117 > num114)
                    {
                        num117 = num114 / num117;
                        num115 *= num117;
                        num116 *= num117;
                        int num118 = (int)(num115 * 1000f);
                        int num119 = (int)(Projectile.velocity.X * 1000f);
                        int num120 = (int)(num116 * 1000f);
                        int num121 = (int)(Projectile.velocity.Y * 1000f);
                        if (num118 != num119 || num120 != num121)
                        {
                            Projectile.netUpdate = true;
                        }

                        else
                        {
                            Projectile.velocity.X = num115;
                            Projectile.velocity.Y = num116;
                        }
                    }
                    else
                    {
                        int num122 = (int)(num115 * 1000f);
                        int num123 = (int)(Projectile.velocity.X * 1000f);
                        int num124 = (int)(num116 * 1000f);
                        int num125 = (int)(Projectile.velocity.Y * 1000f);
                        if (num122 != num123 || num124 != num125)
                        {
                            Projectile.netUpdate = true;
                        }
                    }

                    Projectile.velocity.X = num115;
                    Projectile.velocity.Y = num116;

                    if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
                    {
                        dust2 = true;
                        Projectile.Kill();
                    }

                    Projectile.timeLeft = 250;
                }
                else if (Projectile.ai[0] <= 0f)
                {
                    Projectile.netUpdate = true;
                    float num126 = 12f;
                    Vector2 vector11 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                    float num127 = (float)Main.mouseX + Main.screenPosition.X - vector11.X;
                    float num128 = (float)Main.mouseY + Main.screenPosition.Y - vector11.Y;
                    if (Main.player[Projectile.owner].gravDir == -1f)
                    {
                        num128 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector11.Y;
                    }
                    float num129 = (float)Math.Sqrt((double)(num127 * num127 + num128 * num128));
                    if (num129 == 0f || Projectile.ai[0] < 0f)
                    {
                        vector11 = new Vector2(Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2), Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2));
                        num127 = Projectile.position.X + (float)Projectile.width * 0.5f - vector11.X;
                        num128 = Projectile.position.Y + (float)Projectile.height * 0.5f - vector11.Y;
                        num129 = (float)Math.Sqrt((double)(num127 * num127 + num128 * num128));
                    }
                    num129 = num126 / num129;
                    num127 *= num129;
                    num128 *= num129;
                    Projectile.velocity.X = num127;
                    Projectile.velocity.Y = num128;
                    if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
                    {
                        Projectile.Kill();
                    }

                    Projectile.ai[0] = 1f;
                }
                else
                {
                    Projectile.timeLeft = 550;
                }
            }


            if (Projectile.velocity.X != 0f || Projectile.velocity.Y != 0f)
            {
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) - 2.355f;
            }
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }

            Projectile.rotation = Projectile.velocity.ToRotation();

        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            //SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            //Create Bomb Damage
            //ExplosionDamage(10f, projectile.Center, projectile.damage, 20f, projectile.owner);

            //Create Bomb Explosion
            //CreateExplosion(projectile.Center, 2);

            //Explosion();
            ExplosionDamage();

            //Create Bomb Dust
            if(dust2) DustEffects();
            else DustEffectsRockets(Projectile.oldVelocity);
            
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
                        if (Vector2.Distance(dust.position, Projectile.Center) > (radius + 6) * 8) dust.active = false;
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
                        if (Vector2.Distance(dust.position, Projectile.Center) > (radius + 6) * 8) dust.active = false;
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
                        if (Vector2.Distance(dust.position, Projectile.Center) > (radius + 6) * 8) dust.active = false;
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