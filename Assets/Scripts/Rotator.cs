using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    void Update()
    {
        // transform.position = transform.position + new Vector3(0, 1 * MV * Time.deltaTime, 0);
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
