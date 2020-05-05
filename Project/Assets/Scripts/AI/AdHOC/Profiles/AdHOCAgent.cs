using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class AgentBrainSequence : Sequence
{
    public AgentBrainSequence(AdHOCAgent agent, string name) : base(agent, name)
    {
        AddChild(agent.RootNode);
      //  AddChild(new DelayNode(agent, "Delay", 1));
        AddChild(agent.move_BT);
   //     AddChild(new DelayNode(agent, "Delay", 1));
    }
}
public class AdHOCAgent : Entity
{

    [Header("Brain")]
    public Entity Enemy = null;
    public Selector RootNode;
    public Node CurrentExecutingNode;
    public AgentBrainSequence brainSequence;
    /// <summary>
    /// Contains all information usefull towards the AIS "Brain"
    /// </summary>
    public BrainInformation brain;


    [Header("Movement")]
   public List<Vector3> WayPoints = new List<Vector3>();

    public Move_BT move_BT;
    [Header("Agent Stats")]
    public NavMeshAgent AINavAgent;
    public SearchInformation search;




    public AdHOCAgent() : base()
    {
    }

    private void Start()
    {
        Restart();
        AINavAgent = GetComponent<NavMeshAgent>();
    }
    public virtual new void Restart()
    {
        base.Restart();
        brain = new BrainInformation();
        search = new SearchInformation();
        search = new SearchInformation();
        move_BT = new Move_BT(this, "Movement");
        WayPoints = new List<Vector3>();
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
        float ClosestDistance = float.MaxValue;
        brain.SeePlayer = false;   
        Enemy = null;
        foreach (var item in Physics.OverlapSphere(transform.position, entityStats.SightRange, LayerMask.GetMask("Enemy")))
        {
            if (item.gameObject != gameObject)
                if (Vector3.Distance(transform.position, item.transform.position) < ClosestDistance)
                {                    
                    brain.SeePlayer = true;
                    Enemy = item.GetComponent<Entity>();        
                    
                    ClosestDistance = Vector3.Distance(transform.position, item.transform.position);        

                }
        }

        if (Enemy)
            brain.SeePlayer = true;
 


    }
}
