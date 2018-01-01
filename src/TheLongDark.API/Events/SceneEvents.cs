using System;
using UnityEngine.SceneManagement;

namespace API.Events
{
    public static class SceneEvents
    {
        public static event Action<Scene, LoadSceneMode> SceneLoad;
        public static event Action<Scene, LoadSceneMode> SceneLoaded;
        
        public static void OnSceneLoad(Scene scene, LoadSceneMode mode)
        {
            SceneLoad?.Invoke(scene, mode);
        }
        
        public static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneLoaded?.Invoke(scene, mode);
        }
    }
}