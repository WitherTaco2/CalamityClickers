using CalamityMod.Items;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace CalamityClickers.Content.Items.Weapons.HM
{
    public class CryoClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 3f;
        public override Color RadiusColor => new Color(122, 190, 255);
        public override void SafeSetStaticDefaults()
        {
            CryoClicker.ClickerEffect = ClickerSystem.RegisterClickEffect(Mod, "CoolFlake", 10, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                player.AddBuff(BuffID.CoolWhipPlayerBuff, 4 * 60);
                Projectile.NewProjectile(source, position, Vector2.Zero, ProjectileID.CoolWhipProj, damage, knockBack, player.whoAmI);

            });
        }
        public override void SafeSetDefaults()
        {
            AddEffect(Item, CryoClicker.ClickerEffect);

            Item.damage = 22;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Pink;
            Item.value = CalamityGlobalItem.Rarity5BuyPrice;
        }

    }
}
