using ClickerClass;
using ClickerClass.Items.Armors;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Buffs
{
    public class GodSlayerOverclockBuff : ModBuff
    {
        public override LocalizedText Description => base.Description.WithFormatArgs(OverclockHelmet.SetBonusEffectDecrease);

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
            clickerPlayer.clickerBonusPercent -= OverclockHelmet.SetBonusEffectDecrease / 100f;
            player.GetDamage<ClickerDamage>() -= 0.1f;
        }
    }
}
