using CalamityClickers.Content.NPCs;
using CalamityMod;
using CalamityMod.NPCs.Other;
using CalamityMod.UI.CalamitasEnchants;
using ClickerClass;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Commons
{
    public static class CalamityClickersLoading
    {
        public static void Load()
        {
            LoadEnchantments();
        }
        public static void LoadEnchantments()
        {
            //var changes = EnchantmentManager.EnchantmentList.Find(ench => ench.ID == 1200);
            var index = EnchantmentManager.EnchantmentList.FindIndex(ench => ench.ID == 1200);
            EnchantmentManager.EnchantmentList[index] = new Enchantment(
                CalamityUtils.GetText("UI.Lecherous.DisplayName"), CalamityUtils.GetText("UI.Lecherous.Description"),
                1200,
                "CalamityMod/UI/CalamitasEnchantments/CurseIcon_Lecherous",
                null,
                player =>
                {
                    if (Main.gameMenu)
                        return;

                    player.Calamity().lecherousOrbEnchant = true;

                    bool orbIsPresent = false;
                    int orbType = ModContent.NPCType<LecherousOrb>();
                    foreach (NPC n in Main.ActiveNPCs)
                    {
                        if (n.type != orbType || n.target != player.whoAmI)
                            continue;

                        orbIsPresent = true;
                        break;
                    }

                    if (Main.myPlayer == player.whoAmI && !orbIsPresent && !player.Calamity().awaitingLecherousOrbSpawn)
                    {
                        player.Calamity().awaitingLecherousOrbSpawn = true;
                        CalamityNetcode.NewNPC_ClientSide(player.Center, orbType, player);
                    }
                },
                new Predicate<Item>(EnchantableChanges1)
            );

            ModLoader.GetMod("CalamityMod").Call(
                "CreateEnchantment",
                LangHelper.GetLocalizedText("UI.Enchantments.Lecherous.DisplayName"), LangHelper.GetLocalizedText("UI.Enchantments.Lecherous.Description"),
                20000,
                new Predicate<Item>(EnchantableClicker),
                "CalamityMod/UI/CalamitasEnchantments/CurseIcon_Lecherous",
                delegate (Player player)
                {
                    if (Main.gameMenu)
                        return;
                    player.CalClicker().enchLecherous = true;

                    bool orbIsPresent = false;
                    int orbType = ModContent.NPCType<LecherousOrbClicker>();
                    foreach (NPC n in Main.ActiveNPCs)
                    {
                        if (n.type != orbType || n.target != player.whoAmI)
                            continue;

                        orbIsPresent = true;
                        break;
                    }

                    if (Main.myPlayer == player.whoAmI && !orbIsPresent && !player.Calamity().awaitingLecherousOrbSpawn)
                    {
                        player.Calamity().awaitingLecherousOrbSpawn = true;
                        CalamityNetcode.NewNPC_ClientSide(player.Center, orbType, player);
                    }
                }

            );
        }
        private static bool EnchantableClicker(Item item) => !item.IsEnchantable() && item.damage > 0 && ClickerSystem.IsClickerWeapon(item);
        private static bool EnchantableChanges1(Item item) => item.IsEnchantable() && item.damage > 0 && item.shoot > ProjectileID.None && !item.CountsAsClass<SummonDamageClass>() && !item.IsTrueMelee() && !ClickerSystem.IsClickerWeapon(item);
    }
}
