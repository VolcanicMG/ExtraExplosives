<<<<<<< HEAD
=======
using Terraria;
>>>>>>> Charlie's-Uploads
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class LightweightBombshells : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightweight Bombshells");
<<<<<<< HEAD
            Tooltip.SetDefault("It should make things easier to throw");
=======
            Tooltip.SetDefault("Double the initial velocity of thrown explosives");
>>>>>>> Charlie's-Uploads
        }

        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;
            item.value = 4000;
            item.maxStack = 1;
            item.rare = ItemRarityID.Orange;
            item.accessory = true;
            item.social = false;
        }
<<<<<<< HEAD
=======
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.EE().LightweightBombshells = true;
        }
>>>>>>> Charlie's-Uploads
    }
}