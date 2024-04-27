using CalamityMod.Dusts;
using CalamityMod.Items;
using ClickerClass;
using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.HM
{
    public class CosmicStarClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 6f;
        public override Color RadiusColor => new Color(123, 99, 130);
        public override void SetStaticDefaultsExtra()
        {
            ClickerEffect = ClickerSystem.RegisterClickEffect(Mod, "StarRain", 15, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 vec = Vector2.UnitY.RotateRandom(0.5f);
                    Projectile.NewProjectile(source, position - vec * 250, vec * 30, ModContent.ProjectileType<StarryClickerPro>(), damage * 2, 1, player.whoAmI, position.X, position.Y);
                }
            });
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, ClickerEffect);
            SetDust(Item, ModContent.DustType<AstralBasic>());

            Item.damage = 95;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Cyan;
            Item.value = CalamityGlobalItem.Rarity9BuyPrice;
        }
    }
}
