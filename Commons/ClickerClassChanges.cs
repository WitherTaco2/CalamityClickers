using CalamityMod;
using ClickerClass;
using ClickerClass.Items.Accessories;
using ClickerClass.Items.Tools;
using ClickerClass.Projectiles;
using System;
using System.Collections.Generic;
using System.Text;
using Terraria;
using Terraria.ModLoader;

namespace CalamityClickers.Commons
{
    public class ClickerClassChangesItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.type == ModContent.ItemType<MiceHamaxe>())
            {
                item.axe = 175 / 5;
                item.useTime = 5;
            }
            if (item.type == ModContent.ItemType<MiceDrill>())
            {
                item.useTime = 3;
                item.tileBoost += 1;
            }
        }
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.type == ModContent.ItemType<TheScroller>())
            {
                if (player.Clicker().setMice)
                {
                    player.GetDamage<ClickerDamage>() += 0.07f;
                    player.Clicker().clickerRadius += 0.2f;
                }
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            void ApplyTooltipEdits(IList<TooltipLine> lines, Func<Item, TooltipLine, bool> predicate, Action<TooltipLine> action)
            {
                foreach (TooltipLine line in lines)
                    if (predicate.Invoke(item, line))
                        action.Invoke(line);
            }

            // This function produces simple predicates to match a specific line of a tooltip, by number/index.
            Func<Item, TooltipLine, bool> LineNum(int n) => (Item i, TooltipLine l) => l.Mod == "Terraria" && l.Name == $"Tooltip{n}";
            // This function produces simple predicates to match a specific line of a tooltip, by name.
            Func<Item, TooltipLine, bool> LineName(string s) => (Item i, TooltipLine l) => l.Mod == "Terraria" && l.Name == s;

            void EditTooltipByNum(int lineNum, Action<TooltipLine> action) => ApplyTooltipEdits(tooltips, LineNum(lineNum), action);

            string[] vertSpeedStrings = new string[] { "Bad vertical speed", "Average vertical speed", "Good vertical speed", "Great vertical speed", "Excellent vertical speed" };
            string WingStatsTooltip(float hSpeed, float accelMult, int vertSpeed, int flightTime, string extraTooltip = null)
            {
                StringBuilder sb = new StringBuilder(512);
                sb.Append('\n');
                sb.Append($"Horizontal speed: {hSpeed:N2}\n");
                sb.Append($"Acceleration multiplier: {accelMult:N1}\n");
                sb.Append(vertSpeedStrings[vertSpeed]);
                sb.Append('\n');
                sb.Append($"Flight time: {flightTime}");
                if (extraTooltip != null)
                {
                    sb.Append('\n');
                    sb.Append(extraTooltip);
                }
                return sb.ToString();
            }
            void AddWingStats(float h, float a, int v, int f, string s = null) => EditTooltipByNum(0, (line) => line.Text += WingStatsTooltip(h, a, v, f, s));

            if (item.type == ModContent.ItemType<TheScroller>())
            {
                AddWingStats(3.5f, 1.15f, 4, 180, "7% increased click damage and 10% icreased clicker radius\n" +
                    "while wearing the Mice Armor");
            }
        }
    }
    public class ClickerClassChangesProjectile : GlobalProjectile
    {
        public override void SetDefaults(Projectile proj)
        {
            if (proj.type == ModContent.ProjectileType<MiceDrillPro>())
            {
                proj.DamageType = ModContent.GetInstance<TrueMeleeDamageClass>();
            }
        }
    }
    public class ClickerClassChangesPlayer : ModPlayer
    {

    }
}
