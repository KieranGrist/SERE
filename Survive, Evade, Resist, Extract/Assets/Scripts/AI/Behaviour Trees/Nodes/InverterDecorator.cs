using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inverter decorator simply inverts the result of success/failure of the wrapped node
/// </summary>
[System.Serializable]
public class InverterDecorator : DecoratorNode
{
    public InverterDecorator(Node WrappedNode, Agent bb, string name) : base(WrappedNode, bb, name)
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
