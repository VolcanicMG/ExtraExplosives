using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Misc
{
    public class FuseEelItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fuse Eel");
        }

        public override void SetDefaults()
        {
            Item.maxStack = 99;
            Item.width = 40;
            Item.height = 40;
            Item.rare = ItemRarityID.Blue;
        }

        public override void CaughtFishStack(ref int stack)
        {
            stack = 1;
        }
    }
}