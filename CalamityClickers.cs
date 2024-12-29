using CalamityClickers.Commons;
using Terraria.ModLoader;

namespace CalamityClickers
{
    public class CalamityClickers : Mod
    {
        public static CalamityClickers mod;
        public override void Load()
        {
            mod = this;
            //CooldownRegistry.Register<GodSlayerOverclockCooldown>(GodSlayerOverclockCooldown.ID);
            CalamityClickersLoading.Load();
        }
        public override void Unload()
        {
            mod = null;
        }
        public override object Call(params object[] args) => CalamityClickersModCalls.Call(args);
    }
}