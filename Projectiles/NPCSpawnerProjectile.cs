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
using ExtraExplosives.Items;
using ExtraExplosives;
using System.Timers;
using System.Threading;

namespace ExtraExplosives.Projectiles
{
    public class NPCSpawnerProjectile : ModProjectile
    {

        internal static bool CanBreakWalls;
        //private static Timer _delayTimer;

        //internal static bool detonate;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("NPCSpawner");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 22;   //This defines the hitbox width
            projectile.height = 22;    //This defines the hitbox height
            projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 200; //The amsadount of time the projectile is alive for
            //projectile.extraUpdates = 1;
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Main.myPlayer];
            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
            int radius = 2;     //this is the explosion radius, the highter is the value the bigger is the explosion

            //damage part of the bomb
            ExplosionDamageProjectile.DamageRadius = 2;
            Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("ExplosionDamageProjectile"), 1, 0, Main.myPlayer, 0.0f, 0);

            //NPC.NewNPC((int)player.Center.X, (int)player.Center.Y + 20, mod.NPCType("CaptainExplosive"), 0, 0f, 0f, 0f, 0f, 255);

            Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("InvisibleSpawnerProjectile"), 0, 1, Main.myPlayer, 0.0f, 0);

            Main.NewText("Let's Get Ready To RUMBLE!!!", (byte)34, (byte)255, (byte)10, false);


        }


    }
}