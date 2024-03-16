using CalamityMod.Cooldowns;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace CalamityClickers.Content.Cooldowns
{
    internal class GodSlayerOverclockCooldown : CooldownHandler
    {
        public new static string ID => nameof(GodSlayerOverclockCooldown);
        public override bool ShouldDisplay => true;
        public override LocalizedText DisplayName => Language.GetOrRegister("Mods.Clamity.UI.Cooldowns." + GodSlayerOverclockCooldown.ID);
        public override string Texture => "CalamityClickers/Content/Cooldowns/GodSlayerOverclockCooldown";
        public override Color OutlineColor => new Color(252, 109, 202);
        public override Color CooldownStartColor => new Color(154, 140, 191);
        public override Color CooldownEndColor => new Color(206, 188, 256);

    }
}
