using System.Collections.Generic;
using __ProjectCodeNeon.ImplantsRenderSystem.DataTypes;
using UnityEngine;

namespace __ProjectCodeNeon.ImplantsRenderSystem
{
    public class ImplantsRenderer : MonoBehaviour
    {
        public Skeleton Skeleton;
        public ImplantsConfig ImplantsConfig;
        
        private List<GameObject> _spawnedImplants = new List<GameObject>();
        private const string Separator = "-";

        
        private void Awake()
        {
            Skeleton = new Skeleton(transform);
        }

        public void SetImplant(string command)
        {
            var implant = ParseImplant(command);
            RenderImplant(implant, $"Invalid implant command: {command}");
        }

        public void SetImplant(ImplantPlacement type, int id)
        {
            var implant = ImplantsConfig.GetImplant(type.ToString(), id);
            RenderImplant(implant, $"Invalid implant type: {type}, id: {id}");
        }

        public string BuildCommand(ImplantPlacement type, int id)
            => $"{type}-{id}";

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
                var spawnPoint = Skeleton.GetBone(elementType);
                if (spawnPoint == null)
                {
                    Debug.LogWarning($"Spawn point not found for element {elementType}");
                    continue;
                }

                var newObject = Instantiate(element.Prefab, spawnPoint) as GameObject;
                newObject.transform.SetParent(spawnPoint);
                Skeleton.ToggleOrigin(elementType, element.DisableOrigin);
                _spawnedImplants.Add(newObject);
            }
        }

        private Implant ParseImplant(string command)
        {
            var commandParts = command.Split(Separator);

            if (commandParts.Length != 2 || !int.TryParse(commandParts[1], out int implantId))
            {
                Debug.LogError($"Invalid command format or implant ID: {command}");
                return null;
            }

            return ImplantsConfig.GetImplant(commandParts[0], implantId);
        }
    }
}