using ClickerClass;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace CalamityClickers
{
    public class CalamityClickersSystem : ModSystem
    {
        internal static bool FinalizedRegister { get; private set; }
        public static HashSet<string> PostMLClickerEffects { get; private set; }
        public override void OnModLoad()
        {
            FinalizedRegister = false;

            PostMLClickerEffects = new HashSet<string>();
        }
        public override void OnModUnload()
        {
            FinalizedRegister = false;

            PostMLClickerEffects = null;
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
            PostMLClickerEffects.Add(ClickEffect.Conqueror);
            PostMLClickerEffects.Add(ClickEffect.TheClick);
            PostMLClickerEffects.Add(ClickEffect.Transcend);
        }
    }
}
