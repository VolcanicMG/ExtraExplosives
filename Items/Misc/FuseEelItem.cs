using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Misc
{
    public class FuseEelItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fuse Eel");
        }

        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.width = 40;
            item.height = 40;
            item.rare = ItemRarityID.Blue;
        }

        public override void CaughtFishStack(ref int stack)
        {
            stack = 1;
        }
    }
}