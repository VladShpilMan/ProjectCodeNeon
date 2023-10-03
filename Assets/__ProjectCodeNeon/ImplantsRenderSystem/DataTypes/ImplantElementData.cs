using UnityEngine;

namespace __ProjectCodeNeon.ImplantsRenderSystem.DataTypes
{
    [System.Serializable]
    public struct ImplantElementData
    {
        public ElementType Type;
        public GameObject Prefab;
        public bool DisableOrigin;

        public string GetElementType(string placement)
        {
            switch (placement)
            {
                case "LeftHand":
                    return "Left" + Type.ToString();
                case "RightHand":
                    return "Right" + Type.ToString();
                default:
                    return Type.ToString();
            }
        }
    }
}