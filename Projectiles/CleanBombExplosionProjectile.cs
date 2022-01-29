using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    internal class CleanBombExplosionProjectile : ModProjectile
    {
        //Variables:
        private List<int> buffExceptions = new List<int>();
        private int buffCount = 206;        // NEEDS TO BE UPDATED WHEN TMODLOADER SUPPORTS V1.4

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("CleanBombExplosionProjectile");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = false;
            projectile.width = 1;
            projectile.height = 1;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 10;
            projectile.Opacity = 0f;
            projectile.scale = CleanBombProjectile.Radius;      // Damage Radius

            //Defining the buff removal exceptions
            buffExceptions.Add(BuffID.PotionSickness);
            buffExceptions.Add(BuffID.Horrified);
            buffExceptions.Add(BuffID.TheTongue);
            buffExceptions.Add(BuffID.Suffocation);
            buffExceptions.Add(BuffID.NoBuilding);
        }

        public override string Texture => "ExtraExplosives/Projectiles/ExplosionDamageProjectile";

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            int buffIndex = -1;
            for (int num = 1; num <= buffCount; num++)
            {
                buffIndex = target.FindBuffIndex(num);
                if (buffIndex > -1 && !buffExceptions.Contains(num))
                {
                    target.DelBuff(buffIndex);
                }
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            int buffIndex = -1;
            for (int num = 1; num <= buffCount; num++)
            {
                buffIndex = target.FindBuffIndex(num);
                if (buffIndex > -1 && !buffExceptions.Contains(num))
                {
                    target.DelBuff(buffIndex);
                }
            }
        }
    }
}
