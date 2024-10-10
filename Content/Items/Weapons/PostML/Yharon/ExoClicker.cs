using CalamityMod.Items;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.PostML.Yharon
{
    public class ExoClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 8f;
        public override Color RadiusColor => Color.Lerp(Main.DiscoColor, Color.Gray, 0.5f);
        public override int DustType => DustID.Stone;
        public override void SetStaticDefaultsExtra()
        {
            ClickerEffect = CalamityClickersUtils.RegisterClickEffect(Mod, "ExoCrystal", 15, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                for (int i = 0; i < 7; i++)
                {
                    Vector2 vec = Vector2.UnitY.RotatedByRandom(0.75f);
                    int p = Projectile.NewProjectile(source, position - vec * Main.rand.NextFloat(500, 1000), vec * 10, ModContent.ProjectileType<ExoCrystalArrow>(), damage / 3, knockBack / 2, player.whoAmI);
                    Main.projectile[p].DamageType = ModContent.GetInstance<ClickerDamage>();
                }
            }, postMoonLord: true);
            CalamityClickersUtils.RegisterBlacklistedClickEffect(ClickerEffect);
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, ClickerEffect);
            SetDust(Item, DustType);

            Item.damage = 420;
            Item.knockBack = 1f;
            Item.rare = ModContent.RarityType<Violet>();
            Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
        }
    }
}
