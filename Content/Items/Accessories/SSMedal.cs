using CalamityClickers.Content.Items.Weapons;
using CalamityMod.Items;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using ClickerClass;
using ClickerClass.Items;
using ClickerClass.Items.Accessories;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Accessories
{
    public class SSMedal : ClickerItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
            Item.rare = ModContent.RarityType<DarkBlue>();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Clicker().accSMedalItem = Item;
            player.CalClicker().accSSMedal = Item;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<SMedal>()
                .AddIngredient<CoreofCalamity>()
                .AddIngredient<AscendantSpiritEssence>(4)
                .AddTile<CosmicAnvil>()
                .Register();
        }
    }
    public class SSMedalProjectile : ModdedClickerProjectile
    {
        public override bool UseInvisibleProjectile => false;

        public float MouseoverAlpha
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        public float Rot
        {
            get => Projectile.localAI[0];
            set => Projectile.localAI[0] = value;
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 10;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 150) * MouseoverAlpha * Projectile.Opacity;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
            CalamityClickersPlayer calClickerPlayer = player.GetModPlayer<CalamityClickersPlayer>();

            if (player.whoAmI != Projectile.owner)
            {
                //Hide for everyone but the owner
                Projectile.alpha = 255;
            }

            if (calClickerPlayer.accSSMedal != null && !calClickerPlayer.accSSMedal.IsAir)
            {
                Projectile.timeLeft = 10;
            }

            Rot = clickerPlayer.accMedalRot;
            Projectile.Center = player.Center + new Vector2(0, 40).RotatedBy(-Rot * 1.5f);
            Projectile.gfxOffY = player.gfxOffY;

            if (MouseoverAlpha > 0.1f)
            {
                MouseoverAlpha -= 0.01f;
            }

            if (MouseoverAlpha < 0.1f)
            {
                MouseoverAlpha = 0.1f;
            }
        }

    }
}
