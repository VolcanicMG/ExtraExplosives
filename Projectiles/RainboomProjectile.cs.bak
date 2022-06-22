using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace ExtraExplosives.Projectiles
{
    public class RainboomProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "Sounds/Custom/Explosives/Rainboom_";
        protected override string goreFileLoc => "Gores/Explosives/rainboom_gore";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rainboom");
        }

        public override void SafeSetDefaults()
        {
            radius = 30;
            projectile.tileCollide = true;
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 15;
            explodeSounds = new LegacySoundStyle[4];
            for (int num = 1; num <= explodeSounds.Length; num++)
            {
                explodeSounds[num - 1] = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, explodeSoundsLoc + num);
            }
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)], (int)projectile.Center.X, (int)projectile.Center.Y);

            //Create Bomb Damage
            //ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

            //Create Bomb Explosion
            //CreateExplosion(projectile.Center, 30);

            //Create Bomb Dust
            //CreateDust(projectile.Center, 10);

            Explosion();

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(2.0f, -2.0f);
            Vector2 gVel2 = new Vector2(0.0f, 2.0f);
            Gore.NewGore(projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "1"), projectile.scale);
            Gore.NewGore(projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "2"), projectile.scale);

            //Buff
            Player player = Main.player[projectile.owner];
            player.AddBuff(BuffID.Regeneration, 20000);
            player.AddBuff(BuffID.Clairvoyance, 20000);
        }

        public override void Explosion()
        {
            Vector2 position = projectile.Center;
            RainbowDusts(radius, position, -1, (int)position.X - 10, (int)position.X + 10);
        }

        private void RainbowDusts(int radius, Vector2 position, int dustStyle, int xOffsetLeft, int xOffsetRight)
        {
            Dust dust;
            Vector2 updatedPosition;

            for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left
            {
                for (int y = -radius; y <= 0; y++) //Starts on the Y Axis on the top
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);
                    updatedPosition = new Vector2(xPosition * 16.0f, yPosition * 16.0f);

                    Lighting.AddLight(new Vector2(xPosition * 16.0f, yPosition * 16.0f), new Vector3(.9f, 9f, 9f));
                    Lighting.maxX = 1;
                    Lighting.maxY = 1;

                    if (WorldGen.TileEmpty(xPosition, yPosition)) //Runs when a tile is empty
                    {
                        if (Math.Sqrt(x * x + y * y) <= radius - 8 + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Inner Circle
                        {
                            //Inner circle is ignored
                        }
                        else if (Math.Sqrt(x * x + y * y) <= radius - 7 + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Violet
                        {
                            dust = Terraria.Dust.NewDustPerfect(updatedPosition, 76, new Vector2(0f, 0f), 0, new Color(184, 0, 255), 3f);
                            dust.noGravity = true;
                        }
                        else if (Math.Sqrt(x * x + y * y) <= radius - 6 + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Indigo
                        {
                            dust = Terraria.Dust.NewDustPerfect(updatedPosition, 76, new Vector2(0f, 0f), 0, new Color(234, 0, 255), 3f);
                            dust.noGravity = true;
                        }
                        else if (Math.Sqrt(x * x + y * y) <= radius - 5 + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Blue
                        {
                            dust = Terraria.Dust.NewDustPerfect(updatedPosition, 76, new Vector2(0f, 0f), 0, new Color(0, 92, 255), 3f);
                            dust.noGravity = true;
                        }
                        else if (Math.Sqrt(x * x + y * y) <= radius - 4 + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Green
                        {
                            dust = Terraria.Dust.NewDustPerfect(updatedPosition, 76, new Vector2(0f, 0f), 0, new Color(0, 255, 42), 3f);
                            dust.noGravity = true;
                        }
                        else if (Math.Sqrt(x * x + y * y) <= radius - 3 + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Yellow
                        {
                            dust = Terraria.Dust.NewDustPerfect(updatedPosition, 76, new Vector2(0f, 0f), 0, new Color(255, 251, 0), 3f);
                            dust.noGravity = true;
                        }
                        else if (Math.Sqrt(x * x + y * y) <= radius - 2 + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Orange
                        {
                            dust = Terraria.Dust.NewDustPerfect(updatedPosition, 76, new Vector2(0f, 0f), 0, new Color(255, 150, 0), 3f);
                            dust.noGravity = true;
                        }
                        else if (Math.Sqrt(x * x + y * y) <= radius - 1 + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Red
                        {
                            dust = Terraria.Dust.NewDustPerfect(updatedPosition, 76, new Vector2(0f, 0f), 0, new Color(255, 0, 0), 3f);
                            dust.noGravity = true;
                        }
                        else if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Clouds
                        {
                            dust = Main.dust[Terraria.Dust.NewDust(new Vector2(position.X - (radius * 16.0f) - (170 / 4), position.Y), 170, 110, 86, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
                            dust.noGravity = true;
                            dust.noLight = false;
                            dust.shader = GameShaders.Armor.GetSecondaryShader(79, Main.LocalPlayer);

                            dust = Main.dust[Terraria.Dust.NewDust(new Vector2(position.X + (radius * 16.0f) - 160, position.Y), 170, 110, 86, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
                            dust.noGravity = true;
                            dust.noLight = false;
                            dust.shader = GameShaders.Armor.GetSecondaryShader(79, Main.LocalPlayer);
                        }
                    }
                }
            }
        }
    }
}