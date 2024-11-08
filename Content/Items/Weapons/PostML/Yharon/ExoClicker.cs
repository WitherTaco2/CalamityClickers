using CalamityMod;
using CalamityMod.Items;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.PostML.Yharon
{
    public class ExoClicker : ModdedClickerWeapon
    {
        public static string MiniExoTwins { get; internal set; } = string.Empty;
        public override float Radius => 8f;
        public override Color RadiusColor => Color.Lerp(Main.DiscoColor, Color.Gray, 0.5f);
        public override int DustType => DustID.Stone;
        public override void SetStaticDefaultsExtra()
        {
            MiniExoTwins = CalamityClickersUtils.RegisterClickEffect(Mod, "MiniExoTwins", 15, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                /*for (int i = 0; i < 7; i++)
                {
                    Vector2 vec = Vector2.UnitY.RotatedByRandom(0.75f);
                    int p = Projectile.NewProjectile(source, position - vec * Main.rand.NextFloat(500, 1000), vec * 20, ModContent.ProjectileType<ExoCrystalArrow>(), damage / 3, knockBack / 2, player.whoAmI);
                    Main.projectile[p].DamageType = ModContent.GetInstance<ClickerDamage>();
                }*/
                for (int i = 0; i < 1; i++)
                {
                    int index = Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<ExoClickerProjectile>(), damage, knockBack, player.whoAmI, i);
                }
            }, postMoonLord: true);
            CalamityClickersUtils.RegisterBlacklistedClickEffect(MiniExoTwins);
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, MiniExoTwins);
            SetDust(Item, DustType);

            Item.damage = 420;
            Item.knockBack = 1f;
            Item.rare = ModContent.RarityType<Violet>();
            Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
        }
    }
    public class ExoClickerProjectile : ModdedClickerProjectile
    {
        public override bool UseInvisibleProjectile => false;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 2;
        }
        public override void SetDefaultsExtra()
        {
            Projectile.width = Projectile.height = 50;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 300;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
        }
        public Vector2 LastPos = Vector2.Zero;
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.frame = (int)Projectile.ai[0];
            LastPos = Projectile.Center;
        }
        public override void AI()
        {
            int time = (int)(Main.GlobalTimeWrappedHourly) % 100;
            if (time < 70)
            {
                double perc = Utils.Lerp(0, MathHelper.TwoPi * 3, Utils.GetLerpValue(0, 70, time, true));
                Projectile.Center = Vector2.Lerp(LastPos, Main.MouseWorld, MathF.Pow(time / 70, 0.31f)) + Vector2.UnitX.RotatedBy(perc) * 100;
                Projectile.rotation = (Main.MouseWorld - Projectile.Center).ToRotation();
            }
            else
            {
                Projectile.Center += Vector2.UnitX.RotatedBy(Projectile.rotation) * 10;
            }
            if (time == 99)
            {
                LastPos = Projectile.Center;
            }
            Projectile.velocity = Vector2.Zero;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(LastPos.X);
            writer.Write(LastPos.Y);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            LastPos = new Vector2(reader.ReadSingle(), reader.ReadSingle());
        }
        public override bool PreDraw(ref Color lightColor)
        {
            CalamityUtils.DrawAfterimagesCentered(Projectile, -1, lightColor);
            return false;
        }
    }
}
