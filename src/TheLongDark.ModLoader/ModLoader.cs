using System;
using System.IO;
using System.Reflection;
using Harmony;

namespace TheLongDark.ModLoader
{
    public static class ModLoader
    {
        private const string ModFolder = "Mods";
        
        public static void Initialize()
        {
            Console.WriteLine("Initializing ModLoader");

            Directory.CreateDirectory(GetModsFolder());
            
            var harmony = HarmonyInstance.Create("TheLongDark.ModLoader");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public static string GetModsFolder()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), ModFolder);
        }
    }
}