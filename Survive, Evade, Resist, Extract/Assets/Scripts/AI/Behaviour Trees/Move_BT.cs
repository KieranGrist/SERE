 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_BT : Node
{
    public Move_BT(Agent agent, string NodeName) : base(agent, NodeName)
    {
    }

    public override NodeStatus Execute()
    {
        agent.SensesSystem();
        foreach(var item in agent.WayPoints)
        {
   
           var TargetLocation = item;
            agent.AINavAgent.stoppingDistance = agent.StopDistance;
            agent.AINavAgent.destination = TargetLocation;

            if (Vector3.Distance(agent.transform.position, TargetLocation) < 2)
            {
                agent.WayPoints.Remove(item);
                return NodeStatus.SUCCESS;
            }
            else
                return NodeStatus.RUNNING;
        }
        return NodeStatus.SUCCESS;
    }
}
