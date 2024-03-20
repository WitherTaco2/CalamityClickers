using CalamityMod.Items;
using CalamityMod.Projectiles.Boss;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.HM
{
    public class AnahitasClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 3.4f;
        //public override Color RadiusColor => new Color(67, 207, 130);
        public override Color RadiusColor => new Color(146, 179, 245);

        public override void SafeSetStaticDefaults()
        {
            AnahitasClicker.ClickerEffect = ClickerSystem.RegisterClickEffect(Mod, "AnahitasMusic", 10, new Color(206, 255, 31), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                float extraRot = Main.rand.NextBool() ? (MathHelper.TwoPi / 12) : 0;
                for (int i = 0; i < 6; i++)
                {
                    Vector2 vec1 = Vector2.UnitX.RotatedBy(MathHelper.TwoPi / 6 * i + extraRot);
                    Projectile.NewProjectile(source, position + vec1 * 100, -vec1, ModContent.ProjectileType<MusicalClickerProjectile>(), damage, knockBack, player.whoAmI);
                }
            });
        }
        public override void SafeSetDefaults()
        {
            AddEffect(Item, AnahitasClicker.ClickerEffect);

            Item.damage = 53;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Lime;
            Item.value = CalamityGlobalItem.Rarity7BuyPrice;
        }
    }
    public class MusicalClickerProjectile : SirenSong, ILocalizedModType
    {
        public new string LocalizationCategory => "Projectiles.Clicker";
        public override string Texture => "CalamityMod/Projectiles/Boss/SirenSong";
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.DamageType = ModContent.GetInstance<ClickerDamage>();
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.scale = 0.75f;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 30;
            Projectile.timeLeft = 300;
        }
    }
}
