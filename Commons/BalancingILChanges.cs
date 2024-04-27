using CalamityMod;
using ClickerClass.Items.Weapons.Clickers;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityClickers.Commons
{
    public class BalancingILChanges : ModSystem
    {
        public override void OnModLoad()
        {
            On_ShimmerTransforms.IsItemTransformLocked += AdjustShimmerRequirements;

        }
        private static bool AdjustShimmerRequirements(On_ShimmerTransforms.orig_IsItemTransformLocked orig, int type)
        {
            if (type == ModContent.ItemType<QuintessenceClicker>())
            {
                return !DownedBossSystem.downedCalamitas || !DownedBossSystem.downedExoMechs;
            }

            return orig(type);
        }
    }
}
