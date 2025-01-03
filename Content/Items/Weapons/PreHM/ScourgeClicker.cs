using CalamityMod.Items;
using CalamityMod.Particles;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.PreHM
{
    public class ScourgeClicker : ModdedClickerWeapon
    {
        public static string Sandstorm { get; internal set; } = string.Empty;
        public override float Radius => 2f;
        public override Color RadiusColor => new Color(124, 81, 60);
        public override void SetStaticDefaultsExtra()
        {
            Sandstorm = ClickerSystem.RegisterClickEffect(Mod, "Sandstorm", 8, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<ScourgeClickerProjectile>(), damage * 2, knockBack, player.whoAmI);
            }, true);
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, Sandstorm);
            SetDust(Item, 288);

            Item.damage = 6;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Green;
            Item.value = CalamityGlobalItem.RarityGreenBuyPrice;

        }
    }
    public class ScourgeClickerProjectile : ModdedClickerProjectile
    {
        public bool Spawned
        {
            get => Projectile.ai[0] == 1f;
            set => Projectile.ai[0] = value ? 1f : 0f;
        }
        public override void SetDefaultsExtra()
        {
            Projectile.width = 150;
            Projectile.height = 150;
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
                SoundEngine.PlaySound(SoundID.Item72, Projectile.Center);

                float numberOfDusts = 20f;
                float rotFactor = 360f / numberOfDusts;
                for (int i = 0; i < numberOfDusts; i++)
                {
                    float rot = MathHelper.ToRadians(i * rotFactor);
                    Vector2 offset = new Vector2(Main.rand.NextFloat(1.5f, 5.5f), 0).RotatedBy(rot * Main.rand.NextFloat(3.1f, 9.1f));
                    Vector2 velOffset = new Vector2(Main.rand.NextFloat(1.5f, 5.5f), 0).RotatedBy(rot * Main.rand.NextFloat(3.1f, 9.1f));
                    MediumMistParticle SandCloud = new MediumMistParticle(Projectile.Center + offset, velOffset * Main.rand.NextFloat(1.5f, 3f), Color.Peru, Color.PeachPuff, Main.rand.NextFloat(0.9f, 1.2f), 160f, Main.rand.NextFloat(0.03f, -0.03f));
                    GeneralParticleHandler.SpawnParticle(SandCloud);
                    Dust dust = Dust.NewDustPerfect(Projectile.Center + offset, Main.rand.NextBool() ? 288 : 207, new Vector2(velOffset.X, velOffset.Y));
                    dust.noGravity = false;
                    dust.velocity = velOffset;
                    dust.scale = Main.rand.NextFloat(1.2f, 1.6f);
                }
            }
        }
    }
}
