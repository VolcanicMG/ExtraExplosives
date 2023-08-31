using ExtraExplosives.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Tiles
{
    public class NuclearWasteSurfaceTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileWaterDeath[Type] = false;
            Main.tileLavaDeath[Type] = false;
            Main.tileNoAttach[Type] = true;
            DustType = DustID.GreenBlood;
            AddMapEntry(new Color(124, 252, 0));
            Main.tileBlendAll[Type] = true;
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            //add lighting
            Lighting.AddLight(new Vector2(i, j) * 16, new Vector3(124f / 255f, 252f / 255f, 0f / 255f));

            return base.PreDraw(i, j, spriteBatch);
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
        {
            if (!Main.gamePaused && Main.instance.IsActive)
            {
                Dust dust;

                if (Main.rand.NextFloat() < 0.02631579f)
                {
                    dust = Main.dust[Dust.NewDust(new Vector2(i * 16, j * 16), 30, 30, 74, -0.2631581f, -2.631579f, 0, new Color(255, 255, 255), 1f)];
                    dust.noGravity = true;
                    dust.fadeIn = 0.9473684f;
                }
            }
        }

        public override void FloorVisuals(Player player)
        {
            player.AddBuff(ModContent.BuffType<RadiatedDebuff>(), 1500);
        }
    }
}