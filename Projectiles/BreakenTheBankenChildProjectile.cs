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
            Projectile.CloneDefaults(ProjectileID.GoldCoin);
            Projectile.tileCollide = true;
            Projectile.width = 5;
            Projectile.height = 5;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.penetrate = 20;
            Projectile.timeLeft = 200;
        }

        public override string Texture => "Terraria/Projectile_" + ProjectileID.GoldCoin;

        public override void Kill(int timeLeft)
        {
            Item.NewItem(Projectile.Center, new Vector2(10, 10), ItemID.GoldCoin);
        }
    }
}