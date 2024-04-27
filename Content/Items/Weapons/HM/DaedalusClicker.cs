using CalamityMod.Items;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.HM
{
    public class DaedalusClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 3.3f;
        public override Color RadiusColor => new Color(218, 105, 233);
        public override void SetStaticDefaultsExtra()
        {
            ClickerEffect = ClickerSystem.RegisterClickEffect(Mod, "DaedalusCrystals", 10, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                for (int k = 0; k < 3; k++)
                {
                    int index = Projectile.NewProjectile(source, position.X, position.Y, Main.rand.Next(-35, 36) * 0.2f, Main.rand.Next(-35, 36) * 0.2f, ModContent.ProjectileType<TinyCrystal>(),
                    damage, knockBack * 0.15f, Main.myPlayer, Main.rand.Next(2));
                    Main.projectile[index].DamageType = ModContent.GetInstance<ClickerDamage>();
                }
            });
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, ClickerEffect);
            SetDust(Item, 56);

            Item.damage = 32;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Pink;
            Item.value = CalamityGlobalItem.Rarity5BuyPrice;
        }
        public override void UpdateInventory(Player player)
        {
            SetDust(Item, Main.rand.NextBool() ? 56 : 73);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<CryonicBar>(8)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
