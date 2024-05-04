using CalamityMod.Items;
using CalamityMod.Items.Materials;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace CalamityClickers.Content.Items.Weapons.PreHM
{
    public class WulfrumClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 1.5f;
        public override Color RadiusColor => new Color(160, 237, 0);
        public override void SetStaticDefaultsExtra()
        {
            ClickerEffect = ClickerSystem.RegisterClickEffect(Mod, "BasicAutoclicker", 1, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {

            }, true);
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, ClickerEffect);
            SetDust(Item, 229);

            Item.damage = 4;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Blue;
            Item.value = CalamityGlobalItem.Rarity1BuyPrice;

        }
        public bool autoClicker = true;
        public override void UpdateInventory(Player player)
        {
            if (player.HeldItem.type == Item.type && autoClicker)
            {
                ClickerCompat.SetAutoReuseEffect(player, 8);
            }
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                autoClicker = !autoClicker;
                return true;
            }
            return base.UseItem(player);
        }
        /*public override void RightClick(Player player)
        {
            autoClicker = !autoClicker;
        }*/
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<WulfrumMetalScrap>(8)
                .AddIngredient<EnergyCore>()
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
