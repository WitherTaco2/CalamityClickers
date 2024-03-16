using CalamityMod.Items;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using ClickerClass;
using ClickerClass.Items;
using ClickerClass.Items.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Accessories
{
    public class FingerOfBloodGod : ClickerItem, ILocalizedModType, IModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.value = CalamityGlobalItem.Rarity12BuyPrice;
            Item.rare = ModContent.RarityType<Turquoise>();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
            clickerPlayer.accTriggerFinger = true;
            player.GetModPlayer<CalamityClickersPlayer>().fingerOfBG = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<TriggerFinger>()
                .AddIngredient<BloodstoneCore>(5)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
