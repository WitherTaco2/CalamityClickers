﻿using CalamityMod.Items;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.PostML
{
    public class RuinousClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 7.5f;
        public override Color RadiusColor => new Color(245, 143, 155);
        public override void SetStaticDefaultsExtra()
        {
            ClickerEffect = CalamityClickersUtils.RegisterClickEffect(Mod, "GhastlyPortal", 25, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<RuinousClickerProjectile>(), damage, knockBack, player.whoAmI);
            }, postMoonLord: true);
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, ClickerEffect);
            SetDust(Item, DustID.GemRuby);

            Item.damage = 150;
            Item.knockBack = 1f;
            Item.rare = ModContent.RarityType<PureGreen>();
            Item.value = CalamityGlobalItem.Rarity12BuyPrice;
        }
    }
    public class RuinousClickerProjectile : ClickerProjectileWhichCanShootOnClick
    {
        public override string Texture => ModContent.GetInstance<GhastlyBlast>().Texture;
        public override void SetDefaultsExtra()
        {
            Projectile.width = Projectile.height = 60;
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            //Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = ModContent.GetInstance<ClickerDamage>();
            //Projectile.usesLocalNPCImmunity = true;
            //Projectile.localNPCHitCooldown = 60;
        }
        public override void AIExtra()
        {
            Projectile.rotation -= 0.104719758f;
        }
        public override void ShootOnClick()
        {
            for (int i = 0; i < 2; i++)
            {
                int index = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, new Vector2(10, 0).RotatedByRandom(MathHelper.TwoPi), ModContent.ProjectileType<LostSoulFriendly>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                Main.projectile[index].DamageType = ModContent.GetInstance<ClickerDamage>();
            }
        }
        public override bool? CanHitNPC(NPC target) => false;
        public override bool CanHitPlayer(Player target) => false;
        public override bool CanHitPvp(Player target) => false;
        public override bool PreDraw(ref Color lightColor) => ModContent.GetInstance<GhastlyBlast>().PreDraw(ref lightColor);
        public override Color? GetAlpha(Color lightColor) => Color.White;
        //public override Color? GetAlpha(Color lightColor) => new Color(255, 255, 255, 255);
    }
}
