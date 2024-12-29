using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Rarities;
using ClickerClass;
using ClickerClass.Items.Weapons.Clickers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.Donor
{
    public class HolyGoldenClicker : ModdedClickerWeapon
    {
        public static string Judgement { get; internal set; } = string.Empty;
        public override float Radius => 8;
        public override Color RadiusColor => Color.Yellow;
        //public override bool SetBorderTexture => true;
        public override void SetStaticDefaultsExtra()
        {
            Judgement = ClickerCompat.RegisterClickEffect(Mod, "Judgement", 20, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Item/LanceofDestinyStrong"), position);
                Projectile.NewProjectile(source, position - new Vector2(0, 1000), new Vector2(0, 1), ModContent.ProjectileType<HolyGoldenClickerProjectile>(), damage, knockBack, player.whoAmI);
            });
            CalamityClickersUtils.RegisterPostWildMagicClickEffect(Judgement);
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, Judgement);
            SetDust(Item, DustID.GoldCoin);

            Item.damage = 170;
            Item.knockBack = 1f;
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;

            Item.Calamity().donorItem = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<GoldClicker>()
                .AddIngredient(ItemID.CrossNecklace)
                .AddIngredient(ItemID.HallowedBar, 5)
                .AddIngredient<DivineGeode>(5)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
            CreateRecipe()
                .AddIngredient<PlatinumClicker>()
                .AddIngredient(ItemID.CrossNecklace)
                .AddIngredient(ItemID.HallowedBar, 5)
                .AddIngredient<DivineGeode>(5)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
    public class HolyGoldenClickerProjectile : BaseLaserbeamProjectile, ILocalizedModType
    {
        public new string LocalizationCategory => "Projectiles.Clicker";
        public override float Lifetime => 60;
        public override float MaxLaserLength => 2400;
        public override float MaxScale => 3f;
        //private string StandartTexturePath => base.Texture;
        private string StandartTexturePath => "CalamityClickers/Content/Items/Weapons/Donor/HolyGoldenClickerProjectile";
        public override string Texture => "CalamityMod/Projectiles/InvisibleProj";
        public override Texture2D LaserBeginTexture => ModContent.Request<Texture2D>(StandartTexturePath + "_Start").Value;
        public override Texture2D LaserMiddleTexture => ModContent.Request<Texture2D>(StandartTexturePath + "_Middle").Value;
        public override Texture2D LaserEndTexture => ModContent.Request<Texture2D>(StandartTexturePath + "_End").Value;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            //Main.projFrames[Type] = 5;
        }
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 56;
            Projectile.friendly = true;
            Projectile.DamageType = ModContent.GetInstance<ClickerDamage>();
            Projectile.timeLeft = (int)Lifetime;
            //Projectile.arrow = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.localNPCHitCooldown = 10;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.tileCollide = false;
            //Projectile.ignoreWater = true;
        }
        //public override bool ShouldUpdatePosition() => false;
        //public override void DetermineScale() => Projectile.scale = MathHelper.Lerp(0.01f, 2f, 1f - Projectile.timeLeft / Lifetime);
        public override void DetermineScale() => Projectile.scale = Projectile.timeLeft / Lifetime * MaxScale;
        /*public override void OnSpawn(IEntitySource source)
        {
            Projectile.scale = 0;
        }*/
        /*public override void ExtraBehavior()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 5)
            {
                Projectile.frame++;
                if (Projectile.frameCounter > Main.projFrames[Type])
                {
                    Projectile.frame = 0;
                }
                Projectile.frameCounter = 0;
            }
            //Projectile.scale += 1f / Lifetime;
        }*/
        /*public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.velocity == Vector2.Zero)
            {
                return false;
            }

            DrawBeamWithColor(LaserOverlayColor, Projectile.scale, Projectile.frame, Projectile.frame, Projectile.frame);
            return false;
        }*/
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<HolyFlames>(), 120);
        }
    }
}
