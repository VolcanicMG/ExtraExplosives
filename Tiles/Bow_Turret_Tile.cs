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

namespace Turretaria.Tiles
{
    public class Bow_Turret_Tile : ModTile
    {
        //public override void SetStaticDefaults()
        //{
        //	DisplayName.SetDefault("Basic Bow Turret");
        //	Tooltip.SetDefault("This is a basic level bow turret.");

        //}

        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            //Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = false;
            //Main.tileLighted[Type] = true;
            Main.tileWaterDeath[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileNoAttach[Type] = true;

            drop = mod.ItemType("Bow_Turret_Spawner");
            //AddMapEntry(new Color(444, 222, 435));

        }

    }

}