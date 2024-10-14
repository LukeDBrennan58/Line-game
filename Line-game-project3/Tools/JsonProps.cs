using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tools
{
    public class JsonProps
    {
        public static JsonElement root;
        public static void Start()
        {
            if(root.ValueKind == JsonValueKind.Undefined)
            {
                string file = "properties.json";
                string json;
                if (File.Exists(file))
                {
                    json = File.ReadAllText(file);
                }
                else
                {
                    throw new FileNotFoundException("properties.json file is missing");
                }

                JsonDocument doc = JsonDocument.Parse(json);
                root = doc.RootElement;
            }
        }

        public static JsonElement Get(string key)
        {
            Start();
            return root.GetProperty(key);
        }

        public static JsonElement Get(string key, string key2)
        {
            Start();
            return root.GetProperty(key).GetProperty(key2);
        }

        public static Vector2 GetVector(string key, string key2)
        {
            Start();
            JsonElement element = Get(key, key2);
            return new Vector2(
                    element.GetProperty("X").GetInt16(),
                    element.GetProperty("Y").GetInt16());
        }

        public static int GetInt(string key, string key2)
        {
            Start();
            JsonElement element = Get(key, key2);
            return element.GetInt16();
        }

        public static Color GetColor(string key)
        {
            Start();
            JsonElement element = Get("colors", key);
            return new Color(
                    element.GetProperty("R").GetInt16(),
                    element.GetProperty("G").GetInt16(),
                    element.GetProperty("B").GetInt16());
        }
    }
}
