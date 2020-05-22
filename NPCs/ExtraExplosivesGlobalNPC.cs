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
using System.Text;
using System.Threading.Tasks;
using ExtraExplosives.Items.Explosives;


namespace ExtraExplosives.NPCs
{
    public class ExtraExplosivesGlobalNPC : GlobalNPC
    {

        public override void NPCLoot(NPC npc)
        {
            if(npc.boss && Main.expertMode)
            {
                if (Main.rand.NextFloat() < 0.10210526f && npc.type != mod.NPCType("CaptainExplosive"))
                {
                    Item.NewItem(Main.LocalPlayer.getRect(), ModContent.ItemType<BreakenTheBankenItem>(), 1);
                }

            }

        }

		public override void SetupShop(int type, Chest shop, ref int nextSlot)
		{
			if (type == NPCID.Demolitionist)
			{
                shop.item[nextSlot].SetDefaults(mod.ItemType("BasicExplosiveItem"));
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("SmallExplosiveItem"));
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("MediumExplosiveItem"));
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("LargeExplosiveItem"));
            }
            else if(type == NPCID.TravellingMerchant)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("SpongeItem"));
                nextSlot++;
            }
		}

	}  
}