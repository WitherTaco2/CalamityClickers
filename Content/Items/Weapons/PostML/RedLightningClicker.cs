using CalamityMod.Items;
using CalamityMod.Projectiles.Summon;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.PostML
{
    public class RedLightningClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 6.1f;
        public override Color RadiusColor => Color.Red;
        public override void SetStaticDefaultsExtra()
        {
            RedLightningClicker.ClickerEffect = CalamityClickersUtils.RegisterClickEffect(Mod, "RedLightning", 10, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                for (int n = 0; n < 4; n++)
                {
                    //Projectile proj = CalamityUtils.ProjectileRain(source, position, 400f, 100f, 500f, 800f, 15f, ModContent.ProjectileType<DaedalusLightning>(), damage, 1f, player.whoAmI);
                    //proj.DamageType = ModContent.GetInstance<ClickerDamage>();

                    float x = position.X + Main.rand.NextFloat(-400f, 400f);
                    float y = position.Y - Main.rand.NextFloat(500f, 800f);
                    Vector2 vector = new Vector2(x, y);
                    Vector2 velocity = position - vector;
                    velocity.X += Main.rand.NextFloat(-100f, 100f);
                    float num = velocity.Length();
                    num = 5f / num;
                    velocity.X *= num;
                    velocity.Y *= num;
                    Projectile proj = Projectile.NewProjectileDirect(source, vector, velocity, ModContent.ProjectileType<DaedalusLightning>(), damage, 1f, player.whoAmI, velocity.ToRotation(), Main.rand.Next(100));
                    proj.DamageType = ModContent.GetInstance<ClickerDamage>();
                }
            }, postMoonLord: true);

        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, RedLightningClicker.ClickerEffect);
            SetDust(Item, 160);

            Item.damage = 130;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Purple;
            Item.value = CalamityGlobalItem.Rarity11BuyPrice;

        }
    }
}
