using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AIScript : Agent
{
    public GameObject player;
    private Rigidbody r1;
    public List<GameObject> allPickups;
    public bool hitpickup;
    public bool hitwall;
    private Vector3 offset;

    public override void Initialize()
    {
        r1 = player.GetComponent<Rigidbody>();
        allPickups = new List<GameObject>(21);
        offset = transform.position - player.transform.position;
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(3, 2, -2);
        player.transform.localPosition = new Vector3(3, 2, -2);
        
        SetReward(0f);
        r1.velocity = new Vector3(0.0f, 0.0f, 0.0f);

        if (allPickups.Count != 0) 
        {
            foreach (GameObject pickUp in allPickups)
            {
                // pickUp.transform.localPosition = new Vector3(Random.Range(-15.0f, 17.0f), 2, Random.Range(-15.0f, 19.0f));
                pickUp.SetActive(true);
            }
        }

        allPickups.Clear();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX;
        float moveZ;
        Vector3 move;

        moveX = actions.ContinuousActions[0];
        moveZ = actions.ContinuousActions[1];
        
        move = new Vector3(moveX, 0.0f, moveZ);

        r1.AddForce(move * 5);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continousActions = actionsOut.ContinuousActions;

        continousActions[0] = Input.GetAxisRaw("Horizontal");
        continousActions[1] = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (hitpickup) 
        {
            AddReward(1f);
            hitpickup = false;          
        }

        if (hitwall)
        {
            AddReward(-2f);
            hitwall = false;
            EndEpisode();
        }

        if (GetCumulativeReward() >= 20f)
        {
            AddReward(10f);
            Debug.Log("yay");
            EndEpisode();
        }

        if (r1.velocity.magnitude > 5)
        {
            r1.velocity = r1.velocity.normalized * 5;
        }
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
