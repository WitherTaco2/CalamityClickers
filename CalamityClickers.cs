using CalamityClickers.Commons;
using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers
{
    public class CalamityClickers : Mod
    {
        public static CalamityClickers mod;
        public static Mod extraAPI;
        public override void Load()
        {
            mod = this;
            //CooldownRegistry.Register<GodSlayerOverclockCooldown>(GodSlayerOverclockCooldown.ID);
            ModLoader.TryGetMod("ClickerExtraAPI", out extraAPI);
            CalamityClickersLoading.Load();

            extraAPI.Call("NerfTheClicker");
            extraAPI.Call("AddTheClickerRecipeIngredient", ItemID.WhitePaint, 50);
            extraAPI.Call("AddTheClickerRecipeIngredient", ModContent.ItemType<ShadowspecBar>(), 5);
            extraAPI.Call("SetTheClickerRecipeCraftingStation", ModContent.TileType<DraedonsForge>());
        }
        public override void Unload()
        {
            mod = null;
        }
        public override object Call(params object[] args) => CalamityClickersModCalls.Call(args);
    }
}