using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class AIPlayer : Player
{
    public NavMeshAgent AINavAgent;
    // Start is called before the first frame update
    public new void Start()
    {
        AINavAgent = GetComponent<NavMeshAgent>();
        base.Start();
    }
    private new void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, GM.ExtractionLocation);
        Gizmos.DrawCube(GM.ExtractionLocation, new Vector3(1, 1, 1));
    }
    // Update is called once per frame
    public new void Update()
    {
        AINavAgent.destination = GM.ExtractionLocation;
        AINavAgent.SetDestination(GM.ExtractionLocation);
        switch (Affiliation)
        {
            case Side.Civilian:
                gameObject.layer = 12;
                break;
            case Side.Enemy:
                gameObject.layer = 10;
                break;
            case Side.Friendly:
                gameObject.layer = 11;
                break;
        }


        if (transform.position.y < -1)
        {
            entityStats.Health = 0;
        }


        inventory.CalculateWeight();
        TravelingDirection = transform.forward;
        combat.Update();
        switch (Affiliation)
        {
            case Side.Civilian:
                gameObject.layer = 12;
                break;
            case Side.Enemy:
                gameObject.layer = 10;
                break;
            case Side.Friendly:
                gameObject.layer = 11;
                break;
        }


        if (transform.position.y < -1)
        {
            entityStats.Health = 0;
        }

    }
}
