using ExtraExplosives.Items.Explosives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

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
			Main.tileSolid[Type] = false;
			Main.tileSolidTop[Type] = false;
			//Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = false;
			//Main.tileLighted[Type] = true;
			Main.tileWaterDeath[Type] = true;
			Main.tileLavaDeath[Type] = true;
			Main.tileNoAttach[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);

			//AddMapEntry(new Color(444, 222, 435));
		}

		//public override void AnimateTile(ref int frame, ref int frameCounter)
		//{
		//	if (++frameCounter >= 5)
		//	{
		//		frameCounter = 0;
		//		if (++frame >= 4)
		//		{
		//			frame = 1;
		//		}
		//	}
		//}

		public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
		{
			if (!Main.gamePaused && Main.instance.IsActive)
			{
				Dust dust;

				if (Main.rand.NextFloat() < 0.5f)
				{
					dust = Main.dust[Terraria.Dust.NewDust(new Vector2(i * 16, j * 16), 30, 30, 6, -0.2631581f, -2.631579f, 0, new Color(235, 79, 52), 5f)];
					if (Main.rand.Next(3) != 0)
					{
						dust.noGravity = true;
					}
					dust.velocity *= 0.8f;
					dust.velocity.Y = dust.velocity.Y - 1.5f;
				}

				if(Main.rand.NextFloat() < 0.2f)
				{
					dust = Main.dust[Terraria.Dust.NewDust(new Vector2(i * 16, j * 16), 30, 30, 203, -0.2631581f, -2.631579f, 0, new Color(255, 255, 255), 3.4f)];
					dust.noGravity = false;
					dust.velocity *= 0.5f;
					dust.velocity.Y = dust.velocity.Y - 1.5f;
				}

				if (Main.rand.NextFloat() < 0.3f)
				{
					dust = Main.dust[Terraria.Dust.NewDust(new Vector2(i * 16, j * 16), 30, 30, 31, -0.2631581f, -2.631579f, 0, new Color(255, 255, 255), 3.4f)];
					dust.noGravity = false;
					dust.velocity *= 0.4f;
					dust.velocity.Y = dust.velocity.Y - 1.5f;
				}
			}
		}

		public override void RandomUpdate(int i, int j)
		{
			Tile tile = Framing.GetTileSafely(i, j);
			tile.ClearTile();
			tile.active(false);
		}


	}
}