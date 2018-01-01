using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Harmony;
using TheLongDark.API;

namespace TheLongDark.ModLoader.Patches
{
    [HarmonyPatch(typeof(BootUpdate), "Update")]
    public class LoadModsPatch
    {
        static void Prefix()
        {
            if (!InputManager.instance || !InputManager.CheckForActiveController()) return;
            
            var assemblies = Directory.GetFiles(ModLoader.GetModsFolder())
                .Where(file => Path.GetExtension(file).ToLower() == ".dll")
                .ToArray();
                
            foreach (var path in assemblies)
            {
                var type = Assembly.LoadFrom(path)
                    .GetTypes()
                    .First(t => typeof(Mod).IsAssignableFrom(t));

                type.GetMethod("Start")
                    .Invoke(Activator.CreateInstance(type), null);
            }
        }
    }
}