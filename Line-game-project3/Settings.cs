using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Line_game_project3
{
    public static class Settings
    {
        

        public static Dictionary<string, int> numbers = new Dictionary<string, int>
        {
            {"Margin1", 50},
            {"Margin2", 25},
            {"SpawnBuffer", 100},
            {"MaxLife", 10000},
            {"LifeDec", 20}
        };

        public static Dictionary<string, Vector2> vectors = new Dictionary<string, Vector2>
        {
            {"CoinSpawnTime", new Vector2(2200, 2600) }
        };
    }
}
