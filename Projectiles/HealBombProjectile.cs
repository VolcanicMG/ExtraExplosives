using ExtraExplosives.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class HealBombProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "Gores/Explosives/basic-explosive_gore";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Heal Bomb");
        }

        public override void SafeSetDefaults()
        {
            pickPower = 0;
            radius = 20;
            Projectile.tileCollide = true;
            Projectile.width = 26;
            Projectile.height = 22;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            //projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 150;
            Projectile.damage = 0;
            Projectile.knockBack = 0;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            //SoundEngine.PlaySound(SoundID.DD2_DarkMageCastHeal, Projectile.Center);

            //Create Bomb Dust
            CreateDust(Projectile.Center, radius + 50);

            //Create Bomb Damage
            ExplosionDamage();

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(-1f, 0f);
            Vector2 gVel2 = new Vector2(0f, -1f);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "1").Type, Projectile.scale);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "2").Type, Projectile.scale);
        }

        public override void ExplosionDamage()
        {
            foreach (Player player in Main.player)
            {
                if (player == null || player.whoAmI == 255 || !player.active) return;
                if (!CanHitPlayer(player)) continue;
                float dist = Vector2.Distance(player.Center, Projectile.Center);
                int dir = (dist > 0) ? 1 : -1;
                if (dist / 16f <= radius)
                {
                    Main.player[player.whoAmI].HealEffect(25, true);
                    player.statLife += 25;
                }
            }
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

                        updatedPosition = new Vector2(position.X - radius * 8, position.Y - radius * 8);
                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, radius * 16, radius * 16, ModContent.DustType<HealBombDust>(), 0f, 0.5263162f, 0, new Color(255, 0, 50), 4.539474f)];
                        if (Vector2.Distance(dust.position, Projectile.Center) > radius * 8) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.fadeIn = 2.5f;
                        }

                    }
                }
            }
        }
    }
}