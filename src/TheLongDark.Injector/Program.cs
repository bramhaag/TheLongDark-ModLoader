using System;
using System.IO;
using System.Linq;
using System.Reflection;
using dnlib.DotNet;
using Microsoft.Win32;

namespace TheLongDark.Injector
{
    internal class Program
    {
        private const string SteamKey = @"Software\Valve\Steam";
        private const string TldPath = @"steamapps\common\TheLongDark\tld_Data\Managed";
        
        public static void Main(string[] args)
        {
            var basePath = Path.Combine(args.Length > 0 ? args[0] : GetSteamPath(), TldPath);
            Console.WriteLine($"Base path: {basePath}");
            
            var assemblyPath = Path.Combine(basePath, "Assembly-CSharp.dll");
            Console.WriteLine($"Assembly-CSharp.dll: {assemblyPath}");
            
            BackupFile(assemblyPath);

            var patchedPath = Path.Combine(basePath, "Assembly-CSharp.patched.dll");

            Console.WriteLine("Injecting hook...");
            
            var module = ModuleDefMD.Load(assemblyPath);
            module.Import(typeof(Assembly));

            var assemblyInstructions = module.GetTypes()
                .First(t => t.Name == "BootUpdate")
                .FindMethod("Awake").Body.Instructions;
            
            var patchInstructions = ModuleDefMD.Load(typeof(Program).Module).GetTypes()
                .First(t => t.FullName == typeof(Program).FullName)
                .FindMethod("Inject").Body.Instructions.ToArray();

            assemblyInstructions.RemoveAt(assemblyInstructions.Count - 1);
            Array.ForEach(patchInstructions, instr => assemblyInstructions.Add(instr));
            module.Write(patchedPath);
            
            Console.WriteLine("Hook injected!");
        }

        // ReSharper disable once UnusedMember.Local
        private void Inject()
        {
            Assembly
                .LoadFrom(@"tld_Data\Managed\TheLongDark.ModLoader.dll")
                .GetType("TheLongDark.ModLoader.ModLoader")
                .GetMethod("Initialize")
                .Invoke(null, null);
        }

        private static string GetSteamPath()
        {
            using (var steamPath = Registry.CurrentUser.OpenSubKey(SteamKey))
            {
                return ((string) steamPath?.GetValue("SteamPath"))?.Replace("/", @"\") 
                       ?? throw new FileNotFoundException("Cannot find steam installation folder!");
            }
        }

        private static void BackupFile(string path)
        {
            var parent = Path.GetDirectoryName(path) ?? throw new ArgumentException("Path does not contain a file");
            var filename = Path.GetFileName(path) + ".old";

            var counter = 0;

            while (File.Exists(Path.Combine(parent, filename + counter)))
            {
                counter++;
            }
            
            File.Copy(path, Path.Combine(parent, filename + counter));
        }
    }
}