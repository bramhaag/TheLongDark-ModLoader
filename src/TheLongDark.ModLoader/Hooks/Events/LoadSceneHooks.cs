using API.Events;
using Harmony;
using UnityEngine.SceneManagement;

namespace TheLongDark.ModLoader.Hooks.Events
{
    public class LoadSceneHooks
    {
        [HarmonyPatch(typeof(SceneManager), "LoadScene", new []{typeof(string), typeof(LoadSceneMode)})]
        public class LoadScene1
        {
            static void Postfix(string sceneName, LoadSceneMode mode)
            {
                SceneEvents.OnSceneLoad(SceneManager.GetSceneByName(sceneName), mode);
            }
        }
        
        [HarmonyPatch(typeof(SceneManager), "LoadScene", new []{typeof(int), typeof(LoadSceneMode)})]
        public class LoadScene2
        {
            static void Postfix(int sceneBuildIndex, LoadSceneMode mode)
            {
                SceneEvents.OnSceneLoad(SceneManager.GetSceneByBuildIndex(sceneBuildIndex), mode);
            }
        }
        
        [HarmonyPatch(typeof(SceneManager), "LoadSceneAsync", new []{typeof(string), typeof(LoadSceneMode)})]
        public class LoadSceneAsync1
        {
            static void Postfix(string sceneName, LoadSceneMode mode)
            {
                SceneEvents.OnSceneLoad(SceneManager.GetSceneByName(sceneName), mode);
            }
        }
        
        [HarmonyPatch(typeof(SceneManager), "LoadSceneAsync", new []{typeof(int), typeof(LoadSceneMode)})]
        public class LoadSceneAsync2
        {
            static void Postfix(int sceneBuildIndex, LoadSceneMode mode)
            {
                SceneEvents.OnSceneLoad(SceneManager.GetSceneByBuildIndex(sceneBuildIndex), mode);
            }
        }
    }
}