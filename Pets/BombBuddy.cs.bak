using ExtraExplosives.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Pets
{
    public class BombBuddy : ModProjectile
    {
        private bool firstTick;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bomb Buddy");
            Main.projFrames[projectile.type] = 10;
            Main.projPet[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.CursedSapling);
            aiType = ProjectileID.Bunny;
            projectile.netImportant = true;
            projectile.width = 38;
            projectile.height = 40;
        }

        //public override bool PreAI()
        //{
        //	Player player = Main.player[projectile.owner];
        //	//player.BabyFaceMonster = false; // Relic from aiType
        //	return true;
        //}

        public override void AI()
        {
            Vector2 position = projectile.position;
            Player player = Main.player[projectile.owner];
            ExtraExplosivesPlayer modPlayer = player.GetModPlayer<ExtraExplosivesPlayer>();

            if (!firstTick)
            {
                //Main.NewText(firstTick);
                Projectile.NewProjectile(position.X, position.Y, 0, 0, ModContent.ProjectileType<BombBuddyDetector>(), 50, 0, projectile.owner);
                firstTick = true;
            }
            else if (player.ownedProjectileCounts[ModContent.ProjectileType<BombBuddyDetector>()] == 0 && player.HasBuff(ModContent.BuffType<BombBuddyBuff>()))
            {
                Projectile.NewProjectile(position.X, position.Y, 0, 0, ModContent.ProjectileType<BombBuddyDetector>(), 50, 0, projectile.owner);
            }

            if (player.dead)
            {
                modPlayer.BombBuddy = false;
            }
            if (modPlayer.BombBuddy)
            {
                projectile.timeLeft = 2;
            }

            if (!player.HasBuff(ModContent.BuffType<BombBuddyBuff>()))
            {
                modPlayer.BombBuddy = false;
            }

            modPlayer.BuddyPos = position;
            //Main.NewText(modPlayer.BuddyPos);
        }

        //public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        //{
        //	Vector2 position = projectile.Center;
        //	Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
        //	int radius = 5;	 //this is the explosion radius, the highter is the value the bigger is the explosion

        //	ExplosionDamageProjectile.DamageRadius = (float)(radius * 1.5f);
        //	Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("ExplosionDamageProjectile"), 100, 40, projectile.owner, 0.0f, 0);
        //}
    }
}