using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class GlowingCompound : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glowing Compound");
            Tooltip.SetDefault("Makes your bombs glow");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 4000;
            item.maxStack = 1;
            item.rare = ItemRarityID.Purple;
            item.accessory = true;
            item.social = false;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().GlowingCompound = true;
        }
    }
}