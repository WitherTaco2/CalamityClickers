using CalamityMod;
using CalamityMod.Dusts;
using CalamityMod.Items;
using CalamityMod.Projectiles.Boss;
using ClickerClass;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.HM
{
    public class AureusClicker : ModdedClickerWeapon
    {
        public static string AureusBlast { get; internal set; } = string.Empty;
        public override float Radius => 4f;
        public override Color RadiusColor => new Color(123, 99, 130);
        public override void SetStaticDefaultsExtra()
        {
            AureusBlast = ClickerSystem.RegisterClickEffect(Mod, "AureusBlast", 10, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                SoundEngine.PlaySound(SoundID.Item10, position);

                List<int> indexes = new List<int>() { };
                for (int u = 0; u < Main.maxNPCs; u++)
                {
                    NPC target = Main.npc[u];
                    if (target.CanBeChasedBy() && target.DistanceSQ(position) < 158 * 158)
                    {
                        indexes.Add(u);
                        /*target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 180);
                        //target.Hitbox.Intersects

                        for (int i = 0; i < 15; i++)
                        {
                            int index = Dust.NewDust(target.position, target.width, target.height, DustID.Stone, 0f, 0f, 100, default(Color), 1f);
                            Dust dust = Main.dust[index];
                            dust.noGravity = true;
                            dust.velocity *= 0.75f;
                            int x = Main.rand.Next(-50, 51);
                            int y = Main.rand.Next(-50, 51);
                            dust.position.X += x;
                            dust.position.Y += y;
                            dust.velocity.X = -x * 0.075f;
                            dust.velocity.Y = -y * 0.075f;
                        }*/
                    }
                }
                if (indexes.Count > 0)
                {
                    //foreach (int i in indexes)
                    for (int i = 0; i < MathHelper.Clamp(indexes.Count, 1, 5); i++)
                    {
                        NPC npc = Main.npc[indexes[i]];
                        for (int j = 0; j < (indexes.Count > 2 ? 2 : 3); j++)
                        {
                            Vector2 vec1 = Vector2.UnitX.RotateRandom(MathHelper.TwoPi);
                            Projectile.NewProjectile(source, npc.Center - vec1 * 500, vec1 * 30, ModContent.ProjectileType<AureusClickerProjectile>(), damage * (indexes.Count > 2 ? 3 : 2), 1f, player.whoAmI);
                        }
                    }
                }
            });
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, AureusBlast);
            SetDust(Item, ModContent.DustType<AstralBasic>());

            Item.damage = 66;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Yellow;
            Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
        }
    }
    public class AureusClickerProjectile : ModdedClickerProjectile
    {
        public override string Texture => ModContent.GetInstance<AstralLaser>().Texture;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.projFrames[Projectile.type] = 3;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaultsExtra()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            //Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 1200;
            Projectile.extraUpdates = 1;
            Projectile.scale = 0.75f;
        }
        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 8)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 2)
                Projectile.frame = 0;

            // Speed up after some time
            if (Projectile.timeLeft < 600)
            {
                if (Projectile.velocity.Length() < Projectile.ai[1])
                {
                    Projectile.velocity *= 1.005f;
                    if (Projectile.velocity.Length() > Projectile.ai[1])
                    {
                        Projectile.velocity.Normalize();
                        Projectile.velocity *= Projectile.ai[1];
                    }
                }
            }

            if (Projectile.velocity.X < 0f)
            {
                Projectile.spriteDirection = -1;
                Projectile.rotation = (float)Math.Atan2((double)-(double)Projectile.velocity.Y, (double)-(double)Projectile.velocity.X);
            }
            else
            {
                Projectile.spriteDirection = 1;
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
            }

            Lighting.AddLight(Projectile.Center, 0.3f, 0.3f, 0f);

            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] > 18f)
            {
                if (Projectile.alpha > 0)
                    Projectile.alpha -= 3;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Projectile.type], lightColor, 1);
            return false;
        }
    }
}
