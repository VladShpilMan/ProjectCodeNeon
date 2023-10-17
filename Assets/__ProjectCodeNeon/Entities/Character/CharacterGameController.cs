using __ProjectCodeNeon.ImplantsRenderSystem;
using __ProjectCodeNeon.ImplantsRenderSystem.DataTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace __ProjectCodeNeon.Entities
{

    public class CharacterGameController : MonoBehaviour
    {
        private IInputController _inputController;
        private ImplantController _implantController;
        private ImplantsRenderer _implantsRenderer;

        public List<Implant> ImplantsList { get; set; }

        private string _loadedImplantList = "";

        public void Awake()
        {
            ImplantsList = _implantController.GetAllImplantsBasedOnList(_loadedImplantList);
            _implantsRenderer = new ImplantsRenderer(ImplantsList.ImplantToIRenderableImplant());
        }
    }
}
