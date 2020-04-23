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
using ExtraExplosives.UI;
using ExtraExplosives.Projectiles;

namespace ExtraExplosives.Items
{
    public class BulletBoomItemMeteor : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BulletBoom [c/AC90FF:[Meteor Bullet]]");
            Tooltip.SetDefault("Who said a gun is the only thing that can shoot a bullet. \n" +
                "Blows up upon touching a block.");
        }

        public override string Texture => "ExtraExplosives/Items/BulletBoomItem";

        public override void SetDefaults()
        {
            item.CloneDefaults(ModContent.ItemType<BulletBoomItem>());
            item.damage = 25;     //The damage stat for the Weapon.   
            item.shoot = mod.ProjectileType("BulletBoomProjectile"); //This defines what type of projectile this item will shoot
        }
    }

}