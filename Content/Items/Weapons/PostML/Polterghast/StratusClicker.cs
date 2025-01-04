using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Rarities;
using ClickerClass.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.PostML.Polterghast
{
    public class StratusClicker : ModdedClickerWeapon
    {
        public static string StratusMoon { get; internal set; } = string.Empty;
        public override float Radius => 7.5f;
        public override Color RadiusColor => new Color(123, 228, 234);
        public override void SetStaticDefaultsExtra()
        {
            StratusMoon = ClickerCompat.RegisterClickEffect(Mod, "StratusMoon", 10, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                float rot = Main.rand.NextFloat(MathHelper.TwoPi);
                for (int i = 0; i < 7; i++)
                {
                    //float random = Main.rand.NextFloat(-0.1f, 0.1f);
                    //Projectile.NewProjectile(source, position - Vector2.UnitY.RotatedBy(random) * 100, Vector2.UnitY.RotatedBy(random) * 10, ModContent.ProjectileType<StratusClickerProjectile>(), damage, knockBack, player.whoAmI);
                    Projectile.NewProjectile(source, position, Vector2.UnitY.RotatedBy(MathHelper.TwoPi / 7 * i + rot) * 10, ModContent.ProjectileType<StratusClickerProjectile>(), damage / 4 * 3, knockBack, player.whoAmI);
                }
            });
            CalamityClickersUtils.RegisterPostWildMagicClickEffect(StratusMoon);
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, StratusMoon);
            SetDust(Item, 176);

            Item.damage = 230;
            Item.knockBack = 1f;
            Item.rare = ModContent.RarityType<PureGreen>();
            Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Lumenyl>(6)
                .AddIngredient<RuinousSoul>(4)
                .AddIngredient<ExodiumCluster>(16)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
    public class StratusClickerProjectile : ModdedClickerProjectile
    {
        public override string Texture => "CalamityMod/Projectiles/Melee/CrescentMoonProj";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaultsExtra()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.alpha = 100;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 220 * Projectile.MaxUpdates;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = Projectile.MaxUpdates * 13;
        }

        public override void AI()
        {
            Projectile.rotation += 0.2f;
            if (Projectile.timeLeft > 60)
                Projectile.velocity *= 0.95f;
            if (Projectile.timeLeft == 60)
            {
                MousePlayer mousePlayer = Main.player[Projectile.owner].GetModPlayer<MousePlayer>();
                if (mousePlayer.TryGetMousePosition(out Vector2 mouseWorld))
                {
                    if (Main.player[Projectile.owner].Clicker().HasAimbotModuleTarget)
                        mouseWorld = Main.npc[Main.player[Projectile.owner].Clicker().accAimbotModuleTarget].Center;
                    Vector2 vector = mouseWorld - Projectile.Center;
                    float speed = 13f;
                    float mag = vector.Length();
                    if (mag > speed)
                    {
                        mag = speed / mag;
                        vector *= mag;
                    }
                    Projectile.velocity = vector;
                    Projectile.netUpdate = true;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Projectile.type], lightColor, 1);
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Nightwither>(), 180);
        }
    }
}
