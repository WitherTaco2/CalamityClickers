using CalamityMod.Items;
using CalamityMod.Items.Materials;
using CalamityMod.Particles;
using ClickerClass;
using ClickerClass.Items.Weapons.Clickers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.HM
{
    public class TerraClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 3.5f;
        public override Color RadiusColor => new Color(141, 203, 50);

        public override void SetStaticDefaultsExtra()
        {
            ClickerEffect = ClickerSystem.RegisterClickEffect(Mod, "TerraAura", 14, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<TerraClickerProjectile>(), damage * 2, knockBack, player.whoAmI);
            });
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, ClickerEffect);
            SetDust(Item, DustID.TerraBlade);

            Item.damage = 64;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Yellow;
            Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<UmbralClicker>()
                .AddIngredient<ArthursClicker>()
                .AddIngredient<LivingShard>(12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
    public class TerraClickerProjectile : ModdedClickerProjectile
    {
        public override void SetDefaultsExtra()
        {
            Projectile.width = 250;
            Projectile.height = 250;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
            Projectile.alpha = 255;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
            Projectile.DamageType = ModContent.GetInstance<ClickerDamage>();
        }
        public int ShinkGrow = 0;
        public int Framecounter = 0;
        public int PulseOnce = 1;
        public int PulseOnce2 = 1;
        public int PulseOnce3 = 1;
        public override void AI()
        {
            Framecounter++;

            if (ShinkGrow == 0)
            {
                if (PulseOnce == 1)
                {
                    Particle pulse = new StaticPulseRing(Projectile.Center, Vector2.Zero, new Color(141, 203, 50), new Vector2(1f, 1f), 0f, 0f, 0.152f, 10);
                    GeneralParticleHandler.SpawnParticle(pulse);
                    //SoundEngine.PlaySound(Spawnsound with { Pitch = -0.9f }, Projectile.Center);
                    PulseOnce = 0;
                }

                if (Framecounter == 10)
                {
                    ShinkGrow = 1;
                }
            }
            if (ShinkGrow == 1)
            {
                if (PulseOnce2 == 1)
                {
                    Particle pulse2 = new StaticPulseRing(Projectile.Center, Vector2.Zero, new Color(141, 203, 50), new Vector2(1f, 1f), 0f, 0.152f, 0.152f, 160);
                    GeneralParticleHandler.SpawnParticle(pulse2);
                    PulseOnce2 = 0;
                }

                if (Framecounter == 170)
                {
                    ShinkGrow = 2;
                }
            }
            if (ShinkGrow == 2)
            {
                if (PulseOnce3 == 1)
                {
                    Particle pulse3 = new StaticPulseRing(Projectile.Center, Vector2.Zero, new Color(141, 203, 50), new Vector2(1f, 1f), 0f, 0.152f, 0f, 10);
                    GeneralParticleHandler.SpawnParticle(pulse3);
                    PulseOnce3 = 0;
                }
            }
        }
    }
}
