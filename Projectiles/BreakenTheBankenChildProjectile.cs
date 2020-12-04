using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class BreakenTheBankenChildProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BreakenTheBankenChild");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.GoldCoin);
            projectile.tileCollide = true;
            projectile.width = 5;
            projectile.height = 5;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.penetrate = 20;
            projectile.timeLeft = 200;
        }

        public override string Texture => "Terraria/Projectile_" + ProjectileID.GoldCoin;

        public override void Kill(int timeLeft)
        {
            Item.NewItem(projectile.Center, new Vector2(10, 10), ItemID.GoldCoin);
        }
    }
}