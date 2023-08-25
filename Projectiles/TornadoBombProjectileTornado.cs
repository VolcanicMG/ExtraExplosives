using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class TornadoBombProjectileTornado : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "n/a";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tornado");
            Main.projFrames[Projectile.type] = 6;
        }

        public override void DangerousSetDefaults()
        {
            Projectile.CloneDefaults(386);
            //projectile.tileCollide = true;
            //projectile.width = 162;
            //projectile.height = 42;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.minion = false;
            Projectile.penetrate = -1;
            //projectile.penetrate = -1;
            Projectile.timeLeft = 560;
            Projectile.damage = 20;
        }

        public override void AI()
        {
            int num612 = 10;
            int num613 = 15;
            float num614 = 1f;
            int num615 = 150;
            int num616 = 42;

            Projectile.damage = 20;
            //if (projectile.type == 386)
            //{
            //	num612 = 16;
            //	num613 = 16;
            //	num614 = 1.5f;
            //}
            if (Projectile.velocity.X != 0f)
            {
                Projectile.direction = (Projectile.spriteDirection = -Math.Sign(Projectile.velocity.X));
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame >= 6)
            {
                Projectile.frame = 0;
            }
            if (Projectile.localAI[0] == 0f && Main.myPlayer == Projectile.owner)
            {
                Projectile.localAI[0] = 1f;
                Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
                Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
                Projectile.scale = ((float)(num612 + num613) - Projectile.ai[1]) * num614 / (float)(num613 + num612);
                Projectile.width = (int)((float)num615 * Projectile.scale);
                Projectile.height = (int)((float)num616 * Projectile.scale);
                Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
                Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
                Projectile.netUpdate = true;
            }
            if (Projectile.ai[1] != -1f)
            {
                Projectile.scale = ((float)(num612 + num613) - Projectile.ai[1]) * num614 / (float)(num613 + num612);
                Projectile.width = (int)((float)num615 * Projectile.scale);
                Projectile.height = (int)((float)num616 * Projectile.scale);
            }
            if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                Projectile.alpha -= 30;
                if (Projectile.alpha < 60)
                {
                    Projectile.alpha = 60;
                }
                //if (projectile.type == 386 && projectile.alpha < 100)
                //{
                //	projectile.alpha = 100;
                //}
            }
            else
            {
                Projectile.alpha += 30;
                if (Projectile.alpha > 150)
                {
                    Projectile.alpha = 150;
                }
            }
            if (Projectile.ai[0] > 0f)
            {
                Projectile.ai[0] -= 1f;
            }
            if (Projectile.ai[0] == 1f && Projectile.ai[1] > 0f && Projectile.owner == Main.myPlayer)
            {
                Projectile.netUpdate = true;
                Vector2 center4 = Projectile.Center;
                center4.Y -= (float)num616 * Projectile.scale / 2f;
                float num617 = ((float)(num612 + num613) - Projectile.ai[1] + 1f) * num614 / (float)(num613 + num612);
                center4.Y -= (float)num616 * num617 / 2f;
                center4.Y += 2f;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), center4.X, center4.Y, Projectile.velocity.X, Projectile.velocity.Y, Projectile.type, 20, Projectile.knockBack, Projectile.owner, 10f, Projectile.ai[1] - 1f);
                int num618 = 4;
                //if (projectile.type == 386)
                //{
                //	num618 = 2;
                //}
                if ((int)Projectile.ai[1] % num618 == 0 && Projectile.ai[1] != 0f)
                {
                    int num619 = 372;
                    //if (projectile.type == 386)
                    //{
                    //	num619 = 373;
                    //}
                    //int num620 = NPC.NewNPC((int)center4.X, (int)center4.Y, num619, 0, 0f, 0f, 0f, 0f, 255);
                    //Main.npc[num620].velocity = projectile.velocity;
                    //Main.npc[num620].netUpdate = true;
                    //if (projectile.type == 386)
                    //{
                    //	Main.npc[num620].ai[2] = (float)projectile.width;
                    //	Main.npc[num620].ai[3] = -1.5f;
                    //}
                }
            }
            if (Projectile.ai[0] <= 0f)
            {
                float num621 = 0.104719758f;
                float num622 = (float)Projectile.width / 5f;
                //if (projectile.type == 386)
                //{
                //	num622 *= 2f;
                //}
                float num623 = (float)(Math.Cos((double)num621 * (0.0 - (double)Projectile.ai[0])) - 0.5) * num622;
                Projectile.position.X = Projectile.position.X - num623 * (0f - (float)Projectile.direction);
                Projectile.ai[0] -= 1f;
                num623 = (float)(Math.Cos((double)num621 * (0.0 - (double)Projectile.ai[0])) - 0.5) * num622;
                Projectile.position.X = Projectile.position.X + num623 * (0f - (float)Projectile.direction);
            }

            //The tornado pull effect
            Player player = Main.player[Main.myPlayer];

            //Player
            if (!player.EE().BlastShielding)
            {
                if ((Projectile.position.X / 16) <= ((player.position.X + 700) / 16) &&
                    (Projectile.position.X / 16) >= ((player.position.X - 700) / 16))
                {
                    //X
                    if (player.position.X <= (Projectile.position.X + 30))
                    {
                        //player.velocity.X = 2;
                        player.velocity.X = player.velocity.X + 0.3f;
                    }
                    else
                    {
                        //player.velocity.X = -2;
                        player.velocity.X = player.velocity.X - 0.3f;
                    }

                    //Y
                    if (player.position.Y <= (Projectile.position.Y - 200))
                    {
                        //player.velocity.Y = 2;
                        player.velocity.Y = player.velocity.Y + .5f;
                    }
                    else
                    {
                        //player.velocity.Y = -2;
                        player.velocity.Y = player.velocity.Y - .5f;
                    }
                }
            }

            //NPCS
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if ((Projectile.position.X / 16) <= ((Main.npc[i].position.X + 700) / 16) && (Projectile.position.X / 16) >= ((Main.npc[i].position.X - 700) / 16) && !Main.npc[i].boss && Main.npc[i].type != 488)
                {
                    Main.npc[i].netUpdate = true;

                    Main.npc[i].rotation += Projectile.velocity.X * 0.8f;

                    //X
                    if (Main.npc[i].position.X <= (Projectile.position.X + 37))
                    {
                        //Main.npc[i].velocity.X = 2;
                        Main.npc[i].velocity.X = Main.npc[i].velocity.X + 0.3f;
                    }
                    else
                    {
                        //Main.npc[i].velocity.X = -2;
                        Main.npc[i].velocity.X = Main.npc[i].velocity.X - 0.3f;
                    }

                    //Y
                    if (Main.npc[i].position.Y <= (Projectile.position.Y - 250))
                    {
                        //Main.npc[i].velocity.Y = 2;
                        Main.npc[i].velocity.Y = Main.npc[i].velocity.Y + .5f;
                    }
                    else
                    {
                        //Main.npc[i].velocity.Y = -2;
                        Main.npc[i].velocity.Y = Main.npc[i].velocity.Y - .5f;
                    }
                }
                else
                {
                    Main.npc[i].rotation = 0;
                }
            }

            //Items
            for (int v = 0; v < Main.item.Length; v++)
            {
                if ((Projectile.position.X / 16) <= ((Main.item[v].position.X + 700) / 16) && (Projectile.position.X / 16) >= ((Main.item[v].position.X - 700) / 16))
                {
                    //X
                    if (Main.item[v].position.X <= (Projectile.position.X + 37))
                    {
                        //Main.npc[i].velocity.X = 2;
                        Main.item[v].velocity.X = Main.item[v].velocity.X + 0.3f;
                    }
                    else
                    {
                        //Main.npc[i].velocity.X = -2;
                        Main.item[v].velocity.X = Main.item[v].velocity.X - 0.3f;
                    }

                    //Y
                    if (Main.item[v].position.Y <= (Projectile.position.Y - 200))
                    {
                        //Main.npc[i].velocity.Y = 2;
                        Main.item[v].velocity.Y = Main.item[v].velocity.Y + .5f;
                    }
                    else
                    {
                        //Main.npc[i].velocity.Y = -2;
                        Main.item[v].velocity.Y = Main.item[v].velocity.Y - .5f;
                    }
                }
            }

        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                Main.npc[i].rotation = 0;
            }
        }
    }
}