using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Line_game_project3
{
    public class JsonManager
    {
        public static void Start()
        {
            string file = "properties.json";
            string json = File.ReadAllText(file);

            JsonDocument doc = JsonDocument.Parse(json);
            JsonElement root = doc.RootElement;

            Debug.Write(root.GetProperty("ui").GetProperty("margin1"));
        }
    }
}
