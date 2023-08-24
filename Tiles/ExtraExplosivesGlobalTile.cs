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
        int cntr = 0;
        public override bool Drop(int i, int j, int type)
        {
            if (type == TileID.ShadowOrbs) //Called 5 times once it breaks for some reason (I think its because it breaks all 4-5 of the pieces at once)
            {
                cntr++;
                if (Main.rand.NextFloat() <= .15f && cntr == 5)
                {
                    Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), new Vector2(i * 16, j * 16), new Vector2(0, 0), ModContent.ItemType<TwinDetonator>());
                    Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), new Vector2(i * 16, j * 16), new Vector2(0, 0), ModContent.ItemType<Rocket0Point5>(), 30);
                    cntr = 0;
                    return false;
                }
                if (cntr <= 5)
                {
                    cntr = 0;
                }
            }

            return true;
        }
    }
}