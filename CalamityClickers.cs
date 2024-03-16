using CalamityClickers.Content.Cooldowns;
using CalamityMod.Cooldowns;
using Terraria.ModLoader;

namespace CalamityClickers
{
    public class CalamityClickers : Mod
    {
        public override void Load()
        {
            CooldownRegistry.Register<GodSlayerOverclockCooldown>(GodSlayerOverclockCooldown.ID);
        }
    }
}