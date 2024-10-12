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

        public static void CoinLogic(double time, Character blue)
        {
            CoinSpawner(time, blue.pos);
            Detection(blue);
        }

        private static void CoinSpawner(double time, Vector2 bluePos)
        {
            if (time - lastCoinTime > 2500 && coins.Count < 4)
            {
                totalCoinsSpawned += 1;
                lastCoinTime = time;
                SpawnCoin(bluePos);
            }

        }

        private static void SpawnCoin(Vector2 bluePos)
        {
            Random random = new();
            int x = random.Next(10, (int)Game1.screenWidth - 10);
            int y = random.Next(10, (int)Game1.screenHeight - 10);
            Vector2 coinPos = new(x, y);

            if (Vector2.Distance(bluePos, coinPos) < 100)
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
                if(Vector2.Distance(coin.pos, blue.pos) < 25)
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
