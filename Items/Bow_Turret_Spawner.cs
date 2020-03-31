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

namespace Turretaria.Items
{
	public class Bow_Turret_Spawner : ModItem
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Basic Bow Turret");
            Tooltip.SetDefault("npc is a basic level bow turret.");

        }

        public override void SetDefaults()
        {

            item.width = 24;
			item.height = 24;
			item.maxStack = 999;
			item.value = 50;
			item.rare = 1;
			item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
			item.consumable = true;
            //item.createTile = mod.TileType("Bow_Turret_Tile");

        }

        public override bool UseItem(Player player)
        {
            NPC.NewNPC((int)player.Center.X, (int)player.Center.Y + 20, mod.NPCType("Bow_Turret_AI"), 0, 0f, 0f, 0f, 0f, 255);
            Main.NewText("Turret placed!", (byte)30, (byte)255, (byte)10, false);
            Main.PlaySound(0, (int)player.position.X, (int)player.position.Y, 0);
            return true;

        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.StoneBlock, 3);
			recipe.AddIngredient(ItemID.CopperBow, 1);
			recipe.AddIngredient(ItemID.Wood, 3);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
	}

}