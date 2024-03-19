using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using ClickerClass;
using ClickerClass.Items;
using ClickerClass.Items.Accessories;
using ClickerClass.Items.Weapons.Clickers;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
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
        public override void AddRecipes()
        {
            foreach (Recipe recipe in Main.recipe)
            {
                /*if (recipe.HasResult(ModContent.ItemType<TheAbsorber>()))
                {
                    recipe.RemoveIngredient(ModContent.ItemType<MolluskHusk>());
                    recipe.AddIngredient<HuskOfCalamity>(5);
                }*/
                if (recipe.HasResult<GalacticaSingularity>())
                {
                    recipe.AddIngredient<MiceFragment>();
                }
                /*if (recipe.HasResult(ItemID.CelestialSigil))
                {
                    if (recipe.Mod == null)
                    {
                        recipe.AddIngredient<MiceFragment>(6);
                    }
                }*/
                if (recipe.HasResult<GamerCrate>())
                {
                    recipe.AddIngredient<PurifiedGel>(5);
                }

            }

            Recipe.Create(ModContent.ItemType<TheClicker>())
                .AddIngredient<LordsClicker>()
                .AddIngredient(ItemID.WhitePaint, 50)
                .AddIngredient<ShadowspecBar>(5)
                .AddTile<DraedonsForge>()
                .Register();
        }
    }
}
