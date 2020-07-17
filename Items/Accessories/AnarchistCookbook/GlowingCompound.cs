<<<<<<< HEAD
=======
using Terraria;
>>>>>>> Charlie's-Uploads
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class GlowingCompound : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glowing Compound");
<<<<<<< HEAD
            Tooltip.SetDefault("RADIOACTIVE: DO NO TOUCH");
=======
            Tooltip.SetDefault("Makes your bombs glow");
>>>>>>> Charlie's-Uploads
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
<<<<<<< HEAD
=======
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().GlowingCompound = true;
        }
>>>>>>> Charlie's-Uploads
    }
}