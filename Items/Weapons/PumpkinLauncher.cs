using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class PumpkinLauncher : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bombkin 3000");
            Tooltip.SetDefault("These pumpkins are NOT safe around open flames\n" +
                               "Launches three rockets in quick succession\n" +
                               "Consumes one rocket per burst");
        }

        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.autoReuse = true;
            item.useAnimation = 12;
            item.useTime = 4;
            item.reuseDelay = 14;
            item.useAmmo = AmmoID.Rocket;
            item.width = 66;
            item.height = 34;
            item.shoot = 134;
            item.UseSound = SoundID.Item11;
            item.channel = true;
            item.damage = 30;
            item.shootSpeed = 7;
            item.noMelee = true;
            item.value = Item.buyPrice(0, 15, 0, 50);
            item.knockBack = 4f;
            item.rare = ItemRarityID.Yellow;
            item.ranged = true;
        }

        //public override void ModifyTooltips(List<TooltipLine> tooltips)
        //{
        //    TooltipLine stats = tooltips.FirstOrDefault(t => t.Name == "Damage" && t.mod == "Terraria");
        //    if (stats != null)
        //    {
        //        string[] split = stats.text.Split(' ');
        //        string damageValue = split.First();
        //        string damageWord = split.Last();
        //        stats.text = damageValue + "x3 explosive " + damageWord;
        //    }
        //}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, -6);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 45;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override bool ConsumeAmmo(Player player)
        {
            // Because of how the game works, player.itemAnimation will be 11, 7, and finally 3. (UseAmination - 1, then - useTime until less than 0.) 
            // We can get the Clockwork Assault Riffle Effect by not consuming ammo when itemAnimation is lower than the first shot.
            return !(player.itemAnimation < item.useAnimation - 2);
        }
    }
}