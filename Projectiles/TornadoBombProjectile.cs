using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class TornadoBombProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "n/a";
        private Vector2 vector;
        private bool done;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tornado");
            Main.projFrames[Projectile.type] = 5;
        }

        public override void SafeSetDefaults()
        {
            IgnoreTrinkets = true;
            Projectile.tileCollide = true;
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 860;
            Projectile.damage = 20;
        }

        public override void AI()
        {
            Projectile.rotation = 0;

            if (++Projectile.frameCounter >= 2)
            {
                Projectile.frameCounter = 0;
                //projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
                if (++Projectile.frame >= 5)
                {
                    Projectile.frame = 0;
                }
            }

            //Main.NewText(projectile.ai[1]);

            if (Projectile.ai[1] >= 300f && !done)
            {
                SoundEngine.PlaySound(SoundID.DoubleJump, Projectile.Center);

                if (Projectile.ai[1] >= 1f && !done)
                {
                    int num328 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X - 49, Projectile.Center.Y - 4f, (0f - (float)Projectile.direction) * 0.01f, 0f, ModContent.ProjectileType<TornadoBombProjectileTornado>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 16f, 15f); //384 //376
                    NetMessage.SendData(MessageID.SyncProjectile, number: num328);
                    Main.projectile[num328].netUpdate = true;

                    SoundEngine.PlaySound(new SoundStyle("Sounds/Custom/Tornado"));
                }

                done = true;
            }

            Projectile.ai[1]++;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
            int num324 = 36;
            for (int num325 = 0; num325 < num324; num325++)
            {
                Vector2 vector17 = Vector2.Normalize(Projectile.velocity) * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f;
                Vector2 spinningpoint26 = vector17;
                double radians26 = (double)((float)(num325 - (num324 / 2 - 1)) * 6.28318548f / (float)num324);
                vector = default(Vector2);
                vector17 = spinningpoint26.RotatedBy(radians26, vector) + Projectile.Center;
                Vector2 vector18 = vector17 - Projectile.Center;
                int num326 = Dust.NewDust(vector17 + vector18, 0, 0, 172, vector18.X * 2f, vector18.Y * 2f, 100, new Color(192, 192, 192), 1.4f);
                Main.dust[num326].noGravity = true;
                Main.dust[num326].noLight = true;
                Main.dust[num326].velocity = vector18;
            }
        }
    }
}