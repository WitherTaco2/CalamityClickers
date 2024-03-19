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
    public class MusicalClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 3.4f;
        public override Color RadiusColor => new Color(67, 207, 130);

        public override void SafeSetStaticDefaults()
        {
            MusicalClicker.ClickerEffect = ClickerSystem.RegisterClickEffect(Mod, "AnahitasMusic", 8, new Color(206, 255, 31), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                for (int i = 0; i < 8; i++)
                {
                    Vector2 vec1 = Vector2.UnitX.RotatedBy(MathHelper.TwoPi / 8 * i);
                    Projectile.NewProjectile(source, position + vec1 * 100, -vec1 * 5, ModContent.ProjectileType<TideClickerProjectile>(), damage, knockBack, player.whoAmI);
                }
            });
        }
        public override void SafeSetDefaults()
        {
            AddEffect(Item, MusicalClicker.ClickerEffect);

            Item.damage = 53;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Lime;
            Item.value = CalamityGlobalItem.Rarity5BuyPrice;
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
