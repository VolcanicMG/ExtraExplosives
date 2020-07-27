using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles.Weapons.TrashCannonProjectiles
{
    public class TrashCannonProjectile : ModProjectile
    {
        //protected override string explodeSoundsLoc { get; } = "n/a";
        //protected override string goreFileLoc { get; } = "n/a";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Trash Cannon Projectile");
            Main.projFrames[projectile.type] = 12;
        }

        public override void SetDefaults()
        {
            projectile.height = 22;
            projectile.width = 22;
            projectile.aiStyle = 2;
            projectile.timeLeft = 120;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.frame = Main.rand.Next(13);
        }
    }
}