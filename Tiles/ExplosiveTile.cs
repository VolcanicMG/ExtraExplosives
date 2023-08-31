using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ExtraExplosives.Tiles
{
    
    // TODO is this ever used?? it doesnt look like it is
    // Turning off loading so that it tml will ignore it, but so its still here if we do actually need it
    [Autoload(false)]
    public class ExplosiveTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Mod.Logger.Warn("Class: " + this.ToString() + " should not be loaded by TModLoader! Please check why this class is being loaded!");
            Main.tileSolid[Type] = false;
            Main.tileSolidTop[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileWaterDeath[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);

            //AddMapEntry(new Color(444, 222, 435));
        }

        //public override void AnimateTile(ref int frame, ref int frameCounter)
        //{
        //	if (++frameCounter >= 5)
        //	{
        //		frameCounter = 0;
        //		if (++frame >= 4)
        //		{
        //			frame = 1;
        //		}
        //	}
        //}

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
        {
            if (!Main.gamePaused && Main.instance.IsActive)
            {
                Dust dust;

                if (Main.rand.NextFloat() < 0.5f)
                {
                    dust = Main.dust[Terraria.Dust.NewDust(new Vector2(i * 16, j * 16), 30, 30, 6, -0.2631581f, -2.631579f, 0, new Color(235, 79, 52), 5f)];
                    if (Main.rand.Next(3) != 0)
                    {
                        dust.noGravity = true;
                    }
                    dust.velocity *= 0.8f;
                    dust.velocity.Y = dust.velocity.Y - 1.5f;
                }

                if (Main.rand.NextFloat() < 0.2f)
                {
                    dust = Main.dust[Terraria.Dust.NewDust(new Vector2(i * 16, j * 16), 30, 30, 203, -0.2631581f, -2.631579f, 0, new Color(255, 255, 255), 3.4f)];
                    dust.noGravity = false;
                    dust.velocity *= 0.5f;
                    dust.velocity.Y = dust.velocity.Y - 1.5f;
                }

                if (Main.rand.NextFloat() < 0.3f)
                {
                    dust = Main.dust[Terraria.Dust.NewDust(new Vector2(i * 16, j * 16), 30, 30, 31, -0.2631581f, -2.631579f, 0, new Color(255, 255, 255), 3.4f)];
                    dust.noGravity = false;
                    dust.velocity *= 0.4f;
                    dust.velocity.Y = dust.velocity.Y - 1.5f;
                }
            }
        }

        public override void RandomUpdate(int i, int j)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            tile.ClearTile();
            tile.HasTile = false;
        }


    }
}