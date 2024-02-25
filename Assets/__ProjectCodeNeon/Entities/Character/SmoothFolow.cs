using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFolow : MonoBehaviour
{
    public GameObject target;
    
    
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, 0.1f);
    }
}
