using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//https://hub.packtpub.com/building-your-own-basic-behavior-tree-tutorial/

public enum NodeStatus
{
    RUNNING,
    SUCCESS,
    FAILURE
}

[System.Serializable]
public abstract class Node : ScriptableObject
{
    public BehaviourTree Bt;
    public Node(BehaviourTree Bt)
    {
        this.Bt = Bt;
    }
    public abstract NodeStatus Execute();

    public virtual void Reset()
    {

    }
}