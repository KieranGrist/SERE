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
            if (!Contains)
            {
                agent.SearchLocation = item.Location;
                agent.SearchedGrids.Add(item);
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
        var position = agent.SearchLocation;
        float Degress = 0;
        var Increase = 360 / NumberOfSearchPoints;
        for (int i = 0; i < NumberOfSearchPoints; i++)
        {
            if (i == NumberOfSearchPoints - 1)
                Debug.DrawLine(agent.SearchPoints[i].transform.position, agent.SearchPoints[0].transform.position);
            else
                Debug.DrawLine(agent.SearchPoints[i].transform.position, agent.SearchPoints[i + 1].transform.position);
        }


            for (int i = 0; i < NumberOfSearchPoints; i++)
            {

            agent.SearchPoints[i].transform.eulerAngles = new Vector3(0, Degress, 0);
            agent.SearchPoints[i].transform.position = agent.transform.position;


            if (agent.SearchInsideCicle)
                agent.SearchPoints[i].transform.position += agent.SearchPoints[i].transform.forward * Random.Range(1, agent.SearchDistance) + new Vector3(0, 2, 0);
                else
                agent.SearchPoints[i].transform.position += agent.SearchPoints[i].transform.forward * agent.SearchDistance + new Vector3(0, 2, 0);
            var g = agent.SearchPoints[i].GetComponent<Renderer>().material;
            if (g)
                g.color = Color.red;
                Degress += Increase;


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

        if (bT_Move.Execute() == NodeStatus.SUCCESS)
            i++;
        else
           return NodeStatus.RUNNING;
        if (i == 29)
        {
            i = 0;
            return NodeStatus.SUCCESS;
        }
       


        return NodeStatus.FAILURE;
    }
}
[System.Serializable]
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
