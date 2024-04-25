using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items;
using CalamityMod.Rarities;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.PostML
{
    public class ProfanedClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 7f;
        public override Color RadiusColor => new Color(255, 255, 150);

        public override void SetStaticDefaultsExtra()
        {
            ProfanedClicker.ClickerEffect = ClickerSystem.RegisterClickEffect(Mod, "ProfanedInferno", 7, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<ProfanedClickerProjectile>(), damage * 2, knockBack, player.whoAmI);
            });
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, ProfanedClicker.ClickerEffect);
            SetDust(Item, ModContent.DustType<HolyFireDust>());

            Item.damage = 120;
            Item.knockBack = 1f;
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.value = CalamityGlobalItem.Rarity12BuyPrice;
        }
    }
    public class ProfanedClickerProjectile : ModdedClickerProjectile
    {
        public bool Spawned
        {
            get => Projectile.ai[0] == 1f;
            set => Projectile.ai[0] = value ? 1f : 0f;
        }
        public override void SetDefaultsExtra()
        {
            Projectile.width = Projectile.height = 300;
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

                /*for (int k = 0; k < 45; k++)
                {
                    //Dust dust = Dust.NewDustDirect(Projectile.Center, 8, 8, ModContent.DustType<HolyFireDust>(), Main.rand.NextFloat(-16f, 16f), Main.rand.NextFloat(-16f, 16f), 125, default, 2f);
                    //dust.noGravity = true; 
                    //dust = Dust.NewDustDirect(Projectile.Center, 8, 8, ModContent.DustType<HolyFireDust>(), Main.rand.NextFloat(-16f, 16f), Main.rand.NextFloat(-16f, 16f), 125, default, 2f);

                    //int holy2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 244, 0f, 0f, 100, default, 3f);
                    //Main.dust[holy2].noGravity = true;
                    //holy2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 244, 0f, 0f, 100, default, 2f);

                }*/
                for (int i = 0; i < 30; i++)
                {
                    Dust holyFire = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, (int)CalamityDusts.ProfanedFire, 0f, 0f, 100, default, 2f);
                    holyFire.velocity *= 3f;

                    if (Main.rand.NextBool())
                    {
                        holyFire.scale = 0.5f;
                        holyFire.fadeIn = Main.rand.NextFloat(1f, 2f);
                    }
                }
                for (int i = 0; i < 60; i++)
                {
                    Dust holyFire = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GoldCoin, 0f, 0f, 100, default, 3f);
                    holyFire.noGravity = true;
                    holyFire.velocity *= 5f;

                    holyFire = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GoldCoin, 0f, 0f, 100, default, 2f);
                    holyFire.velocity *= 2f;
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
        }
    }
}
