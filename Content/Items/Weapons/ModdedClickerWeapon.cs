using ClickerClass;
using ClickerClass.Items;
using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons
{
    public abstract class ModdedClickerWeapon : ClickerWeapon, ILocalizedModType, IModType
    {
        public new string LocalizationCategory => "Items.Weapons.Clicker";
        //public static string ClickerEffect { get; internal set; } = string.Empty;
        public override LocalizedText Tooltip => Language.GetOrRegister("Mods.ClickerClass.Common.Tooltips.Clicker");
        public virtual bool SetBorderTexture => false;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            ClickerSystem.RegisterClickerWeapon(this, SetBorderTexture ? (Texture + "_Outline") : null);
            SetStaticDefaultsExtra();

        }
        public virtual void SetStaticDefaultsExtra()
        {

        }
        public abstract float Radius { get; }
        public abstract Microsoft.Xna.Framework.Color RadiusColor { get; }
        public virtual int DustType => -1;
        public override void SetDefaults()
        {
            base.SetDefaults();
            ClickerSystem.SetClickerWeaponDefaults(base.Item);
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
            if (DustType > 0)
                SetDust(Item, DustType);
        }

        #region ClickerWeapon`s Class Functions
        public static void SetColor(Item item, Color color)
        {
            if (ClickerSystem.IsClickerWeapon(item, out var clickerItem))
            {
                clickerItem.clickerRadiusColor = color;
            }
        }

        public static void SetRadius(Item item, float radius)
        {
            if (ClickerSystem.IsClickerWeapon(item, out var clickerItem))
            {
                clickerItem.radiusBoost = radius;
            }
        }

        public static void AddEffect(Item item, string effect)
        {
            AddEffect(item, new List<string> { effect });
        }

        public static void AddEffect(Item item, IEnumerable<string> effects)
        {
            if (!ClickerSystem.IsClickerWeapon(item, out var clickerItem))
            {
                return;
            }

            List<string> itemClickEffects = clickerItem.itemClickEffects;
            foreach (string effect in effects)
            {
                if (!string.IsNullOrEmpty(effect) && !itemClickEffects.Contains(effect) && ClickerSystem.IsClickEffect(effect))
                {
                    itemClickEffects.Add(effect);
                }
            }
        }

        public static void SetDust(Item item, int type)
        {
            if (ClickerSystem.IsClickerWeapon(item, out var clickerItem))
            {
                clickerItem.clickerDustColor = type;
            }
        }

        #endregion
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
