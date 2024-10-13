using Microsoft.Xna.Framework;
using Object;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tools;

namespace Controller
{
    public class CoinController
    {
        public int totalCoinsSpawned;
        public double lastCoinTime;

        public HashSet<Coin> coins;
        public Coin coinP;

        private Vector2 coinSpawnTime;
        private readonly int spawnBuffer;
        private readonly int coinSpawnMargin;
        private readonly int uiMargin;

        public CoinController()
        {
            totalCoinsSpawned = 0;
            lastCoinTime = 0;

            coins = new HashSet<Coin>();

            coinSpawnTime = JsonProps.GetVector("coin", "coinSpawnTime");
            spawnBuffer = JsonProps.GetInt("coin", "spawnBuffer");
            coinSpawnMargin = JsonProps.GetInt("ui", "margin1");
            uiMargin = JsonProps.GetInt("ui", "margin2");
        }

        public void CoinLogic(double time, Character blue)
        {
            CoinSpawner(time, blue.pos);
            Detection(blue);
        }

        private void CoinSpawner(double time, Vector2 bluePos)
        {
            if (time - lastCoinTime > new Random().Next((int)coinSpawnTime.X, (int)coinSpawnTime.Y) && coins.Count < 4)
            {
                totalCoinsSpawned += 1;
                lastCoinTime = time;
                SpawnCoin(bluePos);
            }

        }

        private void SpawnCoin(Vector2 bluePos)
        {
            Random random = new();
            int x = random.Next(coinSpawnMargin, (int)Util.GetScreen().X - coinSpawnMargin);
            int y = random.Next(coinSpawnMargin, (int)Util.GetScreen().Y - coinSpawnMargin);
            Vector2 coinPos = new(x, y);

            if (Vector2.Distance(bluePos, coinPos) < spawnBuffer)
            {
                SpawnCoin(bluePos);
            }
            else
            {
                coins.Add(new Coin(coinPos, coinP));
            }
        }

        private void Detection(Character blue)
        {
            foreach (Coin coin in coins)
            {
                if (Vector2.Distance(coin.pos, blue.pos) < coin.radius + blue.radius)
                {
                    blue.score += 1;
                    blue.life = (short)Math.Min(blue.life + 4000, 10000);
                    coins.Remove(coin);
                    break;
                }
            }
        }

        public void Clean()
        {
            coins.Clear();
        }
    }
}
