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
public class Node
{
    public Agent Bt;
    public string NodeName;
    public Node(Agent Bt, string NodeName)
    {
        this.Bt = Bt;
        this.NodeName = NodeName;
    }
    public virtual NodeStatus Execute()
    {
     
        return NodeStatus.SUCCESS;
    }

    public virtual void Reset()
    {

    }
}


