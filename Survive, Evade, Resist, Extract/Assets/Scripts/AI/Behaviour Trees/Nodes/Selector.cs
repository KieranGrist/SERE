using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Selectors execute their children in order until a child succeeds, at which point it stops execution
/// If a child returns RUNNING, then it will need to stop execution but resume from the same point the next time it executes
/// </summary>

[System.Serializable]
public class Selector : CompositeNode
{
    public Node ExecutingNode;
    public Selector(Agent agent, string name) : base(agent, name)
    {

        this.agent = agent;
        NodeName = name;
    }

    public override NodeStatus Execute()
    {
 
        NodeStatus rv = NodeStatus.FAILURE;

        agent.CurrentExecutingNode = this;
        for (int i = CurrentChildIndex; i < this.children.Count; i++)
        {

            ExecutingNode = this.children[i];
            rv = this.children[i].Execute();
            this.CurrentChildIndex = i;
            if (rv == NodeStatus.RUNNING)
            {
               
                return NodeStatus.RUNNING;
            }
            if (rv == NodeStatus.SUCCESS)
            {
                this.Reset();
            
                return NodeStatus.SUCCESS;
            }
        }
        this.Reset();

        return rv;
    }
}