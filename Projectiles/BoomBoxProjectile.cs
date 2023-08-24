using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class BoomBoxProjectile : ExplosiveProjectile
    {
        private bool setToTrue = false;
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "Gores/Explosives/basic-explosive_gore"; //TODO - needs gores

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boom Box");
        }

        public override void SafeSetDefaults()
        {
            Projectile.tileCollide = true;
            Projectile.width = 40;
            Projectile.height = 28;
            Projectile.aiStyle = 16;
            Projectile.friendly = false;
            Projectile.hostile = false;
            //projectile.penetrate = -1;
            Projectile.timeLeft = 150;
            Projectile.timeLeft = 10000000;
        }
        public override void AI()
        {
            if (!ExtraExplosives.boomBoxMusic && setToTrue)
            {
                Projectile.active = false;
                //Create Bomb Sound
                SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

                //Create Bomb Dust
                CreateDust(Projectile.Center, 10);

                //Create Bomb Gore
                Vector2 gVel1 = new Vector2(-1f, 0f);
                Vector2 gVel2 = new Vector2(0f, -1f);
                Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "1").Type, Projectile.scale);
                Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "2").Type, Projectile.scale);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (!setToTrue)
            {
                ExtraExplosives.boomBoxMusic = true;
                ExtraExplosives.randomMusicID = (Main.rand.Next(41) + 1);
                setToTrue = true;
            }
            return true;
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
                        //updatedPosition = new Vector2(position.X, position.Y);

                        updatedPosition = Projectile.Center;
                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 78, 78, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 4.539474f)];
                        if (Vector2.Distance(dust.position, Projectile.Center) > radius * 16) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.fadeIn = 2.5f;
                        }
                    }

                    //Dust 2
                    if (Main.rand.NextFloat() < 0.6f)
                    {
                        //updatedPosition = new Vector2(position.X - 78 / 2, position.Y - 78 / 2);

                        updatedPosition = Projectile.Center;
                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 78, 78, 203, 0f, 0f, 0, new Color(255, 255, 255), 3.026316f)];
                        if (Vector2.Distance(dust.position, Projectile.Center) > radius * 16) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.noLight = true;
                        }
                    }

                    //Dust 3
                    if (Main.rand.NextFloat() < 0.3f)
                    {
                        //updatedPosition = new Vector2(position.X - 100 / 2, position.Y - 100 / 2);
                        updatedPosition = Projectile.Center;
                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 100, 100, 31, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
                        if (Vector2.Distance(dust.position, Projectile.Center) > radius * 16) dust.active = false;
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