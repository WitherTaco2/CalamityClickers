﻿using CalamityMod.Items;
using CalamityMod.Items.Materials;
using ClickerClass.Items.Weapons.Clickers;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace CalamityClickers.Content.Items.Weapons.PostML
{
    public class PolterplasmeClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 6f;
        public override Color RadiusColor => new Color(255, 80, 128);
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, "ClickerClass:PhaseReach");
            SetDust(Item, 88);

            Item.damage = 130;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Red;
            Item.value = CalamityGlobalItem.Rarity10BuyPrice;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<SpectreClicker>()
                .AddIngredient<Polterplasm>(12)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
