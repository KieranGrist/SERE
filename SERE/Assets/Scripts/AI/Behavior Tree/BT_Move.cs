using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Move : Node
{
    private Agent agent;
    public BT_Move(Node Node, Agent agent) : base( agent)
    {
        this.agent = agent;
    }

    public override NodeStatus Execute()
    {
        agent.MoveTo(agent.MoveToLocation);
        return NodeStatus.SUCCESS;
    }
}
