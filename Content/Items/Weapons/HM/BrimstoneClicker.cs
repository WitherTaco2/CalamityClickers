using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items;
using CalamityMod.Items.Materials;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.HM
{
    public class BrimstoneClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 3f;
        public override Color RadiusColor => new Color(166, 46, 61);
        public override void SafeSetStaticDefaults()
        {
            BrimstoneClicker.ClickerEffect = ClickerSystem.RegisterClickEffect(Mod, "BloodyExplosion", 10, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<BrimstoneClickerProjectile>(), damage * 3, knockBack, player.whoAmI);
            });
        }
        public override void SafeSetDefaults()
        {
            AddEffect(Item, BrimstoneClicker.ClickerEffect);

            Item.damage = 23;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Pink;
            Item.value = CalamityGlobalItem.Rarity5BuyPrice;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<UnholyCore>(5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
    public class BrimstoneClickerProjectile : ModProjectile, ILocalizedModType
    {
        public new string LocalizationCategory => "Projectiles.Clicker";
        public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

        public bool Spawned
        {
            get => Projectile.ai[0] == 1f;
            set => Projectile.ai[0] = value ? 1f : 0f;
        }
        public override void SetDefaults()
        {
            Projectile.width = 250;
            Projectile.height = 250;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 10;
            Projectile.alpha = 255;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 3;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
            Projectile.DamageType = ModContent.GetInstance<ClickerDamage>();
        }
        public override void AI()
        {
            if (!Spawned)
            {
                Spawned = true;

                SoundEngine.PlaySound(SoundID.Item74, Projectile.Center);

                for (int k = 0; k < 30; k++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.Center, 8, 8, ModContent.DustType<BrimstoneFlame>(), Main.rand.NextFloat(-12f, 12f), Main.rand.NextFloat(-12f, 12f), 125, default, 2f);
                    dust.noGravity = true;
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 180);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 180);
        }
    }
}
