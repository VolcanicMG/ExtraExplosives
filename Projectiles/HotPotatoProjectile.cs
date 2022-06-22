using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Effects;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class HotPotatoProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "Gores/Explosives/hot-potato_gore";
        private readonly int _lifeTime = 300 + Main.rand.Next(60);    // How long to keep alive in ticks (currently 5-6 seconds)
        private bool _thrown;    // If the projectile has been thrown yet
        private int _fuze = 30;   // The fuze length 
        private float change;
        private float changeTotal;

        public override bool CloneNewInstances => true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hot Potato");
        }

        public override void SafeSetDefaults()
        {
            pickPower = 0;
            radius = 1;
            Projectile.tileCollide = true;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = _lifeTime + 5; // set higher than _lifeTime
            Projectile.alpha = 0;    // Make it invisible
            Projectile.alpha = 255;    // Make it invisible
            Projectile.velocity = Vector2.Zero;
            Projectile.netUpdate = true;
            Lighting.AddLight(Projectile.Center, 0.5f, 0.5f, 0.3f);
        }

        public override string Texture => "ExtraExplosives/Projectiles/HotPotatoProjectile";

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            if (Main.netMode != NetmodeID.Server && Filters.Scene["BurningScreen"].IsActive() && !_thrown && Main.myPlayer == Projectile.owner)
            {
                float progress = (Projectile.timeLeft) / 60f;
                Filters.Scene["BurningScreen"].GetShader().UseProgress((_lifeTime / 60f) - (_lifeTime - Projectile.localAI[0]) / 60f);
            }

            if (_thrown) return true;

            Projectile.localAI[0]++;
            if (player.releaseUseItem || Projectile.localAI[0] >= _lifeTime - _fuze)
            {
                _thrown = true;
                change = ((_lifeTime - Projectile.localAI[0]) / 60f) / (_lifeTime - Projectile.localAI[0]);
                changeTotal = 0;
                Projectile.alpha = 0;
                Projectile.localAI[1] = Projectile.localAI[0];
                if (Projectile.localAI[0] < _lifeTime - _fuze)
                {
                    float modifier = 0.075f;
                    float screenW = Main.screenWidth;
                    float mouseX = Main.MouseScreen.X - (screenW / 2);
                    float screenH = Main.screenHeight;
                    float mouseY = Main.MouseScreen.Y - (screenH / 2);
                    Projectile.velocity.X = Projectile.localAI[0] / _lifeTime * mouseX * modifier;
                    Projectile.velocity.Y = Projectile.localAI[0] / _lifeTime * mouseY * modifier;
                }
                else
                {
                    Projectile.velocity.Y = 3;
                }
            }
            else
            {
                Projectile.position = player.position;
                int type = Main.rand.Next(2) + 270;
                if (Main.rand.Next(100) < 10) { Dust dust = Main.dust[Terraria.Dust.NewDust(Projectile.position, 70, 70, type, 0f, 0f, 154, new Color(255, 255, 255), 1.55f)]; }

            }
            return _thrown;
        }

        public override void AI()
        {
            if (Main.netMode != NetmodeID.Server && Filters.Scene["BurningScreen"].IsActive() && Main.myPlayer == Projectile.owner)
            {
                changeTotal += change;
                float tmp = (5 - (_lifeTime - Projectile.localAI[0]) / 60f - (changeTotal * (Projectile.localAI[1] - Projectile.localAI[0])));
                if (tmp < 0) tmp = 0;
                Filters.Scene["BurningScreen"].GetShader().UseProgress((tmp > 0) ? tmp : 0);
            }
            Projectile.localAI[1]++;
            if (Projectile.localAI[1] >= _lifeTime)
            {
                Kill(Projectile.timeLeft);    // since this value will vary, just use localAI[0]
            }
            Projectile.velocity.X *= 0.995f;
            if (Projectile.velocity.Y < 20f) Projectile.velocity.Y += 0.08f;
            if (Projectile.velocity.Y < 20f) Projectile.velocity.Y += 0.1f;
        }

        public override void PostAI()
        {
            if (Main.netMode != NetmodeID.Server && !Filters.Scene["BurningScreen"].IsActive() && !_thrown && Main.myPlayer == Projectile.owner)        // Shader stuff
            {
                Filters.Scene.Activate("BurningScreen", Main.player[Projectile.owner].position).GetShader()
                    .UseColor(255f, 0, 0).UseTargetPosition(Main.player[Projectile.owner].position);
            }
        }

        public override void Kill(int ignore)
        {
            if (Main.netMode != NetmodeID.Server && Filters.Scene["BurningScreen"].IsActive() && Main.myPlayer == Projectile.owner)         // Shader stuff
            {
                Filters.Scene["BurningScreen"].Deactivate();
            }

            //Create Bomb Sound
            SoundEngine.PlaySound(SoundID.Item14, (int)Projectile.Center.X, (int)Projectile.Center.Y);

            //Since these values change as the timer ticks down, they need to be set immedietly before an explosion
            // To ensure they are accurate
            Projectile.damage = (int)Projectile.localAI[0];
            Projectile.knockBack = (int)Projectile.localAI[0] / 4f;
            radius = (int)Projectile.localAI[0] / 12;
            //Main.NewText($"Damage: {projectile.damage}, Knockback: {projectile.knockBack}, radius: {radius}");
            Explosion();
            ExplosionDamage();
            CreateDust(Projectile.Center, (int)Projectile.localAI[0] * 2);

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(2f, 2f);
            Vector2 gVel2 = new Vector2(-2f, -1f);
            Gore.NewGore(Projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "1").Type, Projectile.scale);
            Gore.NewGore(Projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "2").Type, Projectile.scale);

            Projectile.timeLeft = 0;
            base.Kill(0);
        }

        private void CreateDust(Vector2 position, int amount)    // TODO UPDATE DUST CODE THIS BIT ACTS STRANGE
        {
            Dust dust;
            Vector2 updatedPosition;

            for (int i = 0; i <= amount; i++)
            {
                if (Main.rand.NextFloat() < DustAmount)
                {
                    //---Dust 1---
                    if (Main.rand.NextFloat() < 0.5f)    // dynamite gibs    // Standard
                    {
                        updatedPosition = new Vector2(position.X - radius * 8, position.Y - radius * 8);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, radius * 16, radius * 16, 90)];
                        if (Vector2.Distance(dust.position, Projectile.Center) > radius * 8) dust.active = false;
                        else dust.noGravity = true;
                    }
                    if (Main.rand.NextFloat() < 0.25f)    // potato gibs    // change if a better dust exists
                    {
                        updatedPosition = new Vector2(position.X - radius * 8, position.Y - radius * 8);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, radius * 16, radius * 16, 216)];//new Color(255, 255, 255)
                        if (Vector2.Distance(dust.position, Projectile.Center) > radius * 8) dust.active = false;
                        else dust.noGravity = true;
                    }
                    //------------
                }
            }
        }
    }
}
