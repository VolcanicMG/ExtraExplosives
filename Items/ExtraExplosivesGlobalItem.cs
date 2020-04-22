using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using System.Text;
using System.Threading.Tasks;

namespace ExtraExplosives.Items
{
    public class ExtraExplosivesGlobalItem : GlobalItem
    {
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("BasicExplosiveItem"), 3);
            recipe.AddIngredient(ItemID.Gel, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.Dynamite);
            recipe.AddRecipe();
            base.AddRecipes();

            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(mod.ItemType("BasicExplosiveItem"), 1);
            recipe2.AddIngredient(ItemID.Grenade, 1);
            recipe2.AddIngredient(ItemID.Gel, 5);
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.SetResult(ItemID.Bomb);
            recipe2.AddRecipe();
            base.AddRecipes();
        }


    }
}
