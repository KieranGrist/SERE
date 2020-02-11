using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inverter decorator simply inverts the result of success/failure of the wrapped node
/// </summary>
public class InverterDecorator : DecoratorNode
{
    public InverterDecorator(Node WrappedNode, Agent bb) : base(WrappedNode, bb)
    {

    }

    public override NodeStatus Execute()
    {
        NodeStatus rv = WrappedNode.Execute();

        if (rv == NodeStatus.FAILURE)
        {
            rv = NodeStatus.SUCCESS;
        }
        else if (rv == NodeStatus.SUCCESS)
        {
            rv = NodeStatus.FAILURE;
        }

        return rv;
    }
}
