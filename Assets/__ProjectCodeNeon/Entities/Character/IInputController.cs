using __ProjectCodeNeon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputController
{
    float GetHorizontalMovement();
    float GetVerticalMovement();
    Quaternion GetLook(Transform player, Transform cursor);
    bool IsShooting();
    bool NextCard();
    bool PreviousCard();
}
