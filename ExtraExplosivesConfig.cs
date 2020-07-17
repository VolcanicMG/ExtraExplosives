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
		[Label("Set Dust/Particle amount; Current Amount")]
		[Tooltip("0 = No Dust; 1 = Lots of Dust")]
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