﻿using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items;
using CalamityMod.Items.Materials;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.PreHM
{
    public class FleshyClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 2.5f;
        public override Color RadiusColor => new Color(153, 54, 63);
        public override void SetStaticDefaultsExtra()
        {
            ClickerEffect = ClickerSystem.RegisterClickEffect(Mod, "BloodyExplosion", 7, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<FleshyClickerProjectile>(), damage * 2, knockBack, player.whoAmI);
            }, true);
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, ClickerEffect);
            SetDust(Item, DustID.CrimtaneWeapons);

            Item.damage = 10;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Orange;
            Item.value = CalamityGlobalItem.Rarity3BuyPrice;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CrimtaneBar, 3)
                .AddIngredient<BloodSample>(9)
                .AddIngredient(ItemID.Vertebrae, 3)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
    public class FleshyClickerProjectile : ModdedClickerProjectile
    {
        public bool Spawned
        {
            get => Projectile.ai[0] == 1f;
            set => Projectile.ai[0] = value ? 1f : 0f;
        }
        public override void SetDefaultsExtra()
        {
            Projectile.width = 200;
            Projectile.height = 200;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 10;
            Projectile.alpha = 255;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 3;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
        }
        public override void AI()
        {
            if (!Spawned)
            {
                Spawned = true;

                SoundEngine.PlaySound(SoundID.Item74, Projectile.Center);

                for (int k = 0; k < 30; k++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.Center, 8, 8, DustID.CrimtaneWeapons, Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 125, default, 2f);
                    dust.noGravity = true;
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<BurningBlood>(), 180);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<BurningBlood>(), 180);
        }
    }
}
