using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using __ProjectCodeNeon.ImplantsRenderSystem.DataTypes;

namespace __ProjectCodeNeon.Entities
{
    public static class EntityHelper
    {
        public static List<IRenderableImplant> ImplantToIRenderableImplant(this List<Implant> values)
        {
            var list = new List<IRenderableImplant>();

            foreach (var value in values)
                list.Add(value);

            return list;
        }
    }
}