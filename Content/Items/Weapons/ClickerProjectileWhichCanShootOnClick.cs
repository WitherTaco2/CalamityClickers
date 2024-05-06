using ClickerClass;
using ClickerClass.Core;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityClickers.Content.Items.Weapons
{
    public abstract class ClickerProjectileWhichCanShootOnClick : ModdedClickerProjectile
    {
        public bool HasSpawnEffects
        {
            get => Projectile.ai[0] == 1f;
            set => Projectile.ai[0] = value ? 1f : 0f;
        }
        public int clickCountCheck = 0;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();

            if (HasSpawnEffects)
            {
                //Avoids weird first shot
                clickCountCheck = clickerPlayer.clickerTotal;

                OnSpawn();
                HasSpawnEffects = false;
            }

            AIExtra();

            MousePlayer mousePlayer = player.GetModPlayer<MousePlayer>();
            if (mousePlayer.TryGetMousePosition(out Vector2 mouseWorld))
            {
                Projectile.spriteDirection = mouseWorld.X > Projectile.Center.X ? 1 : -1;

                if (clickerPlayer.clickerTotal > clickCountCheck && Main.myPlayer == Projectile.owner)
                {
                    ShootOnClick();
                }
            }
            clickCountCheck = clickerPlayer.clickerTotal;
        }
        public virtual void AIExtra()
        {

        }
        public virtual void OnSpawn()
        {

        }
        public virtual void ShootOnClick()
        {

        }
    }
}
