using Terraria;

namespace CalamityClickers.Content.Items.Weapons
{
    public abstract class ClickableClickerProjectile : ModdedClickerProjectile
    {
        public bool HasSpawnEffects
        {
            get => Projectile.ai[0] == 1f;
            set => Projectile.ai[0] = value ? 1f : 0f;
        }

        public bool Trigger
        {
            get => Projectile.ai[1] == 1f;
            set => Projectile.ai[1] = value ? 1f : 0f;
        }

        public bool HasChanged
        {
            get => Projectile.ai[2] == 1f;
            set => Projectile.ai[2] = value ? 1f : 0f;
        }

        public override void AI()
        {
            SafePreAI();

            if (Main.myPlayer == Projectile.owner && !HasChanged && Trigger)
            {
                HasChanged = true;
                Projectile.netUpdate = true;
            }

            if (HasChanged)
            {
                OnClick();
                //Projectile.Kill();
            }

            SafePostAI();
        }

        public virtual void SafePreAI()
        {

        }

        public virtual void SafePostAI()
        {

        }

        public virtual void OnClick()
        {

        }
    }
}
