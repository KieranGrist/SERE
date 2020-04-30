using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class AgentBrainSequence : Sequence
{
    public AgentBrainSequence(Agent agent, string name) : base(agent, name)
    {
        AddChild(agent.RootNode);
      //  AddChild(new DelayNode(agent, "Delay", 1));
        AddChild(agent.move_BT);
   //     AddChild(new DelayNode(agent, "Delay", 1));
    }
}
public class Agent : Entity
{

    [Header("Brain")]
    public Selector RootNode;
    public Node CurrentExecutingNode;
    public AgentBrainSequence brainSequence;
    /// <summary>
    /// Contains all information usefull towards the AIS "Brain"
    /// </summary>
    public BrainInformation brain;

    [Header("Team")]
    public Team AgentsTeam = new Team();


    [Header("Movement")]
   public List<Vector3> WayPoints = new List<Vector3>();

    public Move_BT move_BT;
    [Header("Agent Stats")]
    public NavMeshAgent AINavAgent;
    public float StopDistance = 1;
    public SearchInformation search;

    [Header("Radio")]
    public Radio AIRadio;



    public Agent() : base()
    {
        //  search = new Search();
        AgentsTeam = new Team();
        brain = new BrainInformation();
        //AIRadio = null;
    }
    private new void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.white;
        if (WayPoints.Count > 0)
        {
            Gizmos.DrawLine(transform.position, WayPoints[0]);

            Gizmos.DrawCube(WayPoints[0], new Vector3(1, 1, 1));
        }
        Gizmos.DrawWireSphere(transform.position, entityStats.SightRange);

        Gizmos.color = Color.green;
        if (WayPoints.Count > 0)
            Gizmos.DrawLine(transform.position, WayPoints[0]);
        for (int i = 0; i < WayPoints.Count; i++)
        {
            Gizmos.DrawCube(WayPoints[i], new Vector3(1, 1, 1));

        }
        for (int i = 0; i < WayPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(WayPoints[i], WayPoints[i + 1]);
        }

    }
    public bool InTeam()
    {
        if (AgentsTeam != null)
            return true;
        if (!AgentsTeam.Members.Contains(this))
            return false;
        else
            return true; 
    }

    public virtual new void Restart()
    {
        base.Restart();

        brain = new BrainInformation();
        search = new SearchInformation();

        AINavAgent = GetComponent<NavMeshAgent>();
        AgentsTeam = new Team();
        search = new SearchInformation();
        move_BT = new Move_BT(this, "Movement");
        brainSequence = new AgentBrainSequence(this, "Sequence");
    }

    // Update is called once per frame
    public virtual new void Update()
    {
        base.Update();
        if (!Control)
            brainSequence.Execute();
  
    }


    public void Reload()
    {
        StartCoroutine(combat.CurrentWeapon.Reload(inventory));
    }
    public void SensesSystem()
    {

        brain.UpdateEnemyInfo();
        if (brain.Enemy)
        {
            Sprint();
            brain.EnemyTravelingDirection = brain.Enemy.transform.forward;

        }

        float ClosestDistance = float.MaxValue;
        brain.SeePlayer = false;
        brain.HearPlayer = false;
        brain.SmellPlayer = false;
        brain.Enemy = null;
        foreach (var item in Physics.OverlapSphere(transform.position, entityStats.SightRange, LayerMask.GetMask("Enemy")))
        {
            if (item.gameObject != gameObject)
                if (Vector3.Distance(transform.position, item.transform.position) < ClosestDistance)
                {                    
                    brain.SeePlayer = true;
                    brain.Enemy = item.GetComponent<Entity>();        
                    
                    ClosestDistance = Vector3.Distance(transform.position, item.transform.position);        

                }
        }
   
    }
}
