using CalamityMod.Items;
using CalamityMod.Projectiles.Rogue;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.HM
{
    public class PlagueClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 6f;
        public override Color RadiusColor => new Color(154, 186, 74);
        public override void SetStaticDefaultsExtra()
        {
            ClickerEffect = ClickerSystem.RegisterClickEffect(Mod, "PlagueBees", 15, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                for (int projIndex = 0; projIndex < 20; projIndex++)
                {
                    float speedX = (float)Main.rand.Next(-35, 36) * 0.02f;
                    float speedY = (float)Main.rand.Next(-35, 36) * 0.02f;
                    Projectile.NewProjectile(source, position.X, position.Y, speedX, speedY, ModContent.ProjectileType<PlagueClickerProjectile>(), player.beeDamage(damage / 4), player.beeKB(0f), player.whoAmI);
                }
            });
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, ClickerEffect);
            SetDust(Item, 220);

            Item.damage = 86;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Yellow;
            Item.value = CalamityGlobalItem.Rarity8BuyPrice;
        }
    }
    public class PlagueClickerProjectile : PlaguenadeBee
    {
        public override string Texture => ModContent.GetInstance<PlaguenadeBee>().Texture;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.DamageType = ModContent.GetInstance<ClickerDamage>();
        }
        public override bool? CanHitNPC(NPC target)
        {
            return Projectile.timeLeft < 220;
        }
    }
}
