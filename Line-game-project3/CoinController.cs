using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line_game_project3
{
    public class CoinController
    {
        public static int totalCoinsSpawned = 0;
        public static double lastCoinTime = 0;

        public static HashSet<Coin> coins = new HashSet<Coin>();

        private static Vector2 coinSpawnTime = Settings.vectors["CoinSpawnTime"];
        private static int coinSpawnMargin = Settings.numbers["Margin1"];
        private static int uiMargin = Settings.numbers["Margin2"];
        private static int spawnBuffer = Settings.numbers["SpawnBuffer"];

        public static void CoinLogic(double time, Character blue)
        {
            CoinSpawner(time, blue.pos);
            Detection(blue);
        }

        private static void CoinSpawner(double time, Vector2 bluePos)
        {
            if (time - lastCoinTime > new Random().Next((int)coinSpawnTime.X, (int)coinSpawnTime.Y) && coins.Count < 4)
            {
                totalCoinsSpawned += 1;
                lastCoinTime = time;
                SpawnCoin(bluePos);
            }

        }

        private static void SpawnCoin(Vector2 bluePos)
        {
            Random random = new();
            int x = random.Next(coinSpawnMargin, (int)Game1.screenWidth - coinSpawnMargin);
            int y = random.Next(coinSpawnMargin, (int)Game1.screenHeight - coinSpawnMargin);
            Vector2 coinPos = new(x, y);

            if (Vector2.Distance(bluePos, coinPos) < spawnBuffer)
            {
                SpawnCoin(bluePos);
            }
            else
            {
                coins.Add(new Coin(coinPos));
            }
        }

        private static void Detection(Character blue)
        {
            foreach(Coin coin in coins)
            {
                if(Vector2.Distance(coin.pos, blue.pos) < uiMargin)
                {
                    blue.score += 1;
                    blue.life = (short)Math.Min(blue.life + 4000, 10000);
                    coins.Remove(coin);
                    break;
                }
            }
        }

        public static void Clean()
        {
            coins.Clear();
        }
    }
}
