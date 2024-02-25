using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace __ProjectCodeNeon
{
    public class LevelGenerator : MonoBehaviour
    {
        public GenerationConfig generationConfig;
        
        private void Awake()
        {
            GenerateLevel();
        }

        private void GenerateLevel()
        {
            GameObject[] modules = RandomizeModules(generationConfig.ModulesInfo);
            
            for (int i = 0; i < modules.Length; i++)
            {
                var module = Instantiate(modules[i], new Vector3(0, 0, 0), Quaternion.Euler(new Vector3( 0, 90 * (i+1), 0)));
                module.transform.SetParent(transform);
            }
        }

        private GameObject[] RandomizeModules(GenerationConfig.Modules[] modulesInfo)
        {
            var modules = generationConfig.ModulesInfo;
            var modulesList = new List<GameObject>();
            
            System.Array.Sort(modules, (x, y) => x.Priority.CompareTo(y.Priority));

            foreach (var module in modules)
                for(int i = 0; i < module.Count; i++)
                    modulesList.Add(module.Prefabs[Random.Range(0, module.Prefabs.Length)]);

            return modulesList.GetRange(0, 4).ToArray();
        }
    }
}
