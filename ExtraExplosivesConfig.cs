using ExtraExplosives.Projectiles;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ExtraExplosives
{
	public class ExtraExplosivesConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide; //Change to client to make it only applicable to the client side

		[Header("Explosives Wall Settings")]
		[Label("Toggle Wall Breaking")]
		[DefaultValue(true)]
		public bool CanBreakWalls;

		[Header("Explosives Block Settings")]
		[Label("Toggle Block Breaking for the C4, Da Bomb, and Captain Explosive")]
		[DefaultValue(true)]
		public bool CanBreakTiles;

		[Header("Explosives Dust/Particle Settings")]
		[Label("Set Dust/Particle amount" +
			"\n1 = Max" +
			"\n0 = None" +
			"\nCurrent amount")]
		[Increment(0.1f)]
		[Range(0f, 1f)]
		[DefaultValue(1f)]
		[Slider]
		public float dustAmount;
		
		[Header("Dynamic Bullet Boom Integration")]
		[Label("Use ammo from other mods in bullet booms? (Reload Required)")]
		[Tooltip("Developmental Feature")]
		[DefaultValue(true)]
		public bool generateForeignBulletBooms;

		public override void OnChanged()
		{
			GlobalMethods.CanBreakWalls = CanBreakWalls;
			GlobalMethods.CanBreakTiles = CanBreakTiles;
			GlobalMethods.DustAmount = dustAmount;

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
			ExtraExplosives.generateForeignBulletBooms = generateForeignBulletBooms;
		}
	}
}