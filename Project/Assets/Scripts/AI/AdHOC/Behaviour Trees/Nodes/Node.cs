using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Timers;
/// <summary>
/// Status of the running node
/// </summary>
public enum NodeStatus
{
    RUNNING,
    SUCCESS,
    FAILURE
}
[System.Serializable]
public class Node
{
    /// <summary>
    /// AI Agent Running the node
    /// </summary>
    public AdHOCAgent agent;
    /// <summary>
    /// Name of node
    /// </summary>
    public string NodeName;
    
    /// <summary>
    /// Node Constructor
    /// </summary>
    /// <param name="agent"></param>
    /// <param name="NodeName"></param>
    public Node(AdHOCAgent agent, string NodeName)
    {
        this.agent = agent;
        this.NodeName = NodeName;
    }

    /// <summary>
    /// Executes node
    /// </summary>
    /// <returns></returns>
    public virtual NodeStatus Execute()
    {
        return NodeStatus.SUCCESS;
    }

    /// <summary>
    /// Resets the node 
    /// </summary>
    public virtual void Reset()
    {

    }
}


