using __ProjectCodeNeon.ImplantsRenderSystem.DataTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace __ProjectCodeNeon.Entities
{
    public class Implant : IRenderableImplant
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ImplantPlacement Placement { get; set; }
    }
}
