using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using ExtraExplosives;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class AtomBombProjectile : ModProjectile
    {
        
        //Variables
        private const int PickPower = -1;
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Atom Bomb");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true;
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 600;

            drawOffsetX = -15;
            drawOriginOffsetY = -15;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

            //Create Bomb Damage
            ExplosionDamage(1, projectile.Center, 5000, 1.0f, projectile.owner);

            //Create Bomb Explosion
            CreateExplosion(projectile.Bottom, 40);

            //Create Bomb Dust
            CreateDust(projectile.Center, 30);
        }

        private void CreateExplosion(Vector2 position, int radius)
        {
            int xPosition = (int) (position.X / 16.0f);
            int yPosition = (int) (position.Y / 16.0f);
            ushort tile = Main.tile[xPosition, yPosition].type;
            if (CanBreakTile(tile, PickPower)) // Add this to allow for some level of control of what it can break
            {                                  // Pickaxe Power is currently set to -1, so the method is set to default to true
                WorldGen.KillTile(xPosition, yPosition, false, false, true); //this make the explosion destroy tiles  
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
                        updatedPosition = new Vector2(position.X - 1 / 2, position.Y - 1 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 1, 1, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 15f)];
                        dust.noGravity = true;
                        dust.fadeIn = 2.486842f;
                    }
                    //------------

                    //---Dust 2---
                    if (Main.rand.NextFloat() < 1f)
                    {
                        updatedPosition = new Vector2(position.X - 1 / 2, position.Y - 1 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 1, 1, 203, 0f, 0f, 0, new Color(255, 255, 255), 15f)];
                        dust.noGravity = true;
                        dust.noLight = true;
                    }
                    //------------

                    //---Dust 3---
                    if (Main.rand.NextFloat() < 1f)
                    {
                        updatedPosition = new Vector2(position.X - 1 / 2, position.Y - 1 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 1, 1, 31, 0f, 0f, 0, new Color(255, 255, 255), 15f)];
                        dust.noGravity = true;
                        dust.noLight = true;
                    }
                    //------------
                }
            }
        }
    }
}