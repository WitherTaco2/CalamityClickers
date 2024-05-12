using CalamityMod.Items;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.Ranged;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.HM
{
    public class MeldClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 6f;
        public override Color RadiusColor => new Color(117, 255, 159);
        public override void SetStaticDefaultsExtra()
        {
            ClickerEffect = ClickerSystem.RegisterClickEffect(Mod, "Entropy", 15, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                SoundEngine.PlaySound(DeadSunsWind.Explosion with { Pitch = 0.75f }, position);
                int index = Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<DeadSunExplosion>(), damage * 3, 4f, player.whoAmI, 150, 5);
                Main.projectile[index].DamageType = ModContent.GetInstance<ClickerDamage>();
            });
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, ClickerEffect);
            SetDust(Item, DustID.Obsidian);

            Item.damage = 95;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Cyan;
            Item.value = CalamityGlobalItem.RarityCyanBuyPrice;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<MeldConstruct>(12)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
