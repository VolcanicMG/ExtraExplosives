using Microsoft.CodeAnalysis;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Map;

namespace ExtraExplosives.Projectiles.Weapons;

public class SilentCricketProjectile : ExplosiveProjectile
{
    protected override string explodeSoundsLoc { get; } =
        "ExtraExplosives/Assets/Sounds/Item/Weapons/SilentCricket/SilentCricketExplosion";

    protected override string goreName { get; } = "n/a";

    public override void SafeSetDefaults()
    {
        pickPower = 0;
        radius = 10;
        Projectile.tileCollide = true;
        Projectile.width = 0;
        Projectile.height = 0;
        Projectile.friendly = false;
        Projectile.penetrate = -1;
        Projectile.aiStyle = 27;
        Projectile.extraUpdates = 10;
    }

    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(new SoundStyle(explodeSoundsLoc + (Main.rand.Next(2) + 1)), Projectile.Center);
        
        DustEffects();
        
        ExplosionEntityDamage();
    }
}