using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class AIPlayer : Player
{
    public NavMeshAgent agent;
    // Start is called before the first frame update
    new void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    new void Update()
    {
        agent.destination = GameManager.GM.ExtractionLocation;
    }
}
