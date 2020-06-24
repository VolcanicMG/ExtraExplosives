using System;
using System.Collections.Generic;
using ExtraExplosives.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.NPCs.CaptainExplosiveBoss
{
    public class BossDynamiteNPC : ModNPC
    {
        private int _pickPower = 0;
        private bool collide = false;
        private int fuzeTimer = 300;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 67;
        }


        public override void SetDefaults()
        {
            npc.width = 28;
            npc.height = 72;
            npc.Hitbox = new Rectangle(0,0,28, 52);
            npc.lifeMax = 60;
            npc.defense = 0;
            npc.frame.Height = 72;
            npc.frame.Width = 28;
            npc.hide = true;
            drawOffsetY = 20;
            npc.noGravity = false;
            npc.knockBackResist = 0f;
        }
        
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter > 5)    //<--- updates per frame
            {
                npc.frameCounter = 0;
                npc.frame.Y += frameHeight;
                if (npc.frame.Y > 5980) Explode();
                //npc.frame.Y = (frameHeight * 66);    //<----- number of frames (3 not 6 since only 3 are ever used at one time)
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            FindFrame(92);
            Texture2D glow = (npc.direction == -1) ? mod.GetTexture("NPCs/CaptainExplosiveBoss/BossDynamiteNPC_Glowmask") : mod.GetTexture("NPCs/CaptainExplosiveBoss/BossDynamiteNPC_GlowmaskRev");
            Vector2 pos = npc.position - Main.screenPosition;
            pos.Y -= 16;
            Rectangle frame = new Rectangle(0,(int)(npc.frame.Y+92), glow.Width, glow.Height/67);
            spriteBatch.Draw(glow, pos, frame, Color.White, npc.rotation, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }


        public override void DrawBehind(int index)
        {
            Main.instance.DrawCacheNPCsBehindNonSolidTiles.Add(index);
        }


        public void Explode()
        {
            Kill(0);
            npc.life = 0;
        }
        
        public void Kill(int timeLeft)
        {
            for (int i = 10; i > 0; i--)
            {
                Dust.NewDust(npc.position, 4, 4, ModContent.DustType<BossDynamiteDust>());
            }
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)npc.Center.X, (int)npc.Center.Y);

            //Create Bomb Dust
            CreateDust(npc.Center, 25);

            //Create Bomb Damage
            ExplosionDamage(10f, npc.Center, 75, 20, 255);

            //Create Bomb Explosion
            CreateExplosion(npc.Center, 10);
        }


        public override void AI()
        {
            npc.velocity.Y *= 1.05f;
            npc.velocity.X *= 0.98f;
            fuzeTimer--;
            if(fuzeTimer <= 0) Explode();
        }

        public override void PostAI()
        {
            if (!WorldGen.TileEmpty((int) (npc.position.X / 16f), (int) (npc.position.Y / 16f) + 4) && !collide)
            {
                collide = true;
                for (int i = 3; i > 0; i--)
                {
                    WorldGen.KillTile((int) (npc.position.X / 16f) + 1, (int) (npc.position.Y / 16f) + 4, true, true);
                    WorldGen.KillTile((int) (npc.position.X / 16f) + 1, (int) (npc.position.Y / 16f) + 5, true, true);
                    WorldGen.KillTile((int) (npc.position.X / 16f) + 1, (int) (npc.position.Y / 16f) + 6, true, true);
                }
            }
        }


        private void CreateExplosion(Vector2 position, int radius)
        {
            for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left
            {
                for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Circle
                    {
                        ushort tile = Main.tile[xPosition, yPosition].type;
                        if (!CanBreakTile(tile, _pickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
                        {
                        }
                        else //Breakable
                        {
                            WorldGen.KillTile(xPosition, yPosition, false, false, false); //This destroys Tiles
                            if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false); //This destroys Walls
                        }
                    }
                }
            }
        }
        
        private void CreateDust(Vector2 position, int amount)
        {
            Dust dust;
            Vector2 updatedPosition;
	
            for (int i = 0; i <= amount; i++)
            {
                if (Main.rand.NextFloat() < DustAmount)
                {
                    //---Dust 1---
                    if (Main.rand.NextFloat() < 0.2f)
                    {
                        updatedPosition = new Vector2(position.X - 10 / 2, position.Y - 10 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 10, 10, ModContent.DustType<BossDynamiteDust>(), Main.rand.NextFloat(-1,1), Main.rand.NextFloat(-1,1), 0, new Color(255, 0, 0), 1f)];
                        dust.noGravity = true;
                        dust.fadeIn = 2.5f;
                    }
                    //------------

                    //---Dust 2---
                    if (Main.rand.NextFloat() < 0.2f)
                    {
                        updatedPosition = new Vector2(position.X - 10 / 2, position.Y - 10 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 10, 10, ModContent.DustType<BossDynamiteDust>(), Main.rand.NextFloat(-1,1), Main.rand.NextFloat(-1,1), 0, new Color(255, 255, 255), 1f)];
                        dust.noGravity = true;
                        dust.noLight = true;
                    }
                    //------------

                    //---Dust 3---
                    if (Main.rand.NextFloat() < 0.2f)
                    {
                        updatedPosition = new Vector2(position.X - 10 / 2, position.Y - 10 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 10, 10, ModContent.DustType<BossDynamiteDust>(), Main.rand.NextFloat(-1,1), Main.rand.NextFloat(-1,1), 0, new Color(255, 255, 255), 1f)];
                        dust.noGravity = true;
                        dust.noLight = true;
                    }
                    //------------
                }
            }
        }
        
    }
}
