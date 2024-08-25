using CalamityMod.Items;
using CalamityMod.Projectiles.Magic;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.HM
{
    public class BloodGodsClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 5.9f;
        public override Color RadiusColor => new Color(111, 169, 241);
        public override bool SetBorderTexture => true;

        public static readonly int BlastAmount = 5;
        public override void SetStaticDefaultsExtra()
        {
            ClickerEffect = ClickerSystem.RegisterClickEffect(Mod, "BloodBlasts", 7, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                //Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<BloodGodFlareClickerProjectile>(), damage * 2, knockBack, player.whoAmI);
                SoundEngine.PlaySound(SoundID.Item24, position);

                for (int k = 0; k < BlastAmount; k++)
                {
                    float xChoice = Main.rand.Next(-100, 101);
                    float yChoice = Main.rand.Next(-100, 101);
                    xChoice += xChoice > 0 ? 300 : -300;
                    yChoice += yChoice > 0 ? 300 : -300;
                    Vector2 startSpot = new Vector2(position.X + xChoice, position.Y + yChoice);
                    Vector2 endSpot = new Vector2(position.X + Main.rand.Next(-10, 11), position.Y + Main.rand.Next(-10, 11));
                    Vector2 vector = endSpot - startSpot;
                    float speed = 8f;
                    float mag = vector.Length();
                    if (mag > speed)
                    {
                        mag = speed / mag;
                        vector *= mag;
                    }

                    int bloodBlast = ModContent.ProjectileType<BloodBlast>();
                    int index = Projectile.NewProjectile(source, startSpot, vector, bloodBlast, (int)(damage * 0.5f), 0f, player.whoAmI, Main.rand.Next(Main.projFrames[bloodBlast]), 0f);
                    Main.projectile[index].DamageType = ModContent.GetInstance<ClickerDamage>();
                }
            });
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, ClickerEffect);
            SetDust(Item, DustID.IceTorch);

            Item.damage = 60;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Yellow;
            Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
        }
    }
    public class BloodGodsClickerProjectile : ModdedClickerProjectile
    {
        public bool Spawned
        {
            get => Projectile.ai[0] == 1f;
            set => Projectile.ai[0] = value ? 1f : 0f;
        }
        public override void SetDefaultsExtra()
        {
            Projectile.width = 400;
            Projectile.height = 400;
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

                for (int k = 0; k < 60; k++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.Center, 16, 16, DustID.IceTorch, Main.rand.NextFloat(-20, 20), Main.rand.NextFloat(-20, 20), Scale: 4f);
                    dust.velocity.Y *= 3;
                    dust.noGravity = true;
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 180);
            target.AddBuff(BuffID.OnFire3, 180);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Frostburn, 180);
            target.AddBuff(BuffID.OnFire3, 180);
        }
    }
}
