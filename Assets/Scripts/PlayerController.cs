using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AIScript agentscript;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp") && other.gameObject.activeSelf)
        {
            agentscript.allPickups.Add(other.gameObject);
            other.gameObject.SetActive(false);
            agentscript.hitpickup = true;
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            agentscript.hitwall = true;
        }

    } 
}
