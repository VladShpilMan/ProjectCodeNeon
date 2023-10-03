using __ProjectCodeNeon.ImplantsRenderSystem.DataTypes;
using UnityEngine;

namespace __ProjectCodeNeon.ImplantsRenderSystem
{
    [CreateAssetMenu(fileName = "ImplantsConfig", menuName = "Implants/New Config")]
    public class ImplantsConfig : ScriptableObject
    {
        public Implant[] HeadImplantsPrefab;
        public Implant[] BodyImplantsPrefab;
        public Implant[] HandImplantsPrefab;

        public Implant GetImplant(string type, int id)
        {
            HandImplantsPrefab[id].Name = type;
            switch (type)
            {
                case "Head":
                    return HeadImplantsPrefab[id];
                case "Body":
                    return BodyImplantsPrefab[id];
                case "LeftHand":
                    return HandImplantsPrefab[id];
                case "RightHand":
                    return HandImplantsPrefab[id];
            }

            return null;
        }
    }
}