using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Inherit this and override CheckStatus. If that returns true, then it will execute the WrappedNode otherwise it will return failure
/// </summary>
[System.Serializable]
public abstract class ConditionalDecorator :DecoratorNode
{
    public ConditionalDecorator(Node WrappedNode, Agent bb, string name) : base(WrappedNode, bb, name)
    {
        this.WrappedNode = WrappedNode;
        this.Bt = bb;
        this.NodeName = name;
    }

    public abstract bool CheckStatus();
    public override NodeStatus Execute()
    {
        NodeStatus rv = NodeStatus.FAILURE;
        bool Status = CheckStatus();
        if (Status)
            rv = WrappedNode.Execute();

        return rv;
    }
}
