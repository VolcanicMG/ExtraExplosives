using ExtraExplosives.Projectiles;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ExtraExplosives
{
    public class ExtraExplosivesConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        public override void OnLoaded() => ExtraExplosives.EEConfig = this;

        [DefaultValue(typeof(Vector2), "-300, -50")]
        [Range(-1920f, 0f)]
        public Vector2 AnarchistCookbookPos { get; set; }

        public override void OnChanged()
        {
            base.OnChanged();
        }
    }

    public class ExtraExplosivesServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("Explosives Wall Settings")]
        [DefaultValue(true)]
        public bool CanBreakWalls;

        [Header("Explosive Block-Breaking")]
        [DefaultValue(true)]
        public bool CanBreakTiles;

        [Header("Revert Vanilla Bombs Back To Default")]
        [DefaultValue(false)]
        public bool RevertVanillaBombs;

        [Header("Explosives Dust/Particle Settings")]
        [Increment(0.1f)]
        [Range(0f, 1f)]
        [DefaultValue(1f)]
        [Slider]
        public float dustAmount;

        public override void OnChanged()
        {
            GlobalMethods.CanBreakWalls = CanBreakWalls;
            GlobalMethods.CanBreakTiles = CanBreakTiles;
            GlobalMethods.DustAmount = dustAmount;
            GlobalMethods.RevertVanillaBombs = RevertVanillaBombs;

            TheLevelerProjectile.CanBreakWalls = CanBreakWalls;
            //SmallExplosiveProjectile.CanBreakWalls = CanBreakWalls;
            //ArenaBuilderProjectile.CanBreakWalls = CanBreakWalls;
            PhaseBombProjectile.CanBreakWalls = CanBreakWalls;
            //MegaExplosiveProjectile.CanBreakWalls = CanBreakWalls;
            //MediumExplosiveProjectile.CanBreakWalls = CanBreakWalls;
            //LargeExplosiveProjectile.CanBreakWalls = CanBreakWalls;
            //HeavyBombProjectile.CanBreakWalls = CanBreakWalls;
            //GiganticExplosiveProjectile.CanBreakWalls = CanBreakWalls;
            //DaBombProjectile.CanBreakWalls = CanBreakWalls;
            //ClusterBombChildProjectile.CanBreakWalls = CanBreakWalls;
            //ClusterBombProjectile.CanBreakWalls = CanBreakWalls;
            //C4Projectile.CanBreakWalls = CanBreakWalls;
            //BigBouncyDynamiteProjectile.CanBreakWalls = CanBreakWalls;
            //BasicExplosiveProjectile.CanBreakWalls = CanBreakWalls;
            //C4Projectile.CanBreakTiles = CanBreakTiles;
            //DaBombProjectile.CanBreakTiles = CanBreakTiles;
            ExtraExplosives.dustAmount = dustAmount;
        }
    }
}