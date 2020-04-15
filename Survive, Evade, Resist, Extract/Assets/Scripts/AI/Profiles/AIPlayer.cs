using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIPlayer : Player
{
    public NavMeshAgent AINavAgent;
    // Start is called before the first frame update
    public new void Start()
    {
        AINavAgent = GetComponent<NavMeshAgent>();
        base.Start();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, GameManager.GM.ExtractionLocation);
        Gizmos.DrawCube(GameManager.GM.ExtractionLocation, new Vector3(1, 1, 1));
    }
    // Update is called once per frame
    public new void Update()
    {
        AINavAgent.destination = GameManager.GM.ExtractionLocation;
        AINavAgent.SetDestination(GameManager.GM.ExtractionLocation);
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
