using CalamityClickers.Content.Items.Weapons.HM;
using CalamityClickers.Content.Items.Weapons.PreHM;
using CalamityMod;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.DLC
{
    public class GeleomegeddonClicker : ModdedClickerWeapon
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.HasMod("CatalystMod");
            //return false;
        }
        public static string NovaSlimer { get; internal set; } = string.Empty;
        public override float Radius => 7f;
        public override Color RadiusColor => new Color(193, 136, 246);
        public override int DustType
        {
            get
            {
                if (ModLoader.TryGetMod("CatalystMod", out var result))
                    return Main.rand.NextBool() ? result.Find<ModDust>("AstraDust").Type : result.Find<ModDust>("AstraDustPurple").Type;
                return ModContent.DustType<AstralChunkDust>();
            }
        }
        public override void SetStaticDefaultsExtra()
        {
            NovaSlimer = ClickerCompat.RegisterClickEffect(Mod, "NovaSlimer", 20, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<GeleomegeddonClickerProjectile>(), damage * 2, knockBack, player.whoAmI);
            });
            CalamityClickersUtils.RegisterPostWildMagicClickEffect(NovaSlimer);
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, NovaSlimer);
            //Mod clamity = ModLoader.GetMod("CatalystMod");
            //SetDust(Item, Main.rand.NextBool() ? clamity.Find<ModDust>("AstraDust").Type : clamity.Find<ModDust>("AstraDustPurple").Type);

            Item.damage = 200;
            Item.knockBack = 1f;
            if (ModLoader.TryGetMod("CatalystMod", out var result))
                Item.rare = result.Find<ModRarity>("SuperbossRarity").Type;
            else
                Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 30);
        }
        public override void UpdateInventory(Player player)
        {
            Mod catalyst = ModLoader.GetMod("CatalystMod");
            SetDust(Item, Main.rand.NextBool() ? catalyst.Find<ModDust>("AstraDust").Type : catalyst.Find<ModDust>("AstraDustPurple").Type);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<GooClicker>()
                .AddIngredient<StellarClicker>()
                .AddIngredient(ModLoader.GetMod("CatalystMod").Find<ModItem>("MetanovaBar"), 5)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
    public class GeleomegeddonClickerProjectile : ModdedClickerProjectile
    {
        public override bool UseInvisibleProjectile => false;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 40;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }
        public override void SetDefaultsExtra()
        {
            Projectile.width = Projectile.height = 36;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            AIType = -1;
        }
        public const int NovaSlimeVelocity = 70;
        public override void AI()
        {
            if (Projectile.timeLeft % 30 > 20)
            {
                CalamityUtils.HomeInOnNPC(Projectile, true, 1000, NovaSlimeVelocity, 50);
            }
            //Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero) * NovaSlimeVelocity;

            Projectile.rotation += 0.4f;

            if (Main.rand.NextBool(4))
            {
                Mod clamity = ModLoader.GetMod("CatalystMod");
                int type = Main.rand.NextBool() ? clamity.Find<ModDust>("AstraDust").Type : clamity.Find<ModDust>("AstraDustPurple").Type;
                Dust dust = Dust.NewDustPerfect(Projectile.Center, type, Main.rand.NextVector2Circular(2, 2));
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Type], Color.White);
            return false;
            //return base.PreDraw(ref lightColor);
        }
    }
}
