using CalamityMod.Cooldowns;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace CalamityClickers.Content.Cooldowns
{
    public class TarragonClickerCooldown : CooldownHandler
    {
        public new static string ID => nameof(TarragonClickerCooldown);
        public override bool ShouldDisplay => true;
        public override LocalizedText DisplayName => Language.GetOrRegister("Mods.CalamityClickers.UI.Cooldowns." + UltraboostCooldown.ID);
        public override string Texture => "CalamityClickers/Content/Cooldowns/TarragonClickerCooldown";
        public override Color OutlineColor => new Color(214, 181, 83);
        public override Color CooldownStartColor => new Color(89, 110, 38);
        public override Color CooldownEndColor => new Color(96, 156, 55);
    }
}
