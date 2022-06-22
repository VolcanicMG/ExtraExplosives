using ExtraExplosives.NPCs.CaptainExplosiveBoss;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items
{
    public class Unhinged_Letter : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unhinged Letter");
            Tooltip.SetDefault("Used to summon chaos");
        }

        public override void SetDefaults()
        {
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 4;
            Item.width = 30;    //sprite width
            Item.height = 16;   //sprite height
            Item.maxStack = 20;   //This defines the items max stack
            Item.consumable = true;  //Tells the game that this should be used up once fired
            Item.rare = 5;   //The color the title of your item when hovering over it ingame
                             //item.useTime = 20;	 //How fast the item is used.
            Item.value = Item.buyPrice(0, 0, 10, 0);   //How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 3 silver)
        }


        public override bool CanUseItem(Player player)
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<CaptainExplosiveBoss>()))
            {
                NPC.NewNPC((int)player.position.X, (int)player.position.Y - 600, ModContent.NPCType<CaptainExplosiveBoss>()); //Spawn
                SoundEngine.PlaySound(SoundID.ForceRoar, player.position, 0);
                Item.stack--;

                return true;
            }

            return false;
        }
    }
}