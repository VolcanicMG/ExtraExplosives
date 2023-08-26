using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class GlowingCompound : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Glowing Compound");
            // Tooltip.SetDefault("Makes your bombs glow");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = 4000;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.social = false;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().GlowingCompound = true;
        }
    }
}