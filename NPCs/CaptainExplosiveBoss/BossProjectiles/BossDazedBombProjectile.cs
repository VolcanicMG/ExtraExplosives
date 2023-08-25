using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.NPCs.CaptainExplosiveBoss.BossProjectiles
{
    public class BossDazedBombProjectile : ModProjectile
    {
        //private Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        //private Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dazed Bomb");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            Projectile.width = 28;   //This defines the hitbox width
            Projectile.height = 44; //This defines the hitbox height
            Projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            Projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            Projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            Projectile.timeLeft = 150; //The amount of time the projectile is alive for
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 8)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 4)
                {
                    Projectile.frame = 0;
                }
            }

            if (Projectile.timeLeft <= 3)
            {
                Projectile.hostile = true;
                Projectile.friendly = false;
                Projectile.tileCollide = false;
                // Set to transparent. This projectile technically lives as  transparent for about 3 frames
                Projectile.alpha = 255;
                // change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
                Projectile.position = Projectile.Center;
                //projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
                //projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
                Projectile.width = 450;
                Projectile.height = 450;
                Projectile.Center = Projectile.position;
                //projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
                //projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
                Projectile.damage = 25;
                Projectile.knockBack = 10f;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Projectile.timeLeft <= 3)
            {
                target.AddBuff(BuffID.Dazed, 500);
            }
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            //Create Bomb Dust
            CreateDust(Projectile.Center, 500);

            Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);

        }


        private void CreateDust(Vector2 position, int amount)
        {
            Dust dust;
            Vector2 updatedPosition;

            for (int i = 0; i <= amount; i++)
            {
                if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
                {
                    //Dust 1
                    if (Main.rand.NextFloat() < 0.9f)
                    {
                        updatedPosition = new Vector2(position.X - 500 / 2, position.Y - 500 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 500, 500, 64, 0f, 0f, 0, new Color(255, 226, 0), 5f)];
                        if (Vector2.Distance(dust.position, Projectile.Center) > 15 * 16) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.fadeIn = 2.5f;
                        }
                    }

                    //Dust 2
                    if (Main.rand.NextFloat() < 0.6f)
                    {
                        updatedPosition = new Vector2(position.X - 500 / 2, position.Y - 500 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 500, 500, 203, 0f, 0f, 0, new Color(255, 255, 255), 3.026316f)];
                        if (Vector2.Distance(dust.position, Projectile.Center) > 15 * 16) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.noLight = true;
                        }
                    }

                    //Dust 3
                    if (Main.rand.NextFloat() < 0.3f)
                    {
                        updatedPosition = new Vector2(position.X - 500 / 2, position.Y - 500 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 500, 500, 31, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
                        if (Vector2.Distance(dust.position, Projectile.Center) > 15 * 16) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.noLight = true;
                        }
                    }
                }
            }
        }
    }
}