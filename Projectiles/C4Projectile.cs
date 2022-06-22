using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class C4Projectile : ExplosiveProjectile
    {
        //Variables
        protected override string explodeSoundsLoc => "Sounds/Custom/Explosives/C4_";
        protected override string goreFileLoc => "Gores/Explosives/c4_gore";
        private enum C4State
        {
            Airborne,
            Frozen,
            Primed,
            Exploding
        };
        private C4State projState = C4State.Airborne;
        // private bool freeze;
        private ExtraExplosivesPlayer c4Owner;
        private Vector2 positionToFreeze;
        private LegacySoundStyle indicatorSound;
        private LegacySoundStyle primedSound;
        private SoundEffectInstance indicatorSoundInstance;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("C4");
        }

        public override void SafeSetDefaults()
        {
            pickPower = 70;
            radius = 20;
            Projectile.tileCollide = true;
            Projectile.width = 32;
            Projectile.height = 40;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = Int32.MaxValue;
            //projectile.extraUpdates = 1;
            Terraria.ModLoader.SoundType customType = Terraria.ModLoader.SoundType.Custom;
            indicatorSound = Mod.GetLegacySoundSlot(customType, explodeSoundsLoc + "timer");
            primedSound = Mod.GetLegacySoundSlot(customType, explodeSoundsLoc + "time_to_explode");
            if (!Main.dedServ && indicatorSound != null || primedSound != null) //Checking for nulls might fix the error
            {
                indicatorSound = indicatorSound.WithPitchVariance(0f).WithVolume(0.5f);
                primedSound = primedSound.WithPitchVariance(0f).WithVolume(0.5f);
            }
            else if (indicatorSound != null || primedSound != null)
            {
                indicatorSound = Mod.GetLegacySoundSlot(customType, explodeSoundsLoc + "timer");
                primedSound = Mod.GetLegacySoundSlot(customType, explodeSoundsLoc + "time_to_explode");
            }
            explodeSounds = new LegacySoundStyle[4];
            for (int num = 1; num <= explodeSounds.Length; num++)
            {
                explodeSounds[num - 1] = Mod.GetLegacySoundSlot(customType, explodeSoundsLoc + "Bomb_" + num);
            }
        }

        public override bool OnTileCollide(Vector2 old)
        {
            // if (!freeze)
            if (projState == C4State.Airborne)
            {
                // freeze = true;
                projState = C4State.Frozen;
                positionToFreeze = new Vector2(Projectile.position.X, Projectile.position.Y);
                Projectile.position.X = positionToFreeze.X;
                Projectile.position.Y = positionToFreeze.Y;
                Projectile.velocity.X = 0;
                Projectile.velocity.Y = 0;
                //projectile.rotation = 0;
            }

            return false;
        }

        public override void PostAI()
        {
            switch (projState)
            {
                case C4State.Airborne:
                    if (Projectile.owner == Main.myPlayer && c4Owner == null)
                    {
                        c4Owner = Main.player[Projectile.owner].GetModPlayer<ExtraExplosivesPlayer>();
                    }
                    break;
                case C4State.Frozen:
                    Projectile.position = positionToFreeze;
                    Projectile.velocity = Vector2.Zero;
                    if (indicatorSoundInstance == null)
                        indicatorSoundInstance = SoundEngine.PlaySound(indicatorSound, (int)Projectile.Center.X, (int)Projectile.Center.Y);
                    else if (indicatorSoundInstance.State != SoundState.Playing)    // else if needed to avoid a NullReferenceException
                        indicatorSoundInstance.Play();
                    if (c4Owner != null && c4Owner.detonate)
                    {
                        projState = C4State.Primed;
                        Projectile.ai[1] = 55;
                        SoundEngine.PlaySound(primedSound, (int)Projectile.position.X, (int)Projectile.position.Y);
                    }
                    break;
                case C4State.Primed:
                    Projectile.ai[1]--;
                    if (Projectile.ai[1] < 1)
                    {
                        projState = C4State.Exploding;
                    }
                    break;
                case C4State.Exploding:
                    Projectile.Kill();
                    break;
            }
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            SoundEngine.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)], (int)Projectile.Center.X, (int)Projectile.Center.Y);

            //Create Bomb Dust
            DustEffects();

            Explosion();

            ExplosionDamage();

            //Creating Bomb Gore
            Vector2 gVel1 = new Vector2(-4f, -4f);
            Vector2 gVel2 = new Vector2(4f, -4f);
            Gore.NewGore(Projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "1").Type, Projectile.scale);
            Gore.NewGore(Projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "2").Type, Projectile.scale);
        }

        public override void Explosion()
        {
            if (Main.player[Projectile.owner].EE().BombardEmblem) return;
            Vector2 position = Projectile.Center;
            for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left
            {
                for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    Tile tile = Framing.GetTileSafely(xPosition, yPosition);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Circle
                    {
                        ushort tileP = tile.TileType;
                        if (!CanBreakTile(tileP, pickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
                        {
                        }
                        else //Breakable
                        {
                            if (CanBreakTiles) //User preferences dictates if bombs can break tiles
                            {
                                if (!TileID.Sets.BasicChest[Main.tile[xPosition, yPosition - 1].TileType] && !TileLoader.IsDresser(Main.tile[xPosition, yPosition - 1].TileType))
                                {
                                    tile.ClearTile();
                                    tile.HasTile = false;
                                }
                                if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false); //This destroys Walls
                            }
                        }
                    }
                }
            }
        }
    }
}