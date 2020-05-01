using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class AIPlayer : Player
{

    Vector3 ExtractionPoint;
    public NavMeshAgent AINavAgent;
    // Start is called before the first frame update
    public new void Start()
    {   
       base.Start();
        AINavAgent = GetComponent<NavMeshAgent>();
        Control = false;

    }
    private new void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, MyArea.ExtractionGameObject.transform.position);
        Gizmos.DrawCube(MyArea.ExtractionGameObject.transform.position, new Vector3(1, 1, 1));
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(AINavAgent.destination,50);
    }
    // Update is called once per frame
    public new void Update()
    {
        ExtractionPoint = MyArea.ExtractionGameObject.transform.position;
        AINavAgent.destination=ExtractionPoint;
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
        if (entityStats.Health <= 0)
        {
            MyArea.AgentWins++;
            MyArea.Restart();
        }
    }

}
