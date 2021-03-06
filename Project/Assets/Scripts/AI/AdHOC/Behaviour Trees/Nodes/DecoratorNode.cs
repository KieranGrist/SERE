﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Decorator nodes customise functionality of other nodes by wrapping around them, see InverterDecorator for example
/// </summary>
[System.Serializable]
public abstract class DecoratorNode : Node
{
    public Node WrappedNode;
    public DecoratorNode(Node WrappedNode, AdHOCAgent agent, string name) : base(agent, name)
    {
        this.WrappedNode = WrappedNode;
        NodeName = name;
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
