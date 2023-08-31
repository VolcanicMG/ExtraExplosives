using ExtraExplosives.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ExtraExplosives.Tiles
{
    public class NuclearWasteSubSurfaceTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileWaterDeath[Type] = false;
            Main.tileLavaDeath[Type] = false;
            Main.tileNoAttach[Type] = true;
            DustType = DustID.GreenBlood;
            AddMapEntry(new Color(124, 252, 0), Language.GetText("MapEntry"));
            Main.tileBlendAll[Type] = true;
        }

        public override void WalkDust(ref int dustType, ref bool makeDust, ref Color color)
        {
            base.WalkDust(ref dustType, ref makeDust, ref color);
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            //add lighting
            Lighting.AddLight(new Vector2(i, j) * 16, new Vector3(124f / 255f, 252f / 255f, 0f / 255f));
            return base.PreDraw(i, j, spriteBatch);
        }

        public override void FloorVisuals(Player player)
        {
            player.AddBuff(ModContent.BuffType<RadiatedDebuff>(), 1500);
        }
    }
}