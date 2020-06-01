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
    class CritterBombProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("CritterBomb");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true;
            projectile.width = 10;
            projectile.height = 32;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 100;
            projectile.damage = 0;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

            //Create Bomb Damage
            ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

            //Create Bomb Explosion
            CreateExplosion(projectile.Center, 10);

            //Create Bomb Dust
            CreateDust(projectile.Center, 50);
        }

        private void CreateExplosion(Vector2 position, int radius)
        {
            int spread = 0;
            int pick = 0;
            int[] variety = { 442, 443, 445, 446, 447, 448, 539, 444 }; //442:GoldenBird - 443:GoldenBunny - 445:GoldenFrog - 446:GoldenGrasshopper - 447:GoldenMouse - 539:GoldenSquirrel - 448:GoldenWorm - 444:GoldenButterfly

            for (int i = 0; i <= radius; i++)
            {
                spread = Main.rand.Next(1200); //Random spread

                pick = variety[Main.rand.Next(variety.Length)];

                NPC.NewNPC((int)position.X + (spread - 600), (int)position.Y, pick, 0, 0f, 0f, 0f, 0f, 255); //Spawn 
                spread = 0;
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
                        updatedPosition = new Vector2(position.X - 600 / 2, position.Y - 600 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 600, 100, 1, 0f, 0f, 0, new Color(159, 255, 0), 1.776316f)];
                        dust.noLight = true;
                        dust.shader = GameShaders.Armor.GetSecondaryShader(112, Main.LocalPlayer);
                        dust.fadeIn = 1.697368f;
                    }
                    //------------
                }
            }
        }
    }
}
