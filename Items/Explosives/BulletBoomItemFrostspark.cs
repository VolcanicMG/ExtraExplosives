using ExtraExplosives.Projectiles;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Explosives
{
	public class BulletBoomItemFrostspark : ModItem
	{
		private Mod CalamityMod = ModLoader.GetMod("CalamityMod");

		public override void SetStaticDefaults()
		{
			if (CalamityMod != null)
			{
				DisplayName.SetDefault("BulletBoom [c/AC90FF:(Frostspark Bullet)]");
				Tooltip.SetDefault("Who said a gun is the only thing that can shoot a bullet? \n" +
					"Blows up upon touching a block.");
			}
			else
			{
				DisplayName.SetDefault("ModdedItem");
				Tooltip.SetDefault("Enable Calamity to use this item.");
			}
		}

		public override string Texture => "ExtraExplosives/Items/Explosives/BulletBoomItem";

		public override void SetDefaults()
		{
			if (CalamityMod != null)
			{
				item.CloneDefaults(ModContent.ItemType<BulletBoomItem>());
				item.damage = 40;	 //The damage stat for the Weapon.
				item.shoot = ModContent.ProjectileType<BulletBoomProjectileFrostspark>(); //This defines what type of projectile this item will shoot
			}
		}
	}
}