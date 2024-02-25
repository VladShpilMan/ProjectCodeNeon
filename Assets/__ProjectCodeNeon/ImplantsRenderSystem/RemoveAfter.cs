using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace __ProjectCodeNeon
{
    public class RemoveAfter : MonoBehaviour
    {
        private float time = 5.0f;
        
        private void Start()
        {
            StartCoroutine( RemoveAfterMethod());
        }
        
        private IEnumerator RemoveAfterMethod()
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }
    }
}
