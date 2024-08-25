using CalamityMod;
using CalamityMod.Cooldowns;
using CalamityMod.Items;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using ClickerClass;
using ClickerClass.Items.Weapons.Clickers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.PostML
{
    internal class BloodstoneClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 7f;
        public override Color RadiusColor => new Color(204, 42, 60);
        public override void SetStaticDefaultsExtra()
        {
            ClickerEffect = CalamityClickersUtils.RegisterClickEffect(Mod, "Lifestealing", 1, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                if (player.moonLeech || player.HasCooldown(LifeSteal.ID))
                    return;

                player.statLife += 2;
                player.HealEffect(2);
                player.AddCooldown(LifeSteal.ID, 20);

            }, postMoonLord: true);
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, ClickerEffect);
            AddEffect(Item, ClickEffect.Linger);
            SetDust(Item, DustID.Blood);

            Item.damage = 150;
            Item.knockBack = 1f;
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<HemoClicker>()
                .AddIngredient<BloodstoneCore>(8)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
