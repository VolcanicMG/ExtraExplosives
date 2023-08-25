using ExtraExplosives.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
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
            Main.npcFrameCount[NPC.type] = 66;
        }


        public override void SetDefaults()
        {
            NPC.width = 28;
            NPC.height = 92;
            NPC.Hitbox = new Rectangle(0, 0, 28, 72);
            NPC.lifeMax = 200;
            NPC.defense = 0;
            NPC.frame.Height = 92;
            NPC.frame.Width = 28;
            NPC.hide = true;
            DrawOffsetY = 30;
            NPC.noGravity = false;
            NPC.knockBackResist = 0f;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax += 100;
            base.ScaleExpertStats(numPlayers, bossLifeScale);
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter > 5)    //<--- updates per frame
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y > 8052) Explode();
                //npc.frame.Y = (frameHeight * 66);    //<----- number of frames (3 not 6 since only 3 are ever used at one time)
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            FindFrame(122);
            //Texture2D glow = (NPC.direction == -1) ? Mod.GetTexture("NPCs/CaptainExplosiveBoss/BossDynamiteNPC_Glowmask") : Mod.GetTexture("NPCs/CaptainExplosiveBoss/BossDynamiteNPC_GlowmaskRev");
            Texture2D glow = (NPC.direction == -1)
                ? ModContent.Request<Texture2D>("NPCs/CaptainExplosiveBoss/BossDynamiteNPC_Glowmask").Value
                : ModContent.Request<Texture2D>("NPCs/CaptainExplosiveBoss/BossDynamiteNPC_GlowmaskRev").Value;
            Vector2 pos = NPC.position - Main.screenPosition;
            pos.Y -= 16;
            Rectangle frame = new Rectangle(0, (int)(NPC.frame.Y + 122), glow.Width, glow.Height / 66);
            spriteBatch.Draw(glow, pos, frame, Color.White, NPC.rotation, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }


        public override void DrawBehind(int index)
        {
            Main.instance.DrawCacheNPCsBehindNonSolidTiles.Add(index);
        }


        public void Explode()
        {
            SoundEngine.PlaySound(new SoundStyle("Sounds/Custom/BigDynamite"));
            //SoundEngine.PlaySound(SoundLoader.customSoundType, -1, -1, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/BigDynamite")); //sound
            Kill(0);
            NPC.life = 0;
            NPC.active = false;
        }

        public void Kill(int timeLeft)
        {
            for (int i = 80; i > 0; i--)
            {
                Dust.NewDust(NPC.position, 4, 4, ModContent.DustType<BossDynamiteDust>());
            }
            //Create Bomb Sound
            SoundEngine.PlaySound(SoundID.Item14, NPC.Center);

            //Create Bomb Dust
            CreateDust(NPC.Center, 85);

            //Create Bomb Damage
            //ExplosionDamage(15f, npc.Center, 120, 20, Main.myPlayer);
            ExplosionDamage();

            //Create Bomb Explosion
            if (ExtraExplosives.CheckBossBreak)
            {
                CreateExplosion(NPC.Center, 10);
            }
        }

        public override bool CheckDead()
        {
            Explode();
            return base.CheckDead();
        }

        public virtual void ExplosionDamage()
        {
            float radius = 8f;
            foreach (NPC npcID in Main.npc)
            {
                float dist = Vector2.Distance(npcID.Center, NPC.Center);
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
                float dist = Vector2.Distance(player.Center, NPC.Center);
                int dir = (dist > 0) ? 1 : -1;
                if (dist / 16f <= radius)
                {
                    //Main.NewText("Hit");
                    player.Hurt(PlayerDeathReason.ByNPC(NPC.whoAmI), 120, dir);
                    player.hurtCooldowns[0] += 15;
                }
                if (Main.netMode != 0)
                {
                    NetMessage.SendPlayerHurt(player.whoAmI, PlayerDeathReason.ByNPC(NPC.whoAmI), 120, dir, false, pvp: false, 0);
                }
            }

        }


        public override void AI()
        {
            NPC.velocity.Y *= 1.05f;
            NPC.velocity.X *= 0.98f;
            fuzeTimer--;
            if (fuzeTimer <= 0) Explode();
        }

        public override void PostAI()
        {
            if (!WorldGen.TileEmpty((int)(NPC.position.X / 16f), (int)(NPC.position.Y / 16f) + 4) && !collide)
            {
                collide = true;
                // SoundEngine.PlaySound(SoundLoader.customSoundType, -1, -1, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/BombLanding")); //sound
                SoundEngine.PlaySound(new SoundStyle("ExtraExplosives/Sounds/Custom/BombLanding"));
                for (int i = 3; i > 0; i--)
                {
                    WorldGen.KillTile((int)(NPC.position.X / 16f) + 1, (int)(NPC.position.Y / 16f) + 4, true, true);
                    WorldGen.KillTile((int)(NPC.position.X / 16f) + 1, (int)(NPC.position.Y / 16f) + 5, true, true);
                    WorldGen.KillTile((int)(NPC.position.X / 16f) + 1, (int)(NPC.position.Y / 16f) + 6, true, true);
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
                        ushort tile = Main.tile[xPosition, yPosition].TileType;
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
            /* TODO for (int g = 0; g < 10; g++)
            {
                int goreIndex = Gore.NewGore(Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                goreIndex = Gore.NewGore(new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
            }*/
        }

    }
}
