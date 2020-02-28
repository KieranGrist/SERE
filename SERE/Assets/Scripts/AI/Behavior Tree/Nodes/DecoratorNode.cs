using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Decorator nodes customise functionality of other nodes by wrapping around them, see InverterDecorator for example
/// </summary>
public abstract class DecoratorNode : Node
{
    protected Node WrappedNode;
    public DecoratorNode(Node WrappedNode, Agent bb) : base(bb)
    {
        this.WrappedNode = WrappedNode;
    }

    public Node GetWrappedNode()
    {
        return WrappedNode;
    }

    /// <summary>
    /// Should reset the wrapped node
    /// </summary>
    public override void Reset()
    {
      //
    }
}
