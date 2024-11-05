using ClickerClass;
using ClickerClass.Items.Misc;
using Terraria;
using Terraria.Audio;

namespace CalamityClickers.Content.Items.Misc.SFXButton
{
    public class SFXButtonAstral : SFXButtonBase
    {
        public static void PlaySound(int stack)
        {
            SoundStyle style = new SoundStyle($"CalamityMod/Sounds/NPCHit/AstralEnemyHit{Main.rand.Next(1, 4).ToString()}").WithVolumeScale(0.5f * stack) with
            {
                PitchVariance = 0.2f,
            };
            SoundEngine.PlaySound(style);
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            ClickerSystem.RegisterSFXButton(this, PlaySound); //The cast is necessary here to avoid a warning
        }

        /*public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup("AnyNormalSFXButton")
                .AddIngredient<StarblightSoot>(10)
                .AddTile<CosmicAnvil>()
                .Register();
        }*/
    }
}
