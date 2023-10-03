using UnityEngine;

namespace __ProjectCodeNeon.ImplantsRenderSystem.DataTypes
{
    [CreateAssetMenu(fileName = "NewImplant", menuName = "Implants/New Implant")]
    public class Implant : ScriptableObject
    {
        public ImplantElementData[] Elements;
        public string Name;
    }
}
