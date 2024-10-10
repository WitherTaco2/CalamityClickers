using CalamityMod;
using CalamityMod.CustomRecipes;
using CalamityMod.Items;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using ClickerClass;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.DraedonArsenal
{
    public class NuclearClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 8f;
        public override Color RadiusColor => new Color(236, 255, 31);
        public override int DustType => DustID.CursedTorch;
        public override void SetStaticDefaultsExtra()
        {
            ClickerEffect = CalamityClickersUtils.RegisterClickEffect(Mod, "NuclearExplosion", 25, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                SoundEngine.PlaySound(TeslaCannon.FireSound, position);
                for (int i = 0; i < 7; i++)
                {
                    Projectile explosion = Projectile.NewProjectileDirect(source, position, Vector2.Zero, ModContent.ProjectileType<NuclearClickerBoom>(), damage / 5, knockBack, player.whoAmI);
                    if (explosion.whoAmI.WithinBounds(Main.maxProjectiles))
                    {
                        explosion.ai[1] = Main.rand.NextFloat(320f, 870f) + i * 45f; // Randomize the maximum radius.
                        explosion.localAI[1] = Main.rand.NextFloat(0.08f, 0.25f); // And the interpolation step.
                        explosion.Opacity = MathHelper.Lerp(0.18f, 0.6f, i / 7f) + Main.rand.NextFloat(-0.08f, 0.08f);
                        explosion.Calamity().stealthStrike = true;
                        explosion.netUpdate = true;
                    }
                }
            }, postMoonLord: true);
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, ClickerEffect);
            SetDust(Item, DustType);

            Item.damage = 300;
            Item.knockBack = 1f;
            Item.rare = ModContent.RarityType<DarkBlue>();
            Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips) => CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 5);
        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<MysteriousCircuitry>(20).
                AddIngredient<DubiousPlating>(15).
                AddIngredient<CosmiliteBar>(8).
                AddIngredient<AscendantSpiritEssence>(2).
                AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(5, out Func<bool> condition), condition).
                AddTile<CosmicAnvil>().
                Register();
        }
    }
    public class NuclearClickerBoom : BaseMassiveExplosionProjectile, ILocalizedModType, IModType
    {
        public new string LocalizationCategory => "Projectiles.Clicker";

        public override int Lifetime => 60;

        public override bool UsesScreenshake => true;

        public override float GetScreenshakePower(float pulseCompletionRatio)
        {
            return CalamityUtils.Convert01To010(pulseCompletionRatio) * 16f;
        }

        public override Color GetCurrentExplosionColor(float pulseCompletionRatio)
        {
            return Color.Lerp(Color.Yellow * 1.6f, Color.White, MathHelper.Clamp(pulseCompletionRatio * 2.2f, 0f, 1f));
        }

        public override void SetDefaults()
        {
            base.Projectile.width = (base.Projectile.height = 2);
            base.Projectile.friendly = true;
            base.Projectile.tileCollide = false;
            base.Projectile.penetrate = -1;
            base.Projectile.usesLocalNPCImmunity = true;
            base.Projectile.localNPCHitCooldown = 10;
            base.Projectile.timeLeft = Lifetime;
            base.Projectile.DamageType = ModContent.GetInstance<ClickerDamage>();
        }

        public override void PostAI()
        {
            Lighting.AddLight(base.Projectile.Center, 0.2f, 0.1f, 0f);
        }
    }
}
