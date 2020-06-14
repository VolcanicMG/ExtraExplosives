using ExtraExplosives.Projectiles;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Explosives
{
	public class BulletBoomItemTerrarium : ModItem
	{
		private Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

		public override void SetStaticDefaults()
		{
			if (ThoriumMod != null)
			{
				DisplayName.SetDefault("BulletBoom [c/AC90FF:(Terrarium Pulser Shot)]");
				Tooltip.SetDefault("Who said a gun is the only thing that can shoot a bullet? \n" +
					"Blows up upon touching a block.");
			}
			else
			{
				DisplayName.SetDefault("ModdedItem");
				Tooltip.SetDefault("Enable Thorium to use this item.");
			}
		}

		public override string Texture => "ExtraExplosives/Items/Explosives/BulletBoomItem";

		public override void SetDefaults()
		{
			if (ThoriumMod != null)
			{
				item.CloneDefaults(ModContent.ItemType<BulletBoomItem>());
				item.damage = 50;	 //The damage stat for the Weapon.
				item.shoot = ModContent.ProjectileType<BulletBoomProjectileTerrarium>(); //This defines what type of projectile this item will shoot
			}
		}
	}
}