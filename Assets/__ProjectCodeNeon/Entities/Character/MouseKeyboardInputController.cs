using __ProjectCodeNeon;
using __ProjectCodeNeon.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseKeyboardInputController : IInputController
{

    public float GetHorizontalMovement()
    {
        return Input.GetAxis("Horizontal");
    }

    public float GetVerticalMovement()
    {
        return Input.GetAxis("Vertical");
    }

    public Quaternion GetLook(Transform player, Transform cursor)
    {
       Vector3 forward = cursor.position - player.position;

       return Quaternion.LookRotation(new Vector3(forward.x, 0, forward.z));
    }

    public bool IsShooting()
    {
        return Input.GetButtonDown("Fire1");
    }

    public bool NextCard()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    public bool PreviousCard()
    {
        return Input.GetKeyDown(KeyCode.Q);
    }

    public void Shoot()
    {
        throw new System.NotImplementedException();
    }
}
