using System.Collections.Generic;
using __ProjectCodeNeon.ImplantsRenderSystem.DataTypes;
using UnityEngine;
using UnityEngine.Serialization;

namespace __ProjectCodeNeon.ImplantsRenderSystem
{
    public class ImplantsRenderer : MonoBehaviour
    {
        [SerializeField] private ImplantsConfig _implantsConfig;
        private List<GameObject> _spawnedImplants = new List<GameObject>();
        private Skeleton _skeleton;

        public ImplantsRenderer(List<IRenderableImplant> implantsToRender)
        {
            Initialize(implantsToRender);
        }

        public void Initialize(List<IRenderableImplant> implantsToRender)
        {
            _skeleton = new Skeleton(transform);
            ClearImplants();
            foreach (var implant in implantsToRender)
                RenderImplant(implant.ParseImplant(_implantsConfig), $"Invalid implant: {implant.Placement}-{implant.Id}");
        }
        
        private void ClearImplants()
        {
            foreach (var implant in _spawnedImplants)
                Destroy(implant);
            
            _spawnedImplants.Clear();
            _skeleton.EnableAllOrigins();
        }

        private void RenderImplant(Implant implant, string errorMsg)
        {
            if (implant == null)
            {
                Debug.LogError(errorMsg);
                return;
            }

            foreach (var element in implant.Elements)
            {
                var elementType = element.GetElementType(implant.Name);
                var spawnPoint = _skeleton.GetBone(elementType);
                if (spawnPoint == null)
                {
                    Debug.LogWarning($"Spawn point not found for element {elementType}");
                    continue;
                }

                var newObject = Instantiate(element.Prefab, spawnPoint) as GameObject;
                newObject.transform.SetParent(spawnPoint);
                _skeleton.ToggleOrigin(elementType, element.DisableOrigin);
                _spawnedImplants.Add(newObject);
            }
        }
    }
}