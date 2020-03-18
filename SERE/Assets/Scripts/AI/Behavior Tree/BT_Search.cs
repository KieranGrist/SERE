using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CalculateSearchLocation : Node
{
    Agent agent;

    public CalculateSearchLocation(Agent Bt, string name) : base(Bt, name)
    {
        agent = Bt;
        NodeName = name;
    }

    public override NodeStatus Execute()
    {
        if (!agent.search.OrderedSearch)
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
                AIRadioMessage<Search> SearchMessage = new AIRadioMessage<Search>();
                SearchMessage.Transmit(agent, agent.AgentsTeam.teamLeader, agent.AIRadio, agent.search, "Hello I am searching " + agent.search.CurrentSearchGrid.ID);
                agent.search.SearchedGrids.Add(item);
                return NodeStatus.SUCCESS;
            }
        }
        
        return NodeStatus.FAILURE;
    }
}
[System.Serializable]
public class CalculateSearchPatern : Node
{
    Agent agent;
    public CalculateSearchPatern(Agent Bt, string name) : base(Bt, name)
    {
        agent = Bt;
        NodeName = name;
    }


    public override NodeStatus Execute()
    {       
        var NumberOfSearchPoints = agent.search.SearchPoints.Count;
        float Degress = 0;
        var Increase = 360 / NumberOfSearchPoints;



        for (int i = 0; i < NumberOfSearchPoints; i++)
        {

            agent.search.SearchPoints[i].transform.eulerAngles = new Vector3(0, Degress, 0);
            agent.search.SearchPoints[i].transform.position = agent.search.SearchLocation;


            if (agent.search.SearchInsideCicle)
                agent.search.SearchPoints[i].transform.position += agent.search.SearchPoints[i].transform.forward * Random.Range(1, agent.search.SearchDistance);
            else
                agent.search.SearchPoints[i].transform.position += agent.search.SearchPoints[i].transform.forward * agent.search.SearchDistance;

            Vector3 pos = agent.search.SearchPoints[i].transform.position;
            pos.y = Terrain.activeTerrain.SampleHeight(agent.search.SearchPoints[i].transform.position);
            agent.search.SearchPoints[i].transform.position = pos;
            Degress += Increase;
        }
        return NodeStatus.SUCCESS;
    }
}
[System.Serializable]
public class ExecuteSearch : Node
{
    Agent agent;
    int i = 0;
    public ExecuteSearch(Agent Bt, string name) : base(Bt, name)
    {
        agent = Bt;
        NodeName = name;
    }
    public override NodeStatus Execute()
    {

        BT_Move bT_Move = new BT_Move(agent, "Move");

        agent.MoveToLocation = agent.search.SearchPoints[i].transform.position;
        agent.PerceptionSystem();
        if (agent.brain.SeePlayer)
        {
            agent.search.Searching = false;
            agent.search.SearchedGrids.Clear();
            AIRadioMessage<BrainInformation> SearchMessage = new AIRadioMessage<BrainInformation>();
            SearchMessage.Transmit(agent, agent.AgentsTeam.teamLeader, agent.AIRadio, agent.brain, "Hello I can see the player" + agent.brain.PlayersLastKnownLocation);
            return NodeStatus.SUCCESS;

        }


        if (bT_Move.Execute() == NodeStatus.SUCCESS)
        {

            i++;
            if (i == 29)
            {
                i = 0;
                agent.search.OrderedSearch = false;
                agent.search.SearchInsideCicle = false;
                return NodeStatus.SUCCESS;
            }

            return NodeStatus.RUNNING;
        }
        else
            return NodeStatus.RUNNING;
    }
}

[System.Serializable]
public class BT_Search : Sequence
{
    Agent agent;
    List<Transform> SearchLocations = new List<Transform>();

    public BT_Search(Agent Bt, string name) : base(Bt, name)
    {
        agent = Bt;
        NodeName = name;    
        AddChild(new CalculateSearchLocation(agent, "Calculate Search Location"));
        AddChild(new CalculateSearchPatern(agent, "Calculate Search Patern"));
        AddChild(new ExecuteSearch(agent, "Execute Search"));
    }
}
[System.Serializable]
public class SearchDecorator : ConditionalDecorator
{
    Agent agent;
    public SearchDecorator(Node WrappedNode, Agent bb, string name) : base(WrappedNode, bb, name)
    {
        agent = bb;
    }

    public override bool CheckStatus()
    {
        return agent.search.Searching;
    }
}