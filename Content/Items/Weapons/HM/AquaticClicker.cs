using CalamityClickers.Content.Items.Weapons.PreHM;
using CalamityMod.Items;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace CalamityClickers.Content.Items.Weapons.HM
{
    public class AquaticClicker : ModdedClickerWeapon
    {
        //public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 3f;
        public override Color RadiusColor => new Color(166, 46, 61);
        public override void SetStaticDefaultsExtra()
        {
            /*BrimstoneClicker.ClickerEffect = ClickerSystem.RegisterClickEffect(Mod, "BrimstoneInferno", 10, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<BrimstoneClickerProjectile>(), damage * 3, knockBack, player.whoAmI);
            });*/
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, ScourgeClicker.Sandstorm);
            //SetDust(Item, ModContent.DustType<BrimstoneFlameClickers>());

            Item.damage = 23;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Pink;
            Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
        }
    }
}
