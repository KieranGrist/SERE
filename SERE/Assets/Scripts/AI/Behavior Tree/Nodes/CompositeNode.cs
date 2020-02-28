using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
/// <summary>
/// Base class for node that can take child nodes. Only meant to be used in subclasses like Selector and Sequence,
/// but you can add other subclass types (e.g. RandomSelector, RandomSequence, Parallel etc.)
/// </summary>
public abstract class CompositeNode : Node
{
    protected int CurrentChildIndex = 0;
    protected List<Node> children;
    public CompositeNode(Agent bb) : base(bb)
    {
        children = new List<Node>();
    }
    public void AddChild(Node child)
    {
        children.Add(child);
    }

    /// <summary>
    /// When a composite node is reset it set the child index back to 0, and it should propogate the reset down to all its children
    /// </summary>
    public override void Reset()
    {
        CurrentChildIndex = 0;
        //Reset every child
        foreach (var item in children)
            item.Reset();
    }
}
