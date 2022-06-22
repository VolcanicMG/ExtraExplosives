using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.BombardierClassAccessories
{
    [AutoloadEquip(EquipType.Head)]
    public class BombersCap : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bomber's Cap");
            Tooltip.SetDefault("Increases explosives area of effect by 30%");
        }

        public override void SetDefaults()
        {
            //item.social = true;
            item.accessory = true;
            item.value = Item.buyPrice(0, 30, 0, 0);
            item.rare = ItemRarityID.Orange;
            item.consumable = false;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.EE().BombersHat = true;
        }

        public override bool DrawHead() => true;
    }
}
