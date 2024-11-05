using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace CalamityClickers
{
    public class CalamityClickersConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        public static CalamityClickersConfig Instance;

        [DefaultValue(false)]
        [ReloadRequired]
        public bool LegecyClickerTextures;
    }
}
