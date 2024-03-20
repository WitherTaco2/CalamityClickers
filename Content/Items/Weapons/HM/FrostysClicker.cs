using CalamityMod.Items;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.HM
{
    public class FrostysClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 5.5f;
        public override Color RadiusColor => new Color(122, 190, 255);
        public override void SafeSetStaticDefaults()
        {
            FrostysClicker.ClickerEffect = ClickerSystem.RegisterClickEffect(Mod, "CoolFlake", 10, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                player.AddBuff(BuffID.CoolWhipPlayerBuff, 4 * 60);
                if (player.ownedProjectileCounts[ProjectileID.CoolWhipProj] < 1)
                {
                    int index = Projectile.NewProjectile(source, position, Vector2.Zero, ProjectileID.CoolWhipProj, damage, knockBack, player.whoAmI);
                    Main.projectile[index].DamageType = ModContent.GetInstance<ClickerDamage>();
                }
            });
        }
        public override void SafeSetDefaults()
        {
            AddEffect(Item, FrostysClicker.ClickerEffect);

            Item.damage = 53;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Yellow;
            Item.value = CalamityGlobalItem.Rarity8BuyPrice;
        }

    }
}
