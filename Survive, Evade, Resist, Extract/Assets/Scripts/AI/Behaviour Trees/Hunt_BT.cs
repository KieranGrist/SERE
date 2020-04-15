using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Hunt_BT : Node
{
  public  Node node;
    public Hunt_BT(Agent agent, string NodeName) : base(agent, NodeName)
    {
        node = new ChaseDecorator(new ChaseSequence(agent, "Chase Sequence") , agent, "Chase Decorator");
    }

    public override NodeStatus Execute()
    {

        return node.Execute();
    }
}
[System.Serializable]
class ChaseSequence : Sequence
{
  
    public ChaseSequence(Agent agent, string name) : base(agent, name)
    {
        AddChild(new Chase(agent, "Chase "));
        AddChild(new DelayNode(agent, "Delay 20 seconds", 20));
    }

    public override NodeStatus Execute()
    {
        agent.CurrentExecutingNode = this;
        return base.Execute();
    }
}
class Chase : Node
{
    bool Traveled = false;
    bool Travelling = false;
    public Chase(Agent agent, string name) : base(agent, name)
    {
        this.agent = agent;
        NodeName = name;
    }

    public override NodeStatus Execute()
    {
        agent.CurrentExecutingNode = this;
        agent.brain.Hunting = true;
        agent.brain.WhatIAmDoing = "I am chasing after the player ";
        if (agent.brain.Enemy)
        {
            agent.brain.WhatIAmDoing = "I have found the player";
            agent.brain.Hunting = false;
            agent.AIRadio.TransmitEnemySeen();
            return NodeStatus.FAILURE;
        }

        if (agent.brain.Enemy == false && Traveled == false && Travelling == false)
        {
            agent.brain.WhatIAmDoing = "I am going to the players last known location";
            agent.WayPoints.Add(agent.brain.EnemyLastPosition);
            Travelling = true;
        }

        if (Traveled)
        {
            Travelling = false;
            Traveled = false;
            agent.brain.WhatIAmDoing = "I am travelling in the direction the player was last seen going ";
            agent.WayPoints.Add(agent.brain.EnemyTravelingDirection * 500);
            agent.brain.HasSensedPlayer = false;
            return NodeStatus.SUCCESS;
        }
        if (Vector3.Distance(agent.transform.position, agent.WayPoints[0]) < agent.StopDistance)
            Traveled = true;

        return NodeStatus.RUNNING;
    }
}


[System.Serializable]
class ChaseDecorator : ConditionalDecorator
{
    public ChaseDecorator(Node WrappedNode, Agent agent, string name) : base(WrappedNode, agent, name)
    {
        this.agent = agent;
    }

    public override bool CheckStatus()
    {
        agent.CurrentExecutingNode = this;
        return agent.brain.Hunting;
    
    }
}