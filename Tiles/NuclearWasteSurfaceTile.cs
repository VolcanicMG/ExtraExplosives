using ExtraExplosives.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Tiles
{
	public class NuclearWasteSurfaceTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			//Main.tileSolidTop[Type] = true;
			Main.tileMergeDirt[Type] = true;
			//Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileWaterDeath[Type] = false;
			Main.tileLavaDeath[Type] = false;
			Main.tileNoAttach[Type] = true;
			//Main.tileShine[Type] = 2;
			//Main.shine(new Color(124f, 252f, 0f), 100);
			dustType = DustID.GreenBlood;
			AddMapEntry(new Color(124, 252, 0));
			Main.tileBlendAll[Type] = true;
			//drop = ModContent.ItemType<BasicExplosiveItem>();
			//AddMapEntry(new Color(444, 222, 435));
		}

		public override void WalkDust(ref int dustType, ref bool makeDust, ref Color color)
		{
			base.WalkDust(ref dustType, ref makeDust, ref color);
		}

		public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
		{
			//add lighting
			Lighting.AddLight(new Vector2(i, j) * 16, new Vector3(124f / 255f, 252f / 255f, 0f / 255f));
			Lighting.maxX = 50;
			Lighting.maxY = 50;

			return base.PreDraw(i, j, spriteBatch);
		}

		public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
		{
			if (!Main.gamePaused && Main.instance.IsActive)
			{
				Dust dust;

				if (Main.rand.NextFloat() < 0.02631579f)
				{
					dust = Main.dust[Terraria.Dust.NewDust(new Vector2(i * 16, j * 16), 30, 30, 74, -0.2631581f, -2.631579f, 0, new Color(255, 255, 255), 1f)];
					dust.noGravity = true;
					dust.fadeIn = 0.9473684f;
				}
			}
		}

		public override void FloorVisuals(Player player)
		{
			player.AddBuff(ModContent.BuffType<RadiatedDebuff>(), 1500);
		}
	}
}