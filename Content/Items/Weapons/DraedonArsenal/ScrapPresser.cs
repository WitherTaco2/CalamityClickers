using CalamityMod;
using CalamityMod.CustomRecipes;
using CalamityMod.Items;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables;
using CalamityMod.Particles;
using CalamityMod.Rarities;
using ClickerClass;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.DraedonArsenal
{
    public class ScrapPresser : ModdedClickerWeapon
    {
        public static string ScrapMissile { get; internal set; } = string.Empty;
        public override float Radius => 3.4f;
        public override Color RadiusColor => new Color(255, 64, 31);
        public override void SetStaticDefaultsExtra()
        {
            ScrapMissile = ClickerSystem.RegisterClickEffect(Mod, "ScrapMissile", 10, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                Vector2 pos = position;

                NPC npc = CalamityUtils.ClosestNPCAt(pos, 400, false, true);
                if (npc != null)
                {
                    SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/Codebreaker/LongRangeSensorArrayInstall"), pos);
                    Vector2 vec1 = Vector2.UnitY.RotateRandom(0.1f);
                    Projectile.NewProjectile(source, npc.Center - vec1 * 100, vec1, ModContent.ProjectileType<ScrapPresserPro>(), damage, knockBack, player.whoAmI);
                }
            }, true);
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, ScrapMissile);

            Item.damage = 13;
            Item.knockBack = 1.5f;
            Item.rare = ModContent.RarityType<DarkOrange>();
            Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;

            CalamityGlobalItem modItem = Item.Calamity();
            modItem.UsesCharge = true;
            modItem.MaxCharge = 50f;
            modItem.ChargePerUse = 0.01f;

        }
        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<MysteriousCircuitry>(6).
                AddIngredient<DubiousPlating>(5).
                AddIngredient<AerialiteBar>(4).
                AddIngredient<SeaPrism>(7).
                AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(1, out Func<bool> condition), condition).
                AddTile(TileID.Anvils).
                Register();
        }
    }
    public class ScrapPresserPro : ModdedClickerProjectile
    {
        public override void SetDefaultsExtra()
        {
            Projectile.width = Projectile.height = 16;
            Projectile.aiStyle = -1;
            AIType = -1;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
        }
        public override void AI()
        {
            if (Projectile.ai[1] == 0)
            {
                CalamityUtils.HomeInOnNPC(Projectile, false, 500, 20, 20);
            }
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[1] == 0)
            {
                DirectionalPulseRing ring = new DirectionalPulseRing(Projectile.Center, Vector2.Zero, Color.Orange, Vector2.One / 2, 0, 0, 0.75f, 10);
                GeneralParticleHandler.SpawnParticle(ring);
                ring = new DirectionalPulseRing(Projectile.Center, Vector2.Zero, Color.Red, Vector2.One / 2, 0, 0, 1f, 10);
                GeneralParticleHandler.SpawnParticle(ring);
                for (int i = 0; i < 6; i++)
                {
                    SparkParticle spark = new SparkParticle(Projectile.Center, Vector2.UnitX.RotatedBy(MathHelper.TwoPi / 6 * i), false, 15, 1, Color.Orange);
                    GeneralParticleHandler.SpawnParticle(spark);
                }

                Projectile.ExpandHitboxBy(100);
                Projectile.timeLeft = 10;
                Projectile.velocity = Vector2.Zero;
                Projectile.alpha = 255;
                Projectile.ai[1] = 1;
            }
        }
    }
}
