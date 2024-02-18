using __ProjectCodeNeon.ImplantsRenderSystem.DataTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace __ProjectCodeNeon.Entities
{
    public static class CombatController
    {
        private static float baseDamage = 5.0f;

        public static float CalculateDamage(Implant currentImplant, List<Implant> passiveImplantsList)
        {
            float passiveDamageUpgrade = 0;

            foreach (var implant in passiveImplantsList)
            {
                passiveDamageUpgrade += implant.Damage;
            }

            float totalDamage = (baseDamage + passiveDamageUpgrade) * currentImplant.Damage;

            if (currentImplant.ImplantMode == ImplantMode.Melee)
            {
                totalDamage *= 1.5f;
            }

            return totalDamage;
        }
    }
}
