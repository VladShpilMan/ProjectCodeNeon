using UnityEngine;

namespace __ProjectCodeNeon
{
    [CreateAssetMenu(fileName = "GenerationConfig", menuName = "GenerationConfig")]
    public class GenerationConfig : ScriptableObject
    {
        [System.Serializable]
        public struct Modules
        {
            public ModuleType Type;
            public GameObject[] Prefabs;
            public int Priority;
            public int Count;
        }

        public Modules[] ModulesInfo;
    }
}