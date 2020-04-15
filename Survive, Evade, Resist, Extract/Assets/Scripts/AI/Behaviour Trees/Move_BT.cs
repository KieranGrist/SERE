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
        agent.brain.WhatIAmDoing = "I am moving";
        foreach(var item in agent.WayPoints)
        {
            agent.brain.WhatIAmDoing = "I am moving to my waypoints";
            agent.TargetLocation = item;
            agent.AINavAgent.stoppingDistance = agent.StopDistance;
            agent.AINavAgent.destination = agent.TargetLocation;

            if (Vector3.Distance(agent.TargetLocation, agent.transform.position) < 2)
                return NodeStatus.SUCCESS;
            else
                return NodeStatus.RUNNING;
        }
        agent.AINavAgent.stoppingDistance = agent.StopDistance;
        agent.AINavAgent.destination = agent.TargetLocation;
        agent.brain.WhatIAmDoing = "I am moving to my ordered location";
        if (agent.AINavAgent.desiredVelocity == new Vector3())
            return NodeStatus.SUCCESS;
        if (Vector3.Distance(agent.TargetLocation, agent.transform.position) < 2)
            return NodeStatus.SUCCESS;
        else
            return NodeStatus.RUNNING;
    }
}
