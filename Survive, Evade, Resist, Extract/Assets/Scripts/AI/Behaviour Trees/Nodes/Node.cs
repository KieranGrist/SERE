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
    public Agent agent;
    public string NodeName;
    public Node(Agent agent, string NodeName)
    {
        this.agent = agent;
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


