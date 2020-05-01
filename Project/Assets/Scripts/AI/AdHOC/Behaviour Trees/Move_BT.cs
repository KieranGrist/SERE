 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_BT : Node
{
    public Move_BT(AdHOCAgent agent, string NodeName) : base(agent, NodeName)
    {
    }

    public override NodeStatus Execute()
    {
        agent.SensesSystem();
        if (agent.WayPoints.Count > 0)
        {
            var item = agent.WayPoints[0];
            agent.AINavAgent.destination = item;
            if (Vector3.Distance(agent.transform.position, item) < 2)
            {
                agent.WayPoints.Remove(item);
                return NodeStatus.SUCCESS;
            }

      
        }
        return NodeStatus.SUCCESS;
    }
}
