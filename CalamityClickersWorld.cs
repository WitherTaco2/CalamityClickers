using CalamityClickers.Content.Items.Weapons.PreHM;
using CalamityMod.Tiles.Abyss;
using ClickerClass;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityClickers
{
    public class CalamityClickersWorld : ModSystem
    {

        private void GenerateExtraLoot(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Clicking on the chests";


            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null || !WorldGen.InWorld(chest.x, chest.y, 42)) // don't include chests generated outside the playable area of the map
                {
                    continue;
                }

                Tile tile = Main.tile[chest.x, chest.y];
                if (chest.item == null)
                {
                    continue;
                }
                if (tile.TileType == ModContent.TileType<AbyssTreasureChest>())
                {
                    AddRareItemToChests(chest, ModContent.ItemType<Clickerfin>(), 0.5f);
                }

            }
        }
        public static void AddRareItemToChests(Chest chest, int newItem, float chance, int min = 1, int max = 1)
        {
            if (WorldGen.genRand.NextFloat() <= chance)
            {
                (int start, int end) = ClickerWorld.GetFirstConsecutiveItemChainIndexes(chest);
                if (start != 0 || end != chest.item.Length - 1)
                {
                    if (!(end == -1 || start > 0 || end == chest.item.Length - 1))
                    {
                        ClickerWorld.ShiftSlotRangeBy1Up(chest, start, end);
                    }

                    Item item = chest.item[0];
                    int stack = item.stack;
                    int maxExclusive = max + 1;
                    if (min > 0 && maxExclusive > min)
                    {
                        stack = max > min ? WorldGen.genRand.Next(min, maxExclusive) : min;
                    }
                    item.SetDefaults(newItem);
                    item.stack = stack;
                }
            }
        }
    }
}
