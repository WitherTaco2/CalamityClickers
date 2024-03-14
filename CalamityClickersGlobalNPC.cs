using CalamityClickers.Content.Items.Weapons;
using CalamityMod;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.HiveMind;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace CalamityClickers
{
    public class CalamityClickersGlobalNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            LeadingConditionRule mainRule = npcLoot.DefineNormalOnlyDropSet();
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
        }
    }
}
