using CalamityClickers.Content.Dusts;
using CalamityMod.Buffs.DamageOverTime;
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
    public class BrimstoneClicker : ModdedClickerWeapon
    {
        public static string BrimstoneInferno { get; internal set; } = string.Empty;
        public override float Radius => 3f;
        public override Color RadiusColor => new Color(166, 46, 61);
        public override void SetStaticDefaultsExtra()
        {
            BrimstoneInferno = ClickerSystem.RegisterClickEffect(Mod, "BrimstoneInferno", 10, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<BrimstoneClickerProjectile>(), damage * 3, knockBack, player.whoAmI);
            });
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, BrimstoneInferno);
            SetDust(Item, ModContent.DustType<BrimstoneFlameClickers>());

            Item.damage = 23;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Pink;
            Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<UnholyCore>(5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
    public class BrimstoneClickerProjectile : ModdedClickerProjectile
    {
        public bool Spawned
        {
            get => Projectile.ai[0] == 1f;
            set => Projectile.ai[0] = value ? 1f : 0f;
        }

        public override void SetDefaultsExtra()
        {
            Projectile.width = 250;
            Projectile.height = 250;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 10;
            Projectile.alpha = 255;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 3;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
            //Projectile.DamageType = ModContent.GetInstance<ClickerDamage>();
        }
        public override void AI()
        {
            if (!Spawned)
            {
                Spawned = true;

                SoundEngine.PlaySound(SoundID.Item74, Projectile.Center);

                /*for (int k = 0; k < 30; k++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.Center, 8, 8, ModContent.DustType<BrimstoneFlameClickers>(), Main.rand.NextFloat(-12f, 12f), Main.rand.NextFloat(-12f, 12f), 125, default, 2f);
                    dust.noGravity = true;
                }*/

                // Spawn a circle of fast bullets.
                float starSpeed = 12f;
                for (int i = 0; i < 40; i++)
                {
                    Vector2 shootVelocity = (MathHelper.TwoPi * i / 40f).ToRotationVector2() * starSpeed;
                    //Dust dust = Dust.NewDustDirect(Projectile.Center, 8, 8, ModContent.DustType<BrimstoneFlameClickers>(), Main.rand.NextFloat(-12f, 12f), Main.rand.NextFloat(-12f, 12f), 125, default, 2f);
                    Dust dust = Dust.NewDustPerfect(Projectile.Center + shootVelocity, ModContent.DustType<BrimstoneFlameClickers>(), shootVelocity, Scale: 2f);
                    dust.noGravity = true;
                    //int bullet = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + shootVelocity, shootVelocity, ModContent.ProjectileType<ScarletDevilBullet>(), (int)(Projectile.damage * 0.01), 0f, Projectile.owner);
                    //if (Main.projectile.IndexInRange(bullet))
                    //    Main.projectile[bullet].Calamity().stealthStrike = true;
                }

                // Spawn a pair of stars, one slow, one fast.
                int pointsOnStar = 5;
                for (int k = 0; k < 2; k++)
                {
                    for (int i = 0; i < pointsOnStar; i++)
                    {
                        float angle = MathHelper.Pi * 1.5f - i * MathHelper.TwoPi / pointsOnStar;
                        float nextAngle = MathHelper.Pi * 1.5f - (i + 3) % pointsOnStar * MathHelper.TwoPi / pointsOnStar;
                        if (k == 1)
                            nextAngle = MathHelper.Pi * 1.5f - (i + 2) * MathHelper.TwoPi / pointsOnStar;
                        Vector2 start = angle.ToRotationVector2();
                        Vector2 end = nextAngle.ToRotationVector2();
                        int pointsOnStarSegment = 18;
                        for (int j = 0; j < pointsOnStarSegment; j++)
                        {
                            Vector2 shootVelocity = Vector2.Lerp(start, end, j / (float)pointsOnStarSegment) * starSpeed;
                            Dust dust = Dust.NewDustPerfect(Projectile.Center + shootVelocity, ModContent.DustType<BrimstoneFlameClickers>(), shootVelocity, Scale: 2f);
                            dust.noGravity = true;
                            //int bullet = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + shootVelocity, shootVelocity, ModContent.ProjectileType<ScarletDevilBullet>(), (int)(Projectile.damage * 0.01), 0f, Projectile.owner);
                            //if (Main.projectile.IndexInRange(bullet))
                            //    Main.projectile[bullet].Calamity().stealthStrike = true;
                        }
                    }
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 180);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 180);
        }
    }
}
