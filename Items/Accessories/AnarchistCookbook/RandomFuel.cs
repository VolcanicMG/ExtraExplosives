<<<<<<< HEAD
=======
using Terraria;
>>>>>>> Charlie's-Uploads
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class RandomFuel : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Random Fuel");
<<<<<<< HEAD
            Tooltip.SetDefault("Questionable but flammable");
=======
            Tooltip.SetDefault("Randomly debuffs enemies\n" +
                               "Enemies can be burnt, frozen, or confused\n" +
                               "Debuffs can affect the player");
>>>>>>> Charlie's-Uploads
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 26;
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
            player.GetModPlayer<ExtraExplosivesPlayer>().RandomFuel = true;
        }
>>>>>>> Charlie's-Uploads
    }
}