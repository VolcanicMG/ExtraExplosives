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
    public class ExplosionDamageProjectile : ModProjectile
    {
        //Variables:
        internal static float DamageRadius;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ExplosionDamage");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = false;
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 5;
            projectile.Opacity = 0f;
            projectile.scale = DamageRadius; //DamageRadius
            //projectile.scale = 5;
        }
    }
}