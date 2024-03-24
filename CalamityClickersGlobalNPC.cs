﻿using CalamityClickers.Content.Items.Armor;
using CalamityClickers.Content.Items.Weapons.HM;
using CalamityClickers.Content.Items.Weapons.PostML;
using CalamityClickers.Content.Items.Weapons.PreHM;
using CalamityMod;
using CalamityMod.NPCs.Bumblebirb;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.NPCs.Cryogen;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.HiveMind;
using CalamityMod.NPCs.Leviathan;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.TownNPCs;
using ClickerClass.Items.Weapons.Clickers;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers
{
    public class CalamityClickersGlobalNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            LeadingConditionRule mainRule = npcLoot.DefineNormalOnlyDropSet();


            foreach (var rule in npcLoot.Get())
            {
                if (rule is CommonDrop normalDrop)
                {
                    if (normalDrop.itemId == ModContent.ItemType<TheClicker>())
                    {
                        npcLoot.Remove(rule);
                    }

                }
                /*if (rule is LeadingConditionRule leadingRule)
                {
                    int i = 0;
                    foreach (var chain in leadingRule.ChainedRules)
                    {
                        if (chain.RuleToChain is CommonDrop chainDrop)
                        {
                            if (chainDrop.itemId == ModContent.ItemType<TheClicker>())
                            {
                                leadingRule.ChainedRules.RemoveAt(i);
                            }
                        }
                        i++;
                    }
                }*/
            }
            if (npc.type == ModContent.NPCType<Horse>())
            {
                mainRule.Add(ModContent.ItemType<EarthenClicker>(), 4);
            }

            if (npc.type == ModContent.NPCType<DesertScourgeHead>())
            {
                mainRule.Add(ModContent.ItemType<ScourgeClicker>(), 3);
            }
            if (npc.type == ModContent.NPCType<Crabulon>())
            {
                mainRule.Add(ModContent.ItemType<MushyClicker>(), 3);
            }
            if (npc.type == ModContent.NPCType<HiveMind>())
            {
                mainRule.Add(ModContent.ItemType<RottenClicker>(), 3);
            }

            if (npc.type == ModContent.NPCType<Cryogen>())
            {
                mainRule.Add(ModContent.ItemType<CryoClicker>(), 3);
            }
            if (npc.type == ModContent.NPCType<Leviathan>() || npc.type == ModContent.NPCType<Anahita>())
            {
                var lastStanding = npcLoot.DefineConditionalDropSet(Leviathan.LastAnLStanding);
                LeadingConditionRule normalOnly = new LeadingConditionRule(new Conditions.NotExpert());
                lastStanding.Add(normalOnly);
                normalOnly.Add(ModContent.ItemType<AnahitasClicker>(), 3);
            }

            if (npc.type == ModContent.NPCType<Bumblefuck>())
            {
                mainRule.Add(ModContent.ItemType<RedLightningClicker>(), 3);
            }

        }
        public override void ModifyShop(NPCShop shop)
        {
            int type = shop.NpcType;

            if (type == ModContent.NPCType<DILF>())
            {
                shop.Add(ModContent.ItemType<FrostysClicker>(), Condition.DownedEverscream, Condition.DownedSantaNK1, Condition.DownedIceQueen);
            }
            if (type == NPCID.Clothier)
            {
                shop.AddWithCustomValue(ModContent.ItemType<SilvaCapsuit>(), Item.buyPrice(gold: 8), new Condition(CalamityUtils.GetText("Condition.PostDoG"), () => DownedBossSystem.downedDoG));
            }
        }
    }
}
