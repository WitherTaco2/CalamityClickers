using ClickerClass;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace CalamityClickers
{
    public class CalamityClickersSystem : ModSystem
    {
        internal static bool FinalizedRegister { get; private set; }
        public static HashSet<string> PostMLClickerEffects { get; private set; }
        public static HashSet<string> BlacklistedClickerEffects { get; private set; }
        public override void OnModLoad()
        {
            FinalizedRegister = false;

            PostMLClickerEffects = new HashSet<string>();
            BlacklistedClickerEffects = new HashSet<string>();
        }
        public override void OnModUnload()
        {
            FinalizedRegister = false;

            PostMLClickerEffects = null;
            BlacklistedClickerEffects = null;
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

            BlacklistedClickerEffects.Add(ClickEffect.PhaseReach);
            BlacklistedClickerEffects.Add(ClickEffect.WildMagic);
            BlacklistedClickerEffects.Add(CalamityClickersEffects.WildMagic);
            BlacklistedClickerEffects.Add(ClickEffect.Mania);
            BlacklistedClickerEffects.Add(ClickEffect.AutoClick);
            BlacklistedClickerEffects.Add(ClickEffect.Bold);
            BlacklistedClickerEffects.Add(ClickEffect.Yoink);
            BlacklistedClickerEffects.Add(ClickEffect.Nab);

            BlacklistedClickerEffects.Add(ClickEffect.TheClick);
            BlacklistedClickerEffects.Add(ClickEffect.Transcend);
        }
    }
}
