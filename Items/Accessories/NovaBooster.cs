using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
    public class NovaBooster : ModItem
    {
        private string _boostLowTexture = "ExtraExplosives/Items/Accessories/NovaBooster";
        private string _boostHighTexture = "ExtraExplosives/Items/Accessories/NovaBoosterHigh";
        private string _boosterTexture = "ExtraExplosives/Items/Accessories/NovaBooster";
        public override string Texture => _boosterTexture;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nova Booster");
            Tooltip.SetDefault("The power of a dying star,\n" +
                               "strapped to your back");
            //Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(15, ));
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 38;
            item.value = 1000000;
            item.rare = ItemRarityID.Cyan;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 360;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.7f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 2;
            maxAscentMultiplier = 4;
            constantAscend = 0.4f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 27f;
            acceleration *= 3.5f;
        }

        public override bool AltFunctionUse(Player player)
        {
            _boosterTexture = (_boosterTexture == _boostLowTexture) ? _boostHighTexture : _boostLowTexture;
            return base.AltFunctionUse(player);
        }
    }
}