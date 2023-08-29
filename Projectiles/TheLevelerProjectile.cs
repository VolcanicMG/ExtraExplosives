using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class TheLevelerProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "ExtraExplosives/Assets/Sounds/Custom/Explosives/The_Leveler_";
        protected override string goreName => "the-leveler_gore";
        //private Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        //private Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

        internal static bool CanBreakWalls;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Leveler");
            //Tooltip.SetDefault("");
        }

        public override void SafeSetDefaults()
        {
            pickPower = 64;
            radius = 20;
            Projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            Projectile.width = 10;   //This defines the hitbox width
            Projectile.height = 10; //This defines the hitbox height
            Projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            Projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            Projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            Projectile.timeLeft = 120; //The amount of time the projectile is alive for
            Projectile.damage = 0;

            DrawOffsetX = -15;
            DrawOriginOffsetY = -15;
            explodeSounds = new SoundStyle[4];
            for (int num = 1; num <= explodeSounds.Length; num++)
            {
                explodeSounds[num - 1] = new SoundStyle(explodeSoundsLoc + num);
            }
        }

        public override void AI()
        {
            Projectile.rotation = 0;
        }

        public override bool OnTileCollide(Vector2 old)
        {
            Projectile.Kill();
            return true;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            //SoundEngine.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)], new Vector2(Projectile.Center.X, Projectile.Center.Y));

            /* ===== ABOUT THE BOMB SOUND =====
			 * 
			 * Because the KillTile() and KillWall() methods used in CreateExplosion()
			 * produce a lot of sounds, the bomb's own explosion sound is difficult to
			 * hear. The solution to eliminate those unnecessary sounds is to alter
			 * the fields of each Tile that the explosion affects, but this creates
			 * additional problems (no dropped Tile items, adjacent Tiles not updating
			 * their sprites, etc). I've decided to ignore doing the changes because
			 * it would entail making the same changes to multiple projectiles and the
			 * projectile template.
			 * 
			 * -- V8_Ninja
			 */

            //Create Bomb Dust
            CreateDust(Projectile.Center, 700);

            //Create Bomb Damage
            //ExplosionDamage(20f * 2f, projectile.Center, 450, 40, projectile.owner);

            //Create Bomb Explosion
            ExplosionTileDamage();

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(4.0f, 4.0f);
            Vector2 gVel2 = new Vector2(0.0f, -4.0f);
            Vector2 gVel3 = new Vector2(-4.0f, 0.0f);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}1").Type, Projectile.scale);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}2").Type, Projectile.scale);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel3), gVel3.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}2").Type, Projectile.scale);
        }

        public override void ExplosionTileDamage()
        {
            Vector2 position = Projectile.Center;

            int x = 0;
            int y = 0;

            int width = 100; //Explosion Width
            int height = 20; //Explosion Height

            for (y = height - 1; y >= 0; y--)
            {
                for (x = -width; x < width; x++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(-y + position.Y / 16.0f);

                    if (!WorldGen.InWorld(xPosition, yPosition)) continue;

                    Tile tile = Framing.GetTileSafely(xPosition, yPosition);

                    if (WorldGen.InWorld(xPosition, yPosition) && tile.HasTile) //Circle
                    {
                        if (!CanBreakTile(tile.TileType, pickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
                        {
                        }
                        else //Breakable
                        {
                            /* TODO Dresser fix if (!TileID.Sets.BasicChest[Main.tile[xPosition, yPosition - 1].TileType] && !TileLoader.IsDresser(Main.tile[xPosition, yPosition - 1].TileType) && Main.tile[xPosition, yPosition - 1].TileType != 26)
                            {
                                tile.ClearTile();
                                tile.HasTile = false;

                            }*/

                            if (tile.LiquidAmount == LiquidID.Water || tile.LiquidAmount == LiquidID.Lava || tile.LiquidAmount == LiquidID.Honey)
                            {
                                WorldGen.SquareTileFrame(xPosition, yPosition, true);
                            }

                            if (Main.netMode == NetmodeID.MultiplayerClient)
                            {
                                WorldGen.SquareTileFrame(xPosition, yPosition, true); //Updates Area
                                NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 2, (float)xPosition, (float)yPosition, 0f, 0, 0, 0);
                            }
                        }

                        if (CanBreakWalls)
                        {
                            WorldGen.KillWall(xPosition, yPosition, false);
                            WorldGen.KillWall(xPosition + 1, yPosition + 1, false); //get the last bit
                        }
                    }
                }
                width++; //Increments width to make stairs on each end
            }
        }

        private void CreateDust(Vector2 position, int amount)
        {
            Vector2 updatedPosition;

            for (int i = 0; i <= amount; i++)
            {
                if (Main.rand.NextFloat() < DustAmount)
                {
                    //---Dust 1---
                    if (Main.rand.NextFloat() < 0.3f)
                    {
                        Dust dust1;
                        Dust dust2;

                        updatedPosition = new Vector2(position.X - 2000 / 2, position.Y - 320);
                        dust1 = Main.dust[Terraria.Dust.NewDust(updatedPosition, 2000, 320, 0, 0f, 0f, 171, new Color(33, 0, 255), 5.0f)];
                        if (Vector2.Distance(dust1.position, Projectile.Center) > 1000) dust1.active = false;
                        else
                        {
                            dust1.noLight = true;
                            dust1.noGravity = true;
                            dust1.shader = GameShaders.Armor.GetSecondaryShader(105, Main.LocalPlayer);
                        }
                        updatedPosition = new Vector2(position.X - 2000 / 2, position.Y - 320);
                        dust2 = Main.dust[Terraria.Dust.NewDust(updatedPosition, 2000, 320, 148, 0f, 0.2631581f, 120, new Color(255, 226, 0), 2.039474f)];
                        if (Vector2.Distance(dust2.position, Projectile.Center) > 1000)
                        {
                            dust2.active = false;
                            continue;
                        }
                        dust2.noGravity = true;
                        dust2.noLight = true;
                        dust2.shader = GameShaders.Armor.GetSecondaryShader(111, Main.LocalPlayer);
                        dust2.fadeIn = 3f;
                    }
                    //------------
                }
            }
        }
    }
}