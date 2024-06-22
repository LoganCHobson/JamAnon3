using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIScript: MonoBehaviour
{

    public Camera cam;

    public NavMeshAgent agent;

    public Transform Player;


    void Update()
    {
        agent.SetDestination(Player.position);


    }
}
