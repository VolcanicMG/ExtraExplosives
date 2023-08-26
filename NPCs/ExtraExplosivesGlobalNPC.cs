using ExtraExplosives.Items;
using ExtraExplosives.Items.Accessories.AnarchistCookbook;
using ExtraExplosives.Items.Accessories.BombardierClassAccessories;
using ExtraExplosives.Items.Accessories.ChaosBomb;
using ExtraExplosives.Items.Explosives;
using ExtraExplosives.Items.Rockets;
using ExtraExplosives.Items.Weapons;
using Microsoft.Xna.Framework;
using Terraria.GameContent.ItemDropRules;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.NPCs
{
    public class ExtraExplosivesGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool Radiated;

        public override void ResetEffects(NPC npc)
        {
            Radiated = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (Radiated)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 30;
                if (damage < 2)
                {
                    damage = 2;
                }
            }
        }


        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            switch (npc.type)
            {
                case NPCID.WallofFlesh:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MinerainLauncher>(), 20));
                    break;
            }
        }
        /*public override void OnKill(NPC npc) // TODO swap to ModifyNPCLoot also figure out wtf is up with that magic float, i think its a drop chance but who knows
        {
            if (npc.boss && Main.expertMode)
            {
                if (Main.rand.NextFloat() < 0.10210526f && npc.type != Mod.Find<ModNPC>("CaptainExplosive").Type)
                {
                    // TODO Item.NewItem(Main.LocalPlayer.getRect(), ModContent.ItemType<BreakenTheBankenItem>(), 1);
                }
            }

            switch (npc.type)
            {
                // Bosses
                case NPCID.WallofFlesh:
                    if (Main.rand.NextFloat() < 0.20210526f)
                    {
                        Item.NewItem(npc.position, new Vector2(32, 32), ModContent.ItemType<MinerainLauncher>(), 1);
                    }
                    if (Main.rand.NextFloat() < 0.10210526f)
                    {
                        Item.NewItem(npc.position, new Vector2(32, 32), ModContent.ItemType<FleshyBlastingCaps>(), 1);
                    }
                    break;
                case NPCID.PirateShip:
                    if (Main.rand.NextFloat() < 0.10210526f)
                    {
                        Item.NewItem(npc.position, new Vector2(32, 32), ModContent.ItemType<DutchmansBlaster>(), 1);
                    }
                    break;
                case NPCID.Pumpking:
                    if (Main.rand.NextFloat() < 0.1021f)
                    {
                        Item.NewItem(npc.position, new Vector2(32, 32), ModContent.ItemType<PumpkinLauncher>(), 1);
                    }
                    break;
                case NPCID.DukeFishron:
                    if (Main.rand.NextFloat() < 0.1021f)
                    {
                        Item.NewItem(npc.position, new Vector2(32, 32), ModContent.ItemType<DeepseaEruption>(), 1);
                    }
                    break;

                // NPCs
                case NPCID.DD2KoboldFlyerT2:
                    if (Main.rand.NextFloat() < 0.25f)
                    {
                        Item.NewItem(npc.position, ModContent.ItemType<MysteryBomb>(), 1);
                    }
                    break;
                case NPCID.DD2KoboldFlyerT3:
                    if (Main.rand.NextFloat() < 0.25f)
                    {
                        Item.NewItem(npc.position, ModContent.ItemType<MysteryBomb>(), 1);
                    }
                    break;
                case NPCID.DD2KoboldWalkerT2:
                    if (Main.rand.NextFloat() < 0.25f)
                    {
                        Item.NewItem(npc.position, ModContent.ItemType<MysteryBomb>(), 1);
                    }
                    break;
                case NPCID.DD2KoboldWalkerT3:
                    if (Main.rand.NextFloat() < 0.25f)
                    {
                        Item.NewItem(npc.position, ModContent.ItemType<MysteryBomb>(), 1);
                    }
                    break;
                case NPCID.SkeletonCommando:
                    if (Main.rand.NextFloat() < 0.15f)
                    {
                        Item.NewItem(npc.position, ModContent.ItemType<ReactivePlating>(), 1);
                    }
                    break;
                case NPCID.TacticalSkeleton:
                    if (Main.rand.NextFloat() < 0.15f)
                    {
                        Item.NewItem(npc.position, ModContent.ItemType<BlastShielding>(), 1);
                    }
                    if (Main.rand.NextFloat() < 0.15f)
                    {
                        Item.NewItem(npc.position, ModContent.ItemType<TacticalBonerifle>(), 1);
                    }
                    break;
                case NPCID.GoblinPeon:
                    if (Main.rand.NextFloat() < 0.15f)
                    {
                        Item.NewItem(npc.position, ModContent.ItemType<TrashCannon>(), 1);
                    }
                    break;
                case NPCID.PirateDeadeye:
                    if (Main.rand.NextFloat() < 0.15f)
                    {
                        Item.NewItem(npc.position, ModContent.ItemType<Blunderboom>(), 1);
                    }
                    break;
                case NPCID.Golem:
                    if (Main.rand.NextFloat() < 0.15f)
                    {
                        Item.NewItem(npc.position, ModContent.ItemType<LihzahrdFuzeset>(), 1);
                    }
                    break;
                case NPCID.MartianSaucer:
                    if (Main.rand.NextFloat() < 0.15f)
                    {
                        Item.NewItem(npc.position, ModContent.ItemType<AlienExplosive>(), 1);
                    }
                    break;
                case NPCID.DungeonSpirit:
                    if (Main.rand.NextFloat() < 0.15f)
                    {
                        Item.NewItem(npc.position, ModContent.ItemType<SupernaturalBomb>(), 1);
                    }
                    break;
                case NPCID.FungiBulb:
                    if (Main.rand.NextFloat() < 0.15f)
                    {
                        Item.NewItem(npc.position, ModContent.ItemType<Bombshroom>(), 1);
                    }
                    break;
                case NPCID.GiantFungiBulb:
                    if (Main.rand.NextFloat() < 0.20f)
                    {
                        Item.NewItem(npc.position, ModContent.ItemType<Bombshroom>(), 1);
                    }
                    break;
                //case NPCID.RayGunner:
                //	if (Main.rand.Next(100) == 0)
                //	{
                //		Item.NewItem(Main.LocalPlayer.getRect(), ModContent.ItemType<SilentCricket>(), 1);
                //	}
                //	break;
                default:
                    break;
            }
        }*/

        // TODO this entire bit is either good or very bad, idk which yet
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.Demolitionist)
            {
                shop.Add(ModContent.ItemType<BasicExplosiveItem>());
                shop.Add(ModContent.ItemType<SmallExplosiveItem>());
                shop.Add(ModContent.ItemType<MediumExplosiveItem>());
                shop.Add(ModContent.ItemType<LargeExplosiveItem>());
                shop.Add(ModContent.ItemType<Rocket0>());
                shop.Add(ModContent.ItemType<BombardsPouch>());
                /*shop.item[nextSlot].SetDefaults(ModContent.ItemType<BasicExplosiveItem>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<SmallExplosiveItem>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<MediumExplosiveItem>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<LargeExplosiveItem>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Rocket0>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<BombardsPouch>());*/
                // TODO wrong conditions on this
                shop.Add(ModContent.ItemType<Unhinged_Letter>(), Condition.DownedKingSlime);
                shop.Add(ModContent.ItemType<Rocket0Point5>(), Condition.DownedKingSlime);
                /*if (NPC.downedSlimeKing || NPC.downedBoss1)
                {
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<Unhinged_Letter>());
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<Rocket0Point5>());
                }*/
                shop.Add(ModContent.ItemType<CertificateOfDemolition>(), Condition.DownedDukeFishron);
                /* TODO this one should be good if (NPC.downedFishron)
                {
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<CertificateOfDemolition>());
                }*/
            }

            if (shop.NpcType == NPCID.TravellingMerchant)
            {
                shop.Add(ModContent.ItemType<TornadoBombItem>(), Condition.Hardmode);
                shop.Add(ModContent.ItemType<RainboomItem>());
            }

            if (shop.NpcType == NPCID.Merchant)
            {
                shop.Add(ModContent.ItemType<PotatoItem>());
                shop.Add(ModContent.ItemType<SpongeItem>());
            }

            if (shop.NpcType == NPCID.Truffle)
            {
                shop.Add(ModContent.ItemType<Bombshroom>());
            }
        }

        /*public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
        {
            if (type == NPCID.Demolitionist)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<BasicExplosiveItem>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<SmallExplosiveItem>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<MediumExplosiveItem>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<LargeExplosiveItem>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Rocket0>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<BombardsPouch>());

                if (NPC.downedSlimeKing || NPC.downedBoss1)
                {
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<Unhinged_Letter>());
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<Rocket0Point5>());
                }
                if (NPC.downedFishron)
                {
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<CertificateOfDemolition>());
                }
            }
            else if (type == NPCID.TravellingMerchant)
            {
                if (Main.hardMode)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<TornadoBombItem>());
                    nextSlot++;
                }
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<RainboomItem>());
                nextSlot++;
            }
            else if (type == NPCID.Merchant)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<PotatoItem>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<SpongeItem>());
                nextSlot++;
            }
            else if (type == NPCID.Truffle)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Bombshroom>());
                nextSlot++;
            }
        }*/

        public override void GetChat(NPC npc, ref string chat)
        {
            switch (npc.type)
            {
                case NPCID.Guide:
                default:
                    break;
            }
        }
    }
}