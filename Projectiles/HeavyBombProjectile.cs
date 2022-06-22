using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    //TODO Possible make Explosion() and ExplosionDamage() unique to cut down on radius and damage resets
    public class HeavyBombProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "Gores/Explosives/heavy_gore";

        //Used to track when a tile can be destroyed
        private float counter
        {
            get => Projectile.localAI[0];
            set => Projectile.localAI[0] = value;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("HeavyBomb");
        }

        public override void SafeSetDefaults()
        {
            pickPower = 50;
            radius = 2;
            Projectile.tileCollide = true;
            Projectile.width = 13;
            Projectile.height = 19;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1000;
        }

        public override void PostAI()
        {
            if (counter > 0)
            {
                counter--;
                return;
            }

            counter = 5; // Timer so dust doesnt get spammed
        }

        public override bool OnTileCollide(Vector2 old)
        {
            Vector2 positionS = new Vector2(Projectile.Center.X / 16f, Projectile.Center.Y / 16f);    // Converts to tile cords for convenience
            Tile tileS = Framing.GetTileSafely((int)positionS.X, (int)positionS.Y + 1);

            if (tileS.HasTile && CanBreakTile(tileS.TileType, pickPower))
            {
                Projectile.velocity.Y = -0.8f * old.Y;
                if (Projectile.velocity.Y > 10) Projectile.velocity.Y = 10;
                //Create Bomb Sound
                SoundEngine.PlaySound(SoundID.Item37, (int)Projectile.Center.X, (int)Projectile.Center.Y);

                //Create Bomb Damage
                ExplosionDamage();

                //Create Bomb Explosion
                Vector2 position = Projectile.Center;

                if (!Main.player[Projectile.owner].EE().BombardEmblem)  // Skip this if the emblem is equipped
                {
                    for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left
                    {
                        for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
                        {
                            int xPosition = (int)(x + position.X / 16.0f);
                            int yPosition = (int)(y + position.Y / 16.0f);

                            if (Math.Sqrt(x * x + y * y) <= radius + 0.5 &&
                                (WorldGen.InWorld(xPosition, yPosition))) //Circle
                            {
                                ushort tile = Main.tile[xPosition, yPosition].TileType;
                                if (!CanBreakTile(tile, pickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
                                {
                                }
                                else //Breakable
                                {
                                    WorldGen.KillTile(xPosition, yPosition, false, false, false);
                                }
                            }
                        }
                    }
                }

                if (counter >= 1) return false; // if the dust counter hasn't reset, dont spawn dust

                //Create Bomb Dust
                for (int i = 0; i < 10; i++)
                {
                    if (Main.rand.NextFloat() < DustAmount)
                    {
                        Dust dust = Main.dust[
                            Terraria.Dust.NewDust(new Vector2(position.X - (30 / 2), position.Y - 0), 30, 30, 1, 0f, 0f, 0,
                                new Color(255, 255, 255), 1f)];
                    }
                }
            }

            return false;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            SoundEngine.PlaySound(SoundID.Item14, (int)Projectile.Center.X, (int)Projectile.Center.Y);


            radius = 20;
            Projectile.damage = 500; // Done because two different damage values are required and there is not clean way to alter then besides this

            //Create Bomb Dust
            DustEffects();

            Explosion();
            ExplosionDamage();

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(0f, 2f);
            Vector2 gVel2 = new Vector2(-2f, 2f);
            Gore.NewGore(Projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "1").Type, Projectile.scale);
            Gore.NewGore(Projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "2").Type, Projectile.scale);
        }
    }
}