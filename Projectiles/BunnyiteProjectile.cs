using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    internal class BunnyiteProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "Gores/Explosives/bunnyite_gore";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bunnyite");
        }

        public override void SafeSetDefaults()
        {
            IgnoreTrinkets = true;
            Projectile.tileCollide = true;
            Projectile.width = 10;
            Projectile.height = 32;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 80;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            //Create Bomb Damage
            //ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

            //Create Bomb Explosion
            Explosion();

            //Create Bomb Dust
            CreateDust(Projectile.Center, 50);

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(-3f, -3f);
            Vector2 gVel2 = new Vector2(3f, 3f);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "1").Type, Projectile.scale);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "2").Type, Projectile.scale);
        }

        public override void Explosion()    // custom explosive
        {
            Vector2 position = Projectile.Center;

            int bunnies = 200;

            for (int x = 0; x < bunnies; x++)
            {
                NPC.NewNPC(Projectile.GetSource_FromThis(), (int)position.X + Main.rand.Next(1000) - 500, (int)position.Y, NPCID.Bunny, 0, 0f, 0f, 0f, 0f, 255); //Spawn
            }
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
                        updatedPosition = new Vector2(position.X - 400, position.Y - 50);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 800, 100, 112, 0f, 0f, 0, new Color(255, 0, 0), 1.447368f)];
                        if (Vector2.Distance(dust.position, Projectile.Center) > 400) dust.active = false;
                        {
                            dust.shader = GameShaders.Armor.GetSecondaryShader(36, Main.LocalPlayer);
                            dust.fadeIn = 1.144737f;
                        }
                    }
                    //------------
                }
            }
        }
    }
}