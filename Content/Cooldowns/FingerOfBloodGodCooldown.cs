using CalamityMod.Cooldowns;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace CalamityClickers.Content.Cooldowns
{
    public class FingerOfBloodGodCooldown : CooldownHandler
    {
        public new static string ID => nameof(FingerOfBloodGodCooldown);
        public override bool ShouldDisplay => true;
        public override LocalizedText DisplayName => Language.GetOrRegister("Mods.CalamityClickers.UI.Cooldowns." + FingerOfBloodGodCooldown.ID);
        public override string Texture => "CalamityClickers/Content/Cooldowns/FingerOfBloodGodCooldown";
        public override Color OutlineColor => new Color(129, 62, 75);
        public override Color CooldownStartColor => new Color(145, 102, 94);
        public override Color CooldownEndColor => new Color(180, 152, 125);
    }
}
