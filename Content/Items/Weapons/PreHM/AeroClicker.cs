using CalamityMod;
using CalamityMod.Items;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Typeless;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.PreHM
{
    public class AeroClicker : ModdedClickerWeapon
    {
        public static string FeatherRain { get; internal set; } = string.Empty;
        public override float Radius => 2.6f;
        public override Color RadiusColor => new Color(157, 200, 193);
        public override void SetStaticDefaultsExtra()
        {
            FeatherRain = ClickerSystem.RegisterClickEffect(Mod, "FeatherRain", 10, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                for (int n = 0; n < 4; n++)
                {
                    Projectile proj = CalamityUtils.ProjectileRain(source, position, 400f, 100f, 500f, 800f, 20f, ModContent.ProjectileType<StickyFeatherAero>(), damage, 1f, player.whoAmI);
                    proj.DamageType = ModContent.GetInstance<ClickerDamage>();
                }
            }, true);
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, FeatherRain);
            SetDust(Item, 206);

            Item.damage = 12;
            Item.knockBack = 1.5f;
            Item.rare = ItemRarityID.Orange;
            Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AerialiteBar>(7)
                .AddIngredient(ItemID.SunplateBlock, 3)
                .AddTile(TileID.SkyMill)
                .Register();
        }
    }
}
