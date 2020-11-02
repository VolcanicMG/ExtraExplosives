using System;
using System.Collections.Generic;
using ExtraExplosives.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
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
            Main.npcFrameCount[npc.type] = 66;
        }


        public override void SetDefaults()
        {
            npc.width = 28;
            npc.height = 92;
            npc.Hitbox = new Rectangle(0, 0, 28, 72);
            npc.lifeMax = 200;
            npc.defense = 0;
            npc.frame.Height = 92;
            npc.frame.Width = 28;
            npc.hide = true;
            drawOffsetY = 30;
            npc.noGravity = false;
            npc.knockBackResist = 0f;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax += 100;
            base.ScaleExpertStats(numPlayers, bossLifeScale);
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter > 5)    //<--- updates per frame
            {
                npc.frameCounter = 0;
                npc.frame.Y += frameHeight;
                if (npc.frame.Y > 8052) Explode();
                //npc.frame.Y = (frameHeight * 66);    //<----- number of frames (3 not 6 since only 3 are ever used at one time)
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            FindFrame(122);
            Texture2D glow = (npc.direction == -1) ? mod.GetTexture("NPCs/CaptainExplosiveBoss/BossDynamiteNPC_Glowmask") : mod.GetTexture("NPCs/CaptainExplosiveBoss/BossDynamiteNPC_GlowmaskRev");
            Vector2 pos = npc.position - Main.screenPosition;
            pos.Y -= 16;
            Rectangle frame = new Rectangle(0, (int)(npc.frame.Y + 122), glow.Width, glow.Height / 66);
            spriteBatch.Draw(glow, pos, frame, Color.White, npc.rotation, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }


        public override void DrawBehind(int index)
        {
            Main.instance.DrawCacheNPCsBehindNonSolidTiles.Add(index);
        }


        public void Explode()
        {
            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/BigDynamite")); //sound
            Kill(0);
            npc.life = 0;
            npc.active = false;
        }
        
        public void Kill(int timeLeft)
        {
            for (int i = 80; i > 0; i--)
            {
                Dust.NewDust(npc.position, 4, 4, ModContent.DustType<BossDynamiteDust>());
            }
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)npc.Center.X, (int)npc.Center.Y);

            //Create Bomb Dust
            CreateDust(npc.Center, 85);

            //Create Bomb Damage
            //ExplosionDamage(15f, npc.Center, 120, 20, Main.myPlayer);
            ExplosionDamage();

            //Create Bomb Explosion
            if (ExtraExplosives.CheckBossBreak)
            {
                CreateExplosion(npc.Center, 10);
            }
        }

        public override bool CheckDead()
        {
            Explode();
            return base.CheckDead();
        }

        public virtual void ExplosionDamage()
        {
            float radius = 15f;
            foreach (NPC npcID in Main.npc)
            {
                float dist = Vector2.Distance(npcID.Center, npc.Center);
                if (dist / 16f <= radius && !npcID.boss)
                {
                    int dir = (dist > 0) ? 1 : -1;
                    npcID.StrikeNPC(120, 10f, dir, true);
                }
            }

            foreach (Player player in Main.player)
            {
                if (player == null || player.whoAmI == 255 || !player.active) return;
                //if (!CanHitPlayer(player)) continue;
                if (player.EE().BlastShielding &&
                    player.EE().BlastShieldingActive) continue;
                float dist = Vector2.Distance(player.Center, npc.Center);
                int dir = (dist > 0) ? 1 : -1;
                if (dist / 16f <= radius)
                {
                    //Main.NewText("Hit");
                    player.Hurt(PlayerDeathReason.ByNPC(npc.whoAmI), 120, dir);
                    player.hurtCooldowns[0] += 15;
                }
                if (Main.netMode != 0)
                {
                    NetMessage.SendPlayerHurt(player.whoAmI, PlayerDeathReason.ByNPC(npc.whoAmI), 120, dir, false, pvp: false, 0);
                }
            }

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
                Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/BombLanding")); //sound
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
                            //NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, (float)xPosition, (float)yPosition, 0f, 0, 0, 0);
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
                    if (Main.rand.NextFloat() < 1f)
                    {
                        updatedPosition = new Vector2(position.X - 90 / 2, position.Y - 90 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 90, 90, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 1f)];
                        dust.noGravity = true;
                        dust.fadeIn = 0.986842f;
                    }
                    //------------

                    //---Dust 2---
                    if (Main.rand.NextFloat() < 1f)
                    {
                        updatedPosition = new Vector2(position.X - 90 / 2, position.Y - 90 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 90, 90, 203, 0f, 0f, 0, new Color(255, 255, 255), 2f)];
                        dust.noGravity = true;
                        dust.noLight = true;
                    }
                    //------------

                    //---Dust 3---
                    if (Main.rand.NextFloat() < 1f)
                    {
                        updatedPosition = new Vector2(position.X - 90 / 2, position.Y - 90 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 90, 90, 31, 0f, 0f, 0, new Color(255, 255, 255), 1.5f)];
                        dust.noGravity = true;
                        dust.noLight = true;
                    }
                    //------------
                }
            }

            //gore
            for (int g = 0; g < 10; g++)
            {
                int goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
            }
        }
        
    }
}
