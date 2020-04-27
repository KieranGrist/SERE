using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Search_BT : Node
{
    public Node node;

    public Search_BT(Agent agent, string NodeName) : base(agent, NodeName)
    {
        node = new SearchDecorator(new SearchSequence(agent, "Search Sequence"), agent, "Search Decorator");
    }
    public override NodeStatus Execute()
    {
        agent.SensesSystem();
        return node.Execute();
    }
}

[System.Serializable]
class SearchDecorator : ConditionalDecorator
{
    public SearchDecorator(Node WrappedNode, Agent agent, string name) : base(WrappedNode, agent, name)
    {
        base.agent = agent;
    }

    public override bool CheckStatus()
    {
        return agent.brain.Searching;
    }

}


[System.Serializable]
class SearchSequence : Sequence
{
    public SearchSequence(Agent agent, string name) : base(agent, name)
    {
        AddChild(new CalculateSearchLocation(agent, "Calculate Search Location"));
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
class CalculateSearchLocation : Node
{
    public CalculateSearchLocation(Agent Bt, string name) : base(Bt, name)
    {
        agent = Bt;
        NodeName = name;
    }

    public override NodeStatus Execute()
    {
            foreach (var item in MapGrids.MG.grids)
            {
                bool Contains = false;
                foreach (var sg in agent.search.SearchedGrids)
                    if (item.ID == sg.ID)
                    {
                        Contains = true;
                        break;
                    }
                foreach (var sg in agent.AgentsTeam.SearchedGrids)
                    if (item.ID == sg.ID)
                    {
                        Contains = true;
                        break;
                    }
                if (!Contains)
                {
                    agent.search.CurrentSearchGrid = item;
                    agent.search.SearchLocation = item.Location;
                    agent.AIRadio.TransmitSearchingGrid(item);
                    agent.search.SearchedGrids.Add(item);
                    return NodeStatus.SUCCESS;
                }
            }

        return NodeStatus.FAILURE;
    }
}

[System.Serializable]
class CalculateSearchPattern : Node
{
    public CalculateSearchPattern(Agent agent, string NodeName) : base(agent, NodeName)
    {
    }

    public override NodeStatus Execute()
    {
        agent.brain.Searching = true;
        var WaypointsNeeded = 30;
        var Angle = 0;
        var IncreaseAngle = 360 / WaypointsNeeded;
        for (int i = 0; i < WaypointsNeeded; i++)
        {
            var EndPoint = agent.transform.position;
            var EulerAngle = new Vector3(0, Angle, 0);

            float Pitch = EulerAngle.x * Mathf.Deg2Rad; //Set pitch to be euler angle x in radians
            float Yaw = EulerAngle.y * Mathf.Deg2Rad; //Set yaw to be euler angle y in radians
            Vector3 RV = new Vector3
            {

                z = Mathf.Cos(Yaw) * Mathf.Cos(Pitch), //z = cos of yaw * cos of pitch
                y = Mathf.Sin(Pitch), //Y = sin of pitch
                x = Mathf.Cos(Pitch) * Mathf.Sin(Yaw) //x = cos of pitch * sin of yaw
            };


            EndPoint += RV * agent.search.SearchDistance;

            Angle += IncreaseAngle;
            if (i == WaypointsNeeded - 1)
            {
                agent.WayPoints.Add(agent.WayPoints[0]);
            }
            agent.WayPoints.Add(EndPoint);
        }
        agent.brain.Searching = true;
        return NodeStatus.SUCCESS;
    }
}

[System.Serializable]
class ExecuteSearch : Node
{

    public ExecuteSearch(Agent Bt, string name) : base(Bt, name)
    {
        agent = Bt;
        NodeName = name;
    }
    public override NodeStatus Execute()
    {

        agent.SensesSystem();
        if (agent.brain.SeePlayer)
        {
            agent.brain.Searching = false;
            agent.WayPoints.Clear();
            agent.search.SearchedGrids.Clear();
            agent.AIRadio.TransmitEnemySeen();
            return NodeStatus.SUCCESS;
        }

        if (agent.WayPoints.Count == 0)
            return NodeStatus.SUCCESS;

        return NodeStatus.RUNNING;

    }
}

