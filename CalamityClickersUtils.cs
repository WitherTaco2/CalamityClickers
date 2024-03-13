using ClickerClass;
using Terraria;

namespace CalamityClickers
{
    public static class CalamityClickersUtils
    {
        public static ClickerPlayer Clicker(this Player player) => player.GetModPlayer<ClickerPlayer>();
        public static Player Owner(this Projectile proj) => Main.player[proj.owner];
    }
}
