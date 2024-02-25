using __ProjectCodeNeon.ImplantsRenderSystem.DataTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace __ProjectCodeNeon.Entities
{
    public class Implant : MonoBehaviour, IRenderableImplant
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Damage { get; set; }

        public virtual void Action ()
        {
            Debug.Log("BaseShoot");
        }

        public ImplantPlacement Placement { get; set; }
        public ImplantMode ImplantMode { get; set; }
    }
}
