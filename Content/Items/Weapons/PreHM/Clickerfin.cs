using CalamityMod;
using CalamityMod.Items;
using ClickerClass;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.PreHM
{
    public class Clickerfin : ModdedClickerWeapon
    {
        public static string ReelIn { get; internal set; } = string.Empty;
        public override float Radius => 2.6f;
        public override Color RadiusColor => new Color(249, 179, 27);
        public override void SetStaticDefaultsExtra()
        {
            ReelIn = ClickerSystem.RegisterClickEffect(Mod, "ReelIn", 15, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                Vector2 pos = position + Main.rand.NextVector2CircularEdge(100, 100);
                NPC.NewNPC(source, (int)pos.X, (int)pos.Y, ModContent.NPCType<ClickerfinNPC>(), 0, Main.rand.NextFloat(0.5f, 2f));
            }, true);
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, ReelIn);
            SetDust(Item, DustID.GemAmber);

            Item.damage = 10;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Orange;
            Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
        }
    }
    public class ClickerfinNPC : ModNPC
    {
        public override void SetStaticDefaults()
        {
            this.HideFromBestiary();
        }
        public override void SetDefaults()
        {
            NPC.width = 24;
            NPC.height = 14;
            NPC.aiStyle = -1;
            AIType = -1;
            NPC.life = 50;
            //NPC.friendly = true;
            NPC.DeathSound = SoundID.NPCDeath1;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment) => NPC.lifeMax = 50;
        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (NPC.life < hit.Damage)
            {
                Main.player[projectile.owner].AddBuff(ModContent.BuffType<ClickerfinBuff>(), 10 * 60);
            }
        }
        public override void AI()
        {
            NPC.spriteDirection = NPC.velocity.X > 0 ? 1 : -1;
            NPC.velocity = new Vector2(NPC.velocity.X, MathF.Sin(Main.GlobalTimeWrappedHourly * NPC.ai[0]) / 10f);
            NPC.ai[1]++;
            if (NPC.ai[1] > 50)
            {
                NPC.velocity = new Vector2(-NPC.velocity.X, NPC.velocity.Y);
            }
        }
    }
    public class ClickerfinBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage<ClickerDamage>() += 0.25f;
        }
    }
}
