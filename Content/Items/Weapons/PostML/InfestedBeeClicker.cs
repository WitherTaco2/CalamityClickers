using CalamityClickers.Content.Items.Weapons.HM;
using CalamityMod.Items;
using ClickerClass;
using ClickerClass.Items.Weapons.Clickers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.PostML
{
    public class InfestedBeeClicker : ModdedClickerWeapon
    {
        public static string PlagueBees2 { get; internal set; } = string.Empty;
        public override float Radius => 6f;
        public override Color RadiusColor => new Color(154, 186, 74);
        public override bool SetBorderTexture => true;
        public override void SetStaticDefaultsExtra()
        {
            PlagueBees2 = ClickerCompat.RegisterClickEffect(Mod, "PlagueBees2", 12, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                for (int projIndex = 0; projIndex < 20; projIndex++)
                {
                    float speedX = (float)Main.rand.Next(-35, 36) * 0.02f;
                    float speedY = (float)Main.rand.Next(-35, 36) * 0.02f;
                    Projectile.NewProjectile(source, position.X, position.Y, speedX, speedY, ModContent.ProjectileType<PlagueClickerProjectile>(), player.beeDamage(damage / 4), player.beeKB(0f), player.whoAmI);
                }
            });
            CalamityClickersUtils.RegisterPostWildMagicClickEffect(PlagueBees2);
        }

        public override void SetDefaultsExtra()
        {
            AddEffect(Item, PlagueBees2);
            AddEffect(Item, ClickEffect.StickyHoney);
            //SetColor(Item, color());

            Item.damage = 110;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Red;
            Item.value = CalamityGlobalItem.RarityRedBuyPrice;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<PlagueClicker>()
                .AddIngredient<HoneyGlazedClicker>()
                .AddIngredient(ItemID.LunarBar, 5)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
