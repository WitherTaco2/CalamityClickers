using CalamityMod.Cooldowns;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace CalamityClickers.Content.Cooldowns
{
    public class UltraboostCooldown : CooldownHandler
    {
        public new static string ID => nameof(UltraboostCooldown);
        public override bool ShouldDisplay => true;
        public override LocalizedText DisplayName => Language.GetOrRegister("Mods.CalamityClickers.UI.Cooldowns." + UltraboostCooldown.ID);
        public override string Texture => "CalamityClickers/Content/Cooldowns/" + ID;
        public override Color OutlineColor => new Color(252, 109, 202);
        public override Color CooldownStartColor => new Color(154, 140, 191);
        public override Color CooldownEndColor => new Color(206, 188, 256);

    }
}
