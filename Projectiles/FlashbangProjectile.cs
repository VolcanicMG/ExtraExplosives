using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
	internal class FlashbangProjectile : ExplosiveProjectile
	{
		protected override string explodeSoundsLoc => "n/a";
		protected override string goreFileLoc => "n/a";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flashbang");
		}

		public override void SafeSetDefaults()
		{
			IgnoreTrinkets = true;
			projectile.tileCollide = true;
			projectile.width = 12;
			projectile.height = 32;
			projectile.aiStyle = 16;
			projectile.penetrate = -1;
			projectile.timeLeft = 60;
			projectile.damage = 0;
		}

		public override void Kill(int timeLeft)
		{
			//add lighting
			Lighting.AddLight(projectile.position, new Vector3(255f, 255f, 255f));
			Lighting.maxX = 100;
			Lighting.maxY = 100;

			Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y); //Sound Effect
			Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Flashbang"), (int)projectile.Center.X, (int)projectile.Center.Y); //Custom Sound Effect

			
			//Projectile.NewProjectile(projectile.Center.X - 450, projectile.Center.Y, 0, 0, ModContent.ProjectileType<InvisFlashbangProjectile>(), 1, 0, projectile.owner, 0.0f, 0); //Left
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, ModContent.ProjectileType<InvisFlashbangProjectile>(), 1, 1, projectile.owner, 0.0f, 0);
		}
	}
}