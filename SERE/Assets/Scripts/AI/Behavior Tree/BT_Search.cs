using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
class CalculateSearchLocation : Node
{
    Agent agent;

    public CalculateSearchLocation(Agent agent) : base(agent)
    {
        this.agent = agent;
    }

    public override NodeStatus Execute()
    {

        foreach (var item in MapGrids.MG.grids)
        {
            bool Contains = false;
            foreach (var sg in agent.SearchedGrids)
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
                agent.SearchLocation = item.Location;
                agent.SearchedGrids.Add(item);
                agent.AgentsTeam.SearchedGrids.Add(item);
                return NodeStatus.SUCCESS;
            }
        }

        return NodeStatus.FAILURE;
    }
}
[System.Serializable]
class CalculateSearchPatern : Node
{
    Agent agent;
    public CalculateSearchPatern(Agent Bt) : base(Bt)
    {
        agent = Bt;
        
    }

    public override NodeStatus Execute()
    {       
        var NumberOfSearchPoints = 30;
        float Degress = 0;
        var Increase = 360 / NumberOfSearchPoints;



        for (int i = 0; i < NumberOfSearchPoints; i++)
        {

            agent.SearchPoints[i].transform.eulerAngles = new Vector3(0, Degress, 0);
            agent.SearchPoints[i].transform.position = agent.SearchLocation;


            if (agent.SearchInsideCicle)
                agent.SearchPoints[i].transform.position += agent.SearchPoints[i].transform.forward * Random.Range(1, agent.SearchDistance);
            else
                agent.SearchPoints[i].transform.position += agent.SearchPoints[i].transform.forward * agent.SearchDistance;

            Vector3 pos = agent.SearchPoints[i].transform.position;
            pos.y = Terrain.activeTerrain.SampleHeight(agent.SearchPoints[i].transform.position);
            agent.SearchPoints[i].transform.position = pos;

            var g = agent.SearchPoints[i].GetComponent<Renderer>().material;
            if (g)
                g.color = Color.red;
            Degress += Increase;


        }
        for (int i = 0; i < NumberOfSearchPoints; i++)
        {
            if (i == NumberOfSearchPoints - 1)
                Debug.DrawLine(agent.SearchPoints[i].transform.position, agent.SearchPoints[0].transform.position);
            else
                Debug.DrawLine(agent.SearchPoints[i].transform.position, agent.SearchPoints[i + 1].transform.position);
        }
        return NodeStatus.SUCCESS;
    }
}
[System.Serializable]
 class ExecuteSearch :Node
{
    Agent agent;
     int i = 0;
    public ExecuteSearch(Agent Bt) : base(Bt)
    {
        agent = Bt;
    }

    public override NodeStatus Execute()
    {

        BT_Move bT_Move = new BT_Move(agent);

        agent.MoveToLocation = agent.SearchPoints[i].transform.position ;
        if (agent.SeePlayer)
            return NodeStatus.SUCCESS;
        if (bT_Move.Execute() == NodeStatus.SUCCESS)
        {
            i++;
             if (i == 29)
            {
                i = 0;
                return NodeStatus.SUCCESS;
            }

            return NodeStatus.RUNNING;
        }
        else
            return NodeStatus.RUNNING;



        return NodeStatus.FAILURE;
    }
}
public class BT_Search :Sequence
{
    bool FoundPlayer;
    Agent agent;
    List<Transform> SearchLocations = new List<Transform>();

public BT_Search(Agent bb) : base(bb)
    {
        agent = bb;    
        AddChild(new CalculateSearchLocation(agent));
        AddChild(new CalculateSearchPatern(agent));
        AddChild(new ExecuteSearch(agent));
    }
}
