using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scene
{
    public class SceneManager
    {
        private IScene currentScene;

        public void ChangeScene(IScene scene)
        {
            scene.Load();

            currentScene = scene;
        }
        public IScene GetCurrentScene() { return currentScene; }
    }
}
