using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Timers;

public enum NodeStatus
{
    RUNNING,
    SUCCESS,
    FAILURE
}
[System.Serializable]

public abstract class Node
{
    public Agent Bt;
    public Node(Agent Bt)
    {
        this.Bt = Bt;
    }
    public abstract NodeStatus Execute();

    public virtual void Reset()
    {

    }
}


