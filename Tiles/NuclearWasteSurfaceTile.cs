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

namespace ExtraExplosives.Tiles
{
    public class NuclearWasteSurfaceTile : ModTile
    {

        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileWaterDeath[Type] = false;
            Main.tileLavaDeath[Type] = false;
            Main.tileNoAttach[Type] = true;

            //drop = mod.ItemType("BasicExplosiveItem");
            //AddMapEntry(new Color(444, 222, 435));

        }

        public override void WalkDust(ref int dustType, ref bool makeDust, ref Color color)
        {
            base.WalkDust(ref dustType, ref makeDust, ref color);
        }

        public override void FloorVisuals(Player player)
        {
            base.FloorVisuals(player);
        }
    }

}