using CalamityMod.Items;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace CalamityClickers.Content.Items.Weapons.HM
{
    public class TerraClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 3.5f;
        public override Color RadiusColor => new Color(141, 203, 50);

        public override void SafeSetStaticDefaults()
        {
            TerraClicker.ClickerEffect = ClickerSystem.RegisterClickEffect(Mod, "Terra", 10, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {

            });
        }
        public override void SafeSetDefaults()
        {
            AddEffect(Item, TerraClicker.ClickerEffect);

            Item.damage = 80;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Yellow;
            Item.value = CalamityGlobalItem.Rarity8BuyPrice;
        }
    }
}
