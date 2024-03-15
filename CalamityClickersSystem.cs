using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using ClickerClass.Items.Weapons.Clickers;
using Terraria;
using Terraria.ModLoader;

namespace CalamityClickers
{
    public class CalamityClickersSystem : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe.Create(ModContent.ItemType<TheClicker>())
                .AddIngredient<LordsClicker>()
                .AddIngredient<ShadowspecBar>(5)
                .AddTile<DraedonsForge>()
                .Register();
        }
    }
}
