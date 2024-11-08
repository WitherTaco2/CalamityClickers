using CalamityMod.Items;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Typeless;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.PreHM
{
    public class SeaRemainClicker : ModdedClickerWeapon
    {
        public static string SeaBubble { get; internal set; } = string.Empty;
        public override float Radius => 2f;
        public override Color RadiusColor => new Color(25, 79, 150);
        public override void SetStaticDefaultsExtra()
        {
            SeaBubble = ClickerSystem.RegisterClickEffect(Mod, "SeaBubble", 8, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<SeaRemainClickerProjectile>(), damage * 2, knockBack, player.whoAmI, 1);
            }, true);
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, SeaBubble);

            Item.damage = 6;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Green;
            Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<SeaRemains>(2)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
    public class SeaRemainClickerProjectile : SulphuricAcidBubbleFriendly, ILocalizedModType
    {
        public new string LocalizationCategory => "Projectiles.Clicker";
        public override string Texture => (GetType().Namespace + "." + Name).Replace('.', '/');
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.penetrate = 5;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 60;
            Projectile.DamageType = ModContent.GetInstance<ClickerDamage>();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {

        }
    }
}
