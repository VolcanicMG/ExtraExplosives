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
using ExtraExplosives.Buffs;
using ExtraExplosives.Items.Explosives;

namespace ExtraExplosives.Tiles
{
    public class NuclearWasteSubSurfaceTile : ModTile
    {

        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            //Main.tileSolidTop[Type] = true;
            Main.tileMergeDirt[Type] = true;
            //Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileWaterDeath[Type] = false;
            Main.tileLavaDeath[Type] = false;
            Main.tileNoAttach[Type] = true;
            //Main.tileShine[Type] = 2;
            //Main.shine(new Color(124f, 252f, 0f), 100);
            dustType = DustID.GreenBlood;
            AddMapEntry(new Color(124, 252, 0));
            Main.tileBlendAll[Type] = true;
            //drop = ModContent.ItemType<BasicExplosiveItem>();
            //AddMapEntry(new Color(444, 222, 435));

        }

        public override void WalkDust(ref int dustType, ref bool makeDust, ref Color color)
        {
            base.WalkDust(ref dustType, ref makeDust, ref color);
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            //add lighting
            Lighting.AddLight(new Vector2(i, j) * 16, new Vector3(124f / 255f, 252f / 255f, 0f / 255f));
            Lighting.maxX = 50;
            Lighting.maxY = 50;
            return base.PreDraw(i, j, spriteBatch);
        }

        public override void FloorVisuals(Player player)
        {
            player.AddBuff(ModContent.BuffType<RadiatedDebuff>(), 1500);
        }

    }

}