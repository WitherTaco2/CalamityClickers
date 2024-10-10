using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Tiles.Furniture.CraftingStations;
using ClickerClass;
using ClickerClass.Items.Misc;
using System;
using Terraria;
using Terraria.Audio;

namespace CalamityClickers.Content.Items.Misc
{
    public class SFXButtonAuric : SFXButtonBase
    {
        public static void PlaySound(int stack)
        {
            SoundStyle style = new SoundStyle($"CalamityMod/Sounds/Custom/AuricMine{Main.rand.Next(1, 4).ToString()}").WithVolumeScale(0.5f * stack) with
            {
                PitchVariance = 0.2f,
            };
            SoundEngine.PlaySound(style);
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            ClickerSystem.RegisterSFXButton(this, (Action<int>)PlaySound); //The cast is necessary here to avoid a warning
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup("AnyNormalSFXButton")
                .AddIngredient<AuricOre>(10)
                .AddTile<CosmicAnvil>()
                .Register();
        }
    }
}
