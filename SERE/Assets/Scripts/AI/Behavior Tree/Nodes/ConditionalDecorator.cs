using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Inherit this and override CheckStatus. If that returns true, then it will execute the WrappedNode otherwise it will return failure
/// </summary>
public abstract class ConditionalDecorator :DecoratorNode
{
    public ConditionalDecorator(Node WrappedNode, Agent bb) : base(WrappedNode, bb)
    {
    }

    public abstract bool CheckStatus();
    public override NodeStatus Execute()
    {
        NodeStatus rv = NodeStatus.FAILURE;

        if (CheckStatus())
            rv = WrappedNode.Execute();

        return rv;
    }
}
