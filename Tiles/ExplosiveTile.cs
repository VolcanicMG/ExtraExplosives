using ExtraExplosives.Items.Explosives;
using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Tiles
{
	public class ExplosiveTile : ModTile
	{
		//public override void SetStaticDefaults()
		//{
		//	DisplayName.SetDefault("Basic Bow Turret");
		//	Tooltip.SetDefault("This is a basic level bow turret.");

		//}

		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileSolidTop[Type] = true;
			//Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = false;
			//Main.tileLighted[Type] = true;
			Main.tileWaterDeath[Type] = true;
			Main.tileLavaDeath[Type] = true;
			Main.tileNoAttach[Type] = true;

			drop = ModContent.ItemType<BasicExplosiveItem>();
			//AddMapEntry(new Color(444, 222, 435));
		}
	}
}