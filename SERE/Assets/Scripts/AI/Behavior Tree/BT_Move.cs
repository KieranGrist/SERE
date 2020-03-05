using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Move : Node
{
    private Agent agent;
    public BT_Move( Agent agent) : base( agent)
    {
        this.agent = agent;
    }

    public override NodeStatus Execute()
    {
        agent.MoveTo(agent.MoveToLocation);
        bool ReachedLocation = Vector3.Distance(agent.MoveToLocation, agent.transform.position) < 1;
 if (ReachedLocation)
            return NodeStatus.SUCCESS;
        else
            return NodeStatus.RUNNING;
    }
}
