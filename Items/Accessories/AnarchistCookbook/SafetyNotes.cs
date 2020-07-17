<<<<<<< HEAD
=======
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
>>>>>>> Charlie's-Uploads
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class SafetyNotes : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Safety Notes");
<<<<<<< HEAD
            Tooltip.SetDefault("Chicken scratch");
        }
=======
            Tooltip.SetDefault("Prevents self damage from friendly explosives\n" +
                               "Increased damage output and defence");
        }
        
>>>>>>> Charlie's-Uploads

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.value = 10000;
            item.maxStack = 1;
            item.rare = ItemRarityID.Yellow;
            item.accessory = true;
            item.social = false;
        }

        public override void AddRecipes()
        {
            ModRecipe modRecipe = new ModRecipe(mod);
            modRecipe.AddIngredient(ModContent.ItemType<BlastShielding>());
            modRecipe.AddIngredient(ModContent.ItemType<ReactivePlating>());
            modRecipe.AddTile(TileID.TinkerersWorkbench);
            modRecipe.SetResult(this);
            modRecipe.AddRecipe();
        }
<<<<<<< HEAD
=======
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().BlastShielding = true;
            player.GetModPlayer<ExtraExplosivesPlayer>().ReactivePlating = true;
        }
>>>>>>> Charlie's-Uploads
    }
}