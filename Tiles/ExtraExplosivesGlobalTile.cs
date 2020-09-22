using ExtraExplosives.Items.Weapons;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Tiles
{
    public class ExtraExplosivesGlobalTile : GlobalTile
    {
        public override bool Drop(int i, int j, int type)
        {
            if (type == TileID.ShadowOrbs)
            {
                if (Main.rand.Next(15) + 1 == 1)
                {
                    Item.NewItem(new Vector2(i * 16, j * 16), new Vector2(0, 0), ModContent.ItemType<TwinDetonator>());
                    return false;
                }
            }
            return base.Drop(i, j, type);
        }
    }
}