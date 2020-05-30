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
    public class BreakenTheBankenProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BreakenTheBanken");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true;
            projectile.width = 22;
            projectile.height = 22;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            projectile.penetrate = 20;
            projectile.timeLeft = 140;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

            //Create Bomb Damage
            //ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

            //Create Bomb Explosion
            CreateExplosion(projectile.Center, 20);

            //Create Bomb Dust
            //CreateDust(projectile.Center, 10);
        }

        private void CreateExplosion(Vector2 position, int radius)
        {
            int cntr = 0; //Tracks how many coins have spawned in

            for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left 
            {
                for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5) //Circle
                    {
                        if (CheckForUnbreakableTiles(Main.tile[xPosition, yPosition].type, xPosition, yPosition)) //Unbreakable
                        {

                        }
                        else //Breakable
                        {
                            if (WorldGen.TileEmpty(xPosition, yPosition))
                            {
                                if (++cntr <= 50) Projectile.NewProjectile(position.X + x, position.Y + y, Main.rand.Next(10) - 5, Main.rand.Next(10) - 5, mod.ProjectileType("BreakenTheBankenChildProjectile"), 100, 20, projectile.owner, 0.0f, 0);
                            }
                            else
                            {
                                if (++cntr <= 50) Projectile.NewProjectile(position.X, position.Y, Main.rand.Next(10) - 5, Main.rand.Next(10) - 5, mod.ProjectileType("BreakenTheBankenChildProjectile"), 100, 20, projectile.owner, 0.0f, 0);
                            }
                        }
                    }
                }
            }
        }
    }
}