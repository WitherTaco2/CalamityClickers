using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using ClickerClass.Items;
using ClickerClass.Items.Accessories;
using ClickerClass.Items.Tools;
using ClickerClass.Items.Weapons.Clickers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Commons
{
    public class CalamityClickersRecipeSystem : ModSystem
    {
        public override void AddRecipeGroups()
        {
            //RecipeGroup firefly = RecipeGroup.recipeGroups[Calamity.Fireflies];
            //firefly.ValidItems.Add(ModContent.ItemType<TwinklerItem>());

            if (RecipeGroup.recipeGroupIDs.ContainsKey("LunarHamaxe"))
            {
                int index = RecipeGroup.recipeGroupIDs["LunarHamaxe"];
                RecipeGroup group = RecipeGroup.recipeGroups[index];
                group.ValidItems.Add(ModContent.ItemType<MiceHamaxe>());
            }
            if (RecipeGroup.recipeGroupIDs.ContainsKey("LunarPickaxe"))
            {
                int index = RecipeGroup.recipeGroupIDs["LunarPickaxe"];
                RecipeGroup group = RecipeGroup.recipeGroups[index];
                group.ValidItems.Add(ModContent.ItemType<MicePickaxe>());
            }
            if (RecipeGroup.recipeGroupIDs.ContainsKey("AnyWings"))
            {
                int index = RecipeGroup.recipeGroupIDs["AnyWings"];
                RecipeGroup group = RecipeGroup.recipeGroups[index];
                group.ValidItems.Add(ModContent.ItemType<TheScroller>());
            }
        }

        public override void AddRecipes()
        {
            foreach (Recipe recipe in Main.recipe)
            {
                if (recipe.HasResult<GalacticaSingularity>())
                {
                    recipe.AddIngredient<MiceFragment>();
                }
                /*if (recipe.HasResult(ItemID.CelestialSigil) && recipe.Mod == null)
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
