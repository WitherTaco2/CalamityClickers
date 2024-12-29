using System.Collections.Generic;
using Terraria.ModLoader;

namespace CalamityClickers
{
    public class CalamityClickersSystem : ModSystem
    {
        internal static bool FinalizedRegister { get; private set; }
        public static HashSet<string> PostNightmareClickerEffects { get; private set; }
        public override void OnModLoad()
        {
            FinalizedRegister = false;

            PostNightmareClickerEffects = new HashSet<string>();
        }
        public override void OnModUnload()
        {
            FinalizedRegister = false;

            PostNightmareClickerEffects = null;
        }
        public override void PostAddRecipes()
        {
            FinalizedRegister = true;
        }
        public override void SetStaticDefaults()
        {

        }
        public override void PostSetupContent()
        {

        }
    }
}
