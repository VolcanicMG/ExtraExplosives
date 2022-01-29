using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.NPCs.CaptainExplosiveBoss
{
    public class CEDroneNPC : ModNPC
    {
        private Player _target = null;
        private Vector2 _endTarget;
        private int _targetingFrames = 240 + Main.rand.Next(120) - 60;
        private bool _targetAquired = false;

        private float[] _cordOffset = new float[]
        {
            Main.rand.NextFloat(-3f, 3f),
            Main.rand.NextFloat(-15f, 15f)
        };

        private int spawnTimer = 60;
        // Speed vars, are more of a multiple than a strict value
        private float speedX = 7.5f; // Used to control the x speed during phase 1

        //Testing stuff
        private float[] _velocityStorage = new float[2];
        private bool alert = true;

        private int testTimer = 0;

        private float[] direction = new float[]    // Direction the drone will drift in during its targeting phase
        {
            Main.rand.NextFloat(-0.1f,0.1f),
            Main.rand.NextFloat(-0.01f, -0.1f)
        };


        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 6;
        }

        public override void SetDefaults()
        {
            npc.width = 15;
            npc.height = 15;
            npc.Hitbox = new Rectangle(0, 0, 32, 32);
            npc.damage = 0;
            npc.defense = 5;
            npc.lifeMax = 40;
            npc.knockBackResist = 0f;
            npc.noTileCollide = true;
            npc.frame.Height = 22;
            npc.frame.Width = 22;
            npc.noGravity = false;
            npc.aiStyle = -1;
            npc.Center = new Vector2(11, 11);
            npc.rotation = Main.rand.Next(360);
            drawOffsetY = -5f;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax += 20;
            npc.damage = (int)(npc.damage * 0.6f);
        }

        // Animation code do not touch
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter > 10)    //<--- updates per frame
            {
                npc.frameCounter = 0;
                npc.frame.Y += frameHeight;
                npc.frame.Y %= (frameHeight * 3);    //<----- number of frames (3 not 6 since only 3 are ever used at one time)
                if (_targetAquired) npc.frame.Y += (frameHeight * 3);    // switches the frame set used 
            }
        }

        /*    // IGNORE this is all stuff i tried to get working but couldnt, not deleting for reference
        public override bool PreAI()
        {
            if (Keyboard.GetState().PressingShift() || !alert)
            {
                if (alert)
                {
                    _velocityStorage[0] = npc.velocity.X;
                    npc.velocity.X = 0;
                    _velocityStorage[1] = npc.velocity.Y;
                    npc.velocity.Y = 0;
                    alert = false;
                    Main.NewText("All drones paused");
                }
                return false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.NumPad9)) alert = true;
            if (_velocityStorage[0] != 0)
            {
                Main.NewText("Resuming Drone function");
                npc.velocity.X = _velocityStorage[0];
                npc.velocity.Y = _velocityStorage[1];
            }
            if (_target.X != -1 || _target.Y != -1)     // The drone has found a target, skip to main AI
            {
                return true;    // Return true if a target has been found
            }

            if (_targetingFrames > 0)    // Targeting frames, the drone just hovers and locks in on the player
            {
                _targetingFrames--;
                //npc.velocity.X += direction[0];
                //npc.velocity.Y += direction[1];
                direction[0] *= 0.99f;
                direction[1] *= 0.99f;
            }

            if (_targetingFrames <= 0)        // the drone gets the player its targeting and saves the tile position its targeted
            {
                //if not find a target 
                Main.NewText("DEBUG: Target not yet found");
                Player player = Main.player[npc.target];
                Vector2 playerPosition = Main.player[player].position;
                _target = playerPosition;
                _targetAquired = true;
                npc.velocity.X = 0;
                npc.velocity.Y = 0;
            }
            return false; // Since a target hasnt been found, return false so AI doesnt run
        }

        public override void AI()
        {
            Vector2 normalizedCord = new Vector2(npc.position.X - _target.X, npc.position.Y - _target.Y);
            //double dist = _getDist(normalizedCord);
            //double rotation = _getRotation(normalizedCord);
            //npc.velocity.X += (float)(dist * Math.Cos(rotation)) * 0.0005f;
            //npc.velocity.Y += (float)(dist * Math.Sin(rotation)) * 0.0005f;
            double xVel = Math.Abs((npc.position.X - _target.X) / 8f * 6E-03);
            double yVel = Math.Abs((npc.position.Y - _target.Y) / 16f * 6E-03);
            testTimer--;
            if (testTimer <= 0)
            {
                testTimer += 30;
                Main.NewText("Targeting Player", Color.Aqua);
                Main.NewText("Position: " + (npc.position.X - _target.X) + " " + (npc.position.Y - _target.Y), Color.Aqua);
                Main.NewText($"Velocities PC: {xVel}, {yVel}", Color.Thistle);
                Main.NewText($"Xs: {_target.X},{npc.position.X}, Ys: {_target.Y},{npc.position.Y}, VelX: {npc.velocity.X}, VelY: {npc.velocity.Y}", Color.Thistle);
            }

            npc.velocity.X *= 0.98f;
            npc.velocity.Y *= 0.98f;
            npc.velocity.X -= (float) xVel;
            npc.velocity.Y += (float) yVel;
        }*/

        public override bool PreAI()
        {
            npc.spriteDirection = npc.velocity.X > 0 ? 1 : -1;    // NPC will face the direction its moving
            // if (Keyboard.GetState().PressingShift() && testTimer <= 0)
            //{

            //  Main.NewText($"Vectors {npc.position.X},{npc.position.Y} & {_target.position.X},{_target.position.Y} with single of 0 resulted in {ans}");    // debug info
            //}
            if (spawnTimer-- == 30)
            {
                //npc.velocity = Main.npc[npc.target].velocity;
                return false;
            }
            else if (spawnTimer-- > 0)
            {
                npc.velocity.Y -= 0.05f;
                //npc.velocity.X += (npc.ai[0] - 1) * 3;
                return false;
            }
            else if (spawnTimer-- == 0)
            {
                //Main.NewText("Gravity", Color.Cyan);
                npc.noGravity = true;
                npc.velocity.Y = 0;
                npc.rotation = 0;
            }

            npc.velocity.X *= 0.37f;
            npc.rotation = npc.velocity.X / 15f;    // Rotate the sprite based on x velocity (makes movement seem convincing)
            if (_target == null)
            {
                npc.TargetClosest();
                // Main.NewText($"The target is {npc.HasPlayerTarget}");
                _target = Main.player[npc.target];
                npc.velocity.Y -= 16 / Vector2.Distance(npc.position, _target.position) / 16f;
            }
            if (_targetAquired)     // The drone has found a target, skip to main AI
            {
                return true;    // Return true if a target has been found
            }

            if (_targetingFrames > 0)    // Targeting frames, the drone just hovers and locks in on the player
            {
                float dist = npc.position.Y - (_target.position.Y + _cordOffset[1]);
                _targetingFrames--;
                if (npc.position.Y > (_target.position.Y + _cordOffset[1]) - 256)        // catch in case the drone is below the player, somehow
                {
                    npc.velocity.Y -= 0.5f;
                }


                //else if (dist < 32)    // Raises / Lowers the drone so it is comfortably above the player
                //{
                //    npc.velocity.Y -= Math.Abs(_target.velocity.Y)/8f;    // changes the y value so it stays near the player
                //}
                else if (dist > 60)
                {
                    npc.velocity.Y += 0.09f; // changes the y value so it stays near the player
                }
                else
                {
                    npc.velocity.Y *= 0.75f;    // if its above the player by the set amount slowly lower the y velocity
                }
                //Vector2 direction = (_target.Center - npc.Center).SafeNormalize(Vector2.UnitX);
                //if (npc.position.X > _target.position.X)
                // {
                npc.velocity.X += ((_target.Center - npc.Center).SafeNormalize(Vector2.UnitX).X + (_cordOffset[0] / 10)) * speedX;
                //}
                //else
                // {
                // npc.position.X -= direction.X * speedX;
                // }
            }

            if (_targetingFrames <= 0)
            {
                _endTarget = _linearInterpolation(_target.position, npc.position);    // gets the cords of its target point
                _targetAquired = true;
                _targetingFrames = 30;
            }
            return false;
        }

        public override void AI()
        {
            if (_target.position.Y < npc.position.Y && Vector2.Distance(npc.position, _target.position) / 16f < 8) Kill();
            Vector2 direction = Vector2.Normalize(npc.position - _endTarget);
            if (_targetingFrames > 0)
            {
                _targetingFrames--;
                npc.velocity.X += direction.X * 8;
                if (_target.position.Y < npc.position.Y)
                {
                    npc.velocity.Y += direction.Y / 20f;
                }
                else
                {
                    npc.velocity.Y += Vector2.Subtract(Vector2.Zero, direction).Y / 20f;
                }
            }

            if (_targetingFrames == 0)
            {
                _targetingFrames = -1;
                _endTarget = _linearInterpolation(_target.position, npc.position);    // reaquire the target for better accuracy
            }

            if (_targetingFrames < 0)    // Accelerate the first step for a more 'energetic' attack
            {
                npc.damage = 40;
                npc.velocity.X += direction.X * 8;
                if (_target.position.Y < npc.position.Y)
                {
                    npc.velocity.Y += direction.Y / 3f;
                }
                else
                {
                    npc.velocity.Y -= Vector2.Subtract(Vector2.Zero, direction).Y / 3f;
                }
            }
        }

        public override void PostAI()
        {
            npc.FindFrame();
            if (spawnTimer <= 0) return;
            float rotation = npc.rotation % 360;


            if (rotation != 0)
            {
                if (rotation > 90 && rotation < 270) // rotated between 1 and 180 degrees
                {
                    // Main.NewText($"Rotating: {(float)Math.Cos(npc.rotation/180 * 2 * Math.PI)/10}");
                    npc.rotation -= (float)Math.Cos(npc.rotation / 180 * 2 * Math.PI) / 5;
                }
                else  // rotated between 180 and 359 degrees
                {
                    // Main.NewText($"Rotating: {(float)Math.Sin(npc.rotation/180 * 2 * Math.PI)/10}");
                    npc.rotation += (float)Math.Sin(npc.rotation / 180 * 2 * Math.PI) / 5;
                }
            }

            //Main.NewText($"Rotation: {npc.rotation}", Color.Aquamarine);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (spawnTimer > 0) return;
            if (_targetingFrames < 0)
            {
                Kill();    // on collide kill it (here so it wont deal damage)
            }
        }

        public void Kill()
        {
            // Explode
            if (ExtraExplosives.CheckBossBreak)
            {
                CreateExplosion(npc.position, 4);
            }

            Main.PlaySound(SoundID.Item14, (int)npc.position.X, (int)npc.position.Y);

            CreateDust(npc.Center, 50);
            ExplosionDamageEnemy(4, npc.Center, npc.damage, ModContent.NPCType<CaptainExplosiveBoss>());
            // kill the drone
            npc.life = 0;
        }

        public override bool CheckDead()
        {
            Kill();
            return base.CheckDead();
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
                        if (!CanBreakTile(tile, 0)) //Unbreakable CheckForUnbreakableTiles(tile) ||
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
        }

        public override void NPCLoot()
        {
            base.NPCLoot();
        }

        /*private void _moveEvent(int playerId)
        {
            float dist = Vector2.Distance(Main.player[playerId].position, npc.position) / 16f;
            float distX = (Main.player[playerId].position.X - npc.position.X);
            float distY = (Main.player[playerId].position.Y - npc.position.Y);
            float distCalc = (float)(Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2)) / 16f);
            distX /= 16f;
            distY /= 16f;
            bool check = (distCalc == dist);
            if (!check)
            {
                Main.NewText($"Error found in distance calculation Dist X: {distX} and Y: {distY} add to {distCalc} which doesnt equal {dist}", Color.Purple);
            }
            else
            {
                Main.NewText($"Distance found to be: {dist} which is X{distX}, Y:{distY}", Color.Purple);
            }
        }*/

        /// <summary>
        /// Uses linear interpolation to find the y intercept of a line which passes through 2 points a and b
        /// </summary>
        /// <param name="a">First Point</param>
        /// <param name="b">Second Point</param>
        /// <param name="y">Y cord of the point you wish to find, left in for usability</param>
        /// <returns type="Vector2">y intercept</returns>
        private Vector2 _linearInterpolation(Vector2 a, Vector2 b, float y = 0)
        {
            return new Vector2(((b.Y - y) * a.X + (y - a.Y) * b.X) / (b.Y - a.Y), y);
        }

        // Assumes the vector is normalized, returns the distance the point is from the origin
        private Func<Vector2, double> _getDist = z => Math.Sqrt(Math.Pow(z.X, 2) + Math.Pow(z.Y, 2));
        // Assumes the vector is normalized, returns the rotation the point is around the origin
        private Func<Vector2, double> _getRotation = z => Math.Atan(z.Y / z.X);
    }
}