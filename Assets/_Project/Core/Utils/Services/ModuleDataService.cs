namespace Project.Core
{
    using UnityEngine;
    using System.Collections.Generic;
    using System.Linq;

    public static class ModuleDataService
    {
        private static Dictionary<string, ModuleData> _allModuleData;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void LoadModuleData()
        {
            ModuleData[] allModuleData = Resources.LoadAll<ModuleData>("");

            _allModuleData = allModuleData.ToDictionary(
                data => data.ModuleName,
                data => data
            );
        }


        public static ModuleData GetModuleData(string moduleName)
        {
            if (_allModuleData.TryGetValue(moduleName, out ModuleData moduleData))
            {
                return moduleData;
            }
            else
            {
                return null;
            }
        }
    }
}