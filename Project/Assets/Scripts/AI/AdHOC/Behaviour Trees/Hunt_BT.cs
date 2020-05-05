using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Hunt_BT : Node
{
  public  Node node;
    public Hunt_BT(AdHOCAgent agent, string NodeName) : base(agent, NodeName)
    {
        node = new ChaseDecorator(new ChaseSequence(agent, "Chase Sequence") , agent, "Chase Decorator");
    }

    public override NodeStatus Execute()
    {
        agent.SensesSystem();
        return node.Execute();
    }
}

[System.Serializable]
class ChaseDecorator : ConditionalDecorator
{
    public ChaseDecorator(Node WrappedNode, AdHOCAgent agent, string name) : base(WrappedNode, agent, name)
    {
        this.agent = agent;
    }

    public override bool CheckStatus()
    {
        agent.CurrentExecutingNode = this;
        return agent.brain.Hunting;

    }
}


[System.Serializable]
class ChaseSequence : Sequence
{
  
    public ChaseSequence(AdHOCAgent agent, string name) : base(agent, name)
    {
        AddChild(new Chase(agent, "Chase "));
     //   AddChild(new DelayNode(agent, "Delay 20 seconds", 20));
    }

    public override NodeStatus Execute()
    {
        
        agent.CurrentExecutingNode = this;
        return base.Execute();
    }
}

[System.Serializable]
class Chase : Node
{
    bool Traveled = false;
    bool Travelling = false;
    public Chase(AdHOCAgent agent, string name) : base(agent, name)
    {
        this.agent = agent;
        NodeName = name;
    }

    public override NodeStatus Execute()
    {
        agent.CurrentExecutingNode = this;
        agent.brain.Hunting = true;
      
        if (agent.Enemy)
        {
      
            agent.brain.Hunting = false;
        
            return NodeStatus.FAILURE;
        }

        if (agent.Enemy == false && Traveled == false && Travelling == false)
        {
     
            agent.WayPoints.Add(agent.Enemy.transform.position);
            Travelling = true;
        }

        if (Traveled)
        {
            Travelling = false;
            Traveled = false;
          
            agent.WayPoints.Add(agent.Enemy.transform.forward * 500);
            agent.brain.HasSensedPlayer = false;
            return NodeStatus.SUCCESS;
        }
        else
        {
            if (agent.WayPoints.Count > 0 == false)
            {
                Traveled = true;
            }
        }


        return NodeStatus.RUNNING;
    }
}


