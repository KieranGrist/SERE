using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sequences execute their children in order until a child fails, at which point it stops execution
/// If a child returns RUNNING, then it will need to stop execution but resume from the same point the next time it executes
/// </summary>
public class Sequence : CompositeNode
{
    public Sequence(Agent bb) : base(bb)
    {
    }
    public override NodeStatus Execute()
    {
        NodeStatus rv = NodeStatus.SUCCESS;
        for (int i = CurrentChildIndex; i < this.children.Count; i++)
        {
            rv = this.children[i].Execute();
            this.CurrentChildIndex = i;
            if (rv == NodeStatus.RUNNING)
            {
                return NodeStatus.RUNNING;
            }
            if (rv == NodeStatus.FAILURE)
            {
                this.Reset();
                return NodeStatus.FAILURE;
            }
        }
        this.Reset();
        return rv;
    }
}