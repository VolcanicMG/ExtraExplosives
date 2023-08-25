using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class MeteoriteBusterProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "Gores/Explosives/meteorite-buster_gore";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("MeteoriteBuster");
        }

        public override void SafeSetDefaults()
        {
            pickPower = 50;
            radius = 30;
            Projectile.tileCollide = true;
            Projectile.width = 28;
            Projectile.height = 30;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 400;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            //SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            Explosion();

            //Create Bomb Dust
            DustEffects();

            ExplosionDamage();

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(2.0f, -2.0f);
            Vector2 gVel2 = new Vector2(-2.0f, 2.0f);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "1").Type, Projectile.scale);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "2").Type, Projectile.scale);
        }

        public override void Explosion()
        {
            Vector2 position = Projectile.Center;
            for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left
            {
                for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Circle
                    {
                        ushort tile = Main.tile[xPosition, yPosition].TileType;
                        if (!CanBreakTile(tile, pickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
                        {
                            if (Main.tile[xPosition, yPosition].TileType == TileID.Meteorite)
                            {
                                WorldGen.KillTile(xPosition, yPosition, false, false, false);  //This make the explosion destroy tiles
                            }
                        }
                        else //Breakable
                        {
                            if (Main.tile[xPosition, yPosition].TileType == TileID.Meteorite)
                            {
                                WorldGen.KillTile(xPosition, yPosition, false, false, false);  //This make the explosion destroy tiles
                            }
                        }
                    }
                }
            }
        }
    }
}