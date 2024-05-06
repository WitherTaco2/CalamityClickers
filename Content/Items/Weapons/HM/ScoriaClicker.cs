using CalamityMod.Items;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.HM
{
    public class ScoriaClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 5f;
        public override Color RadiusColor => new Color(224, 108, 29);
        public override void SetStaticDefaultsExtra()
        {
            ClickerEffect = ClickerSystem.RegisterClickEffect(Mod, "Eruption", 7, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                int index = Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<ForbiddenSunburst>(), damage, 0.5f, player.whoAmI);
                Main.projectile[index].DamageType = ModContent.GetInstance<ClickerDamage>();
            });
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, ClickerEffect);
            SetDust(Item, DustID.Flare);

            Item.damage = 80;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Yellow;
            Item.value = CalamityGlobalItem.Rarity8BuyPrice;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ScoriaBar>(10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
