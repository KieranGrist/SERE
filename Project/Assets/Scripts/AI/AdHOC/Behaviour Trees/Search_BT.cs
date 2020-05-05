using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Search_BT : Node
{
    public Node node;

    public Search_BT(AdHOCAgent agent, string NodeName) : base(agent, NodeName)
    {
        node = new SearchDecorator(new SearchSequence(agent, "Search Sequence"), agent, "Search Decorator");
    }
    public override NodeStatus Execute()
    {
        agent.CurrentExecutingNode = this;
        agent.SensesSystem();
        return node.Execute();
    }
}

[System.Serializable]
class SearchDecorator : ConditionalDecorator
{
    public SearchDecorator(Node WrappedNode, AdHOCAgent agent, string name) : base(WrappedNode, agent, name)
    {
        base.agent = agent;
    }

    public override bool CheckStatus()
    {
        agent.CurrentExecutingNode = this;
        return agent.brain.Searching;
    }

}


[System.Serializable]
class SearchSequence : Sequence
{
    public SearchSequence(AdHOCAgent agent, string name) : base(agent, name)
    {
        AddChild(new CalculateSearchPattern(agent, "Calculate Search Pattern"));
        AddChild(new ExecuteSearch(agent, "Executing Search"));
    }
    public override NodeStatus Execute()
    {
        
        agent.CurrentExecutingNode = this;
        return base.Execute();
    }
}


[System.Serializable]
class CalculateSearchPattern : Node
{
    public CalculateSearchPattern(AdHOCAgent agent, string NodeName) : base(agent, NodeName)
    {
    }

    public override NodeStatus Execute()
    {
        agent.CurrentExecutingNode = this;
        if (agent.WayPoints.Count == 0)
        {
            agent.WayPoints.Clear();
            agent.search.SearchLocation = agent.MyArea.transform.position;

            var WaypointsNeeded = 30;
            var Angle = 0;
            var IncreaseAngle = 360 / WaypointsNeeded;
            for (int i = 0; i < WaypointsNeeded; i++)
            {
                var EndPoint = agent.search.SearchLocation;
                var EulerAngle = new Vector3(0, Angle, 0);

                float Pitch = EulerAngle.x * Mathf.Deg2Rad; //Set pitch to be euler angle x in radians
                float Yaw = EulerAngle.y * Mathf.Deg2Rad; //Set yaw to be euler angle y in radians
                Vector3 RV = new Vector3
                {

                    z = Mathf.Cos(Yaw) * Mathf.Cos(Pitch), //z = cos of yaw * cos of pitch
                    y = Mathf.Sin(Pitch), //Y = sin of pitch
                    x = Mathf.Cos(Pitch) * Mathf.Sin(Yaw) //x = cos of pitch * sin of yaw
                };


                EndPoint += RV * Random.Range(1, agent.search.SearchDistance); 
                Angle += IncreaseAngle;  
                agent.WayPoints.Add(EndPoint);
            }       
        }
        return NodeStatus.SUCCESS;
    }
}

[System.Serializable]
class ExecuteSearch : Node
{

    public ExecuteSearch(AdHOCAgent Bt, string name) : base(Bt, name)
    {
        agent = Bt;
        NodeName = name;
    }
    public override NodeStatus Execute()
    {
        agent.CurrentExecutingNode = this;
        agent.SensesSystem();
        if (agent.brain.SeePlayer)
        {
            agent.brain.Searching = false;
            agent.WayPoints.Clear();         

      
            return NodeStatus.SUCCESS;
        }

        if (agent.WayPoints.Count == 0)
            return NodeStatus.SUCCESS;

        return NodeStatus.SUCCESS;

    }
}

