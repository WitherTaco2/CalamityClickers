using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Cooldowns;
using CalamityMod.Items;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using ClickerClass.Items.Weapons.Clickers;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.PostML.Providance
{
    internal class BloodstoneClicker : ModdedClickerWeapon
    {
        public static string Lifestealing { get; internal set; } = string.Empty;
        public static string BloodyKnives { get; internal set; } = string.Empty;
        public override float Radius => 7f;
        public override Color RadiusColor => new Color(204, 42, 60);
        public override void SetStaticDefaultsExtra()
        {
            Lifestealing = CalamityClickersUtils.RegisterClickEffect(Mod, "Lifestealing", 1, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                if (player.moonLeech || player.HasCooldown(LifeSteal.ID))
                    return;

                player.statLife += 2;
                player.HealEffect(2);
                player.AddCooldown(LifeSteal.ID, 20);

            }, postMoonLord: true);
            BloodyKnives = CalamityClickersUtils.RegisterClickEffect(Mod, "BloodyKnives", 1, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                for (int i = 0; i < 8; i++)
                    Projectile.NewProjectile(source, position, Vector2.UnitX.RotatedBy(MathHelper.TwoPi / 8 * i) * 10, ModContent.ProjectileType<BloodstoneClickerProjectile>(), damage / 4, knockBack, player.whoAmI);
            }, postMoonLord: true);
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, Lifestealing);
            AddEffect(Item, BloodyKnives);
            //AddEffect(Item, ClickEffect.Linger);
            SetDust(Item, DustID.Blood);

            Item.damage = 150;
            Item.knockBack = 1f;
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<HemoClicker>()
                .AddIngredient<BloodstoneCore>(8)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
    public class BloodstoneClickerProjectile : ModdedClickerProjectile
    {
        public override bool UseInvisibleProjectile => false;
        public override void SetDefaultsExtra()
        {
            Projectile.width = Projectile.height = 30;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 30;
        }
        public Vector2 EnemyOffset = Vector2.Zero;
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.ai[2] = -1;
        }
        public override void AI()
        {
            if (Projectile.ai[2] != -1)
            {
                NPC npc = Main.npc[(int)Projectile.ai[2]];
                if (npc is null || !npc.active)
                    Projectile.ai[2] = -1;
                Projectile.Center = npc.Center + EnemyOffset;
                npc.AddBuff(ModContent.BuffType<BurningBlood>(), 30);
                npc.AddBuff(ModContent.BuffType<MarkedforDeath>(), 30);
            }
            else
            {
                CalamityUtils.HomeInOnNPC(Projectile, true, 1200, 10, 1.02f);
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            //target.AddBuff()
            Projectile.ai[2] = target.whoAmI;
            EnemyOffset = Projectile.Center - target.Center;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(EnemyOffset.X);
            writer.Write(EnemyOffset.Y);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            EnemyOffset = new Vector2(reader.ReadSingle(), reader.ReadSingle());
        }
        public override bool? CanHitNPC(NPC target)
        {
            return Projectile.ai[2] != -1;
        }
    }
}
