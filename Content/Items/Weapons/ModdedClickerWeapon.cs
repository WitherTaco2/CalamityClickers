using ClickerClass.Items;
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
            SafeSetStaticDefaults();
        }
        public virtual void SafeSetStaticDefaults()
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
            SafeSetDefaults();
            Item.width = 30;
            Item.height = 30;
        }
        public virtual void SafeSetDefaults()
        {

        }
    }
}
