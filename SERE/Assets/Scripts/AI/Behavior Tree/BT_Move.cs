using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BT_Move : Node
{
    private Agent agent;
    public BT_Move(Agent Bt, string name) : base(Bt, name)
    {
        agent = Bt;
        NodeName = name;
    }
    public override NodeStatus Execute()
    {
        agent.MoveTo(agent.MoveToLocation);
        bool ReachedLocation = Vector3.Distance(agent.MoveToLocation, agent.transform.position) < 2;
 if (ReachedLocation)
            return NodeStatus.SUCCESS;
        else
            return NodeStatus.RUNNING;
    }
}
