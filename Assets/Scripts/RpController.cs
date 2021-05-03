using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RpController : MonoBehaviour
{ 
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    
}
