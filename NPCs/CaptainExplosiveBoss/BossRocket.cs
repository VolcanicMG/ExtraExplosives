using ExtraExplosives.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.NPCs.CaptainExplosiveBoss
{
    public class BossRocket : ModNPC
    {
        private int _pickPower = 0;
        private bool collide = false;
        private int fuzeTimer = 300;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Boss Rocket");
        }


        public override void SetDefaults()
        {
            NPC.width = 104;
            NPC.height = 48;
            //npc.Hitbox = new Rectangle(0, 0, 28, 72);
            NPC.lifeMax = 50;
            NPC.defense = 0;
            //npc.hide = true;
            NPC.friendly = false;
            NPC.noGravity = true;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.damage = 120;
            NPC.noTileCollide = true;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax += 40;
            base.ApplyDifficultyAndPlayerScaling(numPlayers, bossLifeScale);
        }

        //public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        //{
        //   // FindFrame(122);
        //    Texture2D texture = mod.GetTexture("NPCs/CaptainExplosiveBoss/BossRocket");
        //    Vector2 pos = npc.position - Main.screenPosition;
        //    pos.Y -= 16;
        //    Rectangle frame = new Rectangle(0, 0, texture.Width, texture.Height);
        //    spriteBatch.Draw(texture, pos, frame, Color.White, npc.rotation, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        //}

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            Explode();
            base.OnHitPlayer(target, damage, crit);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit)
        {
            Explode();
            base.OnHitNPC(target, damage, knockback, crit);
        }

        //public override void DrawBehind(int index)
        //{
        //    Main.instance.DrawCacheNPCsBehindNonSolidTiles.Add(index);
        //}


        public void Explode()
        {
            ////SoundEngine.PlaySound(SoundLoader.customSoundType, -1, -1, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/BigDynamite")); //sound
            //SoundEngine.PlaySound(new SoundStyle("ExtraExplosives/Assets/Sounds/Custom/BigDynamite"));
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
            //SoundEngine.PlaySound(SoundID.Item14, NPC.Center);

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
            float radius = 7f;
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
            //dust
            float num248 = 0;
            float num249 = 0;

            //Vector2 position71 = new Vector2(npc.position.X + num248, npc.position.Y + num249) - npc.velocity;
            //int width67 = 25;
            //int height67 = 25;
            //Color newColor = default(Color);
            //int num250 = Dust.NewDust(position71, width67, height67, 6, 0f, 0f, 100, newColor, 2.5f);
            //Dust dust3 = Main.dust[num250];
            //dust3.scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
            //dust3 = Main.dust[num250];
            //dust3.velocity *= 0.2f;
            //Main.dust[num250].noGravity = true;
            //Vector2 position72 = new Vector2(npc.position.X + num248, npc.position.Y + num249) - npc.velocity;
            //int width68 = 25;
            //int height68 = 25;
            //newColor = default(Color);
            //num250 = Dust.NewDust(position72, width68, height68, 31, 0f, 0f, 100, newColor, 1f);
            //Main.dust[num250].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
            //dust3 = Main.dust[num250];
            //dust3.velocity *= 0.05f;

            //follow code
            NPC.TargetClosest(true);
            Vector2 vector89 = new Vector2(NPC.Center.X, NPC.Center.Y);
            float num716 = Main.player[NPC.target].Center.X - vector89.X;
            float num717 = Main.player[NPC.target].Center.Y - vector89.Y;
            float num718 = (float)Math.Sqrt((double)(num716 * num716 + num717 * num717));
            float num719 = 30f;
            num718 = num719 / num718;
            num716 *= num718;
            num717 *= num718;
            NPC.velocity.X = ((NPC.velocity.X * 100f + num716) / 101f);
            NPC.velocity.Y = ((NPC.velocity.Y * 100f + num717) / 101f);

            NPC.rotation = NPC.velocity.ToRotation();
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
                int goreIndex = Gore.NewGore(new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
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
