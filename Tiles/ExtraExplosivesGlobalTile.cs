using ExtraExplosives.Items.Rockets;
using ExtraExplosives.Items.Weapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Tiles
{
    public class ExtraExplosivesGlobalTile : GlobalTile
    {
        public override void Drop(int i, int j, int type)
        {
            if (type == TileID.ShadowOrbs) //Called 5 times once it breaks for some reason (I think its because it breaks all 4-5 of the pieces at once)
            {
                if (Main.rand.NextFloat() <= .15f)
                {
                    Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), new Vector2(i * 16, j * 16), new Vector2(0, 0), ModContent.ItemType<TwinDetonator>());
                    Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), new Vector2(i * 16, j * 16), new Vector2(0, 0), ModContent.ItemType<Rocket0Point5>(), 30);
                }
            }
        }
    }
}