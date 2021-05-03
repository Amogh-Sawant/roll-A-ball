using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AIScript : Agent
{
    // [SerializeField] private Transform pickUpTransform;
    private Rigidbody r1;
    public float totalReward;
    public List<GameObject> allPickups;
    
    public override void Initialize()
    {
        r1 = GetComponent<Rigidbody>();
        allPickups = new List<GameObject>(21);
        
    }
    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(3, 2, -2);
        SetReward(0f);
        r1.velocity = new Vector3(0.0f, 0.0f, 0.0f);

        if (allPickups.Count != 0) 
        {
            
            foreach (GameObject pickUp in allPickups)
            {
                pickUp.SetActive(true);
            }

        }
        allPickups.Clear();

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        // sensor.AddObservation(pickUpTransform.localPosition);

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX;
        float moveZ;
        Vector3 move;

        moveX = actions.ContinuousActions[0];
        moveZ = actions.ContinuousActions[1];
        
        move = new Vector3(moveX, 0.0f, moveZ);

        r1.AddForce(move * 10);

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continousActions = actionsOut.ContinuousActions;
        continousActions[0] = Input.GetAxisRaw("Horizontal");
        continousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp") && other.gameObject.activeSelf) 
        {
            AddReward(1f);
            totalReward = GetCumulativeReward();
            allPickups.Add(other.gameObject);
            
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            SetReward(-2f);
            EndEpisode();

        }
        if (totalReward >= 20f)
        {
            AddReward(10f);
            Debug.Log("yay");
            EndEpisode();
        }

    }
}
