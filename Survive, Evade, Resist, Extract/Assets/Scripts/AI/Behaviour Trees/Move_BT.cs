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
        agent.brain.WhatIWasDoing.Add(new WhatAmIDoing(Time.time, "I am moving"));
        foreach(var item in agent.WayPoints)
        {
            agent.brain.WhatIWasDoing.Add(new WhatAmIDoing(Time.time, "I am moving to my waypoints"));
            agent.TargetLocation = item;
            agent.AINavAgent.stoppingDistance = agent.StopDistance;
            agent.AINavAgent.destination = agent.TargetLocation;

            if (Vector3.Distance(agent.transform.position, agent.TargetLocation) < 2)
            {
                agent.WayPoints.Remove(item);
                return NodeStatus.SUCCESS;
            }
            else
                return NodeStatus.RUNNING;
        }
        agent.AINavAgent.stoppingDistance = agent.StopDistance;
        agent.AINavAgent.destination = agent.TargetLocation;
        agent.brain.WhatIWasDoing.Add(new WhatAmIDoing(Time.time, "I am moving to my ordered location"));
        if (agent.AINavAgent.desiredVelocity == new Vector3())
            return NodeStatus.SUCCESS;
        if (Vector3.Distance(agent.transform.position, agent.TargetLocation) < 2)
            return NodeStatus.SUCCESS;
        else
            return NodeStatus.RUNNING;
    }
}
