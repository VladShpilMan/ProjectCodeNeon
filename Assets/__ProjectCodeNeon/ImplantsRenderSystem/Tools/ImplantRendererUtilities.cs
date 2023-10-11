using System.Collections;
using System.Collections.Generic;
using __ProjectCodeNeon.ImplantsRenderSystem;
using __ProjectCodeNeon.ImplantsRenderSystem.DataTypes;
using UnityEngine;

namespace __ProjectCodeNeon
{
    public static class ImplantRendererUtilities
    {
        public static Implant ParseImplant(this IRenderableImplant implantToRender, ImplantsConfig config)
        {
            return config.GetImplant(implantToRender.Placement.ToString(), implantToRender.Id);
        }
    }
}
