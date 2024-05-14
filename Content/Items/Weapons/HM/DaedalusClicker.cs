using CalamityMod;
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
    public class DaedalusClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 3.3f;
        public override Color RadiusColor => new Color(218, 105, 233);
        public override void SetStaticDefaultsExtra()
        {
            ClickerEffect = ClickerSystem.RegisterClickEffect(Mod, "DaedalusCrystals", 10, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                for (int k = 0; k < 3; k++)
                {
                    int index = Projectile.NewProjectile(source, position.X, position.Y, Main.rand.Next(-35, 36) * 0.2f, Main.rand.Next(-35, 36) * 0.2f, ModContent.ProjectileType<DaedalusClickerProjectile>(),
                    damage, knockBack * 0.15f, Main.myPlayer, Main.rand.Next(2));
                    //Main.projectile[index].DamageType = ModContent.GetInstance<ClickerDamage>();
                }
            });
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, ClickerEffect);
            SetDust(Item, 56);

            Item.damage = 32;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Pink;
            Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
        }
        public override void UpdateInventory(Player player)
        {
            SetDust(Item, Main.rand.NextBool() ? 56 : 73);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<CryonicBar>(8)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
    public class DaedalusClickerProjectile : ModdedClickerProjectile
    {
        public override void SetDefaultsExtra()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.DamageType = ModContent.GetInstance<ClickerDamage>();
            Projectile.penetrate = 1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 90;
        }
        public override bool? CanHitNPC(NPC target) => Projectile.timeLeft < 75 && target.CanBeChasedBy(Projectile);
        public override void AI()
        {
            int dustType = Projectile.ai[0] == 0f ? 56 : 73;
            for (int i = 0; i < 2; i++)
            {
                float shortXVel = Projectile.velocity.X / 3f * (float)i;
                float shortYVel = Projectile.velocity.Y / 3f * (float)i;
                int dustPosModifier = 4;
                int crystalDust = Dust.NewDust(new Vector2(Projectile.position.X + (float)dustPosModifier, Projectile.position.Y + (float)dustPosModifier), Projectile.width - dustPosModifier * 2, Projectile.height - dustPosModifier * 2, dustType, 0f, 0f, 100, default, 1.2f);
                Main.dust[crystalDust].noGravity = true;
                Main.dust[crystalDust].velocity *= 0.1f;
                Main.dust[crystalDust].velocity += Projectile.velocity * 0.1f;
                Dust expr_47FA_cp_0 = Main.dust[crystalDust];
                expr_47FA_cp_0.position.X -= shortXVel;
                Dust expr_4815_cp_0 = Main.dust[crystalDust];
                expr_4815_cp_0.position.Y -= shortYVel;
            }
            if (Main.rand.NextBool(10))
            {
                int dustPosModifierAgain = 4;
                int extraCrystalDust = Dust.NewDust(new Vector2(Projectile.position.X + (float)dustPosModifierAgain, Projectile.position.Y + (float)dustPosModifierAgain), Projectile.width - dustPosModifierAgain * 2, Projectile.height - dustPosModifierAgain * 2, dustType, 0f, 0f, 100, default, 0.6f);
                Main.dust[extraCrystalDust].velocity *= 0.25f;
                Main.dust[extraCrystalDust].velocity += Projectile.velocity * 0.5f;
            }
            if (Projectile.timeLeft < 75)
                CalamityUtils.HomeInOnNPC(Projectile, !Projectile.tileCollide, 450f, 9f, 20f);
        }
        public override void OnKill(int timeLeft)
        {
            int dustType = Projectile.ai[0] == 0f ? 56 : 73;
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, dustType, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int buffType = Projectile.ai[0] == 0f ? BuffID.Frostburn : BuffID.OnFire;
            int buffType = Projectile.ai[0] == 0f ? BuffID.Frostburn2 : BuffID.OnFire;
            target.AddBuff(buffType, 90);
        }
    }
}
