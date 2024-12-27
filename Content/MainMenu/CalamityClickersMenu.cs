using ClickerClass;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityClickers.Content.MainMenu
{
    internal class NullSurfaceBackground : ModSurfaceBackgroundStyle
    {
        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            for (int i = 0; i < fades.Length; i++)
            {
                if (i == Slot)
                {
                    fades[i] += transitionSpeed;
                    if (fades[i] > 1f)
                    {
                        fades[i] = 1f;
                    }
                }
                else
                {
                    fades[i] -= transitionSpeed;
                    if (fades[i] < 0f)
                    {
                        fades[i] = 0f;
                    }
                }
            }
        }

        private static readonly string TexPath = "CalamityMod/Backgrounds/BlankPixel";
        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b) => BackgroundTextureLoader.GetBackgroundSlot(TexPath);
        public override int ChooseFarTexture() => BackgroundTextureLoader.GetBackgroundSlot(TexPath);
        public override int ChooseMiddleTexture() => BackgroundTextureLoader.GetBackgroundSlot(TexPath);
        public override bool PreDrawCloseBackground(SpriteBatch spriteBatch) => false;
    }
    public class CalamityClickersMenu : ModMenu
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }
        public class MenuClicker
        {
            public int IdentityIndex;
            public int ClickerIndex;
            public Vector2 Center;
            public int Time;
            public int Lifetime;
            public float FlyPower;
            public MenuClicker(int identityIndex, int clickerIndex, Vector2 center, int lifetime, float flyPower)
            {
                Center = center;
                IdentityIndex = identityIndex;
                ClickerIndex = clickerIndex;
                Lifetime = lifetime;
                FlyPower = flyPower;
            }
        }
        public override string DisplayName => "Grandmalamity Theme";
        public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>("CalamityClickers/Content/MainMenu/Logo");
        public override Asset<Texture2D> SunTexture => ModContent.Request<Texture2D>("CalamityMod/Backgrounds/BlankPixel");
        public override Asset<Texture2D> MoonTexture => ModContent.Request<Texture2D>("CalamityMod/Backgrounds/BlankPixel");
        public override int Music => ModContent.GetInstance<CalamityMod.CalamityMod>().GetMusicFromMusicMod("CalamityTitle").GetValueOrDefault(6);
        public override ModSurfaceBackgroundStyle MenuBackgroundStyle => ModContent.GetInstance<NullSurfaceBackground>();

        public static List<MenuClicker> MenuClickers { get; internal set; } = new List<MenuClicker>();
        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/MainMenu/MenuBackground").Value;

            Vector2 drawOffset = Vector2.Zero;
            float xScale = (float)Main.screenWidth / texture.Width;
            float yScale = (float)Main.screenHeight / texture.Height;
            float scale = xScale;

            if (xScale != yScale)
            {
                if (yScale > xScale)
                {
                    scale = yScale;
                    drawOffset.X -= (texture.Width * scale - Main.screenWidth) * 0.5f;
                }
                else
                    drawOffset.Y -= (texture.Height * scale - Main.screenHeight) * 0.5f;
            }

            spriteBatch.Draw(texture, drawOffset, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            int ClickerType()
            {
                List<int> clickers = new List<int>();
                for (int i = 0; i < Main.maxItems; i++)
                {
                    if (ClickerSystem.IsClickerWeapon(i))
                        clickers.Add(i);
                }
                return Main.rand.NextFromList(clickers.ToArray());
                //return clickers[Main.rand.Next(clickers.Count - 1)];
            }

            if (Main.rand.NextBool(4))
            {
                int lifetime = Main.rand.Next(200, 300);
                float depth = Main.rand.NextFloat(1.8f, 5f);
                Vector2 startingPosition = new Vector2(Main.screenWidth * Main.rand.NextFloat(-0.1f, 1.1f), Main.screenHeight * 1.05f);

                MenuClickers.Add(new MenuClicker(MenuClickers.Count, ClickerType(), startingPosition, lifetime, Main.rand.NextFloat(0.9f, 1.1f)));
            }

            for (int i = 0; i < MenuClickers.Count; i++)
            {
                MenuClickers[i].Time++;
                MenuClickers[i].Center += new Vector2(-0.5f, 0.5f) * MenuClickers[i].FlyPower;
            }

            MenuClickers.RemoveAll(c => c.Time >= c.Lifetime);

            for (int i = 0; i < MenuClickers.Count; i++)
            {
                Texture2D cinderTexture = TextureAssets.Item[MenuClickers[i].ClickerIndex].Value;
                Vector2 drawPosition = MenuClickers[i].Center;
                spriteBatch.Draw(cinderTexture, drawPosition, null, Color.White, 0f, cinderTexture.Size() * 0.5f, 1f, 0, 0f);
            }

            drawColor = Color.White;
            Main.time = 27000;
            Main.dayTime = true;

            Vector2 drawPos = new Vector2(Main.screenWidth / 2f, 100f);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
            spriteBatch.Draw(Logo.Value, drawPos, null, drawColor, logoRotation, Logo.Value.Size() * 0.5f, logoScale, SpriteEffects.None, 0f);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
            return false;
        }
    }
}
