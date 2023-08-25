using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Items
{
    public class PotatoItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Potato");
            // Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            Item.width = 40;    //sprite width
            Item.height = 40;   //sprite height
            Item.maxStack = 999;   //This defines the items max stack
            Item.consumable = false;  //Tells the game that this should be used up once fired
            Item.rare = 1;   //The color the title of your item when hovering over it ingame
                             //item.useTime = 20;	 //How fast the item is used.
            Item.value = Item.buyPrice(0, 0, 0, 10);   //How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 3 silver)
        }

    }
}