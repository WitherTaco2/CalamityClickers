using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using ClickerClass.Items;
using ClickerClass.Items.Accessories;
using ClickerClass.Items.Weapons.Clickers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers
{
    public class CalamityClickersSystem : ModSystem
    {
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
