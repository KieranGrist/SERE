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
    Agent agent;
    public Selector(Agent Bt, string name) : base(Bt, name)
    {
        agent = Bt;
        NodeName = name;
    }

    public override NodeStatus Execute()
    {
        NodeStatus rv = NodeStatus.FAILURE;
        for (int i = CurrentChildIndex; i < this.children.Count; i++)
        {
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