using ClickerClass.Items;
using ClickerClass.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons
{
    public abstract class ModdedClickerWeapon : ClickerWeapon, ILocalizedModType, IModType
    {
        public new string LocalizationCategory => "Items.Weapons.Clicker";
        //public static string ClickerEffect { get; internal set; } = string.Empty;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            SetStaticDefaultsExtra();
        }
        public virtual void SetStaticDefaultsExtra()
        {

        }
        public abstract float Radius { get; }
        public abstract Microsoft.Xna.Framework.Color RadiusColor { get; }
        public override void SetDefaults()
        {
            base.SetDefaults();
            SetRadius(Item, Radius);
            SetColor(Item, RadiusColor);
            //AddEffect(Item, ClickerEffect);
            Item.width = 30;
            Item.height = 30;

            SetDefaultsExtra();
        }
        public virtual void SetDefaultsExtra()
        {

        }
        public override void UpdateInventory(Player player)
        {
            SetColor(Item, RadiusColor);
        }
    }
    public abstract class ModdedClickerProjectile : ClickerProjectile, ILocalizedModType
    {
        public new string LocalizationCategory => "Projectiles.Clicker";
        public virtual bool UseInvisibleProjectile => true;
        public override string Texture => UseInvisibleProjectile ? "CalamityMod/Projectiles/InvisibleProj" : base.Texture;

        public override void SetDefaults()
        {
            base.SetDefaults();

            SetDefaultsExtra();
        }
        public virtual void SetDefaultsExtra()
        {

        }
    }
}
