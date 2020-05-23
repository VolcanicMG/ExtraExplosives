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

namespace ExtraExplosives.Projectiles
{
    class FlashbangProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("flashbang");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 12;   //This defines the hitbox width
            projectile.height = 32;    //This defines the hitbox height
            projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 100; //The amount of time the projectile is alive for
            projectile.damage = 0;
 
        }

        public override void Kill(int timeLeft)
        {
            //Player player = Main.player[Main.myPlayer];
            Vector2 position = projectile.Center;

            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Flashbang"), (int)position.X, (int)position.Y);

            Projectile.NewProjectile(position.X - 450, position.Y, 0, 0, ModContent.ProjectileType<InvisFlashbangProjectile>(), 1, 0, projectile.owner, 0.0f, 0); //left
            Projectile.NewProjectile(position.X + 450, position.Y, 0, 0, ModContent.ProjectileType<InvisFlashbangProjectile>(), 1, 1, projectile.owner, 0.0f, 0); //right
        }

    }
}
